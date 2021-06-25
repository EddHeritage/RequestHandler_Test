namespace RequestHandlerV3
{
    public abstract class BaseHandler<TRequest, TReturn> : IHandler where TRequest : IRequest<TReturn> where TReturn : class
    {
        public object Invoke(object input)
        {
            return Invoke((TRequest)input);
        }

        public abstract TReturn Invoke(TRequest input);
    }
}
