using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000180 RID: 384
	internal static class WebExceptionMapping
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x00049C70 File Offset: 0x00047E70
		internal static string GetWebStatusString(WebExceptionStatus status)
		{
			int num = (int)status;
			if (num >= WebExceptionMapping.s_Mapping.Length || num < 0)
			{
				throw new InternalException();
			}
			string text = Volatile.Read<string>(ref WebExceptionMapping.s_Mapping[num]);
			if (text == null)
			{
				text = "net_webstatus_" + status.ToString();
				Volatile.Write<string>(ref WebExceptionMapping.s_Mapping[num], text);
			}
			return text;
		}

		// Token: 0x0400124A RID: 4682
		private static readonly string[] s_Mapping = new string[21];
	}
}
