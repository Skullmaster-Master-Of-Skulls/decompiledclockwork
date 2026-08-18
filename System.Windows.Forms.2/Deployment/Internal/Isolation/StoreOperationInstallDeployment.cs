using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000056 RID: 86
	internal struct StoreOperationInstallDeployment
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00007469 File Offset: 0x00005669
		public StoreOperationInstallDeployment(IDefinitionAppId App, StoreApplicationReference reference)
		{
			this = new StoreOperationInstallDeployment(App, true, reference);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007474 File Offset: 0x00005674
		[SecuritySafeCritical]
		public StoreOperationInstallDeployment(IDefinitionAppId App, bool UninstallOthers, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationInstallDeployment));
			this.Flags = StoreOperationInstallDeployment.OpFlags.Nothing;
			this.Application = App;
			if (UninstallOthers)
			{
				this.Flags |= StoreOperationInstallDeployment.OpFlags.UninstallOthers;
			}
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000074C2 File Offset: 0x000056C2
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04000172 RID: 370
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000173 RID: 371
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationInstallDeployment.OpFlags Flags;

		// Token: 0x04000174 RID: 372
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04000175 RID: 373
		public IntPtr Reference;

		// Token: 0x0200052A RID: 1322
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037BB RID: 14267
			Nothing = 0,
			// Token: 0x040037BC RID: 14268
			UninstallOthers = 1
		}

		// Token: 0x0200052B RID: 1323
		public enum Disposition
		{
			// Token: 0x040037BE RID: 14270
			Failed,
			// Token: 0x040037BF RID: 14271
			AlreadyInstalled,
			// Token: 0x040037C0 RID: 14272
			Installed
		}
	}
}
