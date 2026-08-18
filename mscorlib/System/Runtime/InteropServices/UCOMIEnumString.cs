using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000540 RID: 1344
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000101-0000-0000-C000-000000000046")]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumString instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[ComImport]
	public interface UCOMIEnumString
	{
		// Token: 0x06003358 RID: 13144
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, out int pceltFetched);

		// Token: 0x06003359 RID: 13145
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600335A RID: 13146
		[PreserveSig]
		int Reset();

		// Token: 0x0600335B RID: 13147
		void Clone(out UCOMIEnumString ppenum);
	}
}
