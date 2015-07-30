namespace Botting.Hero.Classes.Native.Enums
{
	public class WindowsStyles
	{
		public const uint Overlapped = 0x00000000;
		public const uint PopUp = 0x80000000;
		public const uint Child = 0x40000000;
		public const uint Minimize = 0x20000000;
		public const uint Visible = 0x10000000;
		public const uint Disabled = 0x08000000;
		public const uint ClipSiblings = 0x04000000;
		public const uint ClipChildren = 0x02000000;
		public const uint Maximize = 0x01000000;
		public const uint Border = 0x00800000;
		public const uint DLGFrame = 0x00400000;
		public const uint VScroll = 0x00200000;
		public const uint HScroll = 0x00100000;
		public const uint SysMenu = 0x00080000;
		public const uint ThickFrame = 0x00040000;
		public const uint Group = 0x00020000;
		public const uint TabStop = 0x00010000;

		public const uint MinimizeBox = 0x00020000;
		public const uint MaximizeBox = 0x00010000;

		public const uint Caption = Border | DLGFrame;
		public const uint Tiled = Overlapped;
		public const uint Iconic = Minimize;
		public const uint SizeBox = ThickFrame;
		public const uint TiledWindow = OverlappedWindow;

		public const uint OverlappedWindow = Overlapped | Caption | SysMenu | ThickFrame | MinimizeBox | MaximizeBox;
		public const uint PopupWindow = PopUp | Border | SysMenu;
		public const uint ChildWindow = Child;

		public const uint Ex_DLGModalFrame = 0x00000001;
		public const uint Ex_NoParentNotify = 0x00000004;
		public const uint Ex_TopMost = 0x00000008;
		public const uint Ex_AcceptFiles = 0x00000010;
		public const uint Ex_Transparent = 0x00000020;
		public const uint Ex_MDIChild = 0x00000040;
		public const uint Ex_ToolWindow = 0x00000080;
		public const uint Ex_WindowEdge = 0x00000100;
		public const uint Ex_ClientEdge = 0x00000200;
		public const uint Ex_ContextHelp = 0x00000400;

		public const uint Ex_Right = 0x00001000;
		public const uint Ex_Left = 0x00000000;
		public const uint Ex_RTLReading = 0x00002000;
		public const uint Ex_LTRReading = 0x00000000;
		public const uint Ex_LeftScrollbar = 0x00004000;
		public const uint Ex_RightScrollbar = 0x00000000;

		public const uint Ex_ControlParent = 0x00010000;
		public const uint Ex_StaticEdge = 0x00020000;
		public const uint Ex_AppWindow = 0x00040000;

		public const uint Ex_OverlappedWindow = Ex_WindowEdge | Ex_ClientEdge;
		public const uint Ex_PaletteWindow = Ex_WindowEdge | Ex_ToolWindow | Ex_TopMost;

		public const uint EX_NoInheritLayout = 0x00100000;
		public const uint EX_LayoutRTL = 0x00400000;

		public const uint EX_Composited = 0x02000000;
		public const uint EX_NoActivate = 0x08000000;

	}
}