using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using Botting.Hero.Classes.HotkeyManager;
using Botting.Hero.Classes.HotkeyManager.Enums;
using Botting.Hero.Classes.HotkeyManager.Events;
using Botting.Hero.Classes.Native;
using Botting.Hero.Classes.Native.Enums;
using Botting.Hero.Classes.Native.Structs;
using Botting.Hero.Classes;
using Point = AForge.Point;

namespace Botting.Hero
{
	class Program
	{
		private static HotkeyManager hkm;

		private static Boolean Quit;
		private static Boolean Start;
		private static Boolean Clicking;
		private static Boolean Fishing;

		private static IntPtr ClickerHeroesHandle;

		static void Main(string[] args)
		{
			Startup();

			Application.Run();
		}

		private static void HotKeyPressed(object Sender, HotkeyPressedEventArgs e)
		{
			if (e.Modifier == Modifiers.None && e.Key == Keys.F5)
			{
				Start = !Start;
			}
			if (e.Modifier == Modifiers.None && e.Key == Keys.F6)
			{
				Clicking = !Clicking;
			}
			if (e.Modifier == Modifiers.None && e.Key == Keys.F7)
			{
				Fishing = !Fishing;
			}
			if (e.Modifier == Modifiers.None && e.Key == Keys.F8) { }
			if (e.Modifier == Modifiers.None && e.Key == Keys.F9)
			{
				Quit = true;
				Helper.DeInitiaalizeWindow(ClickerHeroesHandle);
			}
			WriteInstructions();
		}

		private async static void Startup()
		{
			Console.Title = "Botting Hero - A C# Clicker Heroes Bot!";
			Console.WriteLine("Booting Up...");

			#region .  HotKeys  .
			hkm = new HotkeyManager();
			Console.WriteLine("Registering Hotkeys...");
//			hkm.RegisterHotkey(Modifiers.None, Keys.F5); // Currently unused
			hkm.RegisterHotkey(Modifiers.None, Keys.F6);
			hkm.RegisterHotkey(Modifiers.None, Keys.F7);
//			hkm.RegisterHotkey(Modifiers.None, Keys.F8); // Currently unused
			hkm.RegisterHotkey(Modifiers.None, Keys.F9);
			hkm.HotkeyPressed += HotKeyPressed;
			Console.WriteLine("Successfully Registered Hotkeys...");
			#endregion

			#region .  FindWindow  .

//			await Task.Run(() =>
//			{
//				var ps = Process.GetProcessesByName("Clicker Heroes");
//				foreach (var p in ps)
//				{
//					p.Kill();
//					if (!p.HasExited)
//						p.WaitForExit();
//				}
//			});
			
			ClickerHeroesHandle = NativeMethods.FindWindow(null, "Clicker Heroes");
			if (ClickerHeroesHandle == IntPtr.Zero)
			{
				await Task.Run(new Func<Task>(StartClickerHeroes));
			}
			else
			{
				// Create the Borderless 800x450 window
				Helper.InitializeWindow(ClickerHeroesHandle);
			}

			#endregion

			Console.WriteLine("Bootup Complete");
			Task.Run((Action)Run);
		}

		private async static Task StartClickerHeroes()
		{
			// Launch Clicker Heroes via Steam
			Process.Start("steam://rungameid/363970");

			while (ClickerHeroesHandle == IntPtr.Zero)
			{
				ClickerHeroesHandle = NativeMethods.FindWindow(null, "Clicker Heroes");
				await Task.Delay(2000);
			}

			// Create the Borderless 800x450 window
			Helper.InitializeWindow(ClickerHeroesHandle);

			// Get Area for Clickerings
			Rect ClickersRect;
			var Success = NativeMethods.GetClientRect(ClickerHeroesHandle, out ClickersRect);

			if (!Success)
				throw new Win32Exception();

			// Click dat Play Button
			Helper.PostLeftClick(ClickerHeroesHandle,
				Helper.MakeLParam((Int32)(ClickersRect.Width / 2.0), (Int32)(ClickersRect.Height / 2.4)));

			// Wait for the main game to load
			await Task.Delay(2000);
			
			// Take Screenshot and Compare it to Resources version of the Cross
			TemplateMatch[] m;
			using (var b = Helper.TakeScreenshot(ClickerHeroesHandle))
			using (var s = Helper.Return24bppBitmap(Properties.Resources.Cross))
			{
				m = b.Contains(s, Dividier: 1);

				// Ensure that there are Matches
				if (m != null)
				{
					// There should only be one.
					if (m.Count() > 1)
					{
						b.Save("test.bmp");
						Process.Start("test.bmp");
						Console.WriteLine("How is this even possibru? Too many Crosses to close the Last Login Progress Window With...");
						Console.ReadKey();
						Environment.Exit(1);
					}

					// Get the Match, and return it to it's original size, then Click it
					var Match = m[0];
					Helper.PostLeftClick(ClickerHeroesHandle,
						Helper.MakeLParam(Match.Rectangle.X*4, Match.Rectangle.Y*4));
				}
				else
				{
					Console.WriteLine("Could not find any Crosses to Close the Last Login Progress Window.");
					Console.ReadKey();
					Environment.Exit(1);
				}

			}
			Console.WriteLine("Started Clicker Heroes");

			//			while (ClickerHeroesHandle != IntPtr.Zero)
			//			{
			//				using (var g = Graphics.FromHwnd(ClickerHeroesHandle))
			//				{
			//					g.DrawString("-", new Font("Comic Sans", 24), Brushes.Blue, ClickersRect.Width / (float)2, (float)244/*ClickersRect.Height / (float)2.5*/);
			//				}
			//				ClickerHeroesHandle = NativeMethods.FindWindow(null, "Clicker Heroes");
			//			}
		}

		private async static Task Clicker()
		{
			while (true)
			{
				if (!Clicking)
				{
					await Task.Delay(1000);
					continue;
				}

				Rect ClickerRect;
				NativeMethods.GetClientRect(ClickerHeroesHandle, out ClickerRect);

				var Width = ClickerRect.Width / 2;
//				while (true)
//				{
//					using (var g = Graphics.FromHwnd(ClickerHeroesHandle))
//					{
//						g.DrawLine(Pens.Purple, ClickerRect.Width / 4 + ClickerRect.Width / 2, ClickerRect.Height / 2, 200, 200);
////						g.DrawLine(Brushes.Purple, ClickerRect.Width / 4 + ClickerRect.Width / 2, ClickerRect.Height / 2, 200, 200);
//					}
//				}

//				var Width = ClickerRect.Width/2;
				var ClickLParam = Helper.MakeLParam(ClickerRect.Width /4  + Width, ClickerRect.Height / 2);
				Helper.PostLeftClick(ClickerHeroesHandle, ClickLParam);

				await Task.Delay(50);

			}
		}

		private async static Task Fisher()
		{
			while (true)
			{

				if (!Fishing)
				{
					await Task.Delay(1000);
					continue;
				}

				TemplateMatch[] m;
				using (var b = Helper.TakeScreenshot(ClickerHeroesHandle))
				using (var s = Helper.Return24bppBitmap(Properties.Resources.Fish2))
					m = b.Contains(s, Dividier: 1, Precision: 0.91f);

				if (m != null)
				{
					foreach (var Match in m)
					{
						var MatchLoc = new Point(Match.Rectangle.X + (Match.Rectangle.Width / 2), Match.Rectangle.Y + (Match.Rectangle.Height / 2));
						Console.WriteLine("Found Fishy at {0}x{1} with {2} similarity", (Int32)MatchLoc.X, (Int32)MatchLoc.Y, Match.Similarity);
						Helper.PostLeftClick(ClickerHeroesHandle,
							Helper.MakeLParam((Int32)MatchLoc.X, (Int32)MatchLoc.Y));
					}
//					var match = m[0];
//					if (match == null) continue;
//					var MatchLoc2 = new Point(match.Rectangle.X, match.Rectangle.Y);
//					MatchLoc2.X += match.Rectangle.Width / 2;
//					MatchLoc2.Y += match.Rectangle.Height / 2;
////
////					
//					Console.WriteLine("Found Fishy at {0}x{1} with {2} similarity", MatchLoc2.X, MatchLoc2.Y, match.Similarity);
//					while (Fishing)
//					{
//						using (var g = Graphics.FromHwnd(ClickerHeroesHandle))
//							g.FillEllipse(Brushes.Red, MatchLoc2.X, MatchLoc2.Y, 5, 5);
//					}

				}

				await Task.Delay(1000);
			}
		}

		private static void WriteInstructions()
		{
			Console.Clear();
			Console.WriteLine("Instructions!");
			Console.WriteLine("F5: Start / Stop");
			Console.WriteLine("F6: Auto Clicker - {0}", (Clicking) ? "Running" : "Not Running");
			Console.WriteLine("F7: Golden Fish - {0}", (Fishing) ? "Running" : "Not Running");
			Console.WriteLine("F8: TBA - {0}", "");
			Console.WriteLine("F9: Quit");
		}

		private async static void Run()
		{
			WriteInstructions();
			Task.Run(new Func<Task>(Clicker));
			Task.Run(new Func<Task>(Fisher));
			while (!Quit)
			{
				if (!Start)
				{
					await Task.Delay(1000);
					continue;
				}

				// This really does nothing... lol

				await Task.Delay(1000);
			}
			Console.WriteLine("Quitting...Bye!");
			Environment.Exit(0);
		}
	}
}
