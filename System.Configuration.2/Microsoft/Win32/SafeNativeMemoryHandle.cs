using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000006 RID: 6
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeNativeMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002088 File Offset: 0x00000288
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeNativeMemoryHandle() : this(false)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002091 File Offset: 0x00000291
		internal SafeNativeMemoryHandle(bool useLocalFree) : base(true)
		{
			this._useLocalFree = useLocalFree;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002059 File Offset: 0x00000259
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeNativeMemoryHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A1 File Offset: 0x000002A1
		internal void SetDataHandle(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				if (this._useLocalFree)
				{
					UnsafeNativeMethods.LocalFree(this.handle);
				}
				else
				{
					Marshal.FreeHGlobal(this.handle);
				}
				this.handle = IntPtr.Zero;
				return true;
			}
			return false;
		}

		// Token: 0x04000050 RID: 80
		private bool _useLocalFree;
	}
}
