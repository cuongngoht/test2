using System;

namespace CoffeeMachineService
{
    public sealed class CommonValue
    {
        private static readonly Lazy<CommonValue> lazy =
            new Lazy<CommonValue>(() => new CommonValue());

        public static CommonValue Instance { get { return lazy.Value; } }
        public int Count { get; set; }
        public (int Month, int Day) DateTimeCompare { get; private set; }
        public double Temp { get; private set; }

        private CommonValue()
        {
            Count = 5;
            DateTimeCompare = new(4, 1);
            Temp = 30;
        }
    }
}