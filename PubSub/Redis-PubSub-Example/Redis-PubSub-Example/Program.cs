using StackExchange.Redis;

/*
 * burada localhost kullanıldığı için bu şekilde davranılmıştır.
 * I behaved this way because localhost is used here.
 * İstenirse egrekli tüm konfigürasyon ayarları yapılabilir.
 * If desired, all configuration settings can be made.
 */
ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

/*
 * ISubscriber arayüzünden bir instance oluşturulmuş olması önemli değil. Bu instance üzerinden istenirse pub istenirse sub olarak işlem yapılabilir.
 * It doesn't matter if an instance was created from the ISubscriber interface. If desired via this instance, pub can be operated as sub if desired.
 */
ISubscriber sub = connection.GetSubscriber();

while (true)
{
    Console.Write("Message : ");
    string message = Console.ReadLine();
    /*
     * operatörler implicit overload edildiğinden veriler üzerinde işlem yapmaya gerek yok.
     * Because of operators are implicit overloaded, there is no need to act on the data.
     * result ile istenilen tüm bilgiler elde edilebilir.
     * All desired information can be obtained with result.
     */
    /*var result =*/
    sub.PublishAsync("mychannel", message);
}
