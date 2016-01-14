using System;
using System.Collections.Generic;
using NCalc;
using NCalc.Domain;

namespace MonteCarlo
{
    public class ModelFormula: ModelObject
    {
        public string Formula { get; set; }

        private ICollection<string> mInputs;

        public ModelFormula(string name): base(name)
        {
        }



        public override void Solve(Model model)
        {
            if (mInputs == null)
                GetNeededInputs();

            var e = new Expression(Formula, 
                            EvaluateOptions.IterateParameters | 
                            EvaluateOptions.IgnoreCase);

            foreach (var inputName in mInputs)
            {
                var objectDistribution = model[inputName].Output;
                e.Parameters[inputName] = objectDistribution;
            }

            var result = e.Evaluate() as List<object>;

            if (result == null)
                throw new Exception("");

            Output = new Distribution(GetDoubleArray(result));
        }

        public override ICollection<string> GetNeededInputs()
        {
            // Parse formula to extract references to any object name
            var e = NCalc.Expression.Compile(Formula, false);
            var visitor = new ParameterExtractionVisitor();
            e.Accept(visitor);

            mInputs = visitor.Parameters;
            return mInputs;
        }


        public static double[] GetDoubleArray(List<object> objectList)
        {
            double[] result = new double[objectList.Count];

            for (int i = 0; i < objectList.Count; ++i) 
                result[i] = (double)objectList[i];

            return result;
        }


        // __ Expression parameter extraction without evaluating _________________


        class ParameterExtractionVisitor : LogicalExpressionVisitor
        {
            public HashSet<string> Parameters = new HashSet<string>();

            public override void Visit(NCalc.Domain.Identifier function)
            {
                //Parameter - add to list
                Parameters.Add(function.Name);
            }

            public override void Visit(NCalc.Domain.UnaryExpression expression)
            {
                expression.Accept(this);
            }

            public override void Visit(NCalc.Domain.BinaryExpression expression)
            {
                //Visit left and right
                expression.LeftExpression.Accept(this);
                expression.RightExpression.Accept(this);
            }

            public override void Visit(NCalc.Domain.TernaryExpression expression)
            {
                //Visit left, right and middle
                expression.LeftExpression.Accept(this);
                expression.RightExpression.Accept(this);
                expression.MiddleExpression.Accept(this);
            }

            public override void Visit(Function function)
            {
                foreach (var expression in function.Expressions)
                {
                    expression.Accept(this);
                }
            }

            public override void Visit(LogicalExpression expression)
            {
                expression.Accept(this);
            }

            public override void Visit(ValueExpression expression)
            {

            }
        }
    }
}

