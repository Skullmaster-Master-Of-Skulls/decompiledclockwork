using System;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel
{
	// Token: 0x020000AD RID: 173
	internal static class MsmqError
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x00011C64 File Offset: 0x0000FE64
		public static string GetErrorString(int error)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			bool flag;
			if ((error & 268369920) == 917504)
			{
				int dwFlags = 10752;
				flag = (UnsafeNativeMethods.FormatMessage(dwFlags, Msmq.ErrorStrings, error, CultureInfo.CurrentCulture.LCID, stringBuilder, stringBuilder.Capacity, IntPtr.Zero) != 0);
			}
			else
			{
				int dwFlags2 = 12800;
				flag = (UnsafeNativeMethods.FormatMessage(dwFlags2, IntPtr.Zero, error, CultureInfo.CurrentCulture.LCID, stringBuilder, stringBuilder.Capacity, IntPtr.Zero) != 0);
			}
			if (flag)
			{
				stringBuilder = stringBuilder.Replace("\n", "");
				stringBuilder = stringBuilder.Replace("\r", "");
				return SR.GetString("MsmqKnownWin32Error", new object[]
				{
					stringBuilder.ToString(),
					error.ToString(CultureInfo.InvariantCulture),
					Convert.ToString(error, 16)
				});
			}
			return SR.GetString("MsmqUnknownWin32Error", new object[]
			{
				error.ToString(CultureInfo.InvariantCulture),
				Convert.ToString(error, 16)
			});
		}
	}
}
