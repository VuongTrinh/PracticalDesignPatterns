using System;

namespace CORAndSpecification
{
    /// <summary>
    /// Classic chain of responsiblity
    /// </summary>

    abstract class Handler
    {
        protected Handler successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(Mobile mobile);
    }

    class Employee : Handler
    {
        public override void HandleRequest(Mobile mobile)
        {
            if (CanHandle(mobile))
            {
                Console.WriteLine("{0} handled request {1}",
                  this.GetType().Name, mobile);
            }
            else if (successor != null)
            {
                successor.HandleRequest(mobile);
            }
        }

        public bool CanHandle(Mobile mobile)
        {
            return (mobile.Type == Type.Basic);
        }
    }

    class Supervisor : Handler
    {
        public override void HandleRequest(Mobile mobile)
        {
            if (CanHandle(mobile))
            {
                Console.WriteLine("{0} handled request {1}",
                  this.GetType().Name, mobile);
            }
            else if (successor != null)
            {
                successor.HandleRequest(mobile);
            }
        }
        public bool CanHandle(Mobile mobile)
        {
            return (mobile.Type == Type.Budget);
        }
    }

    class SeniorManager : Handler
    {
        public override void HandleRequest(Mobile mobile)
        {
            if (CanHandle(mobile))
            {
                Console.WriteLine("{0} handled request {1}",
                  this.GetType().Name, mobile);
            }
            else if (successor != null)
            {
                successor.HandleRequest(mobile);
            }
        }

        public bool CanHandle(Mobile mobile)
        {
            return (mobile.Type == Type.Premium);
        }
    }

    class Manager : Handler
    {
        public override void HandleRequest(Mobile mobile)
        {
            if (CanHandle(mobile))
            {
                Console.WriteLine("{0} handled request {1}",
                  this.GetType().Name, mobile);
            }
            else if (successor != null)
            {
                successor.HandleRequest(mobile);
            }
        }

        public bool CanHandle(Mobile mobile)
        {
            return ((mobile.Type == Type.Budget && mobile.Cost >= 200)
                || (mobile.Type == Type.Premium && mobile.Cost < 500));
        }
    }
}
