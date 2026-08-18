using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D4 RID: 212
	public class user_instructor_ExamUploadComplete2 : Page
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0002FC90 File Offset: 0x0002DE90
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
				string settingValueString = AppSettingsV2.GetSettingValueString(Setting.INSTRUCTOR_InstructorThankyouForSubmittingExamInfoMessage, conn, base.Cache);
				bool flag2 = !string.IsNullOrEmpty(settingValueString);
				if (flag2)
				{
					this.lbl_thankyou.Text = settingValueString;
				}
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0002FBED File Offset: 0x0002DDED
		protected void btn_again_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("ExamUpload.aspx");
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002FC01 File Offset: 0x0002DE01
		protected void btn_view_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("courses.aspx", true);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00004233 File Offset: 0x00002433
		protected void btn_logout_Click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x040004B0 RID: 1200
		protected Label lblTitle;

		// Token: 0x040004B1 RID: 1201
		protected Label lbl_thankyou;

		// Token: 0x040004B2 RID: 1202
		protected Button btn_again;

		// Token: 0x040004B3 RID: 1203
		protected Button btn_view;

		// Token: 0x040004B4 RID: 1204
		protected Button btn_logout;
	}
}
