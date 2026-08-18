using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200008D RID: 141
	[StructLayout(LayoutKind.Sequential)]
	internal class HashElementEntry : IDisposable
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00008AD8 File Offset: 0x00006CD8
		~HashElementEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008B08 File Offset: 0x00006D08
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008B14 File Offset: 0x00006D14
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.TransformMetadata != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.TransformMetadata);
				this.TransformMetadata = IntPtr.Zero;
			}
			if (this.DigestValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.DigestValue);
				this.DigestValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400025D RID: 605
		public uint index;

		// Token: 0x0400025E RID: 606
		public byte Transform;

		// Token: 0x0400025F RID: 607
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr TransformMetadata;

		// Token: 0x04000260 RID: 608
		public uint TransformMetadataSize;

		// Token: 0x04000261 RID: 609
		public byte DigestMethod;

		// Token: 0x04000262 RID: 610
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr DigestValue;

		// Token: 0x04000263 RID: 611
		public uint DigestValueSize;

		// Token: 0x04000264 RID: 612
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;
	}
}
