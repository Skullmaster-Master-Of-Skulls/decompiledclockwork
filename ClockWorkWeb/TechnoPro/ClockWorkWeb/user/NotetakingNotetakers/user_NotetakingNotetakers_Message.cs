using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A8 RID: 168
	public class user_NotetakingNotetakers_Message : Page
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0002610C File Offset: 0x0002430C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				object obj = base.Request.QueryString["msgcode"];
				bool flag2 = obj != null;
				if (flag2)
				{
					string text = obj.ToString();
					string a = text;
					if (!(a == "banned"))
					{
						if (a == "registrationIncomplete")
						{
							this.lbl_message.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_Message_RegistrationIncomplete);
						}
					}
				}
			}
		}

		// Token: 0x0400031D RID: 797
		protected ScriptManager bbb;

		// Token: 0x0400031E RID: 798
		protected Panel p_message;

		// Token: 0x0400031F RID: 799
		protected Label lbl_message;
	}
}
