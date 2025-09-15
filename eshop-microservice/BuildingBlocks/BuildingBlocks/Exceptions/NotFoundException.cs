namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        {

        }
        public NotFoundException(string name, object key) : base($"Entity {name} : {key} was not found")
        {
            
        }
    }
}
