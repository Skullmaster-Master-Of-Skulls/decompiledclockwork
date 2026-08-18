using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200053F RID: 1343
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumConnectionPoints instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumConnectionPoints
	{
		// Token: 0x06003354 RID: 13140
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] UCOMIConnectionPoint[] rgelt, out int pceltFetched);

		// Token: 0x06003355 RID: 13141
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06003356 RID: 13142
		[PreserveSig]
		int Reset();

		// Token: 0x06003357 RID: 13143
		void Clone(out UCOMIEnumConnectionPoints ppenum);
	}
}
