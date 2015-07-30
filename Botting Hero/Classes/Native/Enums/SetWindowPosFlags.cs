using System;

namespace Botting.Hero.Classes.Native.Enums
{
	[Flags]
	public enum SetWindowPosFlags : uint
	{
		AsyncWindowPos = 0x4000,
		DeferErase = 0x2000,
		DrawFrame = 0x0020,
		FrameChanged = 0x0020,
		HideWindow = 0x0080,
		NoActivate = 0x0010,
		NoCopyBits = 0x0100,
		NoMove = 0x0002,
		NoOwnerZOrder = 0x0200,
		NoRedraw = 0x008,
		NoReposition = 0x0200,
		NoSendChanging = 0x0400,
		NoSize = 0x0001,
		NoZOrder = 0x0004,
		ShowWindow = 0x0040
	}
}