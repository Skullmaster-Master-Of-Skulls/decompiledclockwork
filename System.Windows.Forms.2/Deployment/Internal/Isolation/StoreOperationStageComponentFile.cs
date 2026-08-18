using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000052 RID: 82
	internal struct StoreOperationStageComponentFile
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000072F6 File Offset: 0x000054F6
		public StoreOperationStageComponentFile(IDefinitionAppId App, string CompRelPath, string SrcFile)
		{
			this = new StoreOperationStageComponentFile(App, null, CompRelPath, SrcFile);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007302 File Offset: 0x00005502
		[SecuritySafeCritical]
		public StoreOperationStageComponentFile(IDefinitionAppId App, IDefinitionIdentity Component, string CompRelPath, string SrcFile)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponentFile));
			this.Flags = StoreOperationStageComponentFile.OpFlags.Nothing;
			this.Application = App;
			this.Component = Component;
			this.ComponentRelativePath = CompRelPath;
			this.SourceFilePath = SrcFile;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000072B6 File Offset: 0x000054B6
		public void Destroy()
		{
		}

		// Token: 0x0400015E RID: 350
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400015F RID: 351
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponentFile.OpFlags Flags;

		// Token: 0x04000160 RID: 352
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04000161 RID: 353
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x04000162 RID: 354
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ComponentRelativePath;

		// Token: 0x04000163 RID: 355
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceFilePath;

		// Token: 0x02000523 RID: 1315
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037A7 RID: 14247
			Nothing = 0
		}

		// Token: 0x02000524 RID: 1316
		public enum Disposition
		{
			// Token: 0x040037A9 RID: 14249
			Failed,
			// Token: 0x040037AA RID: 14250
			Installed,
			// Token: 0x040037AB RID: 14251
			Refreshed,
			// Token: 0x040037AC RID: 14252
			AlreadyInstalled
		}
	}
}
