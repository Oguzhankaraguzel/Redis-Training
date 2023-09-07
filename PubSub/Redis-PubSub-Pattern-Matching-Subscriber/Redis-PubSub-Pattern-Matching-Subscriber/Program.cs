using StackExchange.Redis;

/*
 * burada localhost kullanıldığı için bu şekilde davranılmıştır.
 * I behaved this way because localhost is used here.
 * 
 * İstenirse egrekli tüm konfigürasyon ayarları yapılabilir.
 * If desired, all configuration settings can be made.
 */
ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

/*
 * ISubscriber arayüzünden bir instance oluşturulmuş olması önemli değil. Bu instance üzerinden istenirse pub istenirse sub olarak işlem yapılabilir.
 * It doesn't matter if an instance was created from the ISubscriber interface. If desi-red via this instance, pub can be operated as sub if desired.
 */
ISubscriber sub = connection.GetSubscriber();


/*
 * Burada subscriber olarak mesajları alabiliriz.
 * Here we can receive messages as subscribers.
 * 
 * Mesaların nasıl işleneceği bize kalmış durumda.
 * How the messages are handled is up to us.
 * 
 * ====================================================
 * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
 * ====================================================
 * 
 * Kanal adını .* ile düzenleyerek belirli bir pattern uygulayabiliriz.
 * We can apply a specific pattern by editing the channel name with .*.
 * 
 * Bu sayede kanalları ve dolayısı ile mesajları filtreleyebiliriz.
 * In this way, we can filter the channels and therefore the messages.
 */

//sub.SubscribeAsync("stock.*", (channel, message) =>
//{
//    Console.WriteLine(message);
//});

sub.SubscribeAsync("mychannel.*", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();

/*
 * Redis cli üzerinde ise subscribe yerine psubscribe yazarak pattern desing işlemini gerçekleştirebiliriz.
 * On Redis Cli, we can perform pattern design by typing psubscribe instead of subscribe.
 * 
 * Örnek olarak "psubscribe mychannel.*" şeklinde
 * For example, "psubscribe mychannel.*"
 */

/* !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
 * Burada dikkat edilmesi gereken mesaj yayınlama durumudur.
 * What needs to be taken into consideration here is the message publish situation.
 * 
 * Kanal ismi doğrudan "mychannel" olursa, desing pattern olarak "mychannel.*" sub'u bu mesajı almaz.
 * If the channel name is directly "mychannel", the "mychannel.*" sub will not receive this message.
 * 
 * Çümkü belirlenen pattern'e uymaz.
 * Because it does not fit to the determined pattern.
 * 
 * Eğer mesaj yayınladığımız kanalın ismi "mychannel.x" olursa mesajı; hem "mychannel.*" sub'ı alır, hem de "mychannel.x" olan sub alır. 
 * If the name of the channel on which we publish the message is "mychannel.x", the message; It takes both the sub "mychannel.*" and the sub "mychannel.x".
 */
