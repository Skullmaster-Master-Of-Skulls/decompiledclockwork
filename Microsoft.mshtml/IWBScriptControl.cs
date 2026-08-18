using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000838 RID: 2104
	[Guid("A5170870-0CF8-11D1-8B91-0080C744F389")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IWBScriptControl
	{
		// Token: 0x0600DEEE RID: 57070
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void raiseEvent([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.Struct)] [In] object eventData);

		// Token: 0x0600DEEF RID: 57071
		[DispId(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void bubbleEvent();

		// Token: 0x0600DEF0 RID: 57072
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void setContextMenu([MarshalAs(UnmanagedType.Struct)] [In] object menuItemPairs);

		// Token: 0x17004A09 RID: 18953
		// (get) Token: 0x0600DEF2 RID: 57074
		// (set) Token: 0x0600DEF1 RID: 57073
		[DispId(4)]
		bool selectableContent { [DispId(4)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(4)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17004A0A RID: 18954
		// (get) Token: 0x0600DEF3 RID: 57075
		[DispId(5)]
		bool frozen { [DispId(5)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17004A0B RID: 18955
		// (get) Token: 0x0600DEF5 RID: 57077
		// (set) Token: 0x0600DEF4 RID: 57076
		[DispId(7)]
		bool Scrollbar { [DispId(7)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(7)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17004A0C RID: 18956
		// (get) Token: 0x0600DEF6 RID: 57078
		[DispId(8)]
		string version { [DispId(8)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17004A0D RID: 18957
		// (get) Token: 0x0600DEF7 RID: 57079
		[DispId(9)]
		bool visibility { [DispId(9)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17004A0E RID: 18958
		// (get) Token: 0x0600DEF9 RID: 57081
		// (set) Token: 0x0600DEF8 RID: 57080
		[DispId(10)]
		object onvisibilitychange { [DispId(10)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(10)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }
	}
}
