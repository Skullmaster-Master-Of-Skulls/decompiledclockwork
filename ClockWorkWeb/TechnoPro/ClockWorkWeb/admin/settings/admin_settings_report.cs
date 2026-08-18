using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.ClientManager.Core.Reports;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.Public.Entities.Reports;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.admin.settings
{
	// Token: 0x02000191 RID: 401
	public class admin_settings_report : Page
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x0004CBA4 File Offset: 0x0004ADA4
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string text = base.Request.QueryString["rid"];
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					int.TryParse(text, out this.rid);
					bool flag3 = this.rid > 0;
					if (flag3)
					{
						object obj = this.Session["isadmin"];
						bool flag4 = obj != null && Convert.ToBoolean(obj);
						bool flag5 = !flag4;
						if (flag5)
						{
							this.rid = 0;
							this.ShowMessage("Invalid admin credentials.");
						}
					}
				}
				else
				{
					this.rid = 0;
				}
				string text2 = base.Request.QueryString["pp"];
				this.parameters = ((!string.IsNullOrEmpty(text2)) ? text2 : "");
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0004CC83 File Offset: 0x0004AE83
		private void ShowMessage(string msg)
		{
			this.p_msg.Visible = true;
			this.lbl_msg.Text = msg;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0004CCA0 File Offset: 0x0004AEA0
		protected void btn_runReport_Click(object sender, EventArgs e)
		{
			string text = this.txt_u.Text;
			string pass = this.txt_p.Text;
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
			string query = "SELECT personid,pass FROM userinfo \r\nWHERE username=@u \r\nAND personid IN (SELECT personid FROM peoplegroups WHERE groupid=10) \r\nAND NOT personid IN (SELECT personid FROM people WHERE isactive=0)";
			DbParameter[] array = new DbParameter[]
			{
				clockWork.GetParameter("@u", DbType.Binary, encryption.Encrypt(text.ToUpper()))
			};
			DataTable dataTable = clockWork.ExecuteQuery(query, array);
			dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"pass"
			});
			bool flag = (from DataRow dr in dataTable.Rows
			select dr["pass"].ToString()).Any((string p) => p.CompareTo(pass) == 0);
			bool flag2 = flag;
			if (flag2)
			{
				this.Session.Add("isadmin", true);
				base.Response.Redirect("report.aspx?rid=" + this.txt_reportid.Text + "&pp=" + this.txt_parameters.Text, true);
			}
			else
			{
				this.ShowMessage("FAILED credentials check.");
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0004CDD8 File Offset: 0x0004AFD8
		protected void RadGrid1_NeedDataSource(object sender, EventArgs e)
		{
			bool flag = this.rid > 0;
			if (flag)
			{
				string text = this.parameters;
				string[] source = text.Split(new char[]
				{
					'`'
				});
				IReportClientManager reportClientManager = new ReportClientManager();
				int ctr = 0;
				List<ReportParameterDTO> list = source.ToList<string>().ConvertAll<ReportParameterDTO>(delegate(string g)
				{
					int num = g.IndexOf('=');
					bool flag6 = num < 1;
					ReportParameterDTO result;
					if (flag6)
					{
						ReportParameterDTO reportParameterDTO = new ReportParameterDTO();
						object arg = "unknown";
						int ctr = ctr;
						ctr++;
						reportParameterDTO.Name = arg + ctr;
						reportParameterDTO.Value = g;
						result = reportParameterDTO;
					}
					else
					{
						result = new ReportParameterDTO
						{
							Name = g.Substring(0, num),
							Value = g.Substring(num + 1)
						};
					}
					return result;
				});
				RunReportResultDTO runReportResultDTO = reportClientManager.ExecuteReport(this.rid, eReportExecutedFromLocation.Web, list.ToArray());
				DataTable dataTable = (runReportResultDTO == null || runReportResultDTO.PrimaryData == null) ? null : runReportResultDTO.PrimaryData.Table;
				bool flag2 = dataTable != null;
				if (flag2)
				{
					this.RadGrid1.DataSource = dataTable;
				}
				else
				{
					this.RadGrid1.Visible = false;
				}
				bool flag3 = runReportResultDTO == null;
				if (flag3)
				{
					this.ShowMessage("Report result is null");
				}
				else
				{
					bool flag4 = runReportResultDTO.ReportStatus == null;
					if (flag4)
					{
						this.ShowMessage("Report status is null ");
					}
					else
					{
						bool flag5 = runReportResultDTO.ReportStatus.LastStatusStep != eRunStatusStepDTO.CompletedSuccessfully;
						if (flag5)
						{
							this.ShowMessage("Report failed: " + runReportResultDTO.ReportStatus.LastStatusStep.ToString() + "; err=" + (runReportResultDTO.ReportStatus.ErrorMessage ?? "NULL"));
						}
					}
				}
			}
			else
			{
				this.p_results.Visible = false;
			}
		}

		// Token: 0x040008C3 RID: 2243
		private int rid = 0;

		// Token: 0x040008C4 RID: 2244
		private string parameters = "";

		// Token: 0x040008C5 RID: 2245
		protected ScriptManager bbb;

		// Token: 0x040008C6 RID: 2246
		protected Panel p_msg;

		// Token: 0x040008C7 RID: 2247
		protected Label lbl_msg;

		// Token: 0x040008C8 RID: 2248
		protected Panel p_main;

		// Token: 0x040008C9 RID: 2249
		protected Label lbl_reportid;

		// Token: 0x040008CA RID: 2250
		protected TextBox txt_reportid;

		// Token: 0x040008CB RID: 2251
		protected Label lbl_parameters;

		// Token: 0x040008CC RID: 2252
		protected TextBox txt_parameters;

		// Token: 0x040008CD RID: 2253
		protected Table Table1;

		// Token: 0x040008CE RID: 2254
		protected TableHeaderRow TableHeaderRow1;

		// Token: 0x040008CF RID: 2255
		protected TableHeaderCell TableHeaderCell1;

		// Token: 0x040008D0 RID: 2256
		protected TableRow TableRow1;

		// Token: 0x040008D1 RID: 2257
		protected TableCell TableCell1;

		// Token: 0x040008D2 RID: 2258
		protected Label lbl_username;

		// Token: 0x040008D3 RID: 2259
		protected TableCell TableCell2;

		// Token: 0x040008D4 RID: 2260
		protected TextBox txt_u;

		// Token: 0x040008D5 RID: 2261
		protected TableRow TableRow2;

		// Token: 0x040008D6 RID: 2262
		protected TableCell TableCell3;

		// Token: 0x040008D7 RID: 2263
		protected Label lbl_pass;

		// Token: 0x040008D8 RID: 2264
		protected TableCell TableCell4;

		// Token: 0x040008D9 RID: 2265
		protected TextBox txt_p;

		// Token: 0x040008DA RID: 2266
		protected Button btn_runReport;

		// Token: 0x040008DB RID: 2267
		protected Panel p_results;

		// Token: 0x040008DC RID: 2268
		protected RadGrid RadGrid1;
	}
}
