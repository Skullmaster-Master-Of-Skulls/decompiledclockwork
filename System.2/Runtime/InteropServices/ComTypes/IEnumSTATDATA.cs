using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E4 RID: 996
	[Guid("00000103-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumSTATDATA
	{
		// Token: 0x0600261E RID: 9758
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] STATDATA[] rgelt, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] [Out] int[] pceltFetched);

		// Token: 0x0600261F RID: 9759
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06002620 RID: 9760
		[PreserveSig]
		int Reset();

		// Token: 0x06002621 RID: 9761
		void Clone(out IEnumSTATDATA newEnum);
	}
}
