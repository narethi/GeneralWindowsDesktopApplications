using System;
using System.Runtime.InteropServices;

namespace SimpleUIElements.Helpers
{
    static class NativeMethods
    {
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT lpPoint);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;

        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
