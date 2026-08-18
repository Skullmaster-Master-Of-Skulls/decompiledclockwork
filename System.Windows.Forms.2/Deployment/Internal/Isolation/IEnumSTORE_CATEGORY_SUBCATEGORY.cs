using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000036 RID: 54
	[Guid("19be1967-b2fc-4dc1-9627-f3cb6305d2a7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_SUBCATEGORY
	{
		// Token: 0x06000102 RID: 258
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_SUBCATEGORY[] rgElements);

		// Token: 0x06000103 RID: 259
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06000104 RID: 260
		[SecurityCritical]
		void Reset();

		// Token: 0x06000105 RID: 261
		[SecurityCritical]
		IEnumSTORE_CATEGORY_SUBCATEGORY Clone();
	}
}
