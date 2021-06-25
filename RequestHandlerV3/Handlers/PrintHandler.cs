using System;

namespace RequestHandlerV3
{
    internal class PrintHandler : BaseHandler<PrintRequest, string>
    {
        public override string Invoke(PrintRequest input)
        {
            Console.WriteLine(input.stringToPrint);
            return "Done";
        }
    }
}
