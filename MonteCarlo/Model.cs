using System;
using System.Collections.Generic;

namespace MonteCarlo
{
    public class Model
    {
        private Dictionary<string, ModelObject> mObjects = new Dictionary<string, ModelObject>();
        private Dictionary<string, ModelObject> mSolvedObjects = new Dictionary<string, ModelObject>();
        private List<ModelObject> mPendingObjects = new List<ModelObject>();


        public ModelObject this[string name]
        {
            get { return mObjects[name]; }
        }


        public Model()
        {
            
        }

        public void Add(ModelObject obj)
        {
            if (obj == null)
                throw new ArgumentException("Model: cannot add a null modelObject");
            
            mObjects.Add(obj.Name, obj);
        }

        public void Add(params ModelObject[] objs)
        {
            foreach (var o in objs) 
                Add(o);
        }

        private void PrepareObjectGraph()
        {
            mSolvedObjects.Clear();
            mPendingObjects.Clear();

            foreach (var o in mObjects.Values)
            {
                mPendingObjects.Add(o);
            }
        }

        public void Solve()
        {
            PrepareObjectGraph();

            while (mPendingObjects.Count > 0)
            {
                var obj = mPendingObjects[0];

                if (IsReadyToSolve(obj))
                {
                    SolveObject(obj);
                }
                else
                {
                    MoveObjectToEnd(obj);
                }
            }
        }

        private bool IsReadyToSolve(ModelObject obj)
        {
            var inputs = obj.GetNeededInputs();
            if (inputs == null || inputs.Count == 0)
                return true;

            foreach (var i in inputs)
            {
                if (!mSolvedObjects.ContainsKey(i))
                    return false;
            }

            return true;
        }

        private void SolveObject(ModelObject obj)
        {
            obj.Solve(this);

            mPendingObjects.Remove(obj);
            mSolvedObjects.Add(obj.Name, obj);
        }

        private void MoveObjectToEnd(ModelObject obj)
        {
            mPendingObjects.Remove(obj);
            mPendingObjects.Add(obj);
        }
    }
}

