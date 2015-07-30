using System;
using System.Runtime.InteropServices;

namespace Botting.Hero.Classes.Native.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Rect
	{
		public Int32 Left, Top, Right, Bottom;

		public Rect(Int32 Left, Int32 Top, Int32 Right, Int32 Bottom)
		{
			this.Left = Left;
			this.Top = Top;
			this.Right = Right;
			this.Bottom = Bottom;
		}

		public Int32 x
		{
			get { return this.Left; }
			set
			{
				this.Right -= (this.Left - value);
				this.Left = value;
			}
		}

		public int y
		{
			get { return this.Top; }
			set
			{
				this.Bottom -= (this.Top - value);
				this.Top = value;
			}
		}

		public int Height
		{
			get { return this.Bottom - this.Top; }
			set { this.Bottom = value + this.Top; }
		}

		public int Width
		{
			get { return this.Right - this.Left; }
			set { this.Right = this.Left - value; }
		}

		public bool Equals(Rect r)
		{
			return r.Left == this.Left && r.Top == this.Top && r.Right == this.Right && r.Bottom == this.Bottom;
		}

		public override bool Equals(object obj)
		{
			if (obj is Rect)
				return Equals((Rect)obj);
			return false;
		}

		public override string ToString()
		{
			return string.Format("{{Left={0},Top={1},Right={2},Bottom={3}}}", this.Left, this.Top, this.Right, this.Bottom);
		}

	}
}