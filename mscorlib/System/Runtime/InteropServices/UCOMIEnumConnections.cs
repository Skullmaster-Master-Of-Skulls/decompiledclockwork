using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200053E RID: 1342
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumConnections instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumConnections
	{
		// Token: 0x06003350 RID: 13136
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, out int pceltFetched);

		// Token: 0x06003351 RID: 13137
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06003352 RID: 13138
		[PreserveSig]
		void Reset();

		// Token: 0x06003353 RID: 13139
		void Clone(out UCOMIEnumConnections ppenum);
	}
}
