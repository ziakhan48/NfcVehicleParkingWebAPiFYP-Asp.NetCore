using NdefLibrary.Ndef;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WorkerService.Core
{
    public static class NfcTagReader
    {
        public static void MessageReceivedHandler(byte[] rawMsg)
        {
            // Parse raw byte array to NDEF message
            //var rawMsg = message.ToArray();
            var ndefMessage = NdefMessage.FromByteArray(rawMsg);

            // Loop over all records contained in the NDEF message
            foreach (NdefRecord record in ndefMessage)
            {
                Debug.WriteLine("Record type: " + Encoding.UTF8.GetString(record.Type, 0, record.Type.Length));
                // Go through each record, check if it's a Smart Poster
                if (record.CheckSpecializedType(false) == typeof(NdefSpRecord))
                {
                    // Convert and extract Smart Poster info
                    var spRecord = new NdefSpRecord(record);
                    Debug.WriteLine("URI: " + spRecord.Uri);
                    Debug.WriteLine("Titles: " + spRecord.TitleCount());
                    Debug.WriteLine("1. Title: " + spRecord.Titles[0].Text);
                    Debug.WriteLine("Action set: " + spRecord.ActionInUse());
                }
            }
        }
    }
}
