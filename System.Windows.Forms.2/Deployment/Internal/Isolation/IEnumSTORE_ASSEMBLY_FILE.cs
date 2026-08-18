using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000032 RID: 50
	[Guid("a5c6aaa3-03e4-478d-b9f5-2e45908d5e4f")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_FILE
	{
		// Token: 0x060000EC RID: 236
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY_FILE[] rgelt);

		// Token: 0x060000ED RID: 237
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x060000EE RID: 238
		[SecurityCritical]
		void Reset();

		// Token: 0x060000EF RID: 239
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_FILE Clone();
	}
}
