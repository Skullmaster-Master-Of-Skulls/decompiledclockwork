using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005A RID: 90
	internal struct StoreOperationSetCanonicalizationContext
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00007783 File Offset: 0x00005983
		[SecurityCritical]
		public StoreOperationSetCanonicalizationContext(string Bases, string Exports)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetCanonicalizationContext));
			this.Flags = StoreOperationSetCanonicalizationContext.OpFlags.Nothing;
			this.BaseAddressFilePath = Bases;
			this.ExportsFilePath = Exports;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000072B6 File Offset: 0x000054B6
		public void Destroy()
		{
		}

		// Token: 0x04000186 RID: 390
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000187 RID: 391
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetCanonicalizationContext.OpFlags Flags;

		// Token: 0x04000188 RID: 392
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BaseAddressFilePath;

		// Token: 0x04000189 RID: 393
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ExportsFilePath;

		// Token: 0x02000530 RID: 1328
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037CD RID: 14285
			Nothing = 0
		}
	}
}
