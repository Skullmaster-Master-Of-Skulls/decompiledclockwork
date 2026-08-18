using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000DB RID: 219
	[StructLayout(LayoutKind.Sequential)]
	internal class DirectoryEntry : IDisposable
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00008DF0 File Offset: 0x00006FF0
		~DirectoryEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008E20 File Offset: 0x00007020
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00008E29 File Offset: 0x00007029
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.SecurityDescriptor != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.SecurityDescriptor);
				this.SecurityDescriptor = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000387 RID: 903
		public uint Flags;

		// Token: 0x04000388 RID: 904
		public uint Protection;

		// Token: 0x04000389 RID: 905
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BuildFilter;

		// Token: 0x0400038A RID: 906
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr SecurityDescriptor;

		// Token: 0x0400038B RID: 907
		public uint SecurityDescriptorSize;
	}
}
