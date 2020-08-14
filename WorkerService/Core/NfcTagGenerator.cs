using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NdefLibrary.Ndef;

namespace WorkerService.Core
{
    public static class NfcTagGenerator
    {
        public const string FileDirectory = "NFCtags";
        public const string FileNameHex = "NfcTag-Hex-{0}.ndef";
        public const string FileNameBin = "NfcTag-Bin-{0}.ndef";

        public static  readonly Dictionary<string, NdefRecord> NfcRecords = new Dictionary<string, NdefRecord>
        {
            {"URL-http", new NdefUriRecord { Uri = ""}},
            {"URL", new NdefUriRecord { Uri = "nfcinteractor:compose"}},
            {"URL-SpecialChars", new NdefUriRecord { Uri = "custom:Testmessage -_(){}\":@äöüÄÖÜ"}},
            {"Mailto", new NdefMailtoRecord { Subject = "Feedback for the NDEF Library", Body = "I think the NDEF library is ...", Address = "test@live.com"}},
            {"SMS", new NdefSmsRecord { SmsNumber = "+1234", SmsBody = "Check out the NDEF library"}},
            {"SMS-SpecialChars", new NdefSmsRecord { SmsNumber = "+1 (2) 3456 - 789", SmsBody = "Testmessage -_(){}\":@äöüÄÖÜ"}},
            {"Geo", new NdefGeoRecord { Latitude = 48.168604, Longitude = 16.33375, GeoType = NdefGeoRecord.NfcGeoType.GeoUri}},
            {"Android", new NdefAndroidAppRecord { PackageName = "com.twitter.android"}},
            {"Social", new NdefSocialRecord { SocialType = NdefSocialRecord.NfcSocialType.Twitter, SocialUserName = "test1234"}},
            {"TextUtf8", new NdefTextRecord { Text = "NFC interactor", LanguageCode = "en", TextEncoding = NdefTextRecord.TextEncodingType.Utf8}},
            {"TextUtf16", new NdefTextRecord { Text = "NFC interactor", LanguageCode = "en", TextEncoding = NdefTextRecord.TextEncodingType.Utf16}},
            {"Tel", new NdefTelRecord { TelNumber = "+1234" }},
            {"Ext", new NdefRecord { TypeNameFormat = NdefRecord.TypeNameFormatType.ExternalRtd, Type = Encoding.UTF8.GetBytes("nfcinteractor.com:nfc"), Payload = Encoding.UTF8.GetBytes("Testing")}},
            {"Empty", new NdefRecord { TypeNameFormat = NdefRecord.TypeNameFormatType.Empty }}
        };

       

      
        public static  void WriteTagFile(string pathName, string tagName, NdefRecord ndefRecord)
        {
            WriteTagFile(pathName, tagName, new NdefMessage { ndefRecord });
        }

        public static  void WriteTagFile(string pathName, string tagName, NdefMessage ndefMessage)
        {
            // NDEF message
            var ndefMessageBytes = ndefMessage.ToByteArray();

            // Write NDEF message to binary file
            var binFileName = string.Format(FileNameBin, tagName);
            using (var fs = File.Create(Path.Combine(pathName, binFileName)))
            {
                foreach (var curByte in ndefMessageBytes)
                {
                    fs.WriteByte(curByte);
                }
            }

            // Write NDEF message to hex file
            var hexFileName = string.Format(FileNameHex, tagName);
            using (var fs = File.Create(Path.Combine(pathName, hexFileName)))
            {
                using (var logFileWriter = new StreamWriter(fs))
                {
                    logFileWriter.Write(ConvertToHexByteString(ndefMessageBytes));
                }
            }
        }

        public static string ConvertToHexByteString(byte[] ndefMessageBytes)
        {
            return BitConverter.ToString(ndefMessageBytes).Replace("-", " ");
        }
    }
}
