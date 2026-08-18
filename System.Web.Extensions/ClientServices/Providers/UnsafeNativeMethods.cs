using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000115 RID: 277
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000EA2 RID: 3746
		[DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int InternetSetCookieW(string uri, string cookieName, string cookieValue);

		// Token: 0x06000EA3 RID: 3747
		[DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int InternetGetCookieW(string uri, string cookieName, StringBuilder cookieValue, ref int dwSize);
	}
}
