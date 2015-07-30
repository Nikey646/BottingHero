using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Botting.Hero.Classes.HotkeyManager.Enums;
using Botting.Hero.Classes.HotkeyManager.Events;
using Botting.Hero.Classes.Native;

namespace Botting.Hero.Classes.HotkeyManager
{
	class HotkeyManager
	{
		private HotkeyWindow _Window;
		private Dictionary<KeyValuePair<Modifiers, Keys>, Int32> _HotkeyIDs;
		private Int32 _LastID;

		public event EventHandler<HotkeyPressedEventArgs> HotkeyPressed = delegate { }; 

		public HotkeyManager()
		{
			this._Window = new HotkeyWindow(this);
			this._HotkeyIDs = new Dictionary<KeyValuePair<Modifiers, Keys>, Int32>();
		}

		public void RegisterHotkey(Modifiers Modifier, Keys Key)
		{
			var Success = NativeMethods.RegisterHotKey(this._Window.Handle, this._LastID, (UInt32) Modifier, (UInt32) Key);

			if (!Success)
			{
				throw new Win32Exception();
			}

			this._HotkeyIDs.Add(new KeyValuePair<Modifiers, Keys>(Modifier, Key), this._LastID);
			this._LastID++;
		}

		public void UnRegisterHotkey(Modifiers Modifier, Keys Key)
		{
			var kv = new KeyValuePair<Modifiers, Keys>(Modifier, Key);
			Int32 CurrentID;
			if (this._HotkeyIDs.ContainsKey(kv))
			{
				CurrentID = this._HotkeyIDs[kv];
			}
			else return;

			var Success = NativeMethods.UnregisterHotKey(this._Window.Handle, CurrentID);

			if (!Success)
				throw new Win32Exception();

			this._HotkeyIDs.Remove(kv);
		}

		private sealed class HotkeyWindow : NativeWindow, IDisposable
		{
			private HotkeyManager _Parent;
			public HotkeyWindow(HotkeyManager Parent)
			{
				this._Parent = Parent;
				this.CreateHandle(new CreateParams());
			}

			protected override void WndProc(ref Message m)
			{
				if (m.Msg == NativeMethods.WM_HotKey)
				{
					var LParam = (Int32) m.LParam;
					var Modifier = (Modifiers) (LParam & 0xFFFF);
					var Key = (Keys) (LParam >> 16);

					this._Parent.HotkeyPressed(this, new HotkeyPressedEventArgs(Modifier, Key));
				}
				base.WndProc(ref m);
			}

			public void Dispose()
			{
				this.DestroyHandle();
			}
		}

	}
}
