using System;

namespace CoffeeMachineService.Helpper
{
    public class SystemDateTime
    {

        public Func<DateTime> Now = () => DateTime.Now;

        public void SetDateTime(DateTime dateTimeNow)
        {
            Now = () => dateTimeNow;
        }
        public void ResetDateTime()
        {
            Now = () => DateTime.Now;
        }
    }
}