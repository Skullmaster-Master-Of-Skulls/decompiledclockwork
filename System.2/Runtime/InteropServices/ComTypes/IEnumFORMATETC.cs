using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E3 RID: 995
	[Guid("00000103-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumFORMATETC
	{
		// Token: 0x0600261A RID: 9754
		[__DynamicallyInvokable]
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] FORMATETC[] rgelt, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);

		// Token: 0x0600261B RID: 9755
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600261C RID: 9756
		[__DynamicallyInvokable]
		[PreserveSig]
		int Reset();

		// Token: 0x0600261D RID: 9757
		[__DynamicallyInvokable]
		void Clone(out IEnumFORMATETC newEnum);
	}
}
