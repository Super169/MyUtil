using MyUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSTRUCT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program start");

            Test01();
            Test02();

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();

        }

        private static void WaitKey()
        {
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        private static void DumpSTRUCT(STRUCT data, string message = "")
        {
            Console.WriteLine("\n\n");
            Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine(data.Info());
            WaitKey();
        }

        private static void Test01()
        {
            Console.WriteLine("\n\nTest - 01: direct use STRUCT\n");

            STRUCT a0 = new STRUCT(false);
            a0.AddField("byte", STRUCT.TYPE.BYTE);
            a0.AddField("uint8", STRUCT.TYPE.UINT8);
            a0.AddField("int8", STRUCT.TYPE.INT8);
            a0.AddField("uint16", STRUCT.TYPE.UINT16);
            a0.AddField("int16", STRUCT.TYPE.INT16);
            a0.AddField("uint32", STRUCT.TYPE.UINT32);
            a0.AddField("int32", STRUCT.TYPE.INT32);
            a0.AddField("uint64", STRUCT.TYPE.UINT64);
            a0.AddField("int64", STRUCT.TYPE.INT64);
            a0.AddField("b4", STRUCT.TYPE.BYTE, 4);
            a0.AddField("i4", STRUCT.TYPE.INT16, 4);
            a0.AddField("name", STRUCT.TYPE.CHAR, 20);
            a0.AddField("address", STRUCT.TYPE.CHAR, 20);
            a0.AddField("true", STRUCT.TYPE.BOOL, 4);
            a0.AddField("false", STRUCT.TYPE.BOOL);
            DumpSTRUCT(a0, "Empty STRUCT without object alignment");


            STRUCT a1 = new STRUCT(true);
            a1.AddField("byte", STRUCT.TYPE.BYTE);
            a1.AddField("uint8", STRUCT.TYPE.UINT8);
            a1.AddField("int8", STRUCT.TYPE.INT8);
            a1.AddField("uint16", STRUCT.TYPE.UINT16);
            a1.AddField("int16", STRUCT.TYPE.INT16);
            a1.AddField("uint32", STRUCT.TYPE.UINT32);
            a1.AddField("int32", STRUCT.TYPE.INT32);
            a1.AddField("uint64", STRUCT.TYPE.UINT64);
            a1.AddField("int64", STRUCT.TYPE.INT64);
            a1.AddField("b4", STRUCT.TYPE.BYTE, 4);
            a1.AddField("i4", STRUCT.TYPE.INT16, 4);
            a1.AddField("name", STRUCT.TYPE.CHAR, 20);
            a1.AddField("address", STRUCT.TYPE.CHAR, 20);
            a1.AddField("true", STRUCT.TYPE.BOOL, 4);
            a1.AddField("false", STRUCT.TYPE.BOOL);

            DumpSTRUCT(a1, "Empty STRUCT with object alignment");


            Byte[] buffer = new byte[255];
            for (int i = 0; i < 255; i++) buffer[i] = 0xFF;
            a1.LoadBuffer(buffer);

            DumpSTRUCT(a1, "STRUCT filled with 0xFF");

            a1.Clear();
            DumpSTRUCT(a1, "STRUCT after Clear");

            a1.SetNumeric("byte", 234);
            a1.SetNumeric("uint8", 255);
            a1.SetNumeric("int8", -123);
            a1.SetNumeric("uint16", 45678);
            a1.SetNumeric("int16", -12345);
            a1.SetNumeric("uint32", 2345678901);
            a1.SetNumeric("int32", -1234567890);
            a1.SetNumeric("uint64", 9876543210987654321);
            a1.SetNumeric("int64", -9012345678901234567);
            a1.SetNumeric("b4", 1, 0);
            a1.SetNumeric("b4", 2, 1);
            a1.SetNumeric("b4", 3, 2);
            a1.SetNumeric("b4", 4, 3);
            a1.SetNumeric("i4", 1, 0);
            a1.SetNumeric("i4", -2, 1);
            a1.SetNumeric("i4", 3, 2);
            a1.SetNumeric("i4", -4, 3);
            a1.SetString("name", "123456789012345678901234567890");
            a1.SetString("address", "Location");
            a1.SetBool("true", true, 0);
            a1.SetBool("true", false, 1);
            a1.SetBool("true", true, 2);
            a1.SetBool("true", false, 3);
            a1.SetBool("false", false);

            DumpSTRUCT(a1, "STRUCT filled use Set<Type>");

            a1.Clear();
            a1.SetValue("byte", 234);
            a1.SetValue("uint8", 255);
            a1.SetValue("int8", -123);
            a1.SetValue("uint16", 45678);
            a1.SetValue("int16", -12345);
            a1.SetValue("uint32", 2345678901);
            a1.SetValue("int32", -1234567890);
            a1.SetValue("uint64", 9876543210987654321);
            a1.SetValue("int64", -9012345678901234567);
            a1.SetValue("b4", 1, 0);
            a1.SetValue("b4", 2, 1);
            a1.SetValue("b4", 3, 2);
            a1.SetValue("b4", 4, 3);
            a1.SetValue("i4", 1, 0);
            a1.SetValue("i4", -2, 1);
            a1.SetValue("i4", 3, 2);
            a1.SetValue("i4", -4, 3);
            a1.SetValue("name", "123456789012345678901234567890");
            a1.SetValue("address", "Location");
            a1.SetValue("true", true, 0);
            a1.SetValue("true", false, 1);
            a1.SetValue("true", true, 2);
            a1.SetValue("true", false, 3);
            a1.SetValue("false", false);

            DumpSTRUCT(a1, "STRUCT filled use SetValue");
            
            string s;
            /*
            Console.WriteLine("\n");
            Console.WriteLine("Get content using Get<Type>\n");
            Console.WriteLine(string.Format("{0,-10} : 0x{1:X2}", "byte", a1.GetNumeric<byte>("byte")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint8", a1.GetNumeric<byte>("uint8")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int8", a1.GetNumeric<sbyte>("int8")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint16", a1.GetNumeric<UInt16>("uint16")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int16", a1.GetNumeric<Int16>("int16")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint32", a1.GetNumeric<UInt32>("uint32")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int32", a1.GetNumeric<Int32>("int32")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint64", a1.GetNumeric<UInt64>("uint64")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int64", a1.GetNumeric<Int64>("int64")));
            s = "";
            for (int i = 0; i < 4; i++) s += string.Format("0x{0:X2} ", a1.GetNumeric<byte>("b4", i));
            Console.WriteLine(string.Format("{0,-10} : {1}", "b4", s));
            s = "";
            for (int i = 0; i < 4; i++) s += string.Format("{0} ", a1.GetNumeric<Int32>("i4", i));
            Console.WriteLine(string.Format("{0,-10} : {1}", "i4", s));
            Console.WriteLine(string.Format("{0,-10} : {1}", "name", a1.GetString("name")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "address", a1.GetString("address")));
            s = "";
            for (int i = 0; i < 4; i++) s += string.Format("{0} ", a1.GetBool("true", i));
            Console.WriteLine(string.Format("{0,-10} : {1}", "true", s));
            Console.WriteLine(string.Format("{0,-10} : {1}", "false", a1.GetBool("false")));
            WaitKey();

            Console.WriteLine("\n");
            Console.WriteLine("Get content using GetValue\n");
            Console.WriteLine(string.Format("{0,-10} : 0x{1:X2}", "byte", a1.GetValue<byte>("byte")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint8", a1.GetValue<byte>("uint8")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int8", a1.GetValue<sbyte>("int8")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint16", a1.GetValue<UInt16>("uint16")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int16", a1.GetValue<Int16>("int16")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint32", a1.GetValue<UInt32>("uint32")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int32", a1.GetValue<Int32>("int32")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "uint64", a1.GetValue<UInt64>("uint64")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "int64", a1.GetValue<Int64>("int64")));
            s = "";
            for (int i = 0; i < 4; i++) s += string.Format("0x{0:X2} ", a1.GetValue<byte>("b4", i));
            Console.WriteLine(string.Format("{0,-10} : {1}", "b4", s));
            s = "";
            for (int i = 0; i < 4; i++) s += string.Format("{0} ", a1.GetValue<Int32>("i4", i));
            Console.WriteLine(string.Format("{0,-10} : {1}", "i4", s));
            Console.WriteLine(string.Format("{0,-10} : {1}", "name", a1.GetValue<string>("name")));
            Console.WriteLine(string.Format("{0,-10} : {1}", "address", a1.GetValue<string>("address")));
            s = "";
            for (int i = 0; i < 4; i++) s += string.Format("{0} ", a1.GetValue<bool>("true", i));
            Console.WriteLine(string.Format("{0,-10} : {1}", "true", s));
            Console.WriteLine(string.Format("{0,-10} : {1}", "false", a1.GetValue<bool>("false")));
            WaitKey();

            STRUCT a2 = new STRUCT(a1);
            DumpSTRUCT(a2, "Clone STRUCT using constructor");
                       
            STRUCT a3 = new STRUCT(false);
            a3.Clone(a1, false);
            DumpSTRUCT(a3, "Clone STRUCT using Clone without copy value");

            a3.Copy(a1, true);
            DumpSTRUCT(a3, "Copy value with Copy");
            */
            STRUCT a4 = new STRUCT(false);
            a4.Clone(a1, true);
            DumpSTRUCT(a4, "Clone STRUCT using Clone with copy value");

            a4.Clear();
            a1.ReBuildBuffer();
            a4.LoadBuffer(a1.buffer);
            DumpSTRUCT(a4, "Load from buffer");

            STRUCT a5 = new STRUCT(false);
            a5.AddField("byte", STRUCT.TYPE.BYTE);
            a5.AddField("uint8", STRUCT.TYPE.UINT8);
            a5.AddField("int8", STRUCT.TYPE.INT8);
            a5.AddField("uint32", STRUCT.TYPE.UINT32);
            a5.AddField("int32", STRUCT.TYPE.INT32);
            a5.AddField("New64", STRUCT.TYPE.UINT64);
            a5.AddField("Missing64", STRUCT.TYPE.INT64);
            a5.AddField("b4", STRUCT.TYPE.BYTE, 4);
            a5.AddField("i4", STRUCT.TYPE.INT16, 4);
            a5.AddField("name", STRUCT.TYPE.CHAR, 20);
            a5.AddField("Missing", STRUCT.TYPE.CHAR, 20);
            a5.AddField("true", STRUCT.TYPE.BOOL, 4);
            a5.AddField("false", STRUCT.TYPE.BOOL);
            DumpSTRUCT(a5, "Another STRUCT with different structure");

            a5.Copy(a1, true);
            DumpSTRUCT(a5, "Copy with missing fields allowed");

            a5.Copy(a1, false);
            DumpSTRUCT(a5, "Copy with missing fields NOT allowed");

        }

        private static void Test02()
        {
            PoseInfo data = new PoseInfo();

            data.cmd = 0x12;
            data.actionId = 2;
            data.poseId = 312;
            data.enabled = true;
            data.servoTime = 800;
            data.waitTime = 1000;
            data.SetServoAngle(1, 180);
            data.SetServoAngle(2, 90);
            data.SetServoAngle(3, 120);
            data.SetServoAngle(4, 150);
            data.SetServoAngle(5, 180);
            data.SetServoAngle(6, 240);
            data.SetServoLED(1, 1);
            data.SetServoLED(2, 2);
            data.SetServoLED(3, 3);
            data.SetServoLED(4, 1);
            data.SetServoLED(5, 2);
            data.SetServoLED(6, 3);

            Console.WriteLine("\n\nShow data of PoseInfo\n");
            Console.WriteLine(string.Format("{0,-20} : 0x{1:X2}", "cmd", data.cmd));
            Console.WriteLine(string.Format("{0,-20} : {1}", "actionId", data.actionId));
            Console.WriteLine(string.Format("{0,-20} : {1}", "poseId", data.poseId));
            Console.WriteLine(string.Format("{0,-20} : {1}", "enabled", data.enabled));
            Console.WriteLine(string.Format("{0,-20} : {1}", "servoTime", data.servoTime));
            Console.WriteLine(string.Format("{0,-20} : {1}", "waitTime", data.waitTime));
            string sA, sLED;
            sA = "";
            sLED = "";
            for (int i = 1; i <= 32; i++)
            {
                sA += string.Format(" {0,3}", data.ServoAngle(i));
                sLED += "  " + Convert.ToString(data.ServoLED(i), 2).PadLeft(2, '0');
            }
            Console.WriteLine(string.Format("{0,-20} :{1}", "ServoAngle", sA));
            Console.WriteLine(string.Format("{0,-20} :{1}", "ServoLED", sLED));

            WaitKey();

            DumpSTRUCT(data.GetSTRUCT() ,  "Dump PoseInfo");

            byte[] buffer = data.GetBuffer();

            bool enabled = data.enabled;

            return;

        }
    }
}
