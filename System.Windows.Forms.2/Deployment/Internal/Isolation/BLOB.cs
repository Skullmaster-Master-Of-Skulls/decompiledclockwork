using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200001F RID: 31
	internal struct BLOB : IDisposable
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00006C02 File Offset: 0x00004E02
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.BlobData != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.BlobData);
				this.BlobData = IntPtr.Zero;
			}
		}

		// Token: 0x0400010B RID: 267
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400010C RID: 268
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr BlobData;
	}
}
