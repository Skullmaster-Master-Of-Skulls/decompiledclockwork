using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000055 RID: 85
	internal struct StoreOperationUnpinDeployment
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000742A File Offset: 0x0000562A
		[SecuritySafeCritical]
		public StoreOperationUnpinDeployment(IDefinitionAppId app, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUnpinDeployment));
			this.Flags = StoreOperationUnpinDeployment.OpFlags.Nothing;
			this.Application = app;
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000745C File Offset: 0x0000565C
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x0400016E RID: 366
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400016F RID: 367
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUnpinDeployment.OpFlags Flags;

		// Token: 0x04000170 RID: 368
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04000171 RID: 369
		public IntPtr Reference;

		// Token: 0x02000528 RID: 1320
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037B6 RID: 14262
			Nothing = 0
		}

		// Token: 0x02000529 RID: 1321
		public enum Disposition
		{
			// Token: 0x040037B8 RID: 14264
			Failed,
			// Token: 0x040037B9 RID: 14265
			Unpinned
		}
	}
}
