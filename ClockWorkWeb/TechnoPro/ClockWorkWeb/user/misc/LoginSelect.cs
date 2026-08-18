using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.UI.Web.Entity.WebLogin;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000BB RID: 187
	public class LoginSelect : Page
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0002A190 File Offset: 0x00028390
		protected void Page_Load(object sender, EventArgs e)
		{
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_CollectCredentialsUrl);
			LoginPageUrlRule loginPageUrlRule = settingValue.StartsWith("<") ? settingValue.LoginPageUrlRuleFromXml() : null;
			bool flag = ((loginPageUrlRule != null) ? loginPageUrlRule.LoginUrls : null) == null || loginPageUrlRule.LoginUrls.Count < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.GotoHomePage();
			}
			foreach (KeyValuePair<eWebPageTargetAudience, string> keyValuePair in loginPageUrlRule.LoginUrls)
			{
				HyperLink hyperLink = new HyperLink();
				hyperLink.ID = "link_" + keyValuePair.Key.ToString();
				hyperLink.NavigateUrl = keyValuePair.Value;
				WebPageTargetAudienceAttribute attribute = keyValuePair.Key.GetAttribute<WebPageTargetAudienceAttribute>();
				hyperLink.Text = (((attribute != null) ? attribute.Title : null) ?? keyValuePair.Key.ToString()) + " Login";
				HyperLink hyperLink2 = hyperLink;
				hyperLink2.Attributes.Add("onclick", string.Format("goLogin({0}); return true;", (int)keyValuePair.Key));
				this.p_links.Controls.Add(hyperLink2);
				this.p_links.Controls.Add(new Literal
				{
					Text = "<br /><br />"
				});
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002A318 File Offset: 0x00028518
		[WebMethod]
		public static void UserClickedLoginLink(int targetAudienceId)
		{
			eWebPageTargetAudience eWebPageTargetAudience = (eWebPageTargetAudience)(Enum.IsDefined(typeof(eWebPageTargetAudience), targetAudienceId) ? targetAudienceId : 0);
			HttpContext.Current.Session.Add("LoginAudience", eWebPageTargetAudience.ToString());
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400040A RID: 1034
		protected Panel p_links;
	}
}
