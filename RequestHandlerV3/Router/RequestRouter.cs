using System;
using System.Collections.Generic;

namespace RequestHandlerV3
{
    internal class RequestRouter : IRequestRouter
    {
        private Dictionary<Type, Type> _handlerLookup;
        private Func<Type, object> _dependencyResolver;

        public RequestRouter()
        {
            _handlerLookup = new();
        }

        public void RegisterResolver(Func<Type, object> resolver)
        {
            _dependencyResolver = resolver;
        }

        public void RegisterHandler<TRequest, THandler>()
        {
            _handlerLookup.Add(typeof(TRequest), typeof(THandler));
        }

        public TReturn Send<TReturn>(IRequest<TReturn> request) where TReturn : class
        {
            var requestType = request.GetType();
            var handlerType = _handlerLookup.GetValueOrDefault(requestType);
            var handler = _dependencyResolver(handlerType);
            return (TReturn)((IHandler)handler).Invoke(request);
        }
    }
}
