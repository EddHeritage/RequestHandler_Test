namespace RequestHandlerV3
{
    public class BoolFlipRequest : IRequest<PrimitiveRef<bool>>
    {
        public bool boolToFlip;

        public BoolFlipRequest(bool boolToFlip)
        {
            this.boolToFlip = boolToFlip;
        }
    }
}
