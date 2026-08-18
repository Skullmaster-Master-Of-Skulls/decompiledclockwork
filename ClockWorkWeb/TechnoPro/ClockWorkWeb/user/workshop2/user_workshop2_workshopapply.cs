using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000028 RID: 40
	public class user_workshop2_workshopapply : Page
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000068E8 File Offset: 0x00004AE8
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("workshops.aspx", true);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				db db = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
				int num;
				try
				{
					object obj = base.Request.QueryString["appid"];
					bool flag2 = obj != null;
					if (flag2)
					{
						num = int.Parse((string)obj);
					}
					else
					{
						num = 0;
					}
				}
				catch
				{
					num = 0;
				}
				db.Da.SelectCommand.CommandText = "SELECT app.appointmentid,app.startdate,app.enddate,at.description,w.workshoptitle,w.workshopdescription FROM appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid LEFT JOIN appointmentworkshops aw ON aw.appointmentid=app.appointmentid LEFT JOIN workshops w ON w.workshopid=aw.workshopid WHERE app.appointmentid=" + num.ToString();
				DataTable dataTable = new DataTable();
				db.Da.Fill(dataTable);
				bool flag3 = dataTable.Rows.Count > 0;
				if (flag3)
				{
					DataRow dataRow = dataTable.Rows[0];
					this.lbl_title.Text = dataRow["workshoptitle"].ToString();
					DateTime dateTime = (DateTime)dataRow["startdate"];
					DateTime dateTime2 = (DateTime)dataRow["enddate"];
					this.lbl_datetime.Text = dateTime.ToString("MMM MMMM d, yyyy h:mm tt") + " to " + dateTime2.ToString("h:mm tt");
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006A4C File Offset: 0x00004C4C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006A70 File Offset: 0x00004C70
		private void Page_Init(object sender, EventArgs e)
		{
			db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				bool settingValueBool = AppSettingsV2.GetSettingValueBool(Setting.WORKSHOPS_allowNonClockWorkStudentsToRegister, conn, base.Cache);
				bool flag2 = settingValueBool;
				if (flag2)
				{
					base.Response.Redirect("NewUser.aspx", true);
				}
				else
				{
					base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
				}
			}
			int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.WORKSHOPS_ApplyFormNum, conn, base.Cache);
			bool flag3 = settingValueInt > 0;
			if (flag3)
			{
				string exemptCids = "";
				DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, conn, settingValueInt, this.p_data, null, false, false, exemptCids);
			}
			else
			{
				this.p_data.Visible = false;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006B3C File Offset: 0x00004D3C
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("thankyou.aspx", true);
		}

		// Token: 0x040000A9 RID: 169
		protected Label Label2;

		// Token: 0x040000AA RID: 170
		protected Panel p_info;

		// Token: 0x040000AB RID: 171
		protected Label lbl_title;

		// Token: 0x040000AC RID: 172
		protected Label Label1;

		// Token: 0x040000AD RID: 173
		protected Label lbl_datetime;

		// Token: 0x040000AE RID: 174
		protected Panel p_data;

		// Token: 0x040000AF RID: 175
		protected Button btn_submit;
	}
}
