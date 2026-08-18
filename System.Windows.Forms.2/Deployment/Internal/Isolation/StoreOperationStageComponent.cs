using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000051 RID: 81
	internal struct StoreOperationStageComponent
	{
		// Token: 0x06000183 RID: 387 RVA: 0x000072B6 File Offset: 0x000054B6
		public void Destroy()
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000072B8 File Offset: 0x000054B8
		public StoreOperationStageComponent(IDefinitionAppId app, string Manifest)
		{
			this = new StoreOperationStageComponent(app, null, Manifest);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000072C3 File Offset: 0x000054C3
		[SecuritySafeCritical]
		public StoreOperationStageComponent(IDefinitionAppId app, IDefinitionIdentity comp, string Manifest)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponent));
			this.Flags = StoreOperationStageComponent.OpFlags.Nothing;
			this.Application = app;
			this.Component = comp;
			this.ManifestPath = Manifest;
		}

		// Token: 0x04000159 RID: 345
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400015A RID: 346
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponent.OpFlags Flags;

		// Token: 0x0400015B RID: 347
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400015C RID: 348
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x0400015D RID: 349
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x02000521 RID: 1313
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037A0 RID: 14240
			Nothing = 0
		}

		// Token: 0x02000522 RID: 1314
		public enum Disposition
		{
			// Token: 0x040037A2 RID: 14242
			Failed,
			// Token: 0x040037A3 RID: 14243
			Installed,
			// Token: 0x040037A4 RID: 14244
			Refreshed,
			// Token: 0x040037A5 RID: 14245
			AlreadyInstalled
		}
	}
}
