using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest()
            {
                Name = "JANSON-1991"
            });
            Console.WriteLine(reply.Message);

            var client2 = new Jansoner.JansonerClient(channel);
            var reply2 = await client2.SendInfoAsync(new DataRequest
            {
                Id = 1,
                Name = "janson",
                Pwd = "123"

            });

            Console.WriteLine(reply2.Message + reply2.IsSuccess);

            using (var reply3 = client2.GetList(new NonMessage()))
            {
                while (await reply3.ResponseStream.MoveNext())
                {
                    var current = reply3.ResponseStream.Current;
                    Console.WriteLine("Age" + current.Age + "Email" + current.Email + "Id" + current.Id + "Name" + current.Name);
                }
            }

            Console.ReadKey();
        }
    }
}
