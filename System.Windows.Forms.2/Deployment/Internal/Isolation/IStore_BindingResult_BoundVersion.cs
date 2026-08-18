using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000060 RID: 96
	internal struct IStore_BindingResult_BoundVersion
	{
		// Token: 0x0400019E RID: 414
		[MarshalAs(UnmanagedType.U2)]
		public ushort Revision;

		// Token: 0x0400019F RID: 415
		[MarshalAs(UnmanagedType.U2)]
		public ushort Build;

		// Token: 0x040001A0 RID: 416
		[MarshalAs(UnmanagedType.U2)]
		public ushort Minor;

		// Token: 0x040001A1 RID: 417
		[MarshalAs(UnmanagedType.U2)]
		public ushort Major;
	}
}
