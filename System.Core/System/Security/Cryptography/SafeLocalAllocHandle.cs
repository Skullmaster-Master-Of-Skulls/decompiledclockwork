using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000F2 RID: 242
	internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x00018FE2 File Offset: 0x000171E2
		[SecuritySafeCritical]
		private SafeLocalAllocHandle() : base(true)
		{
		}

		// Token: 0x060007A6 RID: 1958
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x060007A7 RID: 1959 RVA: 0x00018FEC File Offset: 0x000171EC
		[SecuritySafeCritical]
		internal unsafe T Read<T>(int offset) where T : struct
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			T result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = new IntPtr((void*)((byte*)this.handle.ToPointer() + offset));
				result = (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00019050 File Offset: 0x00017250
		[SecuritySafeCritical]
		protected override bool ReleaseHandle()
		{
			return SafeLocalAllocHandle.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00019067 File Offset: 0x00017267
		[SecuritySafeCritical]
		internal SafeLocalAllocHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00019077 File Offset: 0x00017277
		internal static SafeLocalAllocHandle InvalidHandle
		{
			[SecuritySafeCritical]
			get
			{
				return new SafeLocalAllocHandle(IntPtr.Zero);
			}
		}
	}
}
