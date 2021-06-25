namespace RequestHandlerV3
{
    public class PrimitiveRef<T> where T : struct
    {
        public T Value { get; set; }
    }
}
