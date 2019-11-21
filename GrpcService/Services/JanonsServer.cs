using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class JanonsServer : Jansoner.JansonerBase
    {
        public override Task<DataReply> SendInfo(DataRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DataReply
            {
                Message = $"{DateTime.Now} - { request.Name} ",
                IsSuccess = true
            });
        }

        public override async Task GetList(NonMessage request, IServerStreamWriter<ListMessage> responseStream, ServerCallContext context)
        {
            var list = new List<ListMessage>();

            list.Add(new ListMessage
            {
                Id = "1",
                Age = "10",
                Email = "123",
                Name = "Lucy"
            });

            list.Add(new ListMessage
            {
                Id = "2",
                Age = "20",
                Email = "22",
                Name = "222ucy"
            });
            list.Add(new ListMessage
            {
                Id = "3",
                Age = "30",
                Email = "323",
                Name = "3Lucy"
            });
            list.Add(new ListMessage
            {
                Id = "4",
                Age = "40",
                Email = "423",
                Name = "4Lucy"
            });

            foreach (var msg in list)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(msg);
            }
        }
    }
}
