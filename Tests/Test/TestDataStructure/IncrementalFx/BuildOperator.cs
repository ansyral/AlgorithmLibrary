namespace XuanLibrary.Test.TestDataStructure.IncrementalFx
{
    using XuanLibrary.Fx;
    using XuanLibrary.Utility;

    public class BuildOperator : IOperator<BuildInput, string>
    {
        public string Operate(BuildInput input, BuildContext context)
        {
            // report dep
            input.ReportDependencyTo(BuildInput.ForDependencyTestInstance, context);
            return JsonUtility.ToJsonString(input);
        }
    }
}
