using System;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000099 RID: 153
	public class user_NotetakingStudents_NotetakerInfo : Page
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00024420 File Offset: 0x00022620
		protected void Page_Load(object sender, EventArgs e)
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToSeeNotetakerContactInfoAndName);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("courses.aspx", true);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				int lucourseid = this.Lucourseid;
				int pid = this.GetPid();
				bool flag3 = lucourseid < 1 || pid < 1;
				if (flag3)
				{
					base.Response.Redirect("courses.aspx", true);
				}
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				string stringFromUrlParameter = NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@pid", DbType.Int32, pid),
					clockWork.GetParameter("@lucid", DbType.Int32, lucourseid)
				};
				DataTable dataTable = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_NotetakerStudentsCourses, parameters);
				bool flag4 = dataTable.Rows.Count > 0;
				if (flag4)
				{
					DataRow dataRow = dataTable.Rows[0];
					int serviceProviderId = (int)dataRow["serviceproviderid"];
					INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
					NotetakerBaseDTO notetakerBaseDTO = notetakingClientManager.LoadNotetakerBaseById(serviceProviderId);
					bool flag5 = notetakerBaseDTO != null;
					if (flag5)
					{
						this.lbl_notetakerName.Text = (notetakerBaseDTO.FirstName ?? "") + " " + (notetakerBaseDTO.LastName ?? "");
						string text = notetakerBaseDTO.Email ?? "";
						this.lbl_notetakerEmail.Text = ((text.Length > 0) ? string.Format("<a href='mailto:{0}'>{0}</a>", text) : "<i>email not available</i>");
					}
				}
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000245D0 File Offset: 0x000227D0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000245F4 File Offset: 0x000227F4
		private int Lucourseid
		{
			get
			{
				return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
			}
		}

		// Token: 0x040002D8 RID: 728
		protected ScriptManager bbb;

		// Token: 0x040002D9 RID: 729
		protected Panel p_Title;

		// Token: 0x040002DA RID: 730
		protected Label lblTitle;

		// Token: 0x040002DB RID: 731
		protected Label lbl_notetakerName;

		// Token: 0x040002DC RID: 732
		protected Label lbl_notetakerEmail;

		// Token: 0x040002DD RID: 733
		protected Button btn_goBack;
	}
}
