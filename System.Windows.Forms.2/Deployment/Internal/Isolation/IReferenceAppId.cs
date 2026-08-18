using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000045 RID: 69
	[Guid("054f0bef-9e45-4363-8f5a-2f8e142d9a3b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IReferenceAppId
	{
		// Token: 0x06000156 RID: 342
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_SubscriptionId();

		// Token: 0x06000157 RID: 343
		void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr)] [In] string Subscription);

		// Token: 0x06000158 RID: 344
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_Codebase();

		// Token: 0x06000159 RID: 345
		void put_Codebase([MarshalAs(UnmanagedType.LPWStr)] [In] string CodeBase);

		// Token: 0x0600015A RID: 346
		[SecurityCritical]
		IEnumReferenceIdentity EnumAppPath();
	}
}
