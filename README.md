## Azure-Queue-Storage

About azure storage queue details.

	•	Azure Queue Storage is a service for storing large numbers of messages. 
	•	Access messages from anywhere in the world via authenticated calls using HTTP or HTTPS. 
	•	A queue message can be up to 64 KB in size. 
	•	The maximum time that a message can remain in the queue is 7 days. the maximum time-to-live can be any positive number, or -1 indicating that the message doesn't expire.
	•	Messages in Storage queues are typically first-in-first-out, but sometimes they can be out of order; for example, when a message's visibility timeout duration expires.
	•	DequeueCount count is maintained by Azure Queue service, this is per message count. The DequeueCount element has a value of 1 the first time the message is dequeued. This value is incremented each time the message is subsequently dequeued

Add NuGet packages

	•	Azure.storage.queue
	•	Microsoft.extensions.azure
	•	Microsoft.extensions.hosting

Add extension in Configureservices in startup.cs file

	•	AddHostedService – for background service
      
      services.AddHostedService<class file name>();
      
	•	AddAzureClients  -- for adding azure queue storage client
  
      services.AddAzureClients(builder =>
                {
                    builder.AddClient<QueueClient, QueueClientOptions>((_, _, _) =>
                    {
                        return new QueueClient
                        (
                            Configuration["StorageConnectionString"]
                            , Configuration["QueueName"]
                            , new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
                    });
                });

