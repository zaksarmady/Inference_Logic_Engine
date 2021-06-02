using System.Collections.Generic;

namespace iEngine
{
    public class BackwardChaining : LogicAgent
    {
        public BackwardChaining(string ask, string tell) : base(ask, tell)
        {
            Init(Tell);
        }

        // method which sets up initial values for backward chaining
        // takes in string representing Knowledege Base and seperates symbols and clauses
        public void Init(string tell)
        {

            Symbols.Add(Ask);
            string[] query = tell.Split(';');
            for (int i = 0; i < query.Length; i++)
            {
                if (!query[i].Contains("=>"))
                {
                    Facts.Add(query[i]);
                }
                else
                {
                    Clauses.Add(query[i]);
                    Count.Add(query[i].Split('&').Length);
                }
            }

        }

        public override string Execute()
        {
            string output = "";
            if (BCentails())
            {
                // if returned true so it entails
                output = "Yes: ";

                // for each entailed statement
                for (int i = 0; i < Entailed.Count; i++)
                {
                    if (!(Entailed[i] == ""))
                        output += Entailed[i] + ", ";
                }
                output += Ask;
            }
            else
                output = "No";
            return output;
        }

        // backward chaining algorithm
        public bool BCentails()
        {
            while (Symbols.Count > 0)
            {
                // takes the first item in file and process it
                string q = Symbols[Symbols.Count - 1];
                Symbols.RemoveAt(Symbols.Count - 1);

                if (q != Ask)
                    if (!Entailed.Contains(q))
                        Entailed.Insert(0, q);

                if (!(Facts.Contains(q)))
                {
                    List<string> premise = new List<string>();

                    // for each statement
                    for (int i = 0; i < Clauses.Count; i++)
                    {

                        if (ClauseContains(Clauses[i], q, 1))
                        {
                            List<string> temp = GetPremisesSymbols(Clauses[i]);
                            for (int j = 0; j < temp.Count; j++)
                            {
                                premise.Add(temp[j]);
                            }
                        }
                    }
                    if (premise.Count == 0)
                        return false;
                    else
                    {
                        for (int i = 0; i < premise.Count; i++)
                        {
                            if (!Entailed.Contains(premise[i]))
                                Symbols.Add(premise[i]);
                        }
                    }
                }
            }


            return true;
        }
    }
}
