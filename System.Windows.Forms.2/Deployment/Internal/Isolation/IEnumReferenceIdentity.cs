using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000041 RID: 65
	[Guid("b30352cf-23da-4577-9b3f-b4e6573be53b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumReferenceIdentity
	{
		// Token: 0x0600013E RID: 318
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IReferenceIdentity[] ReferenceIdentity);

		// Token: 0x0600013F RID: 319
		[SecurityCritical]
		void Skip(uint celt);

		// Token: 0x06000140 RID: 320
		[SecurityCritical]
		void Reset();

		// Token: 0x06000141 RID: 321
		[SecurityCritical]
		IEnumReferenceIdentity Clone();
	}
}
