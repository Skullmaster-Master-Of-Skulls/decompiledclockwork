using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x020000F3 RID: 243
	internal class X509Utils
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00019084 File Offset: 0x00017284
		[SecuritySafeCritical]
		internal static SafeLocalAllocHandle StringToAnsiPtr(string s)
		{
			byte[] array = new byte[s.Length + 1];
			Encoding.ASCII.GetBytes(s, 0, s.Length, array, 0);
			SafeLocalAllocHandle safeLocalAllocHandle = CapiNative.LocalAlloc(0U, new IntPtr(array.Length));
			Marshal.Copy(array, 0, safeLocalAllocHandle.DangerousGetHandle(), array.Length);
			return safeLocalAllocHandle;
		}
	}
}
