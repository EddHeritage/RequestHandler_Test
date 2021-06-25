using System;
using System.Collections.Generic;

namespace RequestHandler
{
    class Program
    {
        static void Main(string[] args)
        {

            var app = new App();

            var item1 = app.Send(new PrintRequest("Hello"));
            var item2 = app.Send(new BoolFlipRequest(false));

            Console.WriteLine($"ITEM 1: {item1}");
            Console.WriteLine($"ITEM 2: {item2.Value}");

        }
    }

    internal class PrimitiveRef<T> where T : struct
    {
        public T Value { get; set; }
    }

    internal interface IRequest<TReturn> where TReturn : class { }

    internal class BoolFlipRequest : IRequest<PrimitiveRef<bool>>
    {
        public bool boolToFlip;

        public BoolFlipRequest(bool boolToFlip)
        {
            this.boolToFlip = boolToFlip;
        }
    }

    internal class PrintRequest : IRequest<string>
    {
        public string stringToPrint;

        public PrintRequest(string stringToPrint)
        {
            this.stringToPrint = stringToPrint;
        }
    }

    internal class App
    {
        private Dictionary<Type, IHandler> _handlers;

        public App()
        {
            _handlers = new();

            var prh = new PrintHandler();
            var bfh = new BoolFlipHandler();

            RegisterHandler<PrintRequest>(prh);
            RegisterHandler<BoolFlipRequest>(bfh);
        }

        public void RegisterHandler<TRequest>(IHandler handler)
        {
            _handlers.Add(typeof(TRequest), handler);
        }

        internal T Send<T>(IRequest<T> printRequest) where T : class
        {
            var handler = _handlers.GetValueOrDefault(printRequest.GetType());
            return handler.Invoke(printRequest);
        }
    }

    internal interface IHandler
    {
        TReturn Invoke<TReturn>(IRequest<TReturn> input) where TReturn : class;
    }

    internal class PrintHandler : IHandler
    {
        public TReturn Invoke<TReturn>(IRequest<TReturn> input) where TReturn : class
        {
            var request = input as PrintRequest;
            Console.WriteLine(request.stringToPrint);
            return "Done" as TReturn;
        }
    }

    internal class BoolFlipHandler : IHandler
    {
        public TReturn Invoke<TReturn>(IRequest<TReturn> input) where TReturn : class
        {
            var request = input as BoolFlipRequest;
            var response = new PrimitiveRef<bool>()
            {
                Value = !request.boolToFlip
            };
            return response as TReturn;
        }
    }
}
