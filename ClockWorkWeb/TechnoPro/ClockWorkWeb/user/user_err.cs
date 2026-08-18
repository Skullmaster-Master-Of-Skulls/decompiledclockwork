using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.ClockWorkWeb.user
{
	// Token: 0x02000019 RID: 25
	public class user_err : Page
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004138 File Offset: 0x00002338
		protected void Page_Load(object sender, EventArgs e)
		{
			this.Page.Title = "Error occurred";
			string text = base.Request["code"];
			int num;
			bool flag = string.IsNullOrEmpty(text) || !int.TryParse(text, out num);
			if (flag)
			{
				num = 0;
			}
			UserErrorCode userErrorCode = (UserErrorCode)(Enum.IsDefined(typeof(UserErrorCode), num) ? num : 0);
			bool flag2 = false;
			bool flag3 = userErrorCode > UserErrorCode.Unknown;
			if (flag3)
			{
				UserErrorCodeAttribute attribute = UserErrorCodeAttribute.GetAttribute(userErrorCode);
				bool flag4 = attribute != null && !string.IsNullOrEmpty(attribute.Url);
				if (flag4)
				{
					this.hlinkPreviousPage.NavigateUrl = attribute.Url;
					flag2 = true;
				}
			}
			bool flag5 = !flag2;
			if (flag5)
			{
				this.hlinkPreviousPage.NavigateUrl = "~/custom/misc/home.aspx";
				this.lblGoBack.Text = "";
				this.hlinkPreviousPage.Text = "Go to home page";
			}
		}

		// Token: 0x0400002E RID: 46
		protected Panel pnlError;

		// Token: 0x0400002F RID: 47
		protected Label lblError;

		// Token: 0x04000030 RID: 48
		protected Label lblGoBack;

		// Token: 0x04000031 RID: 49
		protected HyperLink hlinkPreviousPage;
	}
}
