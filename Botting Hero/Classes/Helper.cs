using System;
using System.Drawing;
using System.Drawing.Imaging;
using Botting.Hero.Classes.Native;
using Botting.Hero.Classes.Native.Enums;

namespace Botting.Hero.Classes
{
	public static class Helper
	{

		// Credits: http://stackoverflow.com/questions/19237034/c-sharp-need-to-psuedo-click-a-window
		public static IntPtr MakeLParam(Int32 X, Int32 Y)
		{
			return new IntPtr(((Y << 16) | (X & 0xFFFF)));
		}

		public static void PostLeftClick(IntPtr Handle, IntPtr LParams)
		{
			NativeMethods.PostMessage(Handle, NativeMethods.WM_LButtonDown, IntPtr.Zero, LParams);
			NativeMethods.PostMessage(Handle, NativeMethods.WM_LButtonUp, IntPtr.Zero, LParams);
		}

		public static void SendLeftClick(IntPtr Handle, IntPtr LParams)
		{
			NativeMethods.SendMessage(Handle, NativeMethods.WM_LButtonDown, IntPtr.Zero, LParams);
			NativeMethods.SendMessage(Handle, NativeMethods.WM_LButtonUp, IntPtr.Zero, LParams);
		}

		public static void InitializeWindow(IntPtr Handle)
		{
			var Style = NativeMethods.GetWindowLong(Handle, (Int32) GetWindowLong.Style);
			NativeMethods.SetWindowLong(Handle, (Int32) GetWindowLong.Style, Style & ~WindowsStyles.OverlappedWindow);
			NativeMethods.SetWindowPos(Handle, IntPtr.Zero, -1920, 0, 800, 450, SetWindowPosFlags.NoZOrder | SetWindowPosFlags.NoActivate | SetWindowPosFlags.DrawFrame);
		}

		public static void DeInitiaalizeWindow(IntPtr Handle)
		{
			var Style = NativeMethods.GetWindowLong(Handle, (Int32) GetWindowLong.Style);
			NativeMethods.SetWindowLong(Handle, (Int32) GetWindowLong.Style, Style | WindowsStyles.OverlappedWindow);
			NativeMethods.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 1136, 640, SetWindowPosFlags.NoZOrder | SetWindowPosFlags.NoActivate | SetWindowPosFlags.DrawFrame);
		}

		public static Bitmap Return24bppBitmap(Bitmap Image)
		{
			using (Image)
			{
				return Image.Clone(new Rectangle(0, 0, Image.Width, Image.Height), PixelFormat.Format24bppRgb);
			}
		}

		public static Bitmap TakeScreenshot(IntPtr Handle)
		{
			var DeviceContext = NativeMethods.GetWindowDC(Handle);

			var bmp = new Bitmap(800, 600);
			using (var g = Graphics.FromImage(bmp))
			{
				var BitmapDC = g.GetHdc();

				NativeMethods.BitBlt(BitmapDC, 0, 0, 800, 600, DeviceContext, 0, 0, 13369376);

				g.ReleaseHdc(BitmapDC);	
			}

			NativeMethods.ReleaseDC(Handle, DeviceContext);
			using (bmp)
			{
				return bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format24bppRgb);
			}
		}
	}
}
