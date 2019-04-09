using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MapDriveLetter
{
    // Important variables for WNetAddConnection3
    [StructLayout(LayoutKind.Sequential)]
    public struct NETRESOURCE
    {
        public uint dwScope;
        public uint dwType;
        public uint dwDisplayType;
        public uint dwUsage;
        public string lpLocalName;
        public string lpRemoteName;
        public string lpComment;
        public string lpProvider;
    }

    public static class Program
    {
        // Do not modify. Conventional stuff for WNetAddConnection3
        const uint RESOURCETYPE_DISK = 1;

        [DllImport("mpr.dll")]
        static extern uint WNetAddConnection3(IntPtr hWndOwner, ref NETRESOURCE lpNetResource, string lpPassword, string lpUserName, uint dwFlags);

        [DllImport("mpr.dll")]
        static extern uint WNetCancelConnection2(string lpName, uint dwFlags, bool bForce);

        // In order to hide the console window
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main()
        {
            // Hide the console window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            // Map the network drives
            NETRESOURCE networkResource = new NETRESOURCE();
            networkResource.dwType = RESOURCETYPE_DISK;
            String lpUsername = ""; // TODO: Write username here.
            String lpPassword = ""; // TODO: Write password here.
            String compName = System.Environment.MachineName.ToLower();
            networkResource.lpProvider = null;
            uint flags = 0;

            networkResource.lpLocalName = "A:";
            networkResource.lpRemoteName = "\\\\trinity.sch.bme.hu\\adas";
            uint result = WNetAddConnection3(IntPtr.Zero, ref networkResource, lpPassword, lpUsername, flags);

            /*networkResource.lpLocalName = "M:";
            networkResource.lpRemoteName = "\\\\trinity.sch.bme.hu\\avid_"+compName;
            result = WNetAddConnection3(IntPtr.Zero, ref networkResource, lpPassword, lpUsername, flags);*/

            networkResource.lpLocalName = "N:";
            networkResource.lpRemoteName = "\\\\trinity.sch.bme.hu\\nyers";
            result = WNetAddConnection3(IntPtr.Zero, ref networkResource, lpPassword, lpUsername, flags);

            networkResource.lpLocalName = "P:";
            networkResource.lpRemoteName = "\\\\trinity.sch.bme.hu\\projects";
            result = WNetAddConnection3(IntPtr.Zero, ref networkResource, lpPassword, lpUsername, flags);

            networkResource.lpLocalName = "R:";
            networkResource.lpRemoteName = "\\\\coding.sch.bme.hu\\archives";
            result = WNetAddConnection3(IntPtr.Zero, ref networkResource, lpPassword, lpUsername, flags);

            networkResource.lpLocalName = "X:";
            networkResource.lpRemoteName = "\\\\trinity.sch.bme.hu\\exports";
            result = WNetAddConnection3(IntPtr.Zero, ref networkResource, lpPassword, lpUsername, flags);

            networkResource.lpLocalName = "Y:";
            networkResource.lpRemoteName = "\\\\trinity.sch.bme.hu\\commonstuff";
            result = WNetAddConnection3(IntPtr.Zero, ref networkResource, lpPassword, lpUsername, flags);
        }
    }
}
