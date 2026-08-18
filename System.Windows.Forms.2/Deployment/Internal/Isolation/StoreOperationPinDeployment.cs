using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000054 RID: 84
	internal struct StoreOperationPinDeployment
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000073CB File Offset: 0x000055CB
		[SecuritySafeCritical]
		public StoreOperationPinDeployment(IDefinitionAppId AppId, StoreApplicationReference Ref)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationPinDeployment));
			this.Flags = StoreOperationPinDeployment.OpFlags.NeverExpires;
			this.Application = AppId;
			this.Reference = Ref.ToIntPtr();
			this.ExpirationTime = 0L;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007405 File Offset: 0x00005605
		public StoreOperationPinDeployment(IDefinitionAppId AppId, DateTime Expiry, StoreApplicationReference Ref)
		{
			this = new StoreOperationPinDeployment(AppId, Ref);
			this.Flags |= StoreOperationPinDeployment.OpFlags.NeverExpires;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000741D File Offset: 0x0000561D
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04000169 RID: 361
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400016A RID: 362
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationPinDeployment.OpFlags Flags;

		// Token: 0x0400016B RID: 363
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400016C RID: 364
		[MarshalAs(UnmanagedType.I8)]
		public long ExpirationTime;

		// Token: 0x0400016D RID: 365
		public IntPtr Reference;

		// Token: 0x02000526 RID: 1318
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037B0 RID: 14256
			Nothing = 0,
			// Token: 0x040037B1 RID: 14257
			NeverExpires = 1
		}

		// Token: 0x02000527 RID: 1319
		public enum Disposition
		{
			// Token: 0x040037B3 RID: 14259
			Failed,
			// Token: 0x040037B4 RID: 14260
			Pinned
		}
	}
}
