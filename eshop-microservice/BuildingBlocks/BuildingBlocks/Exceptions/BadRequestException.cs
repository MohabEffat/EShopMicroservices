namespace BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {
            
        }
        public BadRequestException(string msg, string details) : base(msg)
        {
            Details = details;
        }
        public string? Details { get; set; }
    }
}
