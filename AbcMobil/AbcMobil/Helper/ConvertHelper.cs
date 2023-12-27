using System;
using System.Text;

namespace AbcMobil.Helper
{
    public static class ConvertHelper 
    {
        public static string TextToHex(string metin)
        {
            return ByteToHex(TextToBytes(metin));
        }
        public static string HexToText(string hex)
        {
            return ByteToTexts(HexToByte(hex));
        }
        public static byte[] TextToBytes(string source)
        {
            return Encoding.ASCII.GetBytes(source);
        }
        public static string ByteToTexts(byte[] source)
        {
            return Encoding.ASCII.GetString(source);
        }
        public static byte[] HexToByte(string hex)
        {
            byte[] byteArray = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
                byteArray[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return byteArray;
        }
        public static string ByteToHex(byte[] data)
        {
            StringBuilder builder = new StringBuilder(data.Length * 2);
            foreach (byte bytee in data)
                builder.AppendFormat("{0:X2}", bytee);
            return builder.ToString();
        }
    }
}
