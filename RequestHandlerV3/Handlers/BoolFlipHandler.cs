namespace RequestHandlerV3
{
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
