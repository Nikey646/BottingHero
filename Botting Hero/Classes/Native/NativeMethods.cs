using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Botting.Hero.Classes.Native.Enums;
using Botting.Hero.Classes.Native.Structs;

namespace Botting.Hero.Classes.Native
{
	static class NativeMethods
	{
		public const Int32 WM_HotKey = 0x0312;

		public const Int32 WM_LButtonDown = 0x0201;
		public const Int32 WM_LButtonUp = 0x0202;

		public const uint WS_PopUp = 0x80000000;
		public const Int32 GWL_Style = -16;
		public const Int32 GWL_EXSTYLE = -0x14;
		public const int WS_EX_APPWINDOW = 0x40000;
		//TODO: Add GetCursorPos
		//TODO: Add GetClientRect
		//TODO: Add PostMessage
		//TODO: Add GetForegroundWindow

		[DllImport("User32", SetLastError = true)]
		public static extern bool RegisterHotKey(IntPtr WindowHandle, Int32 HotKeyIdentifier, UInt32 ModifierCode, UInt32 KeyCode);

		[DllImport("User32", SetLastError = true)]
		public static extern bool UnregisterHotKey(IntPtr WindowHandle, Int32 HotKeyIdentifier);

		[DllImport("User32", SetLastError = true)]
		public static extern bool PostMessage(IntPtr WindowHandle, UInt32 Message, IntPtr WParam, IntPtr LParam);

		[DllImport("User32", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern bool SendMessage(IntPtr WindowHandle, UInt32 Message, IntPtr WParam, IntPtr LParam);

		[DllImport("User32", SetLastError = true)]
		public static extern IntPtr FindWindow(string ClassName, string WindowName);

		[DllImport("User32", SetLastError = true)]
		public static extern bool GetClientRect(IntPtr WindowHandle, out Rect Rect);

		[DllImport("User32", SetLastError = true)]
		public static extern bool SetWindowPos(IntPtr WindowHandle, IntPtr HandleInsertAfter, Int32 x, Int32 y, Int32 Width,
			Int32 Height, SetWindowPosFlags uFlags);

		[DllImport("User32", SetLastError = true)]
		public static extern IntPtr GetWindowDC(IntPtr WindowHandle);

		[DllImport("User32", SetLastError = true)]
		public static extern bool ReleaseDC(IntPtr WindowHandle, IntPtr DeviceContext);

		[DllImport("Gdi32", SetLastError = true)]
		public static extern bool BitBlt(IntPtr OutputDC, Int32 DestY, Int32 DestX, Int32 Width, Int32 Height, IntPtr SourceDC, Int32 SourceX,
			Int32 SourceY, Int32 dwRop);

//		public static IntPtr SetWindowLongPtr(IntPtr WindowHandle, Int32 Index, Int64 NewLong)
//		{
//			if (IntPtr.Size == 8)
//			{
//				// 64bit
//				return SetWindowLong64(WindowHandle, Index, NewLong);
//			}
//			return SetWindowLong32(WindowHandle, Index, NewLong);
//			//32bit
//		}
//
//		[DllImport("User32", SetLastError = true, EntryPoint = "SetWindowLong")]
//		private static extern IntPtr SetWindowLong32(IntPtr WindowHandle, Int32 Index, Int64 NewLong);

		[DllImport("User32", SetLastError = true)]
		public static extern IntPtr SetWindowLong(IntPtr WindowHandle, Int32 Index, uint NewLong);

		[DllImport("User32.dll")]
		public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

	}
}
