using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000034 RID: 52
	[Guid("b840a2f5-a497-4a6d-9038-cd3ec2fbd222")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY
	{
		// Token: 0x060000F7 RID: 247
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY[] rgElements);

		// Token: 0x060000F8 RID: 248
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x060000F9 RID: 249
		[SecurityCritical]
		void Reset();

		// Token: 0x060000FA RID: 250
		[SecurityCritical]
		IEnumSTORE_CATEGORY Clone();
	}
}
