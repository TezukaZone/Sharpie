using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sharpie
{
    public class Native
    {
        public enum EWX_Flags
        {
            LOGOFF = 0,
            POWEROFF = 0x00000008,
            REBOOT = 0x00000002,
            SHUTDOWN = 0x00000001,
            FORCE = 0x00000004,
            FORCEIFHUNG = 0x00000010
        }
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
        [DllImportAttribute("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImportAttribute("User32.dll")]
        public static extern bool ExitWindowsEx(EWX_Flags uFlags, string dwReason);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern bool _CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);
        [DllImport("Kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("Kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        [DllImport("Kernel32.dll")]
        public static extern bool SetDllDirectory(string lpPathName);
        [DllImport("Kernel32.dll")]
        public static extern string GetLastError();


        public static Delegate LoadFunction<T>(string dllPath, string functionName)
        {
            IntPtr hModule = LoadLibrary(dllPath);
            IntPtr address = GetProcAddress(hModule, functionName);
            return Marshal.GetDelegateForFunctionPointer(address, typeof(T));
        }


        
        public static void clickAtPoint(Point p)
        {
            Cursor.Position = p;
            mouse_event(0x00000002, 0, 0, 0, UIntPtr.Zero); /// left mouse button down
            mouse_event(0x00000004, 0, 0, 0, UIntPtr.Zero); /// left mouse button up
            System.Threading.Thread.Sleep(500);
        }
    }
}
