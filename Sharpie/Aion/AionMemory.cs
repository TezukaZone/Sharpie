using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Sharpie.Aion
{
    public class AionMemory
    {
        public static int procID = 0;
        public static IntPtr pHandle = IntPtr.Zero;
        public static int base_adress = 0;

        public static void OpenProcess()
        {
            Process[] process = Process.GetProcessesByName("aion.bin");
            if (process.Length != 0)
            {
                procID = process[0].Id;
                pHandle = Native.OpenProcess(0x1F0FFF, false, procID);
                ProcessModuleCollection modules = process[0].Modules;
                foreach (ProcessModule module in modules)
                {
                    if (module.ModuleName == "Game.dll")
                        base_adress = module.BaseAddress.ToInt32();
                }
            }
            else
                procID = 0;
        }

        public static void setForeground()
        {
            Process p = Process.GetProcessById(procID);
            Native.SetForegroundWindow(p.MainWindowHandle);
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
