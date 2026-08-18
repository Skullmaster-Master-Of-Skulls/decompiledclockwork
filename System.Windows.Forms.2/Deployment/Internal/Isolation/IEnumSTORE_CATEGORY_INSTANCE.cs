using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000038 RID: 56
	[Guid("5ba7cb30-8508-4114-8c77-262fcda4fadb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_INSTANCE
	{
		// Token: 0x0600010D RID: 269
		[SecurityCritical]
		uint Next([In] uint ulElements, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_INSTANCE[] rgInstances);

		// Token: 0x0600010E RID: 270
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x0600010F RID: 271
		[SecurityCritical]
		void Reset();

		// Token: 0x06000110 RID: 272
		[SecurityCritical]
		IEnumSTORE_CATEGORY_INSTANCE Clone();
	}
}
