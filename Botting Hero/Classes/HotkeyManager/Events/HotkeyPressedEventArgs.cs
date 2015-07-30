using System;
using System.Windows.Forms;
using Botting.Hero.Classes.HotkeyManager.Enums;

namespace Botting.Hero.Classes.HotkeyManager.Events
{
	public class HotkeyPressedEventArgs : EventArgs
	{
		public Modifiers Modifier;
		public Keys Key;

		public HotkeyPressedEventArgs(Modifiers Modifier, Keys Key)
		{
			this.Modifier = Modifier;
			this.Key = Key;
		}
	}
}