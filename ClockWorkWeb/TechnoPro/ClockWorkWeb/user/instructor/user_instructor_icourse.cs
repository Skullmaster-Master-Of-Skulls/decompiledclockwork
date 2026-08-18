using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D7 RID: 215
	public class user_instructor_icourse : Page
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x000316E0 File Offset: 0x0002F8E0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00031704 File Offset: 0x0002F904
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed");
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					DateTime dateTime;
					DateTime dateTime2;
					DataTable dataTable = Course.LoadInstructorsCourses(pid, out dateTime, out dateTime2);
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						ListItem item = new ListItem(string.Format("{0} {1} {2} {3}", new object[]
						{
							dataRow["subject"].ToString(),
							dataRow["course"].ToString(),
							dataRow["section"].ToString(),
							dataRow["timeofday"]
						}), dataRow["lucourseid"].ToString());
						this.rl_courses.Items.Add(item);
					}
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00031840 File Offset: 0x0002FA40
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
			ListItem selectedItem = this.rl_courses.SelectedItem;
			bool flag = selectedItem != null;
			if (flag)
			{
				int num = int.Parse(selectedItem.Value);
				base.Response.Redirect("iletter.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num), true);
			}
		}

		// Token: 0x040004E2 RID: 1250
		protected Panel p_top;

		// Token: 0x040004E3 RID: 1251
		protected Label lbl_title;

		// Token: 0x040004E4 RID: 1252
		protected Label p_info;

		// Token: 0x040004E5 RID: 1253
		protected Panel p_list;

		// Token: 0x040004E6 RID: 1254
		protected RadioButtonList rl_courses;

		// Token: 0x040004E7 RID: 1255
		protected Button btn_submit;
	}
}
