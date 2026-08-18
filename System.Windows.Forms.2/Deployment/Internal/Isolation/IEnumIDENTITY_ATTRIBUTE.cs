using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200003E RID: 62
	[Guid("9cdaae75-246e-4b00-a26d-b9aec137a3eb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumIDENTITY_ATTRIBUTE
	{
		// Token: 0x0600012E RID: 302
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDENTITY_ATTRIBUTE[] rgAttributes);

		// Token: 0x0600012F RID: 303
		[SecurityCritical]
		IntPtr CurrentIntoBuffer([In] IntPtr Available, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] Data);

		// Token: 0x06000130 RID: 304
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06000131 RID: 305
		[SecurityCritical]
		void Reset();

		// Token: 0x06000132 RID: 306
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE Clone();
	}
}
