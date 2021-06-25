namespace RequestHandlerV3
{
    internal interface IHandler
    {
        object Invoke(object input);
    }
}
