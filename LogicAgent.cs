using System;
using System.Collections.Generic;
using System.Linq;


namespace iEngine
{
    public abstract class LogicAgent
    {
        private string _tell;
        private string _ask;
        private List<string> _symbols;
        private List<string> _facts;
        private List<string> _clauses;
        private List<int> _count;
        private List<string> _entailment;

        //variable initialization
        public LogicAgent(string ask, string tell)
        {
            _symbols = new List<string>();
            _clauses = new List<string>();
            _entailment = new List<string>();
            _facts = new List<string>();
            _count = new List<int>();
            _tell = Program.RemoveWhiteSpace(tell);
            _ask = ask;
        }

        // method which calls a Chaining Algorithm and returns output back to iengine
        public abstract string Execute();

        // method which checks if something exists in a clause
        public bool ClauseContains(string clause, string p, int position)
        {
            string[] separatingChar = { "=>" };
            string premise = clause.Split(separatingChar, System.StringSplitOptions.RemoveEmptyEntries)[position];
            string[] conjunctions = premise.Split('&');

            // check if p is in the premise
            if (conjunctions.Length == 1)
                return premise.Equals(p);
            else
                return conjunctions.ToList().Contains(p);
        }

        // returns the conjunctions in clause
        public List<String> GetPremisesSymbols(String clause)
        {

            String premise = clause.Split("=>")[0];
            List<String> temp = new List<String>();
            String[] conjunctions = premise.Split("&");

            for (int i = 0; i < conjunctions.Length; i++)
            {
                if (!Symbols.Contains(conjunctions[i]))
                    temp.Add(conjunctions[i]);
            }
            return temp;
        }

        public string Tell
        {
            get { return _tell; }
            set { _tell = value; }
        }
        public string Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }
        public List<string> Symbols
        {
            get { return _symbols; }
            set { _symbols = value; }
        }
        public List<string> Facts
        {
            get { return _facts; }
            set { _facts = value; }
        }
        public List<string> Clauses
        {
            get { return _clauses; }
            set { _clauses = value; }
        }
        public List<int> Count
        {
            get { return _count; }
            set { _count = value; }
        }
        public List<string> Entailed
        {
            get { return _entailment; }
            set { _entailment = value; }
        }
    }
}
