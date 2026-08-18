using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D8 RID: 216
	[StructLayout(LayoutKind.Sequential)]
	internal class RegistryKeyEntry : IDisposable
	{
		// Token: 0x06000302 RID: 770 RVA: 0x00008D24 File Offset: 0x00006F24
		~RegistryKeyEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008D54 File Offset: 0x00006F54
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00008D60 File Offset: 0x00006F60
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.SecurityDescriptor != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.SecurityDescriptor);
				this.SecurityDescriptor = IntPtr.Zero;
			}
			if (this.Values != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.Values);
				this.Values = IntPtr.Zero;
			}
			if (this.Keys != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.Keys);
				this.Keys = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000374 RID: 884
		public uint Flags;

		// Token: 0x04000375 RID: 885
		public uint Protection;

		// Token: 0x04000376 RID: 886
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BuildFilter;

		// Token: 0x04000377 RID: 887
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr SecurityDescriptor;

		// Token: 0x04000378 RID: 888
		public uint SecurityDescriptorSize;

		// Token: 0x04000379 RID: 889
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr Values;

		// Token: 0x0400037A RID: 890
		public uint ValuesSize;

		// Token: 0x0400037B RID: 891
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr Keys;

		// Token: 0x0400037C RID: 892
		public uint KeysSize;
	}
}
