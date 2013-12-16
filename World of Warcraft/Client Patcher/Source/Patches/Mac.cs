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

namespace Connection_Patcher.Patches
{
    class Mac
    {
        public static class x86
        {
            public static byte[] Send  = { 0x31, 0xC9, 0x90 };
            public static byte[] Email = { 0xEB };
            public static byte[] User  = { 0x00 };
            public static byte[] RaF   = { 0x31, 0xC0, 0xEB, 0x47, 0x90, 0x90 };
        }

        public static class x64
        {
            public static byte[] Send  = { 0x31, 0xC9, 0x90 };
            public static byte[] Email = { 0xEB };
            public static byte[] User  = { 0x00 };
            public static byte[] RaF   = { 0x30, 0xDB, 0x31, 0xC0, 0xE9, 0xC6, 0x02, 0x00, 0x00, 0x90, 0x90 };
        }
    }
}
