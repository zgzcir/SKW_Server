using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBytes
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte[] data = Encoding.UTF8.GetBytes("");
            byte[] data = BitConverter.GetBytes(56200000);
            foreach(byte b in data)
            {
                Console.Write(b + ":");
            }
            Console.WriteLine();
        }
    }
}
