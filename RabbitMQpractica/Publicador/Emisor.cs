﻿using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
//se crea una conexión con el broker
using(var connection = factory.CreateConnection())
{
    //se crea un canal
    using(var channel = connection.CreateModel())
    {
        // el canal permite que los mensajes lleguen a la cola Queue. Si la cola de mensaje no existe, se crea.
        channel.QueueDeclare(queue: "Saludo", durable: false, exclusive: false, autoDelete: false, arguments: null);
        // se crea un mensaje
        string mensaje = "Hola conejo!";
        // se codifica el mensaje a bytes
        var cuerpo = Encoding.UTF8.GetBytes(mensaje);
        // se envía el mensaje
        channel.BasicPublish(exchange: "", routingKey: "Saludo", basicProperties: null, body: cuerpo);
        // visualizar el mensaje enviado en consola para ver si la línea de ejecución llegó hasta este punto y qué se envió.
        Console.WriteLine($"[x] {mensaje}");
    }
}