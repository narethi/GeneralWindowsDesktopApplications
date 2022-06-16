using System;
using System.Runtime.InteropServices;

namespace SimpleUIElements.Helpers
{
    /// <summary>
    /// This contains native methods imported from user32 that are useful for some UI operations
    /// </summary>
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT lpPoint);
    }

    /// <summary>
    /// Required by some native methods
    /// </summary>
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
