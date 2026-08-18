using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000571 RID: 1393
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumConnections
	{
		// Token: 0x060033D8 RID: 13272
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] CONNECTDATA[] rgelt, IntPtr pceltFetched);

		// Token: 0x060033D9 RID: 13273
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060033DA RID: 13274
		void Reset();

		// Token: 0x060033DB RID: 13275
		void Clone(out IEnumConnections ppenum);
	}
}
