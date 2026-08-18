using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000143 RID: 323
	internal class HttpProtocolUtils
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x0003E145 File Offset: 0x0003C345
		private HttpProtocolUtils()
		{
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0003E150 File Offset: 0x0003C350
		internal static DateTime string2date(string S)
		{
			DateTime result;
			if (HttpDateParse.ParseHttpDate(S, out result))
			{
				return result;
			}
			throw new ProtocolViolationException(SR.GetString("net_baddate"));
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0003E178 File Offset: 0x0003C378
		internal static string date2string(DateTime D)
		{
			DateTimeFormatInfo provider = new DateTimeFormatInfo();
			return D.ToUniversalTime().ToString("R", provider);
		}
	}
}
