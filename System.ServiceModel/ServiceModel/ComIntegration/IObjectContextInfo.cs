using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200026F RID: 623
	[Guid("75B52DDB-E8ED-11D1-93AD-00AA00BA3258")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IObjectContextInfo
	{
		// Token: 0x060011B6 RID: 4534
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsInTransaction();

		// Token: 0x060011B7 RID: 4535
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetTransaction();

		// Token: 0x060011B8 RID: 4536
		void GetTransactionId(out Guid guid);

		// Token: 0x060011B9 RID: 4537
		void GetActivityId(out Guid guid);

		// Token: 0x060011BA RID: 4538
		void GetContextId(out Guid guid);
	}
}
