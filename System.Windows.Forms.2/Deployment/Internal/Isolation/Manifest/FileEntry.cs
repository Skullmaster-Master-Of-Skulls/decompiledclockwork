using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000090 RID: 144
	[StructLayout(LayoutKind.Sequential)]
	internal class FileEntry : IDisposable
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00008B7C File Offset: 0x00006D7C
		~FileEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008BAC File Offset: 0x00006DAC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008BB8 File Offset: 0x00006DB8
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.HashValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.HashValue);
				this.HashValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				if (this.MuiMapping != null)
				{
					this.MuiMapping.Dispose(true);
					this.MuiMapping = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400026D RID: 621
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400026E RID: 622
		public uint HashAlgorithm;

		// Token: 0x0400026F RID: 623
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LoadFrom;

		// Token: 0x04000270 RID: 624
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourcePath;

		// Token: 0x04000271 RID: 625
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ImportPath;

		// Token: 0x04000272 RID: 626
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceName;

		// Token: 0x04000273 RID: 627
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Location;

		// Token: 0x04000274 RID: 628
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x04000275 RID: 629
		public uint HashValueSize;

		// Token: 0x04000276 RID: 630
		public ulong Size;

		// Token: 0x04000277 RID: 631
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x04000278 RID: 632
		public uint Flags;

		// Token: 0x04000279 RID: 633
		public MuiResourceMapEntry MuiMapping;

		// Token: 0x0400027A RID: 634
		public uint WritableType;

		// Token: 0x0400027B RID: 635
		public ISection HashElements;
	}
}
