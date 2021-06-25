using System;
using System.Collections.Generic;

namespace RequestHandlerV2
{
    class ProgramV2
    {
        static void Main(string[] args)
        {

            var app = new App();

            var prh = new PrintHandler();
            var bfh = new BoolFlipHandler();

            app.RegisterHandler<PrintRequest>(prh);
            app.RegisterHandler<BoolFlipRequest>(bfh);

            var item1 = app.Send(new PrintRequest("Hello"));
            var item2 = app.Send(new BoolFlipRequest(false));

            Console.WriteLine($"ITEM 1: {item1}");
            Console.WriteLine($"ITEM 2: {item2.Value}");

        }
    }

    internal class App
    {
        private Dictionary<Type, IHandler> _handlers;

        public App()
        {
            _handlers = new();
        }

        public void RegisterHandler<TRequest>(IHandler handler)
        {
            _handlers.Add(typeof(TRequest), handler);
        }

        internal TReturn Send<TReturn>(IRequest<TReturn> request) where TReturn : class
        {
            var handler = _handlers.GetValueOrDefault(request.GetType());
            return (TReturn)handler.Invoke(request);
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

    internal interface IHandler
    {
        object Invoke(object input);
    }

    internal abstract class BaseHandler<TRequest, TReturn> : IHandler where TRequest : IRequest<TReturn> where TReturn : class
    {
        public object Invoke(object input)
        {
            return Invoke((TRequest)input);
        }

        public abstract TReturn Invoke(TRequest input);
    }

    internal class PrintHandler : BaseHandler<PrintRequest, string>
    {
        public override string Invoke(PrintRequest input)
        {
            Console.WriteLine(input.stringToPrint);
            return "Done";
        }
    }

    internal class BoolFlipHandler : BaseHandler<BoolFlipRequest, PrimitiveRef<bool>>
    {
        public override PrimitiveRef<bool> Invoke(BoolFlipRequest input)
        {
            return new PrimitiveRef<bool>()
            {
                Value = !input.boolToFlip
            };
        }
    }
}
