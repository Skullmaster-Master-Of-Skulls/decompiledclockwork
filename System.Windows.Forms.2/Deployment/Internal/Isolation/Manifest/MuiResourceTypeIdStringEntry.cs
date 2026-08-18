using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000084 RID: 132
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdStringEntry : IDisposable
	{
		// Token: 0x0600022D RID: 557 RVA: 0x000088EC File Offset: 0x00006AEC
		~MuiResourceTypeIdStringEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000891C File Offset: 0x00006B1C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008928 File Offset: 0x00006B28
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

		// Token: 0x04000242 RID: 578
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x04000243 RID: 579
		public uint StringIdsSize;

		// Token: 0x04000244 RID: 580
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x04000245 RID: 581
		public uint IntegerIdsSize;
	}
}
