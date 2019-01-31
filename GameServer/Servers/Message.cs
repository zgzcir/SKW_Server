using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer
{
    class Message
    {
        public int startIndex = 0;
        public byte[] data { get; } = new byte[1024];

        public int StartIndex { get; } = 0;

        public int RemainSize
        {
            get
            {
                return data.Length - startIndex;
            }
        }
        public void ReadMessage(int newDataAmount, Action<RequestCode, ActionCode, string> processDataCAllBack)
        {
            startIndex += newDataAmount;
            while (true)
            {
                if (startIndex <= 4) return;
                int count = BitConverter.ToInt32(data, 0);
                if (startIndex - 4 >= count)
                {
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                    ActionCode actiopnCode = (ActionCode)BitConverter.ToInt32(data, 8);
                    string s = Encoding.UTF8.GetString(data, 12, count - 8);
                    processDataCAllBack(requestCode, actiopnCode, s);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= count + 4;
                }
                else
                {
                    return;
                }
            }
        }
        public static
        byte[] PackData(ActionCode actionCode, string data)
        {
            byte[] requstCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            int dataAmount = requstCodeBytes.Length + dataBytes.Length;

            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);

            byte[] newBytes = dataAmountBytes.Concat(requstCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();

            return newBytes;
        }
    }
}
