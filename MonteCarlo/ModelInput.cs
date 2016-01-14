using System;
using System.Collections.Generic;

namespace MonteCarlo
{
    public class ModelInput: ModelObject
    {
        public Distribution Distribution { get; set; } 


        public ModelInput()
        {
     
        }


        public override void Solve(Model model)
        {
            Output = Distribution;
        }


        public override ICollection<string> GetNeededInputs()
        {
            return null;
        }
    }
}

