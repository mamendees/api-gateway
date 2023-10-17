Console.WriteLine("Start");

using (var client = new HttpClient())
{
    for (var i = 0; i < 1000; i++)
    {
        try
        {
            var result = await client.GetAsync("https://localhost:5000/microservice1/api/microservice1");
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - {result.StatusCode}, {result.Content.ReadAsStringAsync().Result}");
            //Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine(ex.ToString());
            Console.WriteLine("-------------------------------");
        }
    }
    Console.Read();
}

//using (var client = new HttpClient())
//{
//    for (var i = 0; i < 100; i++)
//    {
//        try
//        {
//            client.GetAsync("https://localhost:5001/api/microservice1");

//        }
//        catch (Exception ex)
//        {

//            Console.WriteLine(ex.ToString());
//        }

//        Thread.Sleep(500);
//        Console.Read();
//    }
//}