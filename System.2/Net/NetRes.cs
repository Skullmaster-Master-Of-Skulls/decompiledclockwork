using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020001CB RID: 459
	internal class NetRes
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x00060F86 File Offset: 0x0005F186
		private NetRes()
		{
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00060F90 File Offset: 0x0005F190
		public static string GetWebStatusString(string Res, WebExceptionStatus Status)
		{
			string @string = SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
			string string2 = SR.GetString(Res);
			return string.Format(CultureInfo.CurrentCulture, string2, new object[]
			{
				@string
			});
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00060FC5 File Offset: 0x0005F1C5
		public static string GetWebStatusString(WebExceptionStatus Status)
		{
			return SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00060FD4 File Offset: 0x0005F1D4
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

		// Token: 0x06001236 RID: 4662 RVA: 0x0006106C File Offset: 0x0005F26C
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
