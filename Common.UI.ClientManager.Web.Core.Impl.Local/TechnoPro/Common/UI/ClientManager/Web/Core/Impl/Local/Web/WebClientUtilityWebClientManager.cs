using System;
using System.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x02000011 RID: 17
	public class WebClientUtilityWebClientManager : IWebClientUtilityWebClientManager
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000040A4 File Offset: 0x000022A4
		public static WebClientUtilityWebClientManager CurrentInstance
		{
			get
			{
				bool flag = WebClientUtilityWebClientManager._currentInstance == null;
				if (flag)
				{
					WebClientUtilityWebClientManager._currentInstance = new WebClientUtilityWebClientManager();
				}
				return WebClientUtilityWebClientManager._currentInstance;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000040D4 File Offset: 0x000022D4
		public string GetCurrentFullUrl()
		{
			HttpRequest request = HttpContext.Current.Request;
			return request.Url.AbsoluteUri;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000040FC File Offset: 0x000022FC
		public string GetUsersIpAddress()
		{
			HttpRequest request = HttpContext.Current.Request;
			string text = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			bool flag = text == string.Empty;
			if (flag)
			{
				text = request.ServerVariables["REMOTE_ADDR"];
			}
			return text;
		}

		// Token: 0x04000014 RID: 20
		private static WebClientUtilityWebClientManager _currentInstance;
	}
}
