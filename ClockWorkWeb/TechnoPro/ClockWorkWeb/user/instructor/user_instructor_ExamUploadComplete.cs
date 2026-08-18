using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D3 RID: 211
	public class user_instructor_ExamUploadComplete : Page
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x0002FB78 File Offset: 0x0002DD78
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
				int lucid = this.GetLucid();
				bool flag3 = lucid < 1;
				if (flag3)
				{
				}
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0002FBED File Offset: 0x0002DDED
		protected void btn_again_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("ExamUpload.aspx");
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0002FC01 File Offset: 0x0002DE01
		protected void btn_view_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("courses.aspx", true);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00004233 File Offset: 0x00002433
		protected void btn_logout_Click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002FC18 File Offset: 0x0002DE18
		protected void btn_tests_Click(object sender, EventArgs e)
		{
			int lucid = this.GetLucid();
			string urlParameterFromString = NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucid);
			base.Response.Redirect("UploadedExams.aspx?lucid=" + urlParameterFromString, true);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002FC54 File Offset: 0x0002DE54
		private int GetLucid()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
		}

		// Token: 0x040004AA RID: 1194
		protected Label lblTitle;

		// Token: 0x040004AB RID: 1195
		protected Label lbl_thankyou;

		// Token: 0x040004AC RID: 1196
		protected Button btn_again;

		// Token: 0x040004AD RID: 1197
		protected Button btn_view;

		// Token: 0x040004AE RID: 1198
		protected Button btn_tests;

		// Token: 0x040004AF RID: 1199
		protected Button btn_logout;
	}
}
