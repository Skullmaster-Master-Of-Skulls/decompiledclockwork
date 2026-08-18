using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002B RID: 43
	[Guid("d8b1aacb-5142-4abb-bcc1-e9dc9052a89e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE
	{
		// Token: 0x060000C7 RID: 199
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] StoreApplicationReference[] rgelt);

		// Token: 0x060000C8 RID: 200
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x060000C9 RID: 201
		[SecurityCritical]
		void Reset();

		// Token: 0x060000CA RID: 202
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE Clone();
	}
}
