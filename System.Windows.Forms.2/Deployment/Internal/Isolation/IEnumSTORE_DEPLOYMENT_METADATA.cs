using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002C RID: 44
	[Guid("f9fd4090-93db-45c0-af87-624940f19cff")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_DEPLOYMENT_METADATA
	{
		// Token: 0x060000CB RID: 203
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDefinitionAppId[] AppIds);

		// Token: 0x060000CC RID: 204
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x060000CD RID: 205
		[SecurityCritical]
		void Reset();

		// Token: 0x060000CE RID: 206
		[SecurityCritical]
		IEnumSTORE_DEPLOYMENT_METADATA Clone();
	}
}
