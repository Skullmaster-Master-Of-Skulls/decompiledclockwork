using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200008A RID: 138
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceMapEntry : IDisposable
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00008A34 File Offset: 0x00006C34
		~MuiResourceMapEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008A64 File Offset: 0x00006C64
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008A70 File Offset: 0x00006C70
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.ResourceTypeIdInt != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdInt);
				this.ResourceTypeIdInt = IntPtr.Zero;
			}
			if (this.ResourceTypeIdString != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdString);
				this.ResourceTypeIdString = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000254 RID: 596
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdInt;

		// Token: 0x04000255 RID: 597
		public uint ResourceTypeIdIntSize;

		// Token: 0x04000256 RID: 598
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdString;

		// Token: 0x04000257 RID: 599
		public uint ResourceTypeIdStringSize;
	}
}
