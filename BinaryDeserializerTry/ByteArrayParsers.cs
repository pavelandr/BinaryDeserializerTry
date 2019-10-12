using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BinaryDeserializerTry
{
    /// <summary>
    /// Parsing Byte arrays to structs
    /// </summary>
    class ByteArrayParsers
    {
        /// <summary>
        /// Parse byte array to struct as LittleEndian
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="b"> byte array </param>
        /// <param name="strct"> struct</param>
        public static void ParseByteArrayToStruct_LittleEndian<T>(byte[] b, ref T strct)
        {
            //Check if the struct parameter is Value type
            if (!typeof(T).IsValueType)
            {
                throw new ArgumentException("The struct parameter is not of Value type");

            }


            int byteCounter = 0; // Counter for iterating byte array 

            /*variables to store the values of the previous Field*/
            TypeCode previosFieldTypeCode = TypeCode.Empty;
            FieldInfo previosField;
            dynamic value = 0;

            /* Iterate through each field of the struct and set value from the byte array.
             * If the byte array is shorter than struct an ArgumentException will occur.
             */
            try
            {
                foreach (var field in typeof(T).GetFields(BindingFlags.Instance |
                                     BindingFlags.NonPublic |
                                     BindingFlags.Public))
                {
                    TypeCode typeCode = Type.GetTypeCode(field.FieldType);
                    switch (typeCode)
                    {

                        case TypeCode.Boolean:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToBoolean(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Char:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToChar(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Byte:
                            field.SetValueDirect(__makeref(strct), b[byteCounter]);
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.SByte:
                            field.SetValueDirect(__makeref(strct), (sbyte)b[byteCounter]);
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Int16:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToInt16(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.UInt16:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToUInt16(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Int32:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToInt32(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.UInt32:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToUInt32(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Int64:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToInt64(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.UInt64:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToUInt64(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Single:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToSingle(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Double:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToDouble(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Decimal:
                            decimal decimalTemp = 0m;
                            Int32[] int32Temp = new Int32[] { BitConverter.ToInt32(b, byteCounter),
                            BitConverter.ToInt32(b, byteCounter + 4),
                            BitConverter.ToInt32(b, byteCounter + 8),
                            BitConverter.ToInt32(b, byteCounter + 12) };
                            try
                            {
                                decimalTemp = new decimal(int32Temp);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            field.SetValueDirect(__makeref(strct), decimalTemp);
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;
                        case TypeCode.DateTime:
                            field.SetValueDirect(__makeref(strct), DateTime.FromBinary(BitConverter.ToInt64(b, byteCounter)));
                            byteCounter += 8;
                            break;
                        case TypeCode.String:
                            //if(previosFieldTypeCode == TypeCode.Byte)
                            if (new List<TypeCode> { TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 }.Contains(previosFieldTypeCode))
                                field.SetValueDirect(__makeref(strct), System.Text.Encoding.UTF8.GetString(b, byteCounter, (int)value));

                            break;
                    }
                    previosFieldTypeCode = typeCode;
                    previosField = field;
                    value = field.GetValue(strct);
                }
            }
            catch (System.ArgumentException)
            {

                throw new System.ArgumentException("Byte array is shorter than struct");
            }


        }


        /// <summary>
        /// Parse byte array to struct as BigEndian
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="b"> byte array </param>
        /// <param name="strct"> struct</param>
        public static void ParseByteArrayToStruct_BigEndian<T>(byte[] b, ref T strct)
        {
            //Check if the struct parameter is Value type
            if (!typeof(T).IsValueType)
            {
                throw new ArgumentException("The struct parameter is not of Value type");
            }


            int byteCounter = 0; // Counter for iterating byte array 

            /*variables to store the values of the previous Field*/
            TypeCode previosFieldTypeCode = TypeCode.Empty;
            FieldInfo previosField;
            dynamic value = 0;

            /* Iterate through each field of the struct and set value from the byte array.
             * If the byte array is shorter than struct an ArgumentException will occur.
             */
            try
            {
                foreach (var field in typeof(T).GetFields(BindingFlags.Instance |
                                     BindingFlags.NonPublic |
                                     BindingFlags.Public))
                {
                    TypeCode typeCode = Type.GetTypeCode(field.FieldType);
                    switch (typeCode)
                    {

                        case TypeCode.Boolean:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToBoolean(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Char:
                            field.SetValueDirect(__makeref(strct), BitConverter.ToChar(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Byte:
                            field.SetValueDirect(__makeref(strct), b[byteCounter]);
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.SByte:
                            field.SetValueDirect(__makeref(strct), (sbyte)b[byteCounter]);
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Int16:
                            Array.Reverse(b, byteCounter, sizeof(Int16));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToInt16(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.UInt16:
                            Array.Reverse(b, byteCounter, sizeof(UInt16));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToUInt16(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Int32:
                            Array.Reverse(b, byteCounter, sizeof(Int32));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToInt32(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.UInt32:
                            Array.Reverse(b, byteCounter, sizeof(UInt32));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToUInt32(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Int64:
                            Array.Reverse(b, byteCounter, sizeof(Int64));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToInt64(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.UInt64:
                            Array.Reverse(b, byteCounter, sizeof(UInt64));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToUInt64(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Single:
                            Array.Reverse(b, byteCounter, sizeof(Single));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToSingle(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Double:
                            Array.Reverse(b, byteCounter, sizeof(Double));
                            field.SetValueDirect(__makeref(strct), BitConverter.ToDouble(b, byteCounter));
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;

                        case TypeCode.Decimal:
                            Array.Reverse(b, byteCounter, sizeof(Decimal));
                            decimal decimalTemp = 0m;
                            Int32[] int32Temp = new Int32[] { BitConverter.ToInt32(b, byteCounter),
                            BitConverter.ToInt32(b, byteCounter + 4),
                            BitConverter.ToInt32(b, byteCounter + 8),
                            BitConverter.ToInt32(b, byteCounter + 12) };
                            try
                            {
                                decimalTemp = new decimal(int32Temp);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            field.SetValueDirect(__makeref(strct), decimalTemp);
                            byteCounter += Marshal.SizeOf(field.FieldType);
                            break;
                        case TypeCode.DateTime:
                            Array.Reverse(b, byteCounter, sizeof(Int64));
                            field.SetValueDirect(__makeref(strct), DateTime.FromBinary(BitConverter.ToInt64(b, byteCounter)));
                            byteCounter += 8;
                            break;
                        case TypeCode.String:
                            Array.Reverse(b, byteCounter, (int)value);
                            if (new List<TypeCode> { TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 }.Contains(previosFieldTypeCode))
                                field.SetValueDirect(__makeref(strct), System.Text.Encoding.UTF8.GetString(b, byteCounter, (int)value));

                            break;
                    }
                    previosFieldTypeCode = typeCode;
                    previosField = field;
                    value = field.GetValue(strct);
                }
            }
            catch (System.ArgumentException)
            {

                throw new System.ArgumentException("Byte array is shorter than the struct");
            }


        }


        /// <summary>
        /// Return struct size in bytes.
        /// Ignores fields that not value type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strct"></param>
        /// <returns></returns>
        public static int GetStructSize<T>(T strct)
        {
            if (!typeof(T).IsValueType)
            {
                throw new ArgumentException("The struct parameter is not of Value type");
            }

            int byteCounter = 0;
            foreach (var field in typeof(T).GetFields(BindingFlags.Instance |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Public))
            {
                if ((field.FieldType).IsValueType)
                    byteCounter += Marshal.SizeOf(field.FieldType);
            }

            return byteCounter;
        }

        /// <summary>
        /// Print struct fields and values to Console.
        /// </summary>
        public static void IterateStruct<T>(ref T strct)
        {
            if (!typeof(T).IsValueType)
            {
                throw new ArgumentException("The struct parameter is not of Value type");
            }

            foreach (var field in typeof(T).GetFields(BindingFlags.Instance |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Public))
            {
                Console.WriteLine("{0} = {1}", field.Name, field.GetValue(strct));
            }
        }

    }
}
