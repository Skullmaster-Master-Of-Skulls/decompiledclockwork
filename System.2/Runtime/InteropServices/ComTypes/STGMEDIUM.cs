using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E6 RID: 998
	[__DynamicallyInvokable]
	public struct STGMEDIUM
	{
		// Token: 0x040020A2 RID: 8354
		[__DynamicallyInvokable]
		public TYMED tymed;

		// Token: 0x040020A3 RID: 8355
		public IntPtr unionmember;

		// Token: 0x040020A4 RID: 8356
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.IUnknown)]
		public object pUnkForRelease;
	}
}
