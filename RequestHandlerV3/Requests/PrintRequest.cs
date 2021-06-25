namespace RequestHandlerV3
{
    public class PrintRequest : IRequest<string>
    {
        public string stringToPrint;

        public PrintRequest(string stringToPrint)
        {
            this.stringToPrint = stringToPrint;
        }
    }
}
