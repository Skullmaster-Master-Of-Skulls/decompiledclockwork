using System;

namespace System.Net
{
	// Token: 0x020004A1 RID: 1185
	internal static class WebExceptionMapping
	{
		// Token: 0x06002424 RID: 9252 RVA: 0x0008D4E4 File Offset: 0x0008C4E4
		internal static string GetWebStatusString(WebExceptionStatus status)
		{
			if (status >= (WebExceptionStatus)WebExceptionMapping.s_Mapping.Length || status < WebExceptionStatus.Success)
			{
				throw new InternalException();
			}
			string text = WebExceptionMapping.s_Mapping[(int)status];
			if (text == null)
			{
				text = "net_webstatus_" + status.ToString();
				WebExceptionMapping.s_Mapping[(int)status] = text;
			}
			return text;
		}

		// Token: 0x04002491 RID: 9361
		private static readonly string[] s_Mapping = new string[21];
	}
}
