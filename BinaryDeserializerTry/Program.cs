using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


namespace BinaryDeserializerTry
{
    class Program
    {
        static void Main(string[] args)
        {
            TestStruct testStruct = new TestStruct();
            decimal x = 44.55m;
            byte[] byteArraywithDecimal = GetBytes(x);
            byte[] byteArray = new byte[] { 0x30, 0x31, 0x42 };
            DateTime dt = new DateTime();
            UInt32 num1 = 4;
            byte[] bytesStringnum = BitConverter.GetBytes(num1);
            byte[] bytesString = Encoding.ASCII.GetBytes("This is some text");
            var s = new MemoryStream();
            s.Write(bytesStringnum, 0, bytesStringnum.Length);
            s.Write(bytesString, 0, bytesString.Length);
            byte[] bytesStringX = s.ToArray();

            
            dt = DateTime.Now;
            byte[] byteArrayTime = BitConverter.GetBytes(dt.Ticks);

            ByteArrayParsers.ParseByteArrayToStruct_LittleEndian(bytesStringX, ref testStruct);

            Console.WriteLine($"StructSize is {ByteArrayParsers.GetStructSize(testStruct)}");

            ByteArrayParsers.IterateStruct(ref testStruct);
        }

        public static byte[] GetBytes(decimal dec)
        {
            //Load four 32 bit integers from the Decimal.GetBits function
            Int32[] bits = decimal.GetBits(dec);
            //Create a temporary list to hold the bytes
            List<byte> bytes = new List<byte>();
            //iterate each 32 bit integer
            foreach (Int32 i in bits)
            {
                //add the bytes of the current 32bit integer
                //to the bytes list
                bytes.AddRange(BitConverter.GetBytes(i));
            }
            //return the bytes list as an array
            return bytes.ToArray();
        }
    }

    public struct TestStruct 
    {

        UInt16 ui1;
        String str1;
    }
}
