using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Promotion_Engine.Config;
using Promotion_Engine.Message;
using Promotion_Engine.ProductReader;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductReader
{
    class ProductReaderConsole
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Product Reader Console");

            QueueClient queueClient = new QueueClient(AccountDetails.ConnectionString, AccountDetails.QueueName);


            // Create a sample order
            SkuTag[] ordercart = new SkuTag[]
            {
                    new SkuTag() { Product = "A", Price = (int)Price.A },
                    new SkuTag() { Product = "A", Price = (int)Price.A },
                     new SkuTag() { Product = "A", Price = (int)Price.A },
                      new SkuTag() { Product = "A", Price = (int)Price.A },
                       new SkuTag() { Product = "A", Price = (int)Price.A },
                    new SkuTag() { Product = "B", Price = (int)Price.B },
                     new SkuTag() { Product = "B", Price = (int)Price.B },
                      new SkuTag() { Product = "B", Price = (int)Price.B },
                       new SkuTag() { Product = "B", Price = (int)Price.B },
                    new SkuTag() { Product = "C", Price = (int)Price.C },
                    new SkuTag() { Product = "D", Price = (int)Price.D },
                     new SkuTag() { Product = "C", Price = (int)Price.C },
                      new SkuTag() { Product = "C", Price = (int)Price.C },
                    new SkuTag() { Product = "D", Price = (int)Price.D },
                    new SkuTag() { Product = "D", Price = (int)Price.D },
                    new SkuTag() { Product = "D", Price = (int)Price.D }

            };

            // Display the order data.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Order contains {0} items.", ordercart.Length);
            Console.ForegroundColor = ConsoleColor.Yellow;

            var calculateTotalPrice = CalculatePrice.calculateTotalPriceCart(ordercart,PromotionType.Onproduct);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Order value = ${0}.", calculateTotalPrice);
            Console.WriteLine();
            Console.ResetColor();

           

            Console.WriteLine("Press enter to Send to Checkout...");
            Console.ReadLine();

            Console.WriteLine("Reading Products...");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;

            var sessionId = Guid.NewGuid().ToString();
            Console.WriteLine($"SessionId: { sessionId }");

            int position = 0;
            while (position < ordercart.Length)
            {
                
                SkuTag skuidTag = ordercart[position];
                skuidTag.TotalPrice = calculateTotalPrice;
                // Create a new  message from the order item RFID tag.
                var orderJson = JsonConvert.SerializeObject(skuidTag);
                var skuReadMessage = new Message(Encoding.UTF8.GetBytes(orderJson));

                // Comment in to set message id.
                skuReadMessage.MessageId = skuidTag.SkuId;
                
                // Comment in to set session id.
                skuReadMessage.SessionId = sessionId;

                // Send the message
                await queueClient.SendAsync(skuReadMessage);
                //Console.WriteLine($"Sent: { orderItems[position].Product }");
                Console.WriteLine($"Sent: { ordercart[position].Product } - MessageId: { skuReadMessage.MessageId }");
                position++;
                if(position==ordercart.Length)
                {
                    var totalJson = JsonConvert.SerializeObject(calculateTotalPrice);
                    var totalMessage = new Message(Encoding.UTF8.GetBytes(totalJson));
                    totalMessage.MessageId = skuidTag.SkuId;
                    totalMessage.SessionId = sessionId;
                    await queueClient.SendAsync(totalMessage);

                    Console.WriteLine($"Sent: { totalJson } - MessageId: { skuReadMessage.MessageId }");

                }
                Thread.Sleep(100);
            }

            }
    }
}
