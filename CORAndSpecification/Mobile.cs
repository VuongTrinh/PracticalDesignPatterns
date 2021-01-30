namespace CORAndSpecification
{
    public class Mobile : IProcessable
    {
        public Type Type { get; set; }
        public double Cost;
        public override string ToString()
        {
            return "Type : " + this.Type + " and Cost: " + this.Cost;
        }

        public Mobile(Type type, int cost = 0)
        {
            this.Type = type;
            this.Cost = cost;
        }

        public void Process()
        {
            //Do some processing
        }
    }

    public enum Type
    {
        Basic,
        Budget,
        Premium
    }
}
