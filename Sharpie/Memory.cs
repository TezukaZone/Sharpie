using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sharpie
{
    public class Memory
    {
        public static int procID;
        public static IntPtr pHandle;
        public static int base_adress;

        public static void OpenProcess(string processName, string processModuleName, uint desiredAccess)
        {
            Process[] procs = Process.GetProcessesByName(processName);
            if (procs.Length == 0)
            {
                procID = 0;
            }
            else
            {
                procID = procs[0].Id;
                pHandle = Native.OpenProcess(desiredAccess, false, procID);
                ProcessModuleCollection modules = procs[0].Modules;
                foreach (ProcessModule module in modules)
                {
                    if (module.ModuleName == processModuleName)
                    {
                        base_adress = module.BaseAddress.ToInt32();
                    }
                }

            }
        }

        public static int readInt(long Address)
        {
            byte[] buffer = new byte[sizeof(int)];
            Native.ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static uint readUInt(long Address)
        {
            byte[] buffer = new byte[sizeof(int)];
            Native.ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
            return (uint)BitConverter.ToUInt32(buffer, 0);
        }

        public static float readFloat(long Address)
        {
            byte[] buffer = new byte[sizeof(float)];
            Native.ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static string ReadString(long Address)
        {
            byte[] buffer = new byte[50];

            Native.ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)50, IntPtr.Zero);

            string ret = Encoding.Unicode.GetString(buffer);

            if (ret.IndexOf('\0') != -1)
                ret = ret.Remove(ret.IndexOf('\0'));
            return ret;
        }

        public static byte readByte(long Address)
        {
            byte[] buffer = new byte[1];
            Native.ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)1, IntPtr.Zero);
            return buffer[0];
        }

        public static void WriteFloat(long Address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Native.WriteProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)buffer.Length, IntPtr.Zero);
        }
        public static void WriteInt(long Address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Native.WriteProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)buffer.Length, IntPtr.Zero);
        }
        public static void WriteUInt(long Address, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Native.WriteProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)buffer.Length, IntPtr.Zero);
        }

    }
}
