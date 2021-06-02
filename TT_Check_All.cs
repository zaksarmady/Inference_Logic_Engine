using System.Collections.Generic;
using System.Linq;

namespace iEngine
{
    public class TT_Check_All : LogicAgent
    {
        private List<string> _rest = new List<string>();
        private int _numOfTruth;

        public TT_Check_All(string ask, string tell) : base(ask, tell)
        {
            Init(Tell);
            _numOfTruth = 0;
        }

        public void Init(string tell)
        {
            string[] query = tell.Split(';');
            for (int i = 0; i < query.Length - 1; i++)
            {
                // Checks to see if i in query is a fact
                if (!query[i].Contains("=>"))
                {
                    // Checks to see if Symbol is not already in Symbols queue
                    if (!Symbols.Contains(query[i]))

                        // Adds this symbol to the Symbols queue
                        Symbols.Add(query[i]);

                    // Checks if Facts contains symbol already
                    if (!Facts.Contains(query[i]))

                        // Adds the symbol to the queue of Symbols that are true
                        Facts.Add(query[i]);
                }
                else
                {
                    // Deals with Clauses 
                    Clauses.Add(query[i]);

                    // Splits implications of a clause into two, i.e p1 => p2 gets split into p1 and p2
                    string[] splitImp = query[i].Split("=>");

                    // If first part of implication does not have an '&'
                    if (!splitImp[0].Contains("&"))
                        for (int j = 0; j < splitImp.Length; j++)
                        {
                            // If Symbols does not contain the symbol
                            if (!Symbols.Contains(splitImp[j]))

                                // Add Symbols
                                Symbols.Add(splitImp[j]);
                        }
                    else
                    {
                        // First part of clause contains '&'
                        // Split by logical separator
                        string[] splitLogic = splitImp[0].Split("&");
                        for (int j = 0; j < splitLogic.Length; j++)
                        {
                            // Adding Symbols to Symbols base
                            if (!Symbols.Contains(splitLogic[j]))
                                Symbols.Add(splitLogic[j]);
                            if (!Symbols.Contains(splitImp[1]))
                                Symbols.Add(splitImp[1]);
                        }
                    }
                }
            }
        }

     

        public bool TTEntailment()
        {
            return TTCheckAll(Symbols.ToList(), new Dictionary<string, bool>());
        }

        public bool TTCheckAll(List<string> rest, Dictionary<string, bool> model)
        {
            List<string> nRest = new List<string>(rest);
            if (rest.Count == 0)
            {
                // Check if there are more Symbols to add to the model
                if (IsKBTrue(model))
                {
                    if (IsAlphaTrue(model))
                    {

                        NumOfTruth++;
                        return true;
                    }
                }

                return true;
            }
            else
            {
                // First item
                string p = nRest[0];

                // Removes first item from Symbols
                nRest.RemoveAt(0);

                Dictionary<string, bool> copyTrue = new Dictionary<string, bool>(model);

                // Adding new item to model
                model.Add(p, false);
                copyTrue.Add(p, true);

                // Recursive to create all models for Truth Tables
                return TTCheckAll(nRest, model) && TTCheckAll(nRest, copyTrue);
            }
        }

        public bool IsKBTrue(Dictionary<string, bool> model)
        {
            for (int i = 0; i < Clauses.Count; i++)
            {
                // split clause
                string[] impSplit = Clauses[i].Split("=>");

                // if clause doesnt contain an '&'
                if (!Clauses[i].Contains("&"))
                {
                    if (!IMPLICATION(model[impSplit[0]], model[impSplit[1]])) return false;
                }
                else
                {
                    // if clause does contain '&'           
                    // splits by '&'
                    string[] oppSplit = impSplit[0].Split("&");
                    if (oppSplit.Length <= 2)
                    {
                        if (!IMPLICATION(AND(model[oppSplit[0]], model[oppSplit[1]]), model[impSplit[1]]))
                            return false;
                    }
                    else
                    {
                        // deals with mulitple '&' in one clause                      
                        if (!IMPLICATION(AND_MULTIPLE(oppSplit, model), model[impSplit[1]]))
                            return false;
                    }
                }
                foreach (string symbol in Facts)
                {
                    if (!model[symbol])
                        return false;
                }
            }
            return true;
        }

        public bool AND(bool a, bool b)
        {
            return a && b;
        }

        public bool AND_MULTIPLE(string[] Symbols, Dictionary<string, bool> model)
        {
            // first part of implication
            bool first_imp = model[Symbols[0]];

            // second part of implication 
            bool second_imp = model[Symbols[1]];

            for (int i = 2; i < Symbols.Length; i++)
            {
                // first part is now AND of the initial first and second part of the implication
                first_imp = AND(first_imp, second_imp);

                // if the new first implication is now false, return false
                if (!first_imp)
                    return false;

                // now make second part of implication equal to the next query in the statement.
                second_imp = model[Symbols[i]];
            }

            if (!AND(first_imp, second_imp))
                return false;
            return true;
        }

        public bool IMPLICATION(bool a, bool b)
        {
            return !a || b;
        }

        public override string Execute()
        {
            string output = "";
            if (TTEntailment())
            {
                // output of Truth Table, in the desired format: "Yes + number on truth table"
                output = "Yes: " + NumOfTruth;
            }
            else
                output = "No";
            return output;
        }

        // all elements contained in Clause list
       

        public bool IsAlphaTrue(Dictionary<string, bool> model)
        {
            return model[Ask];
        }

        public int NumOfTruth
        {
            get { return _numOfTruth; }
            set { _numOfTruth = value; }
        }
    }
}
