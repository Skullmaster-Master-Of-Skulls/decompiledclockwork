using System;
using System.Configuration;
using System.Web;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02000075 RID: 117
	public class PreventableHandler
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000BE94 File Offset: 0x0000A094
		public bool CheckPreventHandler(string flag, HttpContext context = null, string requestIdentifier = "")
		{
			string text = ConfigurationManager.AppSettings[flag];
			return this.isDesiredRequest(context, requestIdentifier) && text != null && text.ToLowerInvariant() == "true";
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000BECC File Offset: 0x0000A0CC
		private bool isDesiredRequest(HttpContext context, string requestIdentifier)
		{
			if (context != null && requestIdentifier != string.Empty)
			{
				bool flag = string.Equals(context.Request["type"], requestIdentifier, StringComparison.OrdinalIgnoreCase);
				bool flag2 = context.Request.Url.PathAndQuery.IndexOf("Telerik.Web.UI.WebResource", StringComparison.OrdinalIgnoreCase) > -1;
				return flag && flag2;
			}
			return true;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000BF28 File Offset: 0x0000A128
		public void CompleteRequest(HttpApplication app, int statusCode)
		{
			app.Response.StatusCode = statusCode;
			app.CompleteRequest();
		}
	}
}
