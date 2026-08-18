using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002E RID: 46
	[Guid("5fa4f590-a416-4b22-ac79-7c3f0d31f303")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY
	{
		// Token: 0x060000D6 RID: 214
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] StoreOperationMetadataProperty[] AppIds);

		// Token: 0x060000D7 RID: 215
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x060000D8 RID: 216
		[SecurityCritical]
		void Reset();

		// Token: 0x060000D9 RID: 217
		[SecurityCritical]
		IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY Clone();
	}
}
