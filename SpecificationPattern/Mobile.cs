namespace SpecificationPattern
{
    public class Mobile
    {
        public BrandName BrandName { get; set; }
        public Type Type { get; set; }
        public int Cost;
        public string GetDescription()
        {
            return "The mobile is of brand : " + this.BrandName + " and of type : " + this.Type;
        }

        public Mobile(BrandName brandName, Type type, int cost = 0)
        {
            this.BrandName = brandName;
            this.Type = type;
            this.Cost = cost;
        }
    }

    public enum BrandName
    {
        Samsung,
        Apple,
        Htc
    }

    public enum Type
    {
        Basic,
        Smart
    }
}
