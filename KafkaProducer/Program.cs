namespace KafkaProducer                                                                                                                    
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
	  Console.WriteLine("### KafkaProducer - start");
		
      var config = new Dictionary<string, object>
      {
        { "bootstrap.servers", "localhost:9092" },
        { "message.timeout.ms", 1000 },
        { "message.send.max.retries", 0 }	    
      };
	  
	  var topicName = "hello-topic";
	  
      using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
      {
        Console.WriteLine($">> KafkaProducer {producer.Name} on {topicName}. quit to exit.");
        string text = null;

        while (text != "quit")
        {
          text = Console.ReadLine();                                                                                                                                                              
          var deliveryReport = producer.ProduceAsync(topicName, null, text);
		  
		  //gestione errori invio
		  deliveryReport.ContinueWith(task => { 		 
			if (task.Result.Error.HasError) {
				Console.WriteLine($"[ERROR-01] IS ERROR RESULT {deliveryReport.Result.Error.Reason}");
			}
			if (task.IsFaulted){
				Console.WriteLine("[ERROR-02] IS FAULTED");
			}
			if (task.Exception != null){
				Console.WriteLine("[ERROR-03] ex: {deliveryReport.Exception.Message}");
			}
			if (task.IsCanceled){
				Console.WriteLine("[ERROR-04] IS CANCELLED");
			}
		  });
		  
        }

        producer.Flush(100);
		
		Console.WriteLine("### KafkaProducer - end");
      }
    }
  }
}
