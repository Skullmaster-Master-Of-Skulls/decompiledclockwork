using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Security
{
	// Token: 0x0200068A RID: 1674
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeBSTRHandle : SafePointer
	{
		// Token: 0x06003C7F RID: 15487 RVA: 0x000CEF00 File Offset: 0x000CDF00
		internal SafeBSTRHandle() : base(true)
		{
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x000CEF0C File Offset: 0x000CDF0C
		internal static SafeBSTRHandle Allocate(string src, uint len)
		{
			SafeBSTRHandle safeBSTRHandle = SafeBSTRHandle.SysAllocStringLen(src, len);
			safeBSTRHandle.Initialize((ulong)(len * 2U));
			return safeBSTRHandle;
		}

		// Token: 0x06003C81 RID: 15489
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		private static extern SafeBSTRHandle SysAllocStringLen(string src, uint len);

		// Token: 0x06003C82 RID: 15490 RVA: 0x000CEF2C File Offset: 0x000CDF2C
		protected override bool ReleaseHandle()
		{
			Win32Native.ZeroMemory(this.handle, (uint)(Win32Native.SysStringLen(this.handle) * 2));
			Win32Native.SysFreeString(this.handle);
			return true;
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x000CEF54 File Offset: 0x000CDF54
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe void ClearBuffer()
		{
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.AcquirePointer(ref ptr);
				Win32Native.ZeroMemory((IntPtr)((void*)ptr), (uint)(Win32Native.SysStringLen((IntPtr)((void*)ptr)) * 2));
			}
			finally
			{
				if (ptr != null)
				{
					base.ReleasePointer();
				}
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000CEFA8 File Offset: 0x000CDFA8
		internal unsafe int Length
		{
			get
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				int result;
				try
				{
					base.AcquirePointer(ref ptr);
					result = Win32Native.SysStringLen((IntPtr)((void*)ptr));
				}
				finally
				{
					if (ptr != null)
					{
						base.ReleasePointer();
					}
				}
				return result;
			}
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000CEFF0 File Offset: 0x000CDFF0
		internal unsafe static void Copy(SafeBSTRHandle source, SafeBSTRHandle target)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				source.AcquirePointer(ref ptr);
				target.AcquirePointer(ref ptr2);
				Buffer.memcpyimpl(ptr, ptr2, Win32Native.SysStringLen((IntPtr)((void*)ptr)) * 2);
			}
			finally
			{
				if (ptr != null)
				{
					source.ReleasePointer();
				}
				if (ptr2 != null)
				{
					target.ReleasePointer();
				}
			}
		}
	}
}
