using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };

using(var conexion = factory.CreateConnection())
{
	using (var canal = conexion.CreateModel())
	{
		canal.QueueDeclare(queue: "Saludo", durable: false, autoDelete: false, arguments: null);

		var consumidor = new EventingBasicConsumer(canal);

		consumidor.Received += (model, ea) =>
		{
			var cuerpo = ea.Body.ToArray();
			var mensaje = Encoding.UTF8.GetString(cuerpo);

			Console.WriteLine($"[x] {mensaje}");
		};

		canal.BasicConsume(queue:"Saludo",autoAck:true, consumer:consumidor);
	}
}

Console.WriteLine("Presione una tecla para salir.");
Console.ReadLine();