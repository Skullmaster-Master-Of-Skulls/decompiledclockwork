using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000572 RID: 1394
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[ComImport]
	public interface IEnumConnectionPoints
	{
		// Token: 0x060033DC RID: 13276
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] IConnectionPoint[] rgelt, IntPtr pceltFetched);

		// Token: 0x060033DD RID: 13277
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060033DE RID: 13278
		void Reset();

		// Token: 0x060033DF RID: 13279
		void Clone(out IEnumConnectionPoints ppenum);
	}
}
