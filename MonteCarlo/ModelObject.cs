using System;
using System.Collections.Generic;

namespace MonteCarlo
{
    public abstract class ModelObject
    {
        private Distribution mOutput;

        public string Name { get; private set; }

        public Distribution Output
        {
            get { return mOutput; }
            protected set { mOutput = value; }
        }

        public ModelObject(string name)
        {
            Name = name;
        }

        public abstract void Solve(Model model);

        public abstract ICollection<string> GetNeededInputs();
    }
}

