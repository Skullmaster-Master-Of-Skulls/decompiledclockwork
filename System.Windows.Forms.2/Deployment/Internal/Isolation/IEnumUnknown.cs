using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200001D RID: 29
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000100-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface IEnumUnknown
	{
		// Token: 0x060000BD RID: 189
		[PreserveSig]
		int Next(uint celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] [Out] object[] rgelt, ref uint celtFetched);

		// Token: 0x060000BE RID: 190
		[PreserveSig]
		int Skip(uint celt);

		// Token: 0x060000BF RID: 191
		[PreserveSig]
		int Reset();

		// Token: 0x060000C0 RID: 192
		[PreserveSig]
		int Clone(out IEnumUnknown enumUnknown);
	}
}
