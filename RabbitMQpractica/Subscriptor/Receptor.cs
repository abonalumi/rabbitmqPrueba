using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Crear una fábrica de conexión para crear la conexión del cliente con el servidor según explica
// la documentación
var factory = new ConnectionFactory() { HostName = "localhost" };
using(var connection = factory.CreateConnection())
using(var canal = connection.CreateModel())
{
	// Buscar la cola del canal con el nombre definido en el atributo queue.
	// Si aún no existe, lo define con el nombre que se pasó como atributo.
	canal.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

	// Se crea un consumidor para detectar los mensajes
	var consumer = new EventingBasicConsumer(canal);
	consumer.Received += (model, ea) =>
	{
		// Si encuentra un mensaje lo mete en un array.
		var cuerpo = ea.Body.ToArray();
		// Se codifica los bytes del array a cadena de texto.
		var mensaje = Encoding.UTF8.GetString(cuerpo);
		Console.WriteLine(" [x] Received {0}", mensaje);
	};

	// Finalmente se ejecuta la acción en el consumidor que debe mostrar el mensaje que recibió el objeto consumidor.
	canal.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

	Console.WriteLine(" Press [enter] to exit.");
	Console.ReadLine();
}