using System;
using System.Collections.Generic;

namespace CORAndSpecification
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Mobile> mobiles = new List<Mobile> {
                new Mobile(Type.Premium, 450),
                new Mobile(Type.Premium, 800),
                new Mobile(Type.Basic, 100),
                new Mobile(Type.Budget, 160),
                new Mobile(Type.Premium, 800) ,
                new Mobile(Type.Budget, 250),
            new Mobile(Type.Budget, 1)};

            ISpecification<Mobile> basicSpec = new Specification<Mobile>(o => o.Type == Type.Basic);
            ISpecification<Mobile> budgetSpec = new Specification<Mobile>(o => o.Type == Type.Budget);
            ISpecification<Mobile> premiumSpec = new Specification<Mobile>(o => (o.Type == Type.Premium));
            //premiumSpec = null; //Test

            //To extract all mobile phones that costs less than 200
            ISpecification<Mobile> budgetLowCostSpec = new Specification<Mobile>(o => o.Cost < 200 && o.Cost >= 100);
            //budgetLowCostSpec = null; //test

            //To extract all mobile phones that costs greater than or equal to 200
            ISpecification<Mobile> budgetHighCostSpec = new Specification<Mobile>(o => (o.Cost >= 200));

            //To extract all mobile phones that costs less than 500
            ISpecification<Mobile> premiumLowCostSpec = new Specification<Mobile>(o => (o.Cost < 500));
            //To extract all mobile phones that costs greater than or equal to 500    
            ISpecification<Mobile> premiumHighCostSpec = new Specification<Mobile>(o => (o.Cost >= 500));
            var invProcess = new InventoryProcess<Mobile>();

            IHandler<Mobile> seniorManager = new Approver<Mobile>("SeniorManager", invProcess.placeOrder);
            IHandler<Mobile> manager = new Approver<Mobile>("Manager", invProcess.placeOrder);
            IHandler<Mobile> supervisor = new Approver<Mobile>("Supervisor", invProcess.placeOrder);
            IHandler<Mobile> employee = new Approver<Mobile>("Employee", invProcess.placeOrder);


            employee.SetSpecification(basicSpec);
            //For supervisor spec we combine the budget spec with budget low cost spec to achieve the constraint all budget mobiles that costs less than 200
            //supervisor.SetSpecification(budgetSpec.And<Mobile>(budgetLowCostSpec));
            supervisor.SetSpecification(basicSpec);
            //For manager spec we combine the budget spec with budget high cost spec to achieve the constraint all budget mobiles that costs more than or equal to 200.
            //For manager spec we combine the premium spec with premium low cost spec to achieve the constraint all premium mobiles that costs less than 500. 
            manager.SetSpecification(budgetSpec.And<Mobile>(budgetHighCostSpec).Or<Mobile>(premiumSpec.And<Mobile>(premiumLowCostSpec)));
            //For senior manager spec we combine the premium spec with premium high cost spec to achieve the constraint all premium mobiles that costs more than or equal to 500.   
            seniorManager.SetSpecification(premiumSpec.And<Mobile>(premiumHighCostSpec));

            employee.SetSuccessor(supervisor);
            supervisor.SetSuccessor(manager);
            manager.SetSuccessor(seniorManager);

            //Default handler
            IHandler<Mobile> defaultApprover = new Approver<Mobile>("Default", invProcess.defaultOrder);
            ISpecification<Mobile> defaultSpec = new Specification<Mobile>(o => true);
            defaultApprover.SetSpecification(defaultSpec);
            seniorManager.SetSuccessor(defaultApprover);

            mobiles.ForEach(o => employee.HandleRequest(o));
            Console.ReadLine();
        }
    }

    public class InventoryProcess<Mobile>
    {
        public void placeOrder(Mobile o)
        {
            Console.WriteLine("Order placed for mobile {0}", o.ToString());
            //Place the order.
        }

        public void defaultOrder(Mobile o)
        {
            Console.WriteLine("The order is not placed for the mobile {0}", o.ToString());
            //Place the order.
        }
    }
}
