using Azure.Storage.Queues;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Queue_Storage
{
    public class WeatherDataService : BackgroundService
    {
        private readonly ILogger<WeatherForecast> logger;
        private readonly QueueClient queueClient;

        public WeatherDataService(ILogger<WeatherForecast> _logger, QueueClient _queueClient)
        {
            this.logger = _logger;
            queueClient = _queueClient;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                logger.LogInformation ("Reading from queue");
                var queueMesaage = await  queueClient.ReceiveMessageAsync();

                if(queueMesaage.Value!=null)
                {
                    var weatherData = JsonSerializer.Deserialize<WeatherForecast>(queueMesaage.Value.MessageText);
                    logger.LogInformation("new message read: { weatherData }", weatherData);
                    await queueClient.DeleteMessageAsync(queueMesaage.Value.MessageId, queueMesaage.Value.PopReceipt);
                }
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}