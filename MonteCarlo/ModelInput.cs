using System;
using System.Collections.Generic;

namespace MonteCarlo
{
    public class ModelInput: ModelObject
    {
        public Distribution Distribution { get; set; } 


        public ModelInput(string name): base(name)
        {
     
        }


        public override void Solve(Model model)
        {
            if (Distribution == null)
                throw new ArgumentNullException("Input distribution has not been initialized");
            
            Output = Distribution;
        }


        public override ICollection<string> GetNeededInputs()
        {
            return null;
        }
    }
}

