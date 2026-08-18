using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000029 RID: 41
	public class user_workshop2_workshopattendance : Page
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00006B54 File Offset: 0x00004D54
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

		// Token: 0x060000EA RID: 234 RVA: 0x00006CB8 File Offset: 0x00004EB8
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006CDC File Offset: 0x00004EDC
		private void Page_Init(object sender, EventArgs e)
		{
			db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				bool settingValueBool = AppSettingsV2.GetSettingValueBool(Setting.APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister, conn, base.Cache);
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
			int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.WORKSHOPS_FacilitatorWorkshopFormNumber, conn, base.Cache);
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

		// Token: 0x060000EC RID: 236 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void btn_submit_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006DA8 File Offset: 0x00004FA8
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			db db = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			int num;
			try
			{
				object obj = base.Request.QueryString["appid"];
				bool flag = obj != null;
				if (flag)
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
			int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.GENERAL_EmailCid, db, base.Cache);
			bool settingValueBool = AppSettingsV2.GetSettingValueBool(Setting.GENERAL_EmailEncrypted, db, base.Cache);
			db.Da.SelectCommand.CommandText = "SELECT att.attendeeid,att.appointmentid,att.personid,p.firstname,p.lastname,p.student_no,att.noshow,oi.controlvalue AS email FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid LEFT JOIN people p ON p.personid=att.personid LEFT JOIN otherinfops oi ON oi.personid=att.personid AND oi.controlid=@cid WHERE att.appointmentid=@appid AND NOT att.misccode=1";
			db.Da.SelectCommand.Parameters.Clear();
			db.Da.SelectCommand.Parameters.Add("@appid", num);
			db.Da.SelectCommand.Parameters.Add("@cid", settingValueInt);
			DataTable dataTable = new DataTable();
			db.Da.Fill(dataTable);
			bool flag2 = settingValueBool;
			if (flag2)
			{
				dataTable = db.TripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no",
					"email"
				});
			}
			else
			{
				dataTable = db.TripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no"
				});
			}
			DataView dataView = new DataView(dataTable);
			dataView.Sort = "lastname,firstname,email";
			StringBuilder stringBuilder = new StringBuilder();
			bool flag3 = true;
			foreach (object obj2 in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				string text = (dataRow["email"] == DBNull.Value) ? "" : ((string)dataRow["email"]);
				bool flag4 = text.Length > 0;
				if (flag4)
				{
					bool flag5 = flag3;
					if (flag5)
					{
						flag3 = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(text);
				}
			}
			this.txt_emails.Text = stringBuilder.ToString();
			this.RadGrid1.DataSource = dataTable;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000701C File Offset: 0x0000521C
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			db db = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			bool flag = e.CommandName == "UpdateAll";
			if (flag)
			{
				try
				{
					object obj = base.Request.QueryString["appid"];
					bool flag2 = obj != null;
					if (flag2)
					{
						int num = int.Parse((string)obj);
					}
				}
				catch
				{
				}
				List<int> list = new List<int>();
				foreach (object obj2 in this.RadGrid1.EditItems)
				{
					GridEditableItem gridEditableItem = (GridEditableItem)obj2;
					int item = int.Parse(gridEditableItem["attendeeid"].Text);
					list.Add(item);
				}
				foreach (object obj3 in this.RadGrid1.Items)
				{
					GridEditableItem gridEditableItem2 = (GridEditableItem)obj3;
					db.Da.SelectCommand.CommandText = "UPDATE attendees SET noshow=@noshow WHERE attendeeid=@id";
					db.Da.SelectCommand.Parameters.Clear();
					int num2 = int.Parse(gridEditableItem2["attendeeid"].Text);
					db.Da.SelectCommand.Parameters.Add("@id", num2);
					db.Da.SelectCommand.Parameters.Add("@noshow", list.Contains(num2));
					db.Da.Fill(new DataTable());
					gridEditableItem2.Edit = false;
				}
			}
			this.RadGrid1.Rebind();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007228 File Offset: 0x00005428
		protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem && e.Item.IsInEditMode;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				gridDataItem["EditCommandColumn"].Controls[0].Visible = false;
			}
		}

		// Token: 0x040000B0 RID: 176
		protected Label Label2;

		// Token: 0x040000B1 RID: 177
		protected Panel p_info;

		// Token: 0x040000B2 RID: 178
		protected Label lbl_title;

		// Token: 0x040000B3 RID: 179
		protected Label Label1;

		// Token: 0x040000B4 RID: 180
		protected Label lbl_datetime;

		// Token: 0x040000B5 RID: 181
		protected Panel p_attendance;

		// Token: 0x040000B6 RID: 182
		protected RadGrid RadGrid1;

		// Token: 0x040000B7 RID: 183
		protected Panel p_emails;

		// Token: 0x040000B8 RID: 184
		protected Label lbl_emails;

		// Token: 0x040000B9 RID: 185
		protected TextBox txt_emails;

		// Token: 0x040000BA RID: 186
		protected Panel p_data;

		// Token: 0x040000BB RID: 187
		protected Button btn_submit;
	}
}
