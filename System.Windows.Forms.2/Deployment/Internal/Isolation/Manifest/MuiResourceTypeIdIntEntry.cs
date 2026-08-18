using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000087 RID: 135
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdIntEntry : IDisposable
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00008990 File Offset: 0x00006B90
		~MuiResourceTypeIdIntEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000089C0 File Offset: 0x00006BC0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000089CC File Offset: 0x00006BCC
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.StringIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.StringIds);
				this.StringIds = IntPtr.Zero;
			}
			if (this.IntegerIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.IntegerIds);
				this.IntegerIds = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400024B RID: 587
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x0400024C RID: 588
		public uint StringIdsSize;

		// Token: 0x0400024D RID: 589
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x0400024E RID: 590
		public uint IntegerIdsSize;
	}
}
