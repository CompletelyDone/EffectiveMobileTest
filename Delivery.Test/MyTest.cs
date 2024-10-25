using System.Globalization;

namespace Delivery.Test
{
    public class MyTest
    {
        [Fact]
        public void Test()
        {
            var str = "2024-10-26_07:15:31";



            DateTime dateTime = DateTime.UtcNow;
            DateTime dateTime2 = DateTime.UtcNow.AddMinutes(15);
            DateTime dateTime3 = DateTime.UtcNow.AddMinutes(30);
            DateTime dateTime4 = DateTime.UtcNow.AddMinutes(35);
            DateTime dateTime5 = DateTime.UtcNow.AddMinutes(-30);

            var x = dateTime - dateTime3;
            var y = dateTime3 - dateTime;

            var date = DateTime.ParseExact(str, "yyyy-MM-dd_HH:mm:ss", CultureInfo.InvariantCulture);

            Assert.NotNull(str);
        }
    }
}
