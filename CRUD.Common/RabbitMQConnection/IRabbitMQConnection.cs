using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Common.RabbitMQConnection
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
