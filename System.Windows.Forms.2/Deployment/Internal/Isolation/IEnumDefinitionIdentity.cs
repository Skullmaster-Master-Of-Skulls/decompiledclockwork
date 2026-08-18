using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200003F RID: 63
	[Guid("f3549d9c-fc73-4793-9c00-1cd204254c0c")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumDefinitionIdentity
	{
		// Token: 0x06000133 RID: 307
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDefinitionIdentity[] DefinitionIdentity);

		// Token: 0x06000134 RID: 308
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06000135 RID: 309
		[SecurityCritical]
		void Reset();

		// Token: 0x06000136 RID: 310
		[SecurityCritical]
		IEnumDefinitionIdentity Clone();
	}
}
