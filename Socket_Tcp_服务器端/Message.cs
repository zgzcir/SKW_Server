using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket_Tcp_服务器端
{
    class Message
    {
        public int startIndex = 0;
        public byte[] Data { get; } = new byte[1024];

        public int StartIndex { get; } = 0;

        public int RemainSize
        {
            get
            {
                return Data.Length - startIndex;
            }
        }
        public void AddCount(int count)
        {
            startIndex += count;
        }

        public void ReadMessage()
        {
            while (true)
            {
                if (startIndex <= 4) return;
                int length = BitConverter.ToInt32(Data, 0);
                if (startIndex - 4 >= length)
                {
                    string s = Encoding.UTF8.GetString(Data, 4, length);
                    Console.WriteLine("Client: " + s);
                    Array.Copy(Data, length + 4, Data, 0, startIndex - 4 - length);
                    startIndex -= length + 4;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
