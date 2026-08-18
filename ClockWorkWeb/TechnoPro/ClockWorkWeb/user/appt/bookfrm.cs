using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.ClientManager.Core.Reports;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F1 RID: 241
	public class bookfrm : Page
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00035E38 File Offset: 0x00034038
		private static PreCalendarQuestionnaireOptions GetPreCalendarQuestionnaireOptions()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string s = (webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_PreCalendarQuestionnaire) ?? "").Trim();
			return s.GetPreCalendarQuestionnaireOptionsFromString();
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00035E70 File Offset: 0x00034070
		private void Page_Init(object sender, EventArgs e)
		{
			PreCalendarQuestionnaireOptions preCalendarQuestionnaireOptions = bookfrm.GetPreCalendarQuestionnaireOptions();
			bool flag = preCalendarQuestionnaireOptions != null && preCalendarQuestionnaireOptions.IsEnabled;
			bool flag2 = !flag;
			if (flag2)
			{
				base.Response.Redirect("book.aspx", true);
			}
			else
			{
				int screenNum = preCalendarQuestionnaireOptions.ScreenNum;
				string exemptCids = "";
				DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
				DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, exemptCids);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00035EDC File Offset: 0x000340DC
		private int GetWhoAmIPid
		{
			get
			{
				return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00035F00 File Offset: 0x00034100
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			int getWhoAmIPid = this.GetWhoAmIPid;
			PreCalendarQuestionnaireOptions preCalendarQuestionnaireOptions = bookfrm.GetPreCalendarQuestionnaireOptions();
			bool flag = preCalendarQuestionnaireOptions == null;
			if (!flag)
			{
				int reportId = preCalendarQuestionnaireOptions.ReportId;
				bool flag2 = reportId < 1;
				if (!flag2)
				{
					List<ReportParameterDTO> list = new List<ReportParameterDTO>();
					int screenNum = preCalendarQuestionnaireOptions.ScreenNum;
					Exception ex;
					DataTable dataTable = DynamicScreenLayout.SaveDynamicDataToDataTable(ScreenType.ScreenType_PerStudent, getWhoAmIPid, 0, screenNum, base.Cache, this.p_data, "", out ex);
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string text = dataRow["controlvaluetouse"].ToString().Trim();
						bool flag3 = text.Length < 1;
						if (!flag3)
						{
							int num = (int)dataRow["controlid"];
							string value = dataRow[text].ToString().Trim();
							list.Add(new ReportParameterDTO
							{
								Name = "cid" + num.ToString(),
								Value = value
							});
						}
					}
					IReportClientManager reportClientManager = new ReportClientManager();
					RunReportResultDTO runReportResultDTO = reportClientManager.ExecuteReport(reportId, eReportExecutedFromLocation.Web, list.ToArray());
					RunFunctionDataDTO primaryData = runReportResultDTO.PrimaryData;
					DataTable dataTable2 = ((primaryData != null) ? primaryData.Table : null) ?? new DataTable("t");
					IList<string> value2 = (from DataRow dr in dataTable2.Rows
					select dr[0].ToString().Trim()).ToList<string>();
					this.Session.Add("allowedChannels", value2);
					base.Response.Redirect("book.aspx", true);
				}
			}
		}

		// Token: 0x04000554 RID: 1364
		protected Panel p_data;

		// Token: 0x04000555 RID: 1365
		protected Button btn_submit;
	}
}
