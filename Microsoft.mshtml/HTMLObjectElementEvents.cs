using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B1D RID: 2845
	[InterfaceType(2)]
	[TypeLibType(4112)]
	[Guid("3050F3C4-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLObjectElementEvents
	{
		// Token: 0x06012728 RID: 75560
		[DispId(-2147418108)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onbeforeupdate();

		// Token: 0x06012729 RID: 75561
		[DispId(-2147418107)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onafterupdate();

		// Token: 0x0601272A RID: 75562
		[DispId(-2147418099)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onerrorupdate();

		// Token: 0x0601272B RID: 75563
		[DispId(-2147418106)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onrowexit();

		// Token: 0x0601272C RID: 75564
		[DispId(-2147418105)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowenter();

		// Token: 0x0601272D RID: 75565
		[DispId(-2147418098)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetchanged();

		// Token: 0x0601272E RID: 75566
		[DispId(-2147418097)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondataavailable();

		// Token: 0x0601272F RID: 75567
		[DispId(-2147418096)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void ondatasetcomplete();

		// Token: 0x06012730 RID: 75568
		[DispId(-2147418093)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onerror();

		// Token: 0x06012731 RID: 75569
		[DispId(-2147418080)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsdelete();

		// Token: 0x06012732 RID: 75570
		[DispId(-2147418079)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onrowsinserted();

		// Token: 0x06012733 RID: 75571
		[DispId(-2147418078)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void oncellchange();

		// Token: 0x06012734 RID: 75572
		[DispId(-2147418092)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onreadystatechange();
	}
}
