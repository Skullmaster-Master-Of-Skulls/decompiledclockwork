using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020004F4 RID: 1268
	internal class NetRes
	{
		// Token: 0x060027AA RID: 10154 RVA: 0x000A32D2 File Offset: 0x000A22D2
		private NetRes()
		{
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000A32DC File Offset: 0x000A22DC
		public static string GetWebStatusString(string Res, WebExceptionStatus Status)
		{
			string @string = SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
			string string2 = SR.GetString(Res);
			return string.Format(CultureInfo.CurrentCulture, string2, new object[]
			{
				@string
			});
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000A3313 File Offset: 0x000A2313
		public static string GetWebStatusString(WebExceptionStatus Status)
		{
			return SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000A3320 File Offset: 0x000A2320
		public static string GetWebStatusCodeString(HttpStatusCode statusCode, string statusDescription)
		{
			string str = "(";
			int num = (int)statusCode;
			string text = str + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text2 = null;
			try
			{
				text2 = SR.GetString("net_httpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text2 != null && text2.Length > 0)
			{
				text = text + " " + text2;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text = text + " " + statusDescription;
			}
			return text;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000A33B4 File Offset: 0x000A23B4
		public static string GetWebStatusCodeString(FtpStatusCode statusCode, string statusDescription)
		{
			string str = "(";
			int num = (int)statusCode;
			string text = str + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text2 = null;
			try
			{
				text2 = SR.GetString("net_ftpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text2 != null && text2.Length > 0)
			{
				text = text + " " + text2;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text = text + " " + statusDescription;
			}
			return text;
		}
	}
}
