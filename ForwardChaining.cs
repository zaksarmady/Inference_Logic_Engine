

namespace iEngine
{
    public class ForwardChaining : LogicAgent
    {
        public ForwardChaining(string a, string t) : base(a, t)
        {
            Init(Tell);
        }

        // method which sets up initial values for forward chaining
        // takes in string representing Knowledege Base and seperates symbols and clauses
        public void Init(string tell)
        {
            string[] query = tell.Split(';');
            for (int i = 0; i < query.Length; i++)
            {
                if (!query[i].Contains("=>"))
                {
                    Symbols.Add(query[i]);
                }
                else
                {
                    // add query
                    Clauses.Add(query[i]);
                    Count.Add(query[i].Split('&').Length);
                }
            }
        }

        public override string Execute()
        {
            {
                string output = "";
                if (FCentails())
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
        }

        // forward chaining algorithm
        public bool FCentails()
        {
            while (Symbols.Count > 0)
            {
                // takes the first item in file and process it
                string p = Symbols[0];
                Symbols.RemoveAt(0);

                // adds to entailed the list of symbols already processed
                Entailed.Add(p);
                if (p == Ask)
                    return true;

                // for each statement
                for (int i = 0; i < Clauses.Count; i++)
                {

                    if (ClauseContains(Clauses[i], p, 0))
                    {
                        // reduce count 
                        int j = Count[i];
                        Count[i] = --j;

                        // all elements in premise are now known
                        if (Count[i] == 0)
                        {
                            string[] separatingChar = { "=>" };
                            string head = Clauses[i].Split(separatingChar, System.StringSplitOptions.RemoveEmptyEntries)[1];
                            if (head.Equals(Ask))
                                return true;
                            if (!Entailed.Contains(head))
                                Symbols.Add(head);
                        }
                    }
                }
            }
            // if we arrive here then ask cannot entailed
            return false;
        }
    }
}
