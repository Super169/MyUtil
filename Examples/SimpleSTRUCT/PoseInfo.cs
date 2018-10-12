using MyUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSTRUCT
{

    public class PoseInfo
    {
        public const byte RECORD_SIZE = 60;
        public const byte DATA_SIZE = RECORD_SIZE - 4;
        public const byte COMMAND_GET = 0x62;
        public const byte COMMAND_UPDATE = 0x72;

        private static class F
        {
            public const string headCode = "headCode";
            public const string dataSize = "dataSize";
            public const string cmd = "cmd";
            public const string id = "id";
            public const string seq = "seq";
            public const string enabled = "enabled";
            public const string execTime = "execTime";
            public const string waitTime = "waitTime";
            public const string servoAngle = "servoAngle";
            public const string servoLED = "servoLED";
            public const string headLED = "headLED";
            public const string mp3Folder = "mp3Folder";
            public const string mp3File = "mp3File";
            public const string mp3Vol = "mp3Vol";
            public const string checkSum = "checkSum";
            public const string endCode = "endCode";
        }

        private STRUCT _data;

        public STRUCT GetSTRUCT()
        {
            return _data;
        }

        #region "Public properties - Data Element"

        public byte cmd
        {
            get { return _data.GetNumeric<byte>(F.cmd); }
            set { _data.SetNumeric(F.cmd, value); }
        }

        public byte actionId
        {
            get { return _data.GetNumeric<byte>(F.id); }
            set { _data.SetNumeric(F.id, value); }
        }

        public UInt16 poseId
        {
            get { return _data.GetNumeric<UInt16>(F.seq); }
            set { _data.SetNumeric(F.seq, value); }
        }

        public bool enabled
        {
            // get { return _data.GetBool(F.enabled); }
            get { return _data.GetValue<bool>(F.enabled); }
            set { _data.SetBool(F.enabled, value); }
        }

        public UInt16 servoTime
        {
            get { return _data.GetNumeric<UInt16>(F.execTime); }
            set { _data.SetNumeric(F.execTime, value); }
        }

        public UInt16 waitTime
        {
            get { return _data.GetNumeric<UInt16>(F.waitTime); }
            set { _data.SetNumeric(F.waitTime, value); }
        }

        public byte headLed
        {
            get { return _data.GetNumeric<byte>(F.headLED); }
            set { _data.SetNumeric(F.headLED, value); }
        }


        public byte mp3Folder
        {
            get { return _data.GetNumeric<byte>(F.mp3Folder); }
            set { _data.SetNumeric(F.mp3Folder, value); }
        }

        public byte mp3File
        {
            get { return _data.GetNumeric<byte>(F.mp3File); }
            set { _data.SetNumeric(F.mp3File, value); }
        }

        public byte mp3Vol
        {
            get { return _data.GetNumeric<byte>(F.mp3Vol); }
            set { _data.SetNumeric(F.mp3Vol, value); }
        }

        public byte checkSum
        {
            get { return _data.GetNumeric<byte>(F.checkSum); }
            set { _data.SetNumeric(F.checkSum, value); }
        }

        public byte ServoAngle(int id)
        {
            return _data.GetNumeric<byte>(F.servoAngle, id - 1);
        }

        public void SetServoAngle(int id, byte value)
        {
            _data.SetNumeric(F.servoAngle, value, id - 1);
        }

        public byte ServoLED(int id)
        {
            int h = (id - 1) / 4;
            int l = 2 * (3 - ((id - 1) % 4));
            byte mixed = _data.GetNumeric<byte>(F.servoLED, h);
            byte value = (byte)((mixed >> l) & 0b11);
            return value;
        }

        public void SetServoLED(int id, byte value)
        {
            value = (byte)(value & 0b11);
            int h = (id - 1) / 4;
            int l = 2 * (3 - ((id - 1) % 4));
            value = (byte)(value << l);
            byte mixed = _data.GetNumeric<byte>(F.servoLED, h);
            byte mask = (byte)(0b11 << l);
            mask = (byte)~mask;
            mixed &= mask;  // clear the value first
            mixed |= value;
            _data.SetNumeric(F.servoLED, mixed, h);
        }

        #endregion "Public properties - Data Element"



        public PoseInfo()
        {
            _data = new STRUCT(true);
            _data.AddField(F.headCode, STRUCT.TYPE.BYTE, 2);
            _data.AddField(F.dataSize, STRUCT.TYPE.BYTE);
            _data.AddField(F.cmd, STRUCT.TYPE.BYTE);
            _data.AddField(F.id, STRUCT.TYPE.BYTE);
            _data.AddField(F.seq, STRUCT.TYPE.UINT16);
            _data.AddField(F.enabled, STRUCT.TYPE.BOOL);
            _data.AddField(F.execTime, STRUCT.TYPE.UINT16);
            _data.AddField(F.waitTime, STRUCT.TYPE.UINT16);
            _data.AddField(F.servoAngle, STRUCT.TYPE.BYTE, 32);
            _data.AddField(F.servoLED, STRUCT.TYPE.BYTE, 8);
            _data.AddField(F.mp3Folder, STRUCT.TYPE.BYTE);
            _data.AddField(F.mp3File, STRUCT.TYPE.BYTE);
            _data.AddField(F.mp3Vol, STRUCT.TYPE.BYTE);
            int fillerSize = RECORD_SIZE - 2 - _data.size;
            if (fillerSize < 0)
            {
                throw new Exception("PoseInfo : record full");
            }
            if (fillerSize > 0)
            {
                _data.AddField("", STRUCT.TYPE.BYTE, fillerSize);
            }
            _data.AddField(F.checkSum, STRUCT.TYPE.BYTE);
            _data.AddField(F.endCode, STRUCT.TYPE.BYTE);

            _data.SetNumeric(F.headCode, 0xA9, 0);
            _data.SetNumeric(F.headCode, 0x9A, 1);
            _data.SetNumeric(F.dataSize, DATA_SIZE);
            _data.SetNumeric(F.cmd, COMMAND_GET);
            _data.SetNumeric(F.endCode, 0xED);

        }


        public byte[] GetBuffer()
        {
            _data.ReBuildBuffer();
            return _data.buffer;
        }

        public bool LoadBuffer(byte[] buffer)
        {
            return _data.LoadBuffer(buffer);
        }
    }
}
