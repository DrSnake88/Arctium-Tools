﻿/*
 * Copyright (C) 2012-2013 Arctium Emulation <http://arctium.org>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Awps.Structures;

namespace Awps.Log
{
    class PacketLog
    {
        static FileLog logger;
        static BlockingCollection<string> logQueue = new BlockingCollection<string>();

        public static async void Initialize(string directory, string file)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            logger = new FileLog(directory, file);

            await Task.Delay(1).ContinueWith(async _ =>
            {
                while (true)
                {
                    var log = logQueue.Take();

                    if (log != null)
                        await logger.Write(log);
                }
            });
        }

        public static async void Write(Packet packet, string type)
        {
            Func<Task> write = async delegate
            {
                var sb = new StringBuilder();

                sb.Append(string.Format("Time: {0};OpcodeType: {1};OpcodeValue: {2};Packet: ", packet.TimeStamp, type, packet.Message));
                
                foreach (var b in packet.Data)
                    sb.Append(string.Format("{0:X2}", b));

                sb.Append(";");

                await Task.Run(new Action(delegate() { logQueue.Add(sb.ToString()); }));
            };

            await write();
        }
    }
}
