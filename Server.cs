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
        private delegate float Command(float firstNum, float secondNum);
        Dictionary<byte, Command> commandsTable = new Dictionary<byte, Command>();
        public float Result;
        public float FirstNum, SecondNum;
        public Packet Response;
        public byte[] ReturnData;

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
                SingleStep();
            }
        }

        public void SingleStep()
        {
            byte[] data = transport.Receive();

            if (data != null)
            {
                byte command = data[0];
                FirstNum = BitConverter.ToSingle(data, 1);
                SecondNum = BitConverter.ToSingle(data, 5);

                Result = commandsTable[command](FirstNum, SecondNum);

                Response = new Packet(0, Result);
                ReturnData = Response.GetData();
                transport.Send(ReturnData);
            }
        }
        
        public float Addition(float firstNum, float secondNum)
        {
            return firstNum + secondNum;
        }

        public float Subtraction(float firstNum, float secondNum)
        {
            return firstNum - secondNum;
        }

        public float Product(float firstNum, float secondNum)
        {
            return firstNum * secondNum;
        }

        public float Division(float firstNum, float secondNum)
        {
            if(secondNum == 0)
            {
                throw new Exception("Cannot divide by 0");
            }

            return firstNum / secondNum;
        }
    }
}
