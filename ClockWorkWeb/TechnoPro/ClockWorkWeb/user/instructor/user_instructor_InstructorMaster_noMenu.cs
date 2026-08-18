using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI.AuthenticationAuthorization;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000DB RID: 219
	public class user_instructor_InstructorMaster_noMenu : MasterPage
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x00032824 File Offset: 0x00030A24
		private int GetIidIfExists()
		{
			object obj = base.Session["userinfo"];
			bool flag = obj != null;
			int result;
			if (flag)
			{
				UserInfo userInfo = (UserInfo)obj;
				result = userInfo.ClockworkIid;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00032864 File Offset: 0x00030A64
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.INSTRUCTOR);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int[] settingValue = new WebSettingsClientManager().GetSettingValue<int[]>(Setting.INSTRUCTOR_RestrictLoginTo);
				bool flag2 = settingValue != null && settingValue.Length != 0;
				if (flag2)
				{
					int iidIfExists = this.GetIidIfExists();
					bool flag3 = iidIfExists > 0;
					if (flag3)
					{
						bool flag4 = Array.IndexOf<int>(settingValue, iidIfExists) < 0;
						if (flag4)
						{
							NavigatorClientManager.CurrentInstance.NotAllowed(Setting.INSTRUCTOR_ErrorMessage_Pilot, this.Page);
						}
					}
				}
			}
		}

		// Token: 0x04000502 RID: 1282
		protected ContentPlaceHolder placeholder_content;
	}
}
