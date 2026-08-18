using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E0 RID: 992
	[__DynamicallyInvokable]
	public struct FORMATETC
	{
		// Token: 0x04002099 RID: 8345
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.U2)]
		public short cfFormat;

		// Token: 0x0400209A RID: 8346
		public IntPtr ptd;

		// Token: 0x0400209B RID: 8347
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.U4)]
		public DVASPECT dwAspect;

		// Token: 0x0400209C RID: 8348
		[__DynamicallyInvokable]
		public int lindex;

		// Token: 0x0400209D RID: 8349
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.U4)]
		public TYMED tymed;
	}
}
