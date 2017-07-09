namespace XuanLibrary.Fx
{
    public interface IOperator<Input, Output>
    {
        Output Operate(Input input, BuildContext context);
    }
}
