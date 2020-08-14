using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NdefLibrary.Ndef;
using WorkerService.Core;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override  Task StartAsync(CancellationToken stoppingToken)
        {
            
                _logger.LogInformation("Worker Start at: {time}", DateTimeOffset.Now);
            return base.StartAsync(stoppingToken);
               
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("Worker Start at: {time}", DateTimeOffset.Now);
            return base.StopAsync(stoppingToken);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (NfcTag_Read())
                {
                    _logger.LogInformation("Worker tag read running at: {time}", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogInformation("Worker tag read not responded at: {time}", DateTimeOffset.Now);
                }
                //if (NfcTag_Write())
                //{
                //    _logger.LogInformation("Worker tag write running at: {time}", DateTimeOffset.Now);
                //}
                //else
                //{
                //    _logger.LogInformation("Worker tag write not responded at: {time}", DateTimeOffset.Now);
                //}
                await Task.Delay(1000, stoppingToken);
            }
        }

       
        private bool NfcTag_Write()
        {

            try
            {
                var spRecord = new NdefSpRecord
                {
                    Uri = "",
                    NfcAction = NdefSpActRecord.NfcActionType.DoAction
                };
                spRecord.AddTitle(new NdefTextRecord { LanguageCode = "en", Text = "NFC Library" });
                spRecord.AddTitle(new NdefTextRecord { LanguageCode = "de", Text = "NFC Bibliothek" });

                NfcTagGenerator.NfcRecords.Add("SmartPoster", spRecord);

                // Ensure the path exists
                var tagsDirectory = Path.Combine(Environment.CurrentDirectory, NfcTagGenerator.FileDirectory);
                Directory.CreateDirectory(tagsDirectory);

                // Write tag contents to files
                foreach (var curNdefRecord in NfcTagGenerator.NfcRecords)
                {
                    NfcTagGenerator.WriteTagFile(tagsDirectory, curNdefRecord.Key, curNdefRecord.Value);
                }

                // Multi-record file
                var record1 = new NdefUriRecord { Uri = "https://www.twitter.com" };
                var record2 = new NdefAndroidAppRecord { PackageName = "com.twitter.android" };
                var twoRecordsMsg = new NdefMessage { record1, record2 };
                NfcTagGenerator.WriteTagFile(tagsDirectory, "TwoRecords", twoRecordsMsg);

                var record3 = new NdefRecord
                {
                    TypeNameFormat = NdefRecord.TypeNameFormatType.ExternalRtd,
                    Type = Encoding.UTF8.GetBytes("custom.com:myapp")
                };
                var threeRecordsMsg = new NdefMessage { record1, record3, record2 };
                NfcTagGenerator.WriteTagFile(tagsDirectory, "ThreeRecords", threeRecordsMsg);

                // Success message on output
                Console.WriteLine("Generated {0} tag files in {1}.", NfcTagGenerator.NfcRecords.Count, tagsDirectory);
                Debug.WriteLine("Generated {0} tag files in {1}.", NfcTagGenerator.NfcRecords.Count, tagsDirectory);
                return true;
            }
            catch(NdefException ex)
            {
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        private bool NfcTag_Read()
        {

            try
            {
                byte[] arr = Enumerable.Repeat((byte)0x20, 10).ToArray();
                NfcTagReader.MessageReceivedHandler(arr);
                return true;
            }
            catch (NdefException ex)
            {
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
