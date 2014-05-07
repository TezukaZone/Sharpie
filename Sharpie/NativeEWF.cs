using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sharpie
{
    public unsafe struct EWF_Volume_Name_Entry
    {
        public EWF_Volume_Name_Entry* Next;
        public char Name;
    }

    public unsafe class NativeEWF
    {
        
        [DllImport("Ewfapi.dll")]
        public static extern IntPtr EwfMgrOpenProtected(string lpVolume);
        [DllImport("Ewfapi.dll")]
        public static extern bool EwfMgrCommit(IntPtr hDevice);
        [DllImport("Ewfapi.dll")]
        public static extern bool EwfMgrClose(IntPtr hDevice);
        [DllImport("Ewfapi.dll")]
        public static extern bool EwfMgrEnable(IntPtr hDevice);
        [DllImport("Ewfapi.dll")]
        public static extern bool EwfMgrDisable(IntPtr hDevice, bool fCommit);
        [DllImport("Ewfapi.dll")]
        public static extern bool EwfMgrVolumeNameListIsEmpty(EWF_Volume_Name_Entry* pVolumeNameList);
        [DllImport("Ewfapi.dll")]
        public static extern EWF_Volume_Name_Entry* EwfMgrGetProtectedVolumeList();
        [DllImport("Ewfapi.dll")]
        public static extern char EwfMgrGetDriveLetterFromVolumeName(string lpcwStr);
        [DllImport("Ewfapi.dll")]
        public static extern void EwfMgrVolumeNameEntryPop(EWF_Volume_Name_Entry** ppVolumeNameList);
        [DllImport("Ewfapi.dll")]
        public static void EwfMgrVolumeNameListDelete(EWF_Volume_Name_Entry* pVolumeNameList);

        public static string Commit(string volumeName, bool reboot)
        {
            IntPtr hDevice = EwfMgrOpenProtected(volumeName);
            if (hDevice.ToInt32() == -1)
            {
                goto exit;
            }

            if (!EwfMgrCommit(hDevice))
            {
                goto exit;
            }

            if (reboot) Reboot();

        exit:
            if (hDevice.ToInt32() != -1)
                EwfMgrClose(hDevice);
            return Native.GetLastError();
        }

        public static string Enable(string volumeName, bool reboot)
        {
            IntPtr hDevice = EwfMgrOpenProtected(volumeName);
            if (hDevice.ToInt32() == -1)
            {
                goto exit;
            }

            if (!EwfMgrEnable(hDevice))
            {
                goto exit;
            }

            if (reboot) Reboot();

        exit:
            if (hDevice.ToInt32() != -1)
                EwfMgrClose(hDevice);
            return Native.GetLastError();
        }

        public static string Disable(string volumeName, bool reboot)
        {
            IntPtr hDevice = EwfMgrOpenProtected(volumeName);
            if (hDevice.ToInt32() == -1)
            {
                goto exit;
            }

            if (!EwfMgrDisable(hDevice, false))
            {
                goto exit;
            }

            if (reboot) Reboot();

        exit:
            if (hDevice.ToInt32() != -1)
                EwfMgrClose(hDevice);
            return Native.GetLastError();
        }


        public static void Reboot()
        {
            Native.ExitWindowsEx(Native.EWX_Flags.REBOOT | Native.EWX_Flags.FORCE, "");
        }

        public unsafe static List<char> GetProtectedVolumeList()
        {
            EWF_Volume_Name_Entry* volumeNames = EwfMgrGetProtectedVolumeList();
            List<char> protectedVolumes = new List<char>();
            while (!EwfMgrVolumeNameListIsEmpty(volumeNames))
            {
                IntPtr hDevice = EwfMgrOpenProtected(volumeNames->Name.ToString());
                if (hDevice.ToInt32() != -1)
                {
                    protectedVolumes.Add(volumeNames->Name);
                    EwfMgrClose(hDevice);
                }
                hDevice = IntPtr.Zero;
                EwfMgrVolumeNameEntryPop(&volumeNames);
            }
            EwfMgrVolumeNameListDelete(volumeNames);
            return protectedVolumes;

        }
    }
}
