using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000030 RID: 48
	[Guid("a5c637bf-6eaa-4e5f-b535-55299657e33e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY
	{
		// Token: 0x060000E1 RID: 225
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY[] rgelt);

		// Token: 0x060000E2 RID: 226
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x060000E3 RID: 227
		[SecurityCritical]
		void Reset();

		// Token: 0x060000E4 RID: 228
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY Clone();
	}
}
