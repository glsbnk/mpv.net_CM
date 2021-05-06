﻿
using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class WinAPI
{
    public const int VK_MEDIA_NEXT_TRACK = 0xB0;
    public const int VK_MEDIA_PREV_TRACK = 0xB1;
    public const int VK_MEDIA_STOP = 0xB2;
    public const int VK_MEDIA_PLAY_PAUSE = 0xB3;

    [DllImport("kernel32.dll")]
    public static extern bool AttachConsole(int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool FreeConsole();

    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string path);

    [DllImport("user32")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr FindWindowEx(
        IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int RegisterWindowMessage(string id);

    [DllImport("user32.dll")]
    public static extern bool AllowSetForegroundWindow(int dwProcessId);

    [DllImport("user32.dll")]
    public static extern void ReleaseCapture();

    [DllImport("user32.dll")]
    public static extern bool AdjustWindowRect(ref RECT lpRect, uint dwStyle, bool bMenu);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
    static extern IntPtr GetWindowLong64(IntPtr hWnd, int nIndex);

    public static IntPtr GetWindowLong(IntPtr hWnd, int nIndex)
    {
        if (IntPtr.Size == 8)
            return GetWindowLong64(hWnd, nIndex);
        else
            return GetWindowLong32(hWnd, nIndex);
    }

    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(Rectangle r)
        {
            Left = r.Left;
            Top = r.Top;
            Right = r.Right;
            Bottom = r.Bottom;
        }

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Rectangle ToRectangle() => Rectangle.FromLTRB(Left, Top, Right, Bottom);
        public Size Size => new Size(Right - Left, Bottom - Top);
        public int Width => Right - Left;
        public int Height => Bottom - Top;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpData;
    }
}