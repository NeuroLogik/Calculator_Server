using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        ITransport transport;
        private delegate void Command(byte[] data);
        Dictionary<byte, Command> commandsTable = new Dictionary<byte, Command>();

        public Server(ITransport transport)
        {
            this.transport = transport;
            commandsTable[0] = Addition;
            commandsTable[1] = Subtraction;
            commandsTable[2] = Product;
            commandsTable[3] = Division;
        }

        public void Run()
        {
            while (true)
            {
                byte[] data = transport.Receive();

                if (data != null)
                {
                    byte command = data[0];
                    commandsTable[command](data);
                }
            }
        }
        
        void Addition(byte[] data)
        {
            float firstNum = BitConverter.ToSingle(data, 1);
            float secondNum = BitConverter.ToSingle(data, 5);

            float result = firstNum + secondNum;
            Packet response = new Packet(result);
            byte[] returnData = response.GetData();
            transport.Send(returnData);
        }

        void Subtraction(byte[] data)
        {
            float firstNum = BitConverter.ToSingle(data, 1);
            float secondNum = BitConverter.ToSingle(data, 5);

            float result = firstNum - secondNum;
            Packet response = new Packet(result);
            byte[] returnData = response.GetData();
            transport.Send(returnData);
        }

        void Product(byte[] data)
        {
            float firstNum = BitConverter.ToSingle(data, 1);
            float secondNum = BitConverter.ToSingle(data, 5);

            float result = firstNum * secondNum;
            Packet response = new Packet(result);
            byte[] returnData = response.GetData();
            transport.Send(returnData);
        }

        void Division(byte[] data)
        {
            float firstNum = BitConverter.ToSingle(data, 1);
            float secondNum = BitConverter.ToSingle(data, 5);

            float result = firstNum / secondNum;
            Packet response = new Packet(result);
            byte[] returnData = response.GetData();
            transport.Send(returnData);
        }
    }
}
