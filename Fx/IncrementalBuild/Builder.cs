namespace XuanLibrary.Fx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Builder<Output>
    {
        public IOperator<IInput, Output> BuildOperation { get; private set; }

        public Builder(IOperator<IInput, Output> operation)
        {
            BuildOperation = operation;
        }

        public List<Output> BuildWithIncremental(IEnumerable<IInput> inputCollection, BuildContext context)
        {
            if (inputCollection == null)
            {
                throw new ArgumentNullException(nameof(inputCollection));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.UpdateInputCache(inputCollection);
            List<Output> outputs = new List<Output>();
            if (!context.CanIncremental)
            {
                outputs.AddRange(inputCollection.Select(input => GetOutputAndUpdateCache(input, context)));
                return outputs;
            }
            var changes = context.GetChangesWithDependencies(inputCollection).OfType<IInput>().ToList();
            if (changes.Count > 0)
            {
                outputs.AddRange(changes.Select(change => GetOutputAndUpdateCache(change, context)));
            }
            var unChanged = inputCollection.Except(changes, new InputKeyEqualityComparer());
            if (unChanged.Any())
            {
                outputs.AddRange(context.GetCachedOutput<Output>(unChanged));
            }
            context.SaveCache();
            return outputs;
        }

        private Output GetOutputAndUpdateCache(IInput input, BuildContext context)
        {
            var output = BuildOperation.Operate(input, context);
            context.DG.ClearDependency(input);
            context.UpdateOutputCache(input, output);
            return output;
        }
    }
}
