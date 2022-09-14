using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MSMQServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestPublicMsgQueue();
            TestPrivateMsgQueue();
            Console.ReadKey();
        }

        private static void TestPublicMsgQueue()
        {
            string path = @".\TestPublicMSMQ";
            if (!MessageQueue.Exists(path))// 判断是否存在当前队列路径对应的消息队列
            {
                using (var mq = MessageQueue.Create(path))
                {
                    mq.Label = "TestPublicMSMQ";
                    Console.WriteLine("公共队列已创建");
                    Console.WriteLine($"路径：{mq.Path}");
                    Console.WriteLine($"队列名称：{mq.QueueName}");
                    Console.WriteLine($"队列标签：{mq.Label}");

                    mq.Send("Hello Public MSMQ", "MSMQ: Public");
                }
            }

            foreach (var mq in MessageQueue.GetPublicQueues())
            {
                mq.Send($"Sending MSMQ public message {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}", "MSMQ: Public");
                Console.WriteLine($"Public message is sent to {mq.Path}");
            }
        }

        private static void TestPrivateMsgQueue()
        {
            string path = @".\Private$\TestPrivateMSMQ";
            if (MessageQueue.Exists(path))
            {
                MessageQueue.Delete(path);
            }

            if (!MessageQueue.Exists(path))
            {
                using (var mq = MessageQueue.Create(path))
                {
                    mq.Label = "TestPrivateMSMQ";
                    Console.WriteLine("Private Queue has been created");
                    Console.WriteLine($"Path：{mq.Path}");
                    Console.WriteLine($"Queue Name：{mq.QueueName}");
                    Console.WriteLine($"Queue Lable：{mq.Label}");
                    Console.WriteLine($"Queue FormatName：{mq.FormatName}");

                    mq.Send("Hello Private MSMQ", "MSMQ: Private");
                }
            }

            if (MessageQueue.Exists(path))
            {
                var mq = new MessageQueue(path);
                mq.Send($"Sending MSMQ private message {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}", "MSMQ: Private");
                Console.WriteLine($"Private message is sent to {mq.Path}");
            }
        }
    }
}
