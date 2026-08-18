using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000061 RID: 97
	internal struct IStore_BindingResult
	{
		// Token: 0x040001A2 RID: 418
		[MarshalAs(UnmanagedType.U4)]
		public uint Flags;

		// Token: 0x040001A3 RID: 419
		[MarshalAs(UnmanagedType.U4)]
		public uint Disposition;

		// Token: 0x040001A4 RID: 420
		public IStore_BindingResult_BoundVersion Component;

		// Token: 0x040001A5 RID: 421
		public Guid CacheCoherencyGuid;

		// Token: 0x040001A6 RID: 422
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr Reserved;
	}
}
