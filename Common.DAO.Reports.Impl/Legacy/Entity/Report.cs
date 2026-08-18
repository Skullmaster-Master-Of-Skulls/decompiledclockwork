using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200001E RID: 30
	public class Report : CollectionBase
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000287A0 File Offset: 0x000269A0
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000287B8 File Offset: 0x000269B8
		public ReportType ReportType
		{
			get
			{
				return this.reportType;
			}
			set
			{
				this.reportType = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000287C4 File Offset: 0x000269C4
		public bool FunctionParametersAreEncrypted
		{
			get
			{
				return this.functionParametersAreEncrypted;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000287DC File Offset: 0x000269DC
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000287E4 File Offset: 0x000269E4
		public string ReportGroupTitle { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000287F0 File Offset: 0x000269F0
		public bool IsTechnoPro
		{
			get
			{
				return this.reportId >= 500000;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00028812 File Offset: 0x00026A12
		public void Start()
		{
			this.startTime = DateTime.Now;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00028820 File Offset: 0x00026A20
		public void End()
		{
			this.endTime = DateTime.Now;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00028830 File Offset: 0x00026A30
		public double GetRunningDurationInMinutes()
		{
			return (this.endTime - this.startTime).TotalMinutes;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0002885C File Offset: 0x00026A5C
		public string GetRunningDurationString()
		{
			return (this.endTime - this.startTime).ToString();
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00028890 File Offset: 0x00026A90
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000288A8 File Offset: 0x00026AA8
		public string ReportTitle
		{
			get
			{
				return this.reportTitle;
			}
			set
			{
				this.reportTitle = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000288B4 File Offset: 0x00026AB4
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000288CC File Offset: 0x00026ACC
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000288D6 File Offset: 0x00026AD6
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000288DE File Offset: 0x00026ADE
		public string ReportDescription { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000288E7 File Offset: 0x00026AE7
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000288EF File Offset: 0x00026AEF
		public string ParentGroupTitle { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000288F8 File Offset: 0x00026AF8
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00028910 File Offset: 0x00026B10
		public int ReportGroupId
		{
			get
			{
				return this.reportGroupId;
			}
			set
			{
				this.reportGroupId = value;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0002891C File Offset: 0x00026B1C
		public Report()
		{
			this.reportResults = new ReportResults();
			this.reportId = -1;
			this.reportTitle = "-";
			this.reportType = ReportType.Unknown;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00028984 File Offset: 0x00026B84
		public VariableCollection Variables
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0002899C File Offset: 0x00026B9C
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000289B4 File Offset: 0x00026BB4
		public int ReportId
		{
			get
			{
				return this.reportId;
			}
			set
			{
				this.reportId = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000289C0 File Offset: 0x00026BC0
		public int OverrideDynamicControlsScreenNum
		{
			get
			{
				return this.overrideDynamicControlsScreenNum;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000289D8 File Offset: 0x00026BD8
		public int AddResult(DataSet ds)
		{
			return this.reportResults.AddResult(ds);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000289F6 File Offset: 0x00026BF6
		public void RemoveAllBut(DataView dv)
		{
			this.reportResults.RemoveAllBut(dv);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00028A08 File Offset: 0x00026C08
		public ReportResults ReportResults
		{
			get
			{
				return this.reportResults;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00028A20 File Offset: 0x00026C20
		public Report(DataView dv)
		{
			this.reportResults = new ReportResults();
			this.AddResult(dv);
			this.reportId = -1;
			this.reportTitle = "-";
			this.reportType = ReportType.Unknown;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00028A90 File Offset: 0x00026C90
		public Report(DataRow reportDR)
		{
			this.reportResults = new ReportResults();
			this.reportId = (int)reportDR[0];
			this.reportTitle = reportDR[1].ToString().Trim();
			this.overrideDynamicControlsScreenNum = ((reportDR[12] == DBNull.Value) ? -1 : ((int)reportDR[12]));
			this.functionParametersAreEncrypted = (reportDR["searchchartinfoid"] != DBNull.Value && (int)reportDR["searchchartinfoid"] == 999);
			try
			{
				bool flag = reportDR.Table.Columns.Count < 14;
				if (flag)
				{
					this.reportType = ReportType.Custom;
				}
				else
				{
					int num = (reportDR[13] is DBNull) ? 1 : ((int)reportDR[13]);
					bool flag2 = num == 0;
					if (flag2)
					{
						this.reportType = ReportType.Unknown;
					}
					else
					{
						bool flag3 = num == 1;
						if (flag3)
						{
							this.reportType = ReportType.Custom;
						}
						else
						{
							this.reportType = ReportType.Unknown;
						}
					}
				}
			}
			catch
			{
				this.reportType = ReportType.Unknown;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00028BEC File Offset: 0x00026DEC
		public int Add(ReportStep reportStep)
		{
			return base.List.Add(reportStep);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00028C0C File Offset: 0x00026E0C
		public DataView GetCurrentDataView()
		{
			return this.reportResults.GetCurrentDataView();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00028C2C File Offset: 0x00026E2C
		public DataTable[] GetTablesExceptCurrent()
		{
			return this.reportResults.GetTablesExceptCurrent();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00028C4C File Offset: 0x00026E4C
		public DataView GetDataView(string tableName)
		{
			return this.reportResults.GetDataView(tableName);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00028C6A File Offset: 0x00026E6A
		public void MakeATableTheCurrentTable(string tableName)
		{
			this.reportResults.MakeATableTheCurrentTable(tableName);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00028C7A File Offset: 0x00026E7A
		public void ReplaceDataView(DataView dvToReplace, DataView dvToKeep)
		{
			this.reportResults.ReplaceDataView(dvToReplace, dvToKeep);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00028C8B File Offset: 0x00026E8B
		public void LogError(string msg, Exception ex)
		{
			this.lastException = ex;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00028C98 File Offset: 0x00026E98
		public int AddResult(DataView dv)
		{
			return this.reportResults.AddResult(dv);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00028CB8 File Offset: 0x00026EB8
		public int AddResultNotPrimary(DataView dv, string name)
		{
			return this.reportResults.AddResultNotPrimary(dv, name);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00028CD8 File Offset: 0x00026ED8
		public int AddResultNotPrimary(DataView dv)
		{
			return this.reportResults.AddResultNotPrimary(dv);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public void MergeInReportResults(ReportResults rr)
		{
			this.reportResults.MergeInReportResults(rr);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00028D06 File Offset: 0x00026F06
		public void NameCurrentTable(string name)
		{
			this.reportResults.NameCurrentTable(name);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00028D18 File Offset: 0x00026F18
		public int AddVariable(string vname, object vdata)
		{
			bool flag = this.args == null;
			if (flag)
			{
				this.args = new VariableCollection();
			}
			return this.args.Add(vname, vdata);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00028D50 File Offset: 0x00026F50
		public VariableCollection RememberedVariables
		{
			get
			{
				return this.rememberedVariables;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00028D68 File Offset: 0x00026F68
		public VariableCollection GetRememberedVariables()
		{
			return this.rememberedVariables;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00028D80 File Offset: 0x00026F80
		public List<Variable> RememberedVariables2
		{
			get
			{
				bool flag = this.rememberedVariables2 == null;
				if (flag)
				{
					this.rememberedVariables2 = new List<Variable>(this.rememberedVariables.Count);
					foreach (object obj in this.rememberedVariables)
					{
						Variable item = (Variable)obj;
						this.rememberedVariables2.Add(item);
					}
				}
				return this.rememberedVariables2;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00028E14 File Offset: 0x00027014
		public void SetRememberedVariables2(ArrayList variables)
		{
			this.rememberedVariables2 = new List<Variable>(variables.Count);
			foreach (object obj in variables)
			{
				Variable item = (Variable)obj;
				this.rememberedVariables2.Add(item);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00028E84 File Offset: 0x00027084
		public void RememberVariables(ArrayList variables)
		{
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				this.rememberedVariables.Add(new Variable(variable.VariableName, variable.VariableValue));
			}
		}

		// Token: 0x040000D1 RID: 209
		private int reportId;

		// Token: 0x040000D2 RID: 210
		private string reportTitle;

		// Token: 0x040000D3 RID: 211
		private string reportDescription;

		// Token: 0x040000D4 RID: 212
		private ReportType reportType;

		// Token: 0x040000D5 RID: 213
		private int overrideDynamicControlsScreenNum;

		// Token: 0x040000D6 RID: 214
		private ReportResults reportResults;

		// Token: 0x040000D7 RID: 215
		private Exception lastException = null;

		// Token: 0x040000D8 RID: 216
		private VariableCollection args = null;

		// Token: 0x040000D9 RID: 217
		private int orderNum = 0;

		// Token: 0x040000DA RID: 218
		private bool functionParametersAreEncrypted;

		// Token: 0x040000DB RID: 219
		private DateTime startTime;

		// Token: 0x040000DC RID: 220
		private DateTime endTime;

		// Token: 0x040000E0 RID: 224
		private int reportGroupId = 0;

		// Token: 0x040000E1 RID: 225
		private VariableCollection rememberedVariables = new VariableCollection();

		// Token: 0x040000E2 RID: 226
		private List<Variable> rememberedVariables2 = null;
	}
}
