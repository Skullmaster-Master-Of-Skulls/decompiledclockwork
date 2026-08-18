using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200053C RID: 1340
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumMoniker instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[ComImport]
	public interface UCOMIEnumMoniker
	{
		// Token: 0x0600334C RID: 13132
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] UCOMIMoniker[] rgelt, out int pceltFetched);

		// Token: 0x0600334D RID: 13133
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600334E RID: 13134
		[PreserveSig]
		int Reset();

		// Token: 0x0600334F RID: 13135
		void Clone(out UCOMIEnumMoniker ppenum);
	}
}
