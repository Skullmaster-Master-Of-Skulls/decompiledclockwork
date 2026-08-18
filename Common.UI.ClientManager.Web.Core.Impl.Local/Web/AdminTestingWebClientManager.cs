using System;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using ClockWorkLogger;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x02000009 RID: 9
	public class AdminTestingWebClientManager : IAdminTestingWebClientManager
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002804 File Offset: 0x00000A04
		public string SessionToString(Page page)
		{
			bool flag = page == null || page.Session == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				HttpSessionState session = page.Session;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < session.Contents.Count; i++)
				{
					bool flag2 = stringBuilder.Length > 0;
					if (flag2)
					{
						stringBuilder.Append("``");
					}
					stringBuilder.AppendFormat("{0}={1}", session.Keys[i], session[i].ToString());
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028A8 File Offset: 0x00000AA8
		public void ShowAdminMessage(object Page, AdminTestMessageView Message)
		{
			Page page = (Page != null) ? ((Page)Page) : null;
			bool showSessionContents = Message.ShowSessionContents;
			if (showSessionContents)
			{
				CWLogger.Logger.Info("AdminTestingWebClientManager:context={0}:msg={1}", Message.Context ?? "", Message.Message ?? "");
			}
			else
			{
				CWLogger.Logger.Info("AdminTestingWebClientManager:context={0}:session={1}:msg={2}", Message.Context ?? "", this.SessionToString(page), Message.Message ?? "");
			}
		}
	}
}
