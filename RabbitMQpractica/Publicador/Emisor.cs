using RabbitMQ.Client;
using System.Text;

var fabrica = new ConnectionFactory() { HostName = "localhost" };
using(var connection = fabrica.CreateConnection())
//se crea un canal
using(var canal = connection.CreateModel())
{
    // el canal permite que los mensajes lleguen a la cola Queue. Si la cola de mensaje no existe, se crea.
    canal.QueueDeclare(queue: "hello",durable: false, 
        exclusive: false, 
        autoDelete: false, 
        arguments: null);
    // se crea un mensaje
    string mensaje = "Hola conejo!";
    // se codifica el mensaje a bytes
    var cuerpo = Encoding.UTF8.GetBytes(mensaje);
    // se envía el mensaje
    canal.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: cuerpo);
    // visualizar el mensaje enviado en consola para ver si la línea de ejecución llegó hasta este punto y qué se envió.
    Console.WriteLine("[x] Enviado {0}", mensaje);
}

Console.WriteLine("Presione una tecla para salir.");
Console.ReadLine();



