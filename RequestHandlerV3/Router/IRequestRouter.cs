using System;

namespace RequestHandlerV3
{
    internal interface IRequestRouter
    {
        void RegisterHandler<TRequest, THandler>();
        void RegisterResolver(Func<Type, object> resolver);
        TReturn Send<TReturn>(IRequest<TReturn> request) where TReturn : class;
    }
}