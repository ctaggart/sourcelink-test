using System;

namespace TryJson
{
    // https://www.newtonsoft.com/json/help/html/SerializeObject.htm

    class Account
    {
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var account = new Account
            {
                Email = "james@example.com",
                Active = true
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(account);
            Console.WriteLine(json);
        }
    }
}
