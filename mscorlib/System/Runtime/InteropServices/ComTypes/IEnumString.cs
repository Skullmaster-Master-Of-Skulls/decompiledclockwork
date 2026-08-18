using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000573 RID: 1395
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000101-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IEnumString
	{
		// Token: 0x060033E0 RID: 13280
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, IntPtr pceltFetched);

		// Token: 0x060033E1 RID: 13281
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060033E2 RID: 13282
		void Reset();

		// Token: 0x060033E3 RID: 13283
		void Clone(out IEnumString ppenum);
	}
}
