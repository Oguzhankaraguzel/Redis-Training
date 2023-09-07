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


/*
 * Burada subscriber olarak mesajları alabiliriz.
 * Here we can receive messages as subscribers.
 * Mesaların nasıl işleneceği bize kalmış durumda.
 * How the messages are handled is up to us.
 */
sub.SubscribeAsync("mychannel", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();