using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000438 RID: 1080
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class SecureStringMarshal
	{
		// Token: 0x0600287E RID: 10366 RVA: 0x000BA274 File Offset: 0x000B8474
		[SecuritySafeCritical]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemAnsi(s);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000BA27C File Offset: 0x000B847C
		[SecuritySafeCritical]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocAnsi(s);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000BA284 File Offset: 0x000B8484
		[SecuritySafeCritical]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemUnicode(s);
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000BA28C File Offset: 0x000B848C
		[SecuritySafeCritical]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocUnicode(s);
		}
	}
}
