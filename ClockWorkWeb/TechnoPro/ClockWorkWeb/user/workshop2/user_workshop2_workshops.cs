using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Databases;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x0200002C RID: 44
	public class user_workshop2_workshops : Page
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00007A10 File Offset: 0x00005C10
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.WORKSHOPS_WorkshopsListingPageInstructions);
				this.lbl_intro.Text = settingValue;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007A50 File Offset: 0x00005C50
		protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
		{
			bool flag = !(e.Item is GridDataItem);
			if (!flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				string text = gridDataItem["col_availableSpaces"].Text.Trim();
				int num = (text.Length > 0 && text.CompareTo("&nbsp;") != 0) ? int.Parse(text) : 0;
				TableCell tableCell = gridDataItem["col_action"];
				Control control = tableCell.FindControl("btn_book");
				Control control2 = tableCell.FindControl("btn_book2");
				bool flag2 = num > 0;
				bool flag3 = !flag2;
				if (flag3)
				{
					control.Visible = false;
					control2.Visible = false;
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007B10 File Offset: 0x00005D10
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007B34 File Offset: 0x00005D34
		private static List<int> IntListFromString(string commaSeparatedNumbers)
		{
			List<int> list = new List<int>();
			bool flag = commaSeparatedNumbers == null;
			List<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = commaSeparatedNumbers.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						int item;
						bool flag3 = int.TryParse(text2, out item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007BC0 File Offset: 0x00005DC0
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string text = webSettingsClientManager.GetSettingValue<string>(Setting.WORKSHOPS_PublishedWorkshops);
			bool flag = text.Length < 1;
			if (flag)
			{
				text = "0";
			}
			int num = this.LookupStudentPid();
			string value = "<span style='color: #666666; font-size: small;'>";
			bool flag2 = false;
			bool flag3 = num > 0;
			string query = "SELECT w.workshopid,w.workshoptitle\r\n    ,aw.location,aw.maxattendees\r\n    ,app.appointmentid,app.startdate,app.enddate\r\n    ,DATEDIFF(n,app.startdate,app.enddate) AS duration\r\n    ,app.apptypeid\r\n    ,at.description\r\n    ,@span + COALESCE(w.workshopdescription,'') + '</span>' AS workshopdescription\r\n    ,COUNT(att.attendeeid) AS NumAttendees,\r\n    CASE WHEN aw.maxattendees > 0 THEN (aw.maxattendees - COUNT(att.attendeeid) )\r\n    ELSE (SELECT NULL )\r\n    END\r\n    AS AvailableSpots,\r\n    CASE WHEN COUNT(att.attendeeid) - aw.maxattendees > 0 THEN '1'\r\n    ELSE '0'\r\n    END AS isavailable\r\n    ,@isfacilitator AS isfacilitator\r\n    ,coalesce(ab.appointmentid,0) AS alreadybookedappid\r\n    ,@isauthenticated AS isauthenticated\r\nFROM workshops w LEFT OUTER JOIN appointmentworkshops aw ON aw.workshopid=w.workshopid\r\n    LEFT JOIN appointments app ON app.appointmentid=aw.appointmentid LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n    LEFT JOIN attendees att ON att.appointmentid=app.appointmentid AND NOT att.misccode=1 AND NOT att.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n    LEFT JOIN (SELECT DISTINCT appointmentid FROM attendees WHERE personid=@pid) ab ON ab.appointmentid=app.appointmentid\r\nWHERE w.workshopid IN (SELECT orderid AS workshopid FROM splitorderids(@wids,','))\r\n    AND app.startdate > @cutoffdate\r\n    AND app.cancelled=0\r\nGROUP BY w.workshopid,w.workshoptitle,w.workshopdescription,aw.location,aw.maxattendees,\r\n    app.appointmentid,app.startdate,app.enddate,app.apptypeid,at.description,ab.appointmentid\r\nORDER BY w.workshoptitle,app.startdate";
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@wids", DbType.String, text),
				clockWork.GetParameter("@pid", DbType.Int32, num),
				clockWork.GetParameter("@span", DbType.String, value),
				clockWork.GetParameter("@isfacilitator", DbType.Boolean, flag2),
				clockWork.GetParameter("@cutoffdate", DbType.DateTime, DateTime.Now),
				clockWork.GetParameter("@isauthenticated", DbType.Boolean, flag3)
			};
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			List<user_workshop2_workshops.WorkshopItemWrapper> list = new List<user_workshop2_workshops.WorkshopItemWrapper>();
			bool flag4 = dataTable.Rows.Count > 0;
			if (flag4)
			{
				int num2 = 0;
				string b = "";
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num3 = (dataRow["workshopid"] is DBNull) ? 0 : ((int)dataRow["workshopid"]);
					string text2 = dataRow["workshoptitle"].ToString().Trim();
					string workshopDescription = dataRow["workshopdescription"].ToString().Trim();
					bool flag5 = num3 != num2 || text2 != b;
					if (flag5)
					{
						list.Add(new user_workshop2_workshops.WorkshopItemWrapper
						{
							IsNewWorkshopHeader = true,
							WorkshopTitle = text2,
							WorkshopDescription = workshopDescription
						});
						num2 = num3;
						b = text2;
					}
					user_workshop2_workshops.WorkshopItemWrapper item = new user_workshop2_workshops.WorkshopItemWrapper
					{
						StartDateTime = ((dataRow["startdate"] is DBNull) ? null : new DateTime?((DateTime)dataRow["startdate"])),
						EndDateTime = ((dataRow["enddate"] is DBNull) ? null : new DateTime?((DateTime)dataRow["enddate"])),
						WorkshopId = num3,
						WorkshopTitle = text2,
						WorkshopDescription = workshopDescription,
						AvailableSpaces = ((dataRow["AvailableSpots"] is DBNull) ? null : new int?((int)dataRow["AvailableSpots"])),
						Duration = ((dataRow["duration"] is DBNull) ? 0 : ((int)dataRow["duration"])),
						AppointmentId = ((dataRow["appointmentid"] is DBNull) ? null : new int?((int)dataRow["appointmentid"])),
						AlreadyBookedAppId = (int)dataRow["alreadybookedappid"],
						IsAuthenticated = flag3,
						IsFacilitator = flag2
					};
					list.Add(item);
				}
			}
			CutoffTime cutoffTime = webSettingsClientManager.GetSettingValue<string>(Setting.WORKSHOPS_CutoffTimeForStudentToBookWorkshop).CutoffTimeFromXml() ?? CutoffTime.None;
			bool flag6 = !cutoffTime.Enabled;
			if (flag6)
			{
				cutoffTime = new CutoffTime
				{
					Interval = eTimeInterval.Minutes,
					Amount = 1,
					Enabled = true
				};
			}
			list = list.Where(delegate(user_workshop2_workshops.WorkshopItemWrapper g)
			{
				bool flag7 = g.StartDateTime == null;
				bool result;
				if (flag7)
				{
					result = true;
				}
				else
				{
					bool? flag8 = cutoffTime.IsRightNowBeforeCutoffTime(g.StartDateTime.Value);
					result = (flag8 == null || flag8.Value);
				}
				return result;
			}).ToList<user_workshop2_workshops.WorkshopItemWrapper>();
			this.RadGrid1.DataSource = list;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007FE8 File Offset: 0x000061E8
		protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_title"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
			bool flag3 = e.Item is GridDataItem;
			if (flag3)
			{
				GridDataItem gridDataItem2 = (GridDataItem)e.Item;
				bool flag4 = gridDataItem2.DataItem != null && gridDataItem2.DataItem is user_workshop2_workshops.WorkshopItemWrapper;
				if (flag4)
				{
					user_workshop2_workshops.WorkshopItemWrapper workshopItemWrapper = (user_workshop2_workshops.WorkshopItemWrapper)gridDataItem2.DataItem;
					bool isNewWorkshopHeader = workshopItemWrapper.IsNewWorkshopHeader;
					if (isNewWorkshopHeader)
					{
						gridDataItem2["col_title"].CssClass = "GridHeaderRow";
						gridDataItem2["col_title"].ColumnSpan = 4;
						gridDataItem2["col_duration"].Visible = false;
						gridDataItem2["col_availableSpaces"].Visible = false;
						gridDataItem2["col_action"].Visible = false;
					}
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000810C File Offset: 0x0000630C
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			bool flag = e.CommandArgument != null;
			int parameter;
			if (flag)
			{
				string text = e.CommandArgument.ToString();
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					try
					{
						parameter = int.Parse(text);
					}
					catch
					{
						parameter = 0;
					}
				}
				else
				{
					parameter = 0;
				}
			}
			else
			{
				parameter = 0;
			}
			string str = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(parameter);
			bool flag3 = e.CommandName.CompareTo("book") == 0;
			if (flag3)
			{
				base.Response.Redirect("workshopbook.aspx?appid=" + str, true);
			}
			else
			{
				bool flag4 = e.CommandName.CompareTo("attendance") == 0;
				if (flag4)
				{
					base.Response.Redirect("workshopattendance.aspx?appid=" + str, true);
				}
				else
				{
					bool flag5 = e.CommandName.Equals("upcomingapps");
					if (flag5)
					{
						base.Response.Redirect("myupcomingappts.aspx?appid=" + str, true);
					}
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008210 File Offset: 0x00006410
		protected void RadGrid1_PreRender(object sender, EventArgs e)
		{
			foreach (object obj in this.RadGrid1.MasterTableView.Items)
			{
				GridDataItem gridDataItem = (GridDataItem)obj;
				bool flag = gridDataItem.KeyValues.Contains("workshoptitle");
				if (flag)
				{
					string text = gridDataItem["workshoptitle"].Text;
					gridDataItem["workshoptitle"].Attributes.Add("onClick ", "alert('hi');");
				}
			}
		}

		// Token: 0x040000CB RID: 203
		protected Panel p_title;

		// Token: 0x040000CC RID: 204
		protected Label lbl_title;

		// Token: 0x040000CD RID: 205
		protected Panel p_intro;

		// Token: 0x040000CE RID: 206
		protected Label lbl_intro;

		// Token: 0x040000CF RID: 207
		protected RadGrid RadGrid1;

		// Token: 0x020001A7 RID: 423
		internal class WorkshopItemWrapper
		{
			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0004D77E File Offset: 0x0004B97E
			// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0004D786 File Offset: 0x0004B986
			public int WorkshopId { get; set; }

			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0004D78F File Offset: 0x0004B98F
			// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0004D797 File Offset: 0x0004B997
			public string WorkshopTitle { get; set; }

			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0004D7A0 File Offset: 0x0004B9A0
			// (set) Token: 0x06000C0C RID: 3084 RVA: 0x0004D7A8 File Offset: 0x0004B9A8
			public string WorkshopDescription { get; set; }

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0004D7B1 File Offset: 0x0004B9B1
			// (set) Token: 0x06000C0E RID: 3086 RVA: 0x0004D7B9 File Offset: 0x0004B9B9
			public DateTime? StartDateTime { get; set; }

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0004D7C2 File Offset: 0x0004B9C2
			// (set) Token: 0x06000C10 RID: 3088 RVA: 0x0004D7CA File Offset: 0x0004B9CA
			public DateTime? EndDateTime { get; set; }

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0004D7D3 File Offset: 0x0004B9D3
			// (set) Token: 0x06000C12 RID: 3090 RVA: 0x0004D7DB File Offset: 0x0004B9DB
			public int? AvailableSpaces { get; set; }

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0004D7E4 File Offset: 0x0004B9E4
			// (set) Token: 0x06000C14 RID: 3092 RVA: 0x0004D7EC File Offset: 0x0004B9EC
			public bool IsAvailable { get; set; }

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0004D7F5 File Offset: 0x0004B9F5
			// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0004D7FD File Offset: 0x0004B9FD
			public int Duration { get; set; }

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0004D806 File Offset: 0x0004BA06
			// (set) Token: 0x06000C18 RID: 3096 RVA: 0x0004D80E File Offset: 0x0004BA0E
			public int? AppointmentId { get; set; }

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0004D817 File Offset: 0x0004BA17
			// (set) Token: 0x06000C1A RID: 3098 RVA: 0x0004D81F File Offset: 0x0004BA1F
			public bool IsAuthenticated { get; set; }

			// Token: 0x170002BC RID: 700
			// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0004D828 File Offset: 0x0004BA28
			// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0004D830 File Offset: 0x0004BA30
			public bool IsFacilitator { get; set; }

			// Token: 0x170002BD RID: 701
			// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0004D839 File Offset: 0x0004BA39
			// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0004D841 File Offset: 0x0004BA41
			public int AlreadyBookedAppId { get; set; }

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0004D84A File Offset: 0x0004BA4A
			// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0004D852 File Offset: 0x0004BA52
			public bool IsNewWorkshopHeader { get; set; }

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0004D85C File Offset: 0x0004BA5C
			public string WorkshopTitleAndDescription
			{
				get
				{
					string text = this.WorkshopTitle ?? "";
					string text2 = this.WorkshopDescription ?? "";
					bool flag = text2.Length > 0;
					string result;
					if (flag)
					{
						string text3 = "";
						bool flag2 = text.Length > 0;
						if (flag2)
						{
							text3 = string.Format("<div style='font-size: 1em; color: DarkGray; margin: 8px'>{0}</div>", text2);
							result = text + " " + text3;
						}
						else
						{
							result = text3;
						}
					}
					else
					{
						result = text;
					}
					return result;
				}
			}
		}
	}
}
