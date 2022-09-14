using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MSMQClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPrivateMSMQ();
            Console.ReadKey();
        }

        private static void TestPrivateMSMQ()
        {
            string path = @".\Private$\TestPrivateMSMQ";
            if (!MessageQueue.Exists(path))
            {
                Console.WriteLine($"队列不存在！{path}");
                return;
            }

            using (var mq = new MessageQueue(path))
            {
                mq.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                Console.WriteLine($"Current Queue Message Length:{mq.GetAllMessages().Length}");
                foreach (var msg in mq.GetAllMessages())
                {
                    Console.WriteLine($"Received Private Message is: {msg.Body}");
                }
                Console.WriteLine($"Current Queue Message Length:{mq.GetAllMessages().Length}");

                Console.WriteLine($"Received the first private message is : {mq.Receive().Body}");

                Console.WriteLine($"Current Queue Message Length:{mq.GetAllMessages().Length}");
                foreach (var msg in mq.GetAllMessages())
                {
                    Console.WriteLine($"Received Private Message is: {msg.Body}");
                }
            }
        }
    }
}
