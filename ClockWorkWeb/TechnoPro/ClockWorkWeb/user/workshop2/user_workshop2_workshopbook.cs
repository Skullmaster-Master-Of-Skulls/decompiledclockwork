using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using ClockWorkWebAPIWeb;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.Modules;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x0200002A RID: 42
	public class user_workshop2_workshopbook : Page
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00007280 File Offset: 0x00005480
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				int appId = this.GetAppId();
				string query = "SELECT    app.appointmentid,app.startdate,app.enddate,at.description,w.workshoptitle,w.workshopdescription \r\nFROM        appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n            LEFT JOIN appointmentworkshops aw ON aw.appointmentid=app.appointmentid \r\n            LEFT JOIN workshops w ON w.workshopid=aw.workshopid WHERE app.appointmentid=@appid";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@appid", DbType.Int32, appId)
				});
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					DataRow dataRow = dataTable.Rows[0];
					this.p_info.GroupingText = dataRow["workshoptitle"].ToString();
					DateTime dateTime = (DateTime)dataRow["startdate"];
					DateTime dateTime2 = (DateTime)dataRow["enddate"];
					this.lbl_date.Text = dateTime.ToString("dddd MMMM d");
					this.lbl_time.Text = string.Concat(new string[]
					{
						"<b>",
						dateTime.ToString("h:mm tt"),
						"</b> to <b>",
						dateTime2.ToString("h:mm tt"),
						"</b>"
					});
					this.lbl_desc.Text = dataRow["workshopdescription"].ToString();
				}
				else
				{
					base.Response.Redirect("workshops.aspx", true);
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000073E4 File Offset: 0x000055E4
		private int GetAppId()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["appid"] ?? "");
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007420 File Offset: 0x00005620
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			int appId = this.GetAppId();
			int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			bool flag = studentPid <= 0;
			if (flag)
			{
				base.Response.Redirect("NewUser.aspx", true);
			}
			else
			{
				string query = "INSERT INTO attendees (appointmentid,personid) \r\n    SELECT @appid,@pid \r\n        WHERE NOT EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid)\r\n; SELECT attendeeid,appointmentid FROM attendees WHERE attendeeid=@@identity";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@appid", DbType.Int32, appId),
					clockWork.GetParameter("@pid", DbType.Int32, studentPid)
				});
				bool flag2 = dataTable.Rows.Count > 0;
				string str;
				if (flag2)
				{
					string arg = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString((dataTable.Rows[0][1] is DBNull) ? 0 : ((int)dataTable.Rows[0][1]));
					str = string.Format("?appid={0}{1}", arg, "&refresh=1");
				}
				else
				{
					str = "?refresh=1";
				}
				db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
				int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.WORKSHOPS_BookFormNumber, conn, base.Cache);
				bool flag3 = settingValueInt > 0;
				if (flag3)
				{
					DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerAppointment, studentPid, appId, settingValueInt, base.Cache, this.p_data, "");
				}
				StringDictionary stringDictionary = new StringDictionary();
				stringDictionary.Add("workshoptitle", this.p_info.GroupingText);
				stringDictionary.Add("workshopdescription", this.lbl_desc.Text);
				stringDictionary.Add("date", this.lbl_date.Text);
				stringDictionary.Add("time", this.lbl_time.Text);
				stringDictionary.Add("appointmentid", appId.ToString());
				stringDictionary.Add("personid", studentPid.ToString());
				IMailMergeCodes mailMergeCodes = new MailMergeCodes();
				stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Workshops));
				stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Workshops));
				IEmailClientManager emailClientManager = new EmailClientManager();
				MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
				{
					PersonId = studentPid,
					AppointmentId = appId
				};
				emailClientManager.SendEmail(Setting.WORKSHOPS_StudentEmailConfirmation, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "WorkshopBook");
				string key = "studentapps" + studentPid.ToString();
				bool flag4 = base.Cache[key] != null;
				if (flag4)
				{
					base.Cache.Remove(key);
				}
				base.Response.Redirect("myupcomingappts.aspx" + str);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000076CD File Offset: 0x000058CD
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("workshops.aspx");
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000076E4 File Offset: 0x000058E4
		private void Page_Init(object sender, EventArgs e)
		{
			db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			bool flag = studentPid <= 0;
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
			int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.WORKSHOPS_BookFormNumber, conn, base.Cache);
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

		// Token: 0x040000BC RID: 188
		protected Label lbl_pagetitle;

		// Token: 0x040000BD RID: 189
		protected Panel p_info;

		// Token: 0x040000BE RID: 190
		protected Label lbl_desc;

		// Token: 0x040000BF RID: 191
		protected Label lbl_date;

		// Token: 0x040000C0 RID: 192
		protected Label lbl_time;

		// Token: 0x040000C1 RID: 193
		protected Panel p_data;

		// Token: 0x040000C2 RID: 194
		protected Button btn_cancel;

		// Token: 0x040000C3 RID: 195
		protected Button btn_submit;
	}
}
