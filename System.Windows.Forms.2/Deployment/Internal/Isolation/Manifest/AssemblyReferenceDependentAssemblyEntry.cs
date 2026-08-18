using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A8 RID: 168
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceDependentAssemblyEntry : IDisposable
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00008C14 File Offset: 0x00006E14
		~AssemblyReferenceDependentAssemblyEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008C44 File Offset: 0x00006E44
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008C4D File Offset: 0x00006E4D
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
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x040002BD RID: 701
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x040002BE RID: 702
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Codebase;

		// Token: 0x040002BF RID: 703
		public ulong Size;

		// Token: 0x040002C0 RID: 704
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x040002C1 RID: 705
		public uint HashValueSize;

		// Token: 0x040002C2 RID: 706
		public uint HashAlgorithm;

		// Token: 0x040002C3 RID: 707
		public uint Flags;

		// Token: 0x040002C4 RID: 708
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ResourceFallbackCulture;

		// Token: 0x040002C5 RID: 709
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x040002C6 RID: 710
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040002C7 RID: 711
		public ISection HashElements;
	}
}
