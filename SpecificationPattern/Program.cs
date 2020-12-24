using System;
using System.Collections.Generic;

namespace SpecificationPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Mobile> mobiles = new List<Mobile> {
                new Mobile(BrandName.Samsung, Type.Smart, 700),
                new Mobile(BrandName.Apple, Type.Smart, 800),
                new Mobile(BrandName.Htc, Type.Basic),
                new Mobile(BrandName.Samsung, Type.Basic) };

            ISpecification<Mobile> samsungExpSpec =
               new ExpressionSpecification<Mobile>(o => o.BrandName == BrandName.Samsung);
            ISpecification<Mobile> htcExpSpec =
               new ExpressionSpecification<Mobile>(o => o.BrandName == BrandName.Htc);
            ISpecification<Mobile> SamsungAndHtcSpec = samsungExpSpec.And(htcExpSpec);


            ISpecification<Mobile> SamsungHtcExpSpec =
               samsungExpSpec.Or(htcExpSpec);
            ISpecification<Mobile> NoSamsungExpSpec = new ExpressionSpecification<Mobile>(o => o.BrandName != BrandName.Samsung);

            ISpecification<Mobile> brandExpSpec = new ExpressionSpecification<Mobile>(o => o.Type == Type.Smart);
            ISpecification<Mobile> premiumSpecification = new PremiumSpecification<Mobile>(600);
            ISpecification<Mobile> complexSpec = (samsungExpSpec.Or(htcExpSpec)).And(brandExpSpec);
            ISpecification<Mobile> linqNonLinqExpSpec = NoSamsungExpSpec.And(premiumSpecification);

            //Some fun
            Console.WriteLine("\n***Samsung mobiles*****\n");
            var result = mobiles.FindAll(o => samsungExpSpec.IsSatisfiedBy(o));
            result.ForEach(o => Console.WriteLine(o.GetDescription()));

            Console.WriteLine("\n*****Htc mobiles********\n");
            result = mobiles.FindAll(o => htcExpSpec.IsSatisfiedBy(o));
            result.ForEach(o => Console.WriteLine(o.GetDescription()));

            Console.WriteLine("\n****Htc and samsung mobiles*******\n");
            result = mobiles.FindAll(o => SamsungHtcExpSpec.IsSatisfiedBy(o));
            result.ForEach(o => Console.WriteLine(o.GetDescription()));

            Console.WriteLine("\n****Not samsung*******\n");
            result = mobiles.FindAll(o => NoSamsungExpSpec.IsSatisfiedBy(o));
            result.ForEach(o => Console.WriteLine(o.GetDescription()));

            Console.WriteLine("\n****Htc and samsung mobiles (only smart)*******\n");
            result = mobiles.FindAll(o => complexSpec.IsSatisfiedBy(o));
            result.ForEach(o => Console.WriteLine(o.GetDescription()));

            //More fun
            Console.WriteLine("\n****All premium mobile phones*******\n");

            result = mobiles.FindAll(o => premiumSpecification.IsSatisfiedBy(o));
            result.ForEach(o => Console.WriteLine(o.GetDescription()));


            Console.WriteLine("\n****All premium mobile phones except samsung*******\n");
            result = mobiles.FindAll(o => linqNonLinqExpSpec.IsSatisfiedBy(o));
            result.ForEach(o => Console.WriteLine(o.GetDescription()));
            Console.ReadLine();
        }
    }
}
