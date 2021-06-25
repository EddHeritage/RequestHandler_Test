using Autofac;
using System;

namespace RequestHandlerV3
{
    class ProgramV3
    {
        static void Main(string[] args)
        {
            // DI
            var builder = new ContainerBuilder();
            builder.RegisterType<RequestRouter>().As<IRequestRouter>().SingleInstance();
            builder.RegisterType<PrintHandler>().AsSelf();
            builder.RegisterType<BoolFlipHandler>().AsSelf();
            var container = builder.Build();

            // SETUP ROUTER
            var requestRouter = container.Resolve<IRequestRouter>();
            requestRouter.RegisterResolver(container.Resolve);
            requestRouter.RegisterHandler<PrintRequest, PrintHandler>();
            requestRouter.RegisterHandler<BoolFlipRequest, BoolFlipHandler>();

            // USE ROUTER
            string item1 = requestRouter.Send(new PrintRequest("Hello"));
            PrimitiveRef<bool> item2 = requestRouter.Send(new BoolFlipRequest(false));

            Console.WriteLine($"ITEM 1: {item1}");
            Console.WriteLine($"ITEM 2: {item2.Value}");

        }
    }
}
