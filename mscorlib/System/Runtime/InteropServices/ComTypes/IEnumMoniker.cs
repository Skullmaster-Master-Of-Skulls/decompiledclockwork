using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200056F RID: 1391
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumMoniker
	{
		// Token: 0x060033D4 RID: 13268
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] IMoniker[] rgelt, IntPtr pceltFetched);

		// Token: 0x060033D5 RID: 13269
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060033D6 RID: 13270
		void Reset();

		// Token: 0x060033D7 RID: 13271
		void Clone(out IEnumMoniker ppenum);
	}
}
