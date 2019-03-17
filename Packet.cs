﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    class Packet
    {
        MemoryStream stream;
        BinaryWriter writer;

        public Packet()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public Packet(params object[] elements) : this()
        {
            foreach(object element in elements)
            {
                if (element is float)
                {
                    writer.Write((float)element);
                }
                else if (element is int)
                {
                    writer.Write((int)element);
                }
                else
                {
                    throw new Exception("(Server): Data submitted not supported, please insert int or float values only");
                }
            }
        }

        public byte[] GetData()
        {
            return stream.ToArray();
        }
    }
}
