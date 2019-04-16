namespace KafkaConsumer
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using Confluent.Kafka;
  using Confluent.Kafka.Serialization;

  public class Program
  {
    static void Main(string[] args)
    {
		Console.WriteLine("### KafkaConsumer - start");
		
      var config = new Dictionary<string, object>
      {
          { "group.id", "sample-consumer1" },
          { "bootstrap.servers", "localhost:9092" },
          { "enable.auto.commit", "false"}
		  
      };
	  
	  var topicName = "hello-topic";

      using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
      {                
        consumer.Subscribe(new string[]{topicName});

        consumer.OnMessage += (_, msg) => 
        {
          Console.WriteLine($"############# Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
          consumer.CommitAsync(msg);
        };
		
		//gestione errore 
		consumer.OnError += (_, error) => 
		{
		  Console.WriteLine($"[ERROR] Error: {error}");
		};

        while (true)
        {
            consumer.Poll(100);
        }
      }
    }
  }
}