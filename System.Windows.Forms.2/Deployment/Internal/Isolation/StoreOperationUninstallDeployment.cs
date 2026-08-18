using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000057 RID: 87
	internal struct StoreOperationUninstallDeployment
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000074CF File Offset: 0x000056CF
		[SecuritySafeCritical]
		public StoreOperationUninstallDeployment(IDefinitionAppId appid, StoreApplicationReference AppRef)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUninstallDeployment));
			this.Flags = StoreOperationUninstallDeployment.OpFlags.Nothing;
			this.Application = appid;
			this.Reference = AppRef.ToIntPtr();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007501 File Offset: 0x00005701
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04000176 RID: 374
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000177 RID: 375
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUninstallDeployment.OpFlags Flags;

		// Token: 0x04000178 RID: 376
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04000179 RID: 377
		public IntPtr Reference;

		// Token: 0x0200052C RID: 1324
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037C2 RID: 14274
			Nothing = 0
		}

		// Token: 0x0200052D RID: 1325
		public enum Disposition
		{
			// Token: 0x040037C4 RID: 14276
			Failed,
			// Token: 0x040037C5 RID: 14277
			DidNotExist,
			// Token: 0x040037C6 RID: 14278
			Uninstalled
		}
	}
}
