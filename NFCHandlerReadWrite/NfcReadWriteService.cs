using NdefLibrary.Ndef;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Core;

namespace NFCHandlerReadWrite
{
    partial class NfcReadWriteService : ServiceBase
    {
        public NfcReadWriteService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
        private bool NfcTag_ReadWrite()
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
