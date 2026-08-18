using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000574 RID: 1396
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumVARIANT
	{
		// Token: 0x060033E4 RID: 13284
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] object[] rgVar, IntPtr pceltFetched);

		// Token: 0x060033E5 RID: 13285
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060033E6 RID: 13286
		[PreserveSig]
		int Reset();

		// Token: 0x060033E7 RID: 13287
		IEnumVARIANT Clone();
	}
}
