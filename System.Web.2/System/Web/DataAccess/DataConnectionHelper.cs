using System;
using System.Security.Principal;
using System.Text;

namespace System.Web.DataAccess
{
	// Token: 0x020001AB RID: 427
	internal static class DataConnectionHelper
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x00046640 File Offset: 0x00044840
		internal static string GetCurrentName()
		{
			string text = "NETWORK SERVICE";
			string str = "NT AUTHORITY";
			IntPtr zero = IntPtr.Zero;
			try
			{
				if (UnsafeNativeMethods.ConvertStringSidToSid("S-1-5-20", out zero) != 0 && zero != IntPtr.Zero)
				{
					int capacity = 256;
					int capacity2 = 256;
					int num = 0;
					StringBuilder stringBuilder = new StringBuilder(capacity);
					StringBuilder stringBuilder2 = new StringBuilder(capacity2);
					if (UnsafeNativeMethods.LookupAccountSid(null, zero, stringBuilder, ref capacity, stringBuilder2, ref capacity2, ref num) != 0)
					{
						text = stringBuilder.ToString();
						str = stringBuilder2.ToString();
					}
				}
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				if (current != null && current.Name != null)
				{
					if (string.Compare(current.Name, str + "\\" + text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return text;
					}
					return current.Name;
				}
			}
			catch
			{
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					UnsafeNativeMethods.LocalFree(zero);
				}
			}
			return string.Empty;
		}
	}
}
