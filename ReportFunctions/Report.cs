using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000021 RID: 33
	public class Report : CollectionBase
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000390BC File Offset: 0x000380BC
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000390D4 File Offset: 0x000380D4
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000390E0 File Offset: 0x000380E0
		public bool FunctionParametersAreEncrypted
		{
			get
			{
				return this.functionParametersAreEncrypted;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000390F8 File Offset: 0x000380F8
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0003910F File Offset: 0x0003810F
		public string ReportGroupTitle { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00039118 File Offset: 0x00038118
		public bool IsTechnoPro
		{
			get
			{
				return this.reportId >= 500000;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0003913A File Offset: 0x0003813A
		public void Start()
		{
			this.startTime = DateTime.Now;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00039148 File Offset: 0x00038148
		public void End()
		{
			this.endTime = DateTime.Now;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00039158 File Offset: 0x00038158
		public double GetRunningDurationInMinutes()
		{
			return (this.endTime - this.startTime).TotalMinutes;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00039184 File Offset: 0x00038184
		public string GetRunningDurationString()
		{
			return (this.endTime - this.startTime).ToString();
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000391B8 File Offset: 0x000381B8
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000391D0 File Offset: 0x000381D0
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000391DC File Offset: 0x000381DC
		// (set) Token: 0x0600026F RID: 623 RVA: 0x000391F4 File Offset: 0x000381F4
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00039200 File Offset: 0x00038200
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00039217 File Offset: 0x00038217
		public string ReportDescription { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00039220 File Offset: 0x00038220
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00039237 File Offset: 0x00038237
		public string ParentGroupTitle { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00039240 File Offset: 0x00038240
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00039258 File Offset: 0x00038258
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

		// Token: 0x06000276 RID: 630 RVA: 0x00039264 File Offset: 0x00038264
		public Report()
		{
			this.reportResults = new ReportResults();
			this.reportId = -1;
			this.reportTitle = "-";
			this.reportType = ReportType.Unknown;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000392CC File Offset: 0x000382CC
		public VariableCollection Variables
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000392E4 File Offset: 0x000382E4
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000392FC File Offset: 0x000382FC
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00039308 File Offset: 0x00038308
		public int OverrideDynamicControlsScreenNum
		{
			get
			{
				return this.overrideDynamicControlsScreenNum;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00039320 File Offset: 0x00038320
		public int AddResult(DataSet ds)
		{
			return this.reportResults.AddResult(ds);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0003933E File Offset: 0x0003833E
		public void RemoveAllBut(DataView dv)
		{
			this.reportResults.RemoveAllBut(dv);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00039350 File Offset: 0x00038350
		public ReportResults ReportResults
		{
			get
			{
				return this.reportResults;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00039368 File Offset: 0x00038368
		public Report(DataView dv)
		{
			this.reportResults = new ReportResults();
			this.AddResult(dv);
			this.reportId = -1;
			this.reportTitle = "-";
			this.reportType = ReportType.Unknown;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000393D8 File Offset: 0x000383D8
		public Report(DataRow reportDR)
		{
			this.reportResults = new ReportResults();
			this.reportId = (int)reportDR[0];
			this.reportTitle = reportDR[1].ToString().Trim();
			this.overrideDynamicControlsScreenNum = ((reportDR[12] == DBNull.Value) ? -1 : ((int)reportDR[12]));
			this.functionParametersAreEncrypted = (reportDR["searchchartinfoid"] != DBNull.Value && (int)reportDR["searchchartinfoid"] == 999);
			try
			{
				if (reportDR.Table.Columns.Count < 14)
				{
					this.reportType = ReportType.Custom;
				}
				else
				{
					int num = (reportDR[13] is DBNull) ? 1 : ((int)reportDR[13]);
					if (num == 0)
					{
						this.reportType = ReportType.Unknown;
					}
					else if (num == 1)
					{
						this.reportType = ReportType.Custom;
					}
					else
					{
						this.reportType = ReportType.Unknown;
					}
				}
			}
			catch
			{
				this.reportType = ReportType.Unknown;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00039540 File Offset: 0x00038540
		public int Add(ReportStep reportStep)
		{
			return base.List.Add(reportStep);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00039560 File Offset: 0x00038560
		public DataView GetCurrentDataView()
		{
			return this.reportResults.GetCurrentDataView();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00039580 File Offset: 0x00038580
		public DataTable[] GetTablesExceptCurrent()
		{
			return this.reportResults.GetTablesExceptCurrent();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000395A0 File Offset: 0x000385A0
		public DataView GetDataView(string tableName)
		{
			return this.reportResults.GetDataView(tableName);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000395BE File Offset: 0x000385BE
		public void MakeATableTheCurrentTable(string tableName)
		{
			this.reportResults.MakeATableTheCurrentTable(tableName);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000395CE File Offset: 0x000385CE
		public void ReplaceDataView(DataView dvToReplace, DataView dvToKeep)
		{
			this.reportResults.ReplaceDataView(dvToReplace, dvToKeep);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000395DF File Offset: 0x000385DF
		public void LogError(string msg, Exception ex)
		{
			this.lastException = ex;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000395EC File Offset: 0x000385EC
		public int AddResult(DataView dv)
		{
			return this.reportResults.AddResult(dv);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0003960C File Offset: 0x0003860C
		public int AddResultNotPrimary(DataView dv, string name)
		{
			return this.reportResults.AddResultNotPrimary(dv, name);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0003962C File Offset: 0x0003862C
		public int AddResultNotPrimary(DataView dv)
		{
			return this.reportResults.AddResultNotPrimary(dv);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0003964A File Offset: 0x0003864A
		public void MergeInReportResults(ReportResults rr)
		{
			this.reportResults.MergeInReportResults(rr);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0003965A File Offset: 0x0003865A
		public void NameCurrentTable(string name)
		{
			this.reportResults.NameCurrentTable(name);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0003966C File Offset: 0x0003866C
		public int AddVariable(string vname, object vdata)
		{
			if (this.args == null)
			{
				this.args = new VariableCollection();
			}
			return this.args.Add(vname, vdata);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000396A8 File Offset: 0x000386A8
		public VariableCollection RememberedVariables
		{
			get
			{
				return this.rememberedVariables;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000396C0 File Offset: 0x000386C0
		public VariableCollection GetRememberedVariables()
		{
			return this.rememberedVariables;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000396D8 File Offset: 0x000386D8
		public List<Variable> RememberedVariables2
		{
			get
			{
				if (this.rememberedVariables2 == null)
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

		// Token: 0x06000290 RID: 656 RVA: 0x00039778 File Offset: 0x00038778
		public void SetRememberedVariables2(ArrayList variables)
		{
			this.rememberedVariables2 = new List<Variable>(variables.Count);
			foreach (object obj in variables)
			{
				Variable item = (Variable)obj;
				this.rememberedVariables2.Add(item);
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000397F0 File Offset: 0x000387F0
		public void RememberVariables(ArrayList variables)
		{
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				this.rememberedVariables.Add(new Variable(variable.VariableName, variable.VariableValue));
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00039868 File Offset: 0x00038868
		public void SetVariables(UnivDataAdapter da)
		{
			string[] array = new string[]
			{
				"true",
				"false"
			};
			this.args = new VariableCollection();
			for (int i = 0; i < da.SelectCommand.Parameters.Count; i++)
			{
				string text = da.SelectCommand.Parameters.ParameterName(i).Substring(1);
				if (text[0] == '@')
				{
					text = text.Substring(1);
				}
				string value = text.ToLower();
				if (Array.IndexOf<string>(array, value) < 0)
				{
					this.args.Add(text, da.SelectCommand.Parameters.Value(i));
				}
			}
		}

		// Token: 0x04000123 RID: 291
		private int reportId;

		// Token: 0x04000124 RID: 292
		private string reportTitle;

		// Token: 0x04000125 RID: 293
		private string reportDescription;

		// Token: 0x04000126 RID: 294
		private ReportType reportType;

		// Token: 0x04000127 RID: 295
		private int overrideDynamicControlsScreenNum;

		// Token: 0x04000128 RID: 296
		private ReportResults reportResults;

		// Token: 0x04000129 RID: 297
		private Exception lastException = null;

		// Token: 0x0400012A RID: 298
		private VariableCollection args = null;

		// Token: 0x0400012B RID: 299
		private int orderNum = 0;

		// Token: 0x0400012C RID: 300
		private bool functionParametersAreEncrypted;

		// Token: 0x0400012D RID: 301
		private DateTime startTime;

		// Token: 0x0400012E RID: 302
		private DateTime endTime;

		// Token: 0x0400012F RID: 303
		private int reportGroupId = 0;

		// Token: 0x04000130 RID: 304
		private VariableCollection rememberedVariables = new VariableCollection();

		// Token: 0x04000131 RID: 305
		private List<Variable> rememberedVariables2 = null;
	}
}
