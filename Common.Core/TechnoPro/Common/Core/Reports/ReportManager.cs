using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.FileSign.Impl;
using TechnoPro.Common.DAO.Reports;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports.Serialization;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.Reports
{
	// Token: 0x0200005A RID: 90
	public class ReportManager : IReportManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00012601 File Offset: 0x00010801
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00012609 File Offset: 0x00010809
		public IReportDAO dao { get; set; }

		// Token: 0x060003A5 RID: 933 RVA: 0x00012612 File Offset: 0x00010812
		public ReportManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00012631 File Offset: 0x00010831
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00012639 File Offset: 0x00010839
		public OperationContext OpContext { get; set; }

		// Token: 0x060003A8 RID: 936 RVA: 0x00012644 File Offset: 0x00010844
		public RunFunctionResultWithData RunFunction(RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			bool flag = CurrentWholeReportResult == null || CurrentWholeReportResult.Report == null || CurrentWholeReportResult.Report.Functions == null;
			RunFunctionResultWithData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eFunctionType functionCode = function.FunctionCode;
				RunFunctionResultWithData runFunctionResultWithData = new RunFunctionResultWithData
				{
					Result = new RunFunctionResult
					{
						Function = function,
						Status = new RunStatus
						{
							LastStatusStep = eRunStatusStep.Started
						}
					},
					Data = new RunFunctionData
					{
						AddToAdditionalData = false,
						IsPrimary = true
					}
				};
				try
				{
					ReportFunctionTypeAttribute attribute = functionCode.GetAttribute<ReportFunctionTypeAttribute>();
					bool flag2 = attribute == null;
					string str;
					if (flag2)
					{
						str = "";
						string text = functionCode.ToString().Replace("_", " ");
					}
					else
					{
						str = attribute.ExecutionClass;
						string text2 = attribute.Title ?? functionCode.ToString().Replace("_", " ");
					}
					string typeName = "TechnoPro.Common.Core.Reports.ReportFunctionExecutions." + str;
					Type type = Type.GetType(typeName);
					IReportFunctionExecute reportFunctionExecute = (IReportFunctionExecute)Activator.CreateInstance(type, new object[]
					{
						this.OpContext
					});
					reportFunctionExecute.OpContext = this.OpContext;
					reportFunctionExecute.ExecuteReportFunction(ref runFunctionResultWithData, CurrentWholeReportResult, function);
					bool flag3;
					if (runFunctionResultWithData == null)
					{
						flag3 = (null != null);
					}
					else
					{
						RunFunctionData data = runFunctionResultWithData.Data;
						flag3 = (((data != null) ? data.Table : null) != null);
					}
					bool flag4 = flag3 && string.IsNullOrEmpty(runFunctionResultWithData.Data.Table.TableName);
					if (flag4)
					{
						runFunctionResultWithData.Data.Table.TableName = "t";
					}
					runFunctionResultWithData.Result.Status.LastStatusStep = eRunStatusStep.CompletedSuccessfully;
				}
				catch (Exception ex)
				{
					runFunctionResultWithData.Result.Status.ErrorMessage = "Step=" + functionCode.ToString() + "; error=" + ex.ToString();
					runFunctionResultWithData.Result.Status.LastStatusStep = eRunStatusStep.Failed;
					CWLogger.Logger.Error("RunFunction:{0}", runFunctionResultWithData.Result.Status.ErrorMessage);
				}
				result = runFunctionResultWithData;
			}
			return result;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00012880 File Offset: 0x00010A80
		public RunReportResult ExecuteReport2(int reportId, params ReportParameter[] parameters)
		{
			return this.ExecuteReport2(reportId, null, parameters);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001289C File Offset: 0x00010A9C
		public RunReportResult ExecuteReport2(int reportId, IList<eFunctionType> FunctionTypesToSkip, params ReportParameter[] parameters)
		{
			return this.ExecuteReport2(reportId, null, FunctionTypesToSkip, parameters);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000128B8 File Offset: 0x00010AB8
		public RunReportResult ExecuteReport2(int reportId, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, params ReportParameter[] parameters)
		{
			Report report = this.LoadReport(reportId);
			RunReportResult runReportResult = this.ExecuteReport2(report, OnlyRunFunctionIds, FunctionTypesToSkip, parameters);
			bool flag = report == null;
			if (flag)
			{
				runReportResult.ReportStatus.ErrorMessage = "Couldn't load report#" + reportId.ToString();
			}
			return runReportResult;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012904 File Offset: 0x00010B04
		public int CreateReportGroup(ReportGroup Group)
		{
			return this.dao.CreateClientReportGroup(Group);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012924 File Offset: 0x00010B24
		public Report LoadReport(int ReportId)
		{
			ReportContext reportContext = new ReportContext
			{
				ReportIds = new List<int>()
			};
			Report report = this.dao.LoadClientReportById(ReportId);
			bool flag = report != null;
			Report result;
			if (flag)
			{
				result = report;
			}
			else
			{
				ReportCollection reportCollection = this.dao.LoadTproReports(reportContext);
				bool flag2 = reportCollection != null && reportCollection.Reports != null;
				if (flag2)
				{
					Report report2 = reportCollection.Reports.FirstOrDefault((Report g) => g.ReportId == ReportId);
					bool flag3 = report2 != null;
					if (flag3)
					{
						return report2;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000129C8 File Offset: 0x00010BC8
		public ReportCollection LoadReports(ReportContext ReportContext)
		{
			bool flag = ReportContext.ReportSource == eReportSource.Unknown || (ReportContext.ReportSource & eReportSource.Client) > eReportSource.Unknown;
			bool flag2 = ReportContext.ReportSource == eReportSource.Unknown || (ReportContext.ReportSource & eReportSource.TechnoPro) > eReportSource.Unknown;
			ReportCollection reportCollection = flag ? this.dao.LoadClientReports(ReportContext) : ReportCollection.Empty;
			ReportCollection reportCollection2 = flag2 ? this.dao.LoadTproReports(ReportContext) : ReportCollection.Empty;
			List<Report> list = reportCollection.Reports.ToList<Report>();
			List<ReportGroup> list2 = reportCollection.ReportGroups.ToList<ReportGroup>();
			list.AddRange(reportCollection2.Reports);
			list2.AddRange(reportCollection2.ReportGroups);
			reportCollection.Reports = list;
			reportCollection.ReportGroups = list2;
			return reportCollection;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012A84 File Offset: 0x00010C84
		public Forest<ReportGroup> LoadReportGroupForest(ReportContext ReportContext)
		{
			Forest<ReportOrGroup> forest = this.LoadReportForest(ReportContext);
			Forest<ReportGroup> forest2 = new Forest<ReportGroup>();
			this.ExtractGroupNodesOnly(null, forest.Nodes, forest2);
			return forest2;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012AB4 File Offset: 0x00010CB4
		private void ExtractGroupNodesOnly(TreeNode<ReportGroup> currDestParent, TreeNodeCollection<ReportOrGroup> sourceNodes, Forest<ReportGroup> destForest)
		{
			foreach (TreeNode<ReportOrGroup> treeNode in from g in sourceNodes
			where g.Value != null && g.Value.Group != null
			select g)
			{
				TreeNode<ReportGroup> currDestParent2 = destForest.AppendNode(currDestParent, treeNode.Value.Group);
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					this.ExtractGroupNodesOnly(currDestParent2, treeNode.Nodes, destForest);
				}
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012B54 File Offset: 0x00010D54
		public Forest<ReportOrGroup> LoadReportForest(ReportContext ReportContext)
		{
			bool flag = (ReportContext.ReportSource & eReportSource.Client) > eReportSource.Unknown;
			bool flag2 = (ReportContext.ReportSource & eReportSource.TechnoPro) > eReportSource.Unknown;
			ReportCollection reportCollection = flag ? this.dao.LoadClientReports(ReportContext) : ReportCollection.Empty;
			ReportCollection reportCollection2 = flag2 ? this.dao.LoadTproReports(ReportContext) : ReportCollection.Empty;
			List<Report> list = reportCollection.Reports.ToList<Report>();
			List<ReportGroup> list2 = reportCollection.ReportGroups.ToList<ReportGroup>();
			list.AddRange(reportCollection2.Reports);
			list2.AddRange(reportCollection2.ReportGroups);
			reportCollection.Reports = list;
			reportCollection.ReportGroups = list2;
			return reportCollection.BuildReportForest();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012BFC File Offset: 0x00010DFC
		public Forest<ReportOrGroup> LoadReportForestBySource(string ReportXml, ReportContext ReportContext)
		{
			ReportCollection reportCollection = this.dao.LoadReportsFromXml(ReportXml, ReportContext);
			return reportCollection.BuildReportForest();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012C24 File Offset: 0x00010E24
		public ReportCollection LoadReportsInAGroup(params string[] GroupTitles)
		{
			return this.dao.LoadReportsInAGroup(GroupTitles);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012C44 File Offset: 0x00010E44
		public ReportCollection LoadReportsInAGroup(params int[] GroupIds)
		{
			return this.dao.LoadReportsInAGroup(GroupIds);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00012C64 File Offset: 0x00010E64
		public RunReportResult ExecuteReport2(Report rpt, IList<eFunctionType> FunctionTypesToSkip, params ReportParameter[] parameters)
		{
			return this.ExecuteReport2(rpt, null, FunctionTypesToSkip, parameters);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00012C80 File Offset: 0x00010E80
		private void InsertAnotherReport(int reportId, List<ReportFunction> reportFunctions, ref List<ReportFunction> functions)
		{
			bool flag = reportFunctions.FirstOrDefault((ReportFunction g) => g.FunctionCode == eFunctionType.Run_Another_Report) == null;
			if (flag)
			{
				functions.AddRange(reportFunctions);
			}
			else
			{
				foreach (ReportFunction reportFunction in reportFunctions)
				{
					bool flag2 = reportFunction.FunctionCode == eFunctionType.Run_Another_Report;
					if (flag2)
					{
						string defaultFunctionParameter = reportFunction.GetDefaultFunctionParameter();
						int num;
						bool flag3 = int.TryParse(defaultFunctionParameter, out num) && num > 0 && num != reportId;
						if (flag3)
						{
							Report report = this.LoadReport(num);
							bool flag4 = report != null;
							if (flag4)
							{
								List<ReportFunction> reportFunctions2 = (from g in report.Functions
								where g.FunctionCode != eFunctionType.Parameters_Collection && g.FunctionCode != eFunctionType.Set_Variables
								select g).ToList<ReportFunction>();
								this.InsertAnotherReport(num, reportFunctions2, ref functions);
							}
						}
					}
					else
					{
						functions.Add(reportFunction);
					}
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012DAC File Offset: 0x00010FAC
		public RunReportResult ExecuteReport2(RunReportResult PreviousRunReportResult, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip)
		{
			Report report = PreviousRunReportResult.Report;
			ReportParameter[] parameters = PreviousRunReportResult.CurrentReportParameters.ToArray<ReportParameter>();
			return this.ExecuteReport2(report, OnlyRunFunctionIds, FunctionTypesToSkip, PreviousRunReportResult.PrimaryData, parameters);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00012DE4 File Offset: 0x00010FE4
		public RunReportResult ExecuteReport2(Report rpt, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, params ReportParameter[] parameters)
		{
			return this.ExecuteReport2(rpt, OnlyRunFunctionIds, FunctionTypesToSkip, null, parameters);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012E04 File Offset: 0x00011004
		public ReportExecutionPlan FigureOutExecutionPlan(IList<ReportFunction> functions)
		{
			bool flag = functions == null || functions.Count < 1;
			ReportExecutionPlan result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<ExecuteReportPlanItem> list = new List<ExecuteReportPlanItem>();
				int j;
				for (int i = 0; i < functions.Count; i = j)
				{
					ReportFunction reportFunction = functions[i];
					for (j = i + 1; j < functions.Count; j++)
					{
						ReportFunction reportFunction2 = functions[i];
						bool flag2 = reportFunction2.ExecuteThisFunctionOnClientIfPossible != reportFunction.ExecuteThisFunctionOnClientIfPossible;
						if (flag2)
						{
							break;
						}
					}
					List<int> list2 = new List<int>();
					for (int k = i; k < j; k++)
					{
						list2.Add(functions[k].ReportFunctionId);
					}
					list.Add(new ExecuteReportPlanItem
					{
						HasCompleted = false,
						ReportFunctionIdsToRun = list2,
						RunOnClient = reportFunction.ExecuteThisFunctionOnClientIfPossible
					});
				}
				CWLogger.Logger.Trace("ReportManager:FigureOutExecutionPlan:Steps={0}", string.Join(", ", (from g in list
				select "RunOnClient=" + g.RunOnClient.ToString() + " functionIds=" + string.Join("/", (from m in g.ReportFunctionIdsToRun
				select m.ToString()).ToArray<string>())).ToArray<string>()));
				result = new ReportExecutionPlan
				{
					ExecutionSteps = list
				};
			}
			return result;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00012F50 File Offset: 0x00011150
		public IList<ReportFunction> GetReportFunctionsToRun(int ReportId, List<ReportFunction> ReportFunctions, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip)
		{
			bool flag = ReportFunctions == null || ReportFunctions.Count < 1;
			IList<ReportFunction> result;
			if (flag)
			{
				result = new List<ReportFunction>();
			}
			else
			{
				List<ReportFunction> list = new List<ReportFunction>(ReportFunctions);
				List<ReportFunction> list2 = new List<ReportFunction>();
				bool flag2 = FunctionTypesToSkip != null && FunctionTypesToSkip.Count > 0;
				if (flag2)
				{
					list2.AddRange(from function in list
					where FunctionTypesToSkip.Contains(function.FunctionCode)
					select function);
				}
				bool flag3 = OnlyRunFunctionIds != null && OnlyRunFunctionIds.Count > 0;
				if (flag3)
				{
					List<ReportFunction> list3 = (from f in list
					where !OnlyRunFunctionIds.Contains(f.ReportFunctionId)
					select f).ToList<ReportFunction>();
					bool flag4 = list3.Count > 0;
					if (flag4)
					{
						list2.AddRange(list3);
					}
				}
				foreach (ReportFunction item in list2)
				{
					list.Remove(item);
				}
				eFunctionType[] priorityFunctionTypes = new eFunctionType[]
				{
					eFunctionType.Parameters_Collection,
					eFunctionType.Set_Variables
				};
				List<ReportFunction> list4 = (from f in list
				where priorityFunctionTypes.Contains(f.FunctionCode)
				select f).ToList<ReportFunction>();
				list4.Sort(delegate(ReportFunction f1, ReportFunction f2)
				{
					int num = Array.IndexOf<eFunctionType>(priorityFunctionTypes, f1.FunctionCode);
					int value = Array.IndexOf<eFunctionType>(priorityFunctionTypes, f2.FunctionCode);
					return num.CompareTo(value);
				});
				List<ReportFunction> list5 = (from f in list
				where !priorityFunctionTypes.Contains(f.FunctionCode)
				select f).ToList<ReportFunction>();
				list5.Sort((ReportFunction f1, ReportFunction f2) => f1.OrderNum.CompareTo(f2.OrderNum));
				List<ReportFunction> list6 = list4;
				list6.AddRange(list5.ToArray());
				List<ReportFunction> list7 = new List<ReportFunction>();
				this.InsertAnotherReport(ReportId, list6, ref list7);
				result = list7;
			}
			return result;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00013124 File Offset: 0x00011324
		public RunReportResult ExecuteReport2(Report rpt, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, RunFunctionData DefaultPrimaryData, params ReportParameter[] parameters)
		{
			RunReportResult runReportResult = new RunReportResult
			{
				AdditionalData = new List<RunFunctionData>(),
				Started = DateTime.Now,
				ReportStatus = new RunStatus
				{
					LastStatusStep = eRunStatusStep.Started
				},
				FunctionResults = new List<RunFunctionResult>(),
				CurrentReportParameters = ((parameters == null) ? null : parameters.ToList<ReportParameter>()),
				PrimaryData = DefaultPrimaryData
			};
			bool flag = rpt != null;
			if (flag)
			{
				IList<ReportFunction> reportFunctionsToRun = this.GetReportFunctionsToRun(rpt.ReportId, rpt.Functions, OnlyRunFunctionIds, FunctionTypesToSkip);
				runReportResult.Report = rpt;
				bool flag2 = rpt.ReportParameters == null;
				if (flag2)
				{
					rpt.ReportParameters = new List<ReportParameter>();
				}
				bool flag3 = parameters != null;
				if (flag3)
				{
					foreach (ReportParameter item in parameters)
					{
						rpt.ReportParameters.Add(item);
					}
				}
				ReportParameter reportParameter = rpt.ReportParameters.FirstOrDefault((ReportParameter g) => g.Name == "primarydatatable" && g.Value != null && g.Value is DataTable);
				bool flag4 = reportParameter != null;
				if (flag4)
				{
					DataTable dataTable = (DataTable)reportParameter.Value;
					bool flag5 = dataTable != null && string.IsNullOrEmpty(dataTable.TableName);
					if (flag5)
					{
						dataTable.TableName = "t";
					}
					runReportResult.PrimaryData = new RunFunctionData
					{
						IsPrimary = true,
						Name = reportParameter.Name,
						Table = dataTable
					};
				}
				int num = 0;
				foreach (ReportFunction reportFunction in reportFunctionsToRun)
				{
					num++;
					bool flag6 = reportFunction != null;
					if (flag6)
					{
						RunFunctionResultWithData functionResult = this.RunFunction(runReportResult, reportFunction);
						bool flag7 = this.UpdateReportWithFunctionResult(functionResult, reportFunction, runReportResult, num);
						if (flag7)
						{
							break;
						}
					}
				}
				runReportResult.Ended = DateTime.Now;
				bool flag8 = runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.Started;
				if (flag8)
				{
					runReportResult.ReportStatus.LastStatusStep = eRunStatusStep.CompletedSuccessfully;
				}
			}
			else
			{
				runReportResult.Ended = DateTime.Now;
				runReportResult.ReportStatus.LastStatusStep = eRunStatusStep.FailedUnableToStart;
				runReportResult.ReportStatus.ErrorMessage = "Couldn't load report";
			}
			return runReportResult;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00013378 File Offset: 0x00011578
		public bool UpdateReportWithFunctionResult(RunFunctionResultWithData functionResult, ReportFunction function, RunReportResult result, int ctr)
		{
			bool flag = functionResult == null;
			if (flag)
			{
				functionResult = new RunFunctionResultWithData();
			}
			bool flag2 = functionResult.Result == null;
			if (flag2)
			{
				functionResult.Result = new RunFunctionResult
				{
					Function = function,
					Status = new RunStatus
					{
						ErrorMessage = "Unspecified error; result was null.",
						LastStatusStep = eRunStatusStep.Failed
					}
				};
			}
			bool flag3 = functionResult.Result.Status == null;
			if (flag3)
			{
				functionResult.Result.Status = new RunStatus
				{
					ErrorMessage = "Unspecified error 2; result was null.",
					LastStatusStep = eRunStatusStep.Failed
				};
			}
			bool flag4 = functionResult.ReportParametersOut != null;
			if (flag4)
			{
				using (IEnumerator<ReportParameter> enumerator = functionResult.ReportParametersOut.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReportParameter rp = enumerator.Current;
						ReportParameter reportParameter = result.CurrentReportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(rp.Name, StringComparison.OrdinalIgnoreCase));
						bool flag5 = reportParameter == null;
						if (flag5)
						{
							result.CurrentReportParameters.Add(new ReportParameter
							{
								Name = rp.Name,
								Value = rp.Value
							});
						}
						else
						{
							reportParameter.Value = rp.Value;
						}
					}
				}
			}
			result.FunctionResults.Add(functionResult.Result);
			bool flag6 = functionResult.Result.Status.LastStatusStep != eRunStatusStep.CompletedSuccessfully;
			bool result2;
			if (flag6)
			{
				result.ReportStatus.LastStatusStep = eRunStatusStep.Failed;
				result.ReportStatus.ErrorMessage = string.Format("Failed at step {0}.  Step error={1}", ctr.ToString(), string.IsNullOrEmpty(functionResult.Result.Status.ErrorMessage) ? "Un-specified error." : functionResult.Result.Status.ErrorMessage);
				result2 = true;
			}
			else
			{
				bool flag7 = functionResult.Data != null;
				if (flag7)
				{
					bool flag8 = string.IsNullOrEmpty(functionResult.Data.Name);
					if (flag8)
					{
						functionResult.Data.Name = "step" + ctr.ToString();
					}
					bool isPrimary = functionResult.Data.IsPrimary;
					if (isPrimary)
					{
						result.PrimaryData = functionResult.Data;
					}
					bool addToAdditionalData = functionResult.Data.AddToAdditionalData;
					if (addToAdditionalData)
					{
						result.AdditionalData.Add(functionResult.Data);
					}
				}
				result2 = false;
			}
			return result2;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013608 File Offset: 0x00011808
		public void UpdateReport(Report Report)
		{
			this.dao.UpdateClientReport(Report);
			this.MarkReportChange(Report.ReportId, this.OpContext.WhoAmI);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013630 File Offset: 0x00011830
		public void MarkReportChange(int ReportId, int WhoChangedPersonId)
		{
			try
			{
				Report reportAfterChange = this.LoadReport(ReportId);
				this.dao.MarkReportChange(reportAfterChange, WhoChangedPersonId);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Core.Reports.ReportManager.MarkReportChange:ex={0}", ex.ToString());
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00013684 File Offset: 0x00011884
		public int CreateReport(Report Report)
		{
			int num = this.dao.CreateClientReport(Report);
			bool flag = num > 0;
			if (flag)
			{
				this.MarkReportChange(num, (Report.WhoCreated != null && Report.WhoCreated.PersonId > 0) ? Report.WhoCreated.PersonId : this.OpContext.WhoAmI);
			}
			return num;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000136E2 File Offset: 0x000118E2
		public void DeleteReport(int ReportId)
		{
			this.dao.DeleteClientReport(ReportId);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000136F2 File Offset: 0x000118F2
		public void RecordReportExecution(ReportExecutionContext Context)
		{
			this.dao.RecordReportExecution(Context);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013702 File Offset: 0x00011902
		public void DeleteClientReportGroup(int ReportGroupId)
		{
			this.dao.DeleteClientReportGroup(ReportGroupId);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013714 File Offset: 0x00011914
		public string LoadReportTechnoProNote(int ReportId)
		{
			return this.dao.LoadReportTechnoProNote(ReportId);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013732 File Offset: 0x00011932
		public void SaveReportTechnoProNote(int ReportId, string Rtf)
		{
			this.dao.SaveReportTechnoProNote(ReportId, Rtf);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013744 File Offset: 0x00011944
		public IList<ReportCompileLineWarningOrError> TryToCompileCSharp(string Code, IList<string> Imports, out bool Successful)
		{
			return this.dao.TryToCompileCSharp(Code, Imports, out Successful);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013764 File Offset: 0x00011964
		public RunFunctionData ExecuteReportFunction(eFunctionType FunctionToExecute, IList<ReportParameter> FunctionParameters, RunFunctionData CurrentData)
		{
			ReportFunction function = new ReportFunction
			{
				FunctionCode = FunctionToExecute,
				FunctionParameters = FunctionParameters,
				Description = "",
				Title = ""
			};
			RunReportResult currentWholeReportResult = new RunReportResult
			{
				CurrentReportParameters = FunctionParameters,
				PrimaryData = CurrentData,
				Report = new Report
				{
					Functions = new List<ReportFunction>(),
					ReportParameters = new ReportParameter[0]
				}
			};
			RunFunctionResultWithData runFunctionResultWithData = this.RunFunction(currentWholeReportResult, function);
			return (runFunctionResultWithData == null) ? null : runFunctionResultWithData.Data;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000137FE File Offset: 0x000119FE
		public void UpdateClientReportBuiltByTpro(Report Report, byte[] BuiltByTproSignedAndEncrypted)
		{
			this.UpdateReport(Report);
			this.dao.UpdateBuiltByTpro(Report.ReportId, BuiltByTproSignedAndEncrypted);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001381C File Offset: 0x00011A1C
		public int CreateClientReportBuiltByTpro(Report Report, byte[] BuiltByTproSignedAndEncrypted)
		{
			int num = this.CreateReport(Report);
			bool flag = num < 1;
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				this.dao.UpdateBuiltByTpro(num, BuiltByTproSignedAndEncrypted);
				result = num;
			}
			return result;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013854 File Offset: 0x00011A54
		public bool ValidateClientReportBuiltByTproIsNotTamperedWith(int ReportId)
		{
			byte[] array = this.dao.LoadBuiltByTpro(ReportId);
			bool flag = array == null;
			bool result;
			if (flag)
			{
				CWLogger.Logger.Warn("ReportManager.ValidateClientReportBuiltByTpro:Can't load builtbytpro from searchinfo for reportid={0}", ReportId.ToString());
				result = false;
			}
			else
			{
				IFileSignDAO fileSignDAO = new FileSignDAO();
				byte[] array2 = fileSignDAO.DecryptAndVerify(array);
				bool flag2 = array2 == null;
				if (flag2)
				{
					CWLogger.Logger.Warn("ReportManager.ValidateClientReportBuiltByTpro:FailedToDecrypt");
					result = false;
				}
				else
				{
					string @string;
					try
					{
						@string = Encoding.UTF8.GetString(array2);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("ReportManager:ConvertingByteArrayToStringXml:ex={0}", ex.ToString());
						return false;
					}
					Report report = this.LoadReport(ReportId);
					result = report.AreBuiltByTprosEqual(@string);
				}
			}
			return result;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001391C File Offset: 0x00011B1C
		public bool RevertClientReportBuiltByTproToLastTproChange(int ReportId)
		{
			byte[] array = this.dao.LoadBuiltByTpro(ReportId);
			bool flag = array == null;
			bool result;
			if (flag)
			{
				CWLogger.Logger.Warn("ReportManager.RevertClientReportBuiltByTproToLastTproChange:Can't load builtbytpro from searchinfo for reportid={0}", ReportId.ToString());
				result = false;
			}
			else
			{
				IFileSignDAO fileSignDAO = new FileSignDAO();
				byte[] array2 = fileSignDAO.DecryptAndVerify(array);
				bool flag2 = array2 == null;
				if (flag2)
				{
					CWLogger.Logger.Warn("ReportManager.RevertClientReportBuiltByTproToLastTproChange:FailedToDecrypt");
					result = false;
				}
				else
				{
					string @string;
					try
					{
						@string = Encoding.UTF8.GetString(array2);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("ReportManager.RevertClientReportBuiltByTproToLastTproChange:ConvertingByteArrayToStringXml:ex={0}", ex.ToString());
						return false;
					}
					ReportCollection reportCollection = @string.ParseReportsFromNewXml(true);
					bool flag3 = reportCollection == null || reportCollection.Reports == null;
					if (flag3)
					{
						CWLogger.Logger.Error("ReportManager.RevertClientReportBuiltByTproToLastTproChange:Couldn't parse xml:xml={0}", @string ?? "NULL");
						result = false;
					}
					else
					{
						Report report = (reportCollection.Reports.Count > 0) ? reportCollection.Reports[0] : null;
						bool flag4 = report == null;
						if (flag4)
						{
							CWLogger.Logger.Error("ReportManager.RevertClientReportBuiltByTproToLastTproChange:Xml was parsed but reports collection is empty:xml={0}", @string ?? "NULL");
							result = false;
						}
						else
						{
							report.ReportId = ReportId;
							this.UpdateReport(report);
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00013A7C File Offset: 0x00011C7C
		public IDictionary<string, int> ImportReportFromXmlForUser(string Xml, int ParentGroupId = 0)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			ReportCollection reportCollection = Xml.ParseReportsFromNewXml(true);
			foreach (Report report in reportCollection.Reports)
			{
				report.Title = "Copy of " + (report.Title ?? "");
				string text = report.ReportUniqueId.ToString();
				bool flag = dictionary.ContainsKey(text);
				if (!flag)
				{
					Report report2 = this.dao.LoadReportByUniqueId(text);
					bool flag2 = report2 != null;
					if (flag2)
					{
						Guid reportUniqueId = Guid.NewGuid();
						string arg = text;
						text = reportUniqueId.ToString();
						report.ReportUniqueId = reportUniqueId;
						CWLogger.Logger.Warn("ReportManager.ImportReportFromXmlForUser:Report with same unique id already exists, generating a new unique id:guid={0}:newguid={1}", arg, text);
					}
					report.GroupId = ParentGroupId;
					report.BuiltByTproSignedAndEncryptedReportXml = null;
					report.IsBuiltByTpro = false;
					int num = this.CreateReport(report);
					report.ReportId = num;
					dictionary.Add(text, num);
					bool flag3 = num > 0 && report.BuiltByTproSignedAndEncryptedReportXml != null && report.BuiltByTproSignedAndEncryptedReportXml.Length != 0;
					if (flag3)
					{
						this.UpdateClientReportBuiltByTpro(report, report.BuiltByTproSignedAndEncryptedReportXml);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public IDictionary<string, int> ImportReportsFromXmlForUpdatingSystem(string Xml, int OverrideParentGroupId = 0)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			ReportCollection reportCollection = Xml.ParseReportsFromNewXml(true);
			foreach (Report report in reportCollection.Reports)
			{
				string text = report.ReportUniqueId.ToString();
				bool flag = dictionary.ContainsKey(text);
				if (!flag)
				{
					Report report2 = this.dao.LoadReportByUniqueId(text);
					bool flag2 = report2 != null;
					if (flag2)
					{
						dictionary.Add(text, 0);
						CWLogger.Logger.Debug("ReportManager.ImportReportsFromXmlForUpdatingSystem:Skipped importing report because already exists:guid={0}", text);
					}
					else
					{
						bool flag3 = OverrideParentGroupId > 0;
						if (flag3)
						{
							report.GroupId = OverrideParentGroupId;
						}
						int num = this.CreateReport(report);
						report.ReportId = num;
						dictionary.Add(text, num);
						bool flag4 = num > 0 && report.BuiltByTproSignedAndEncryptedReportXml != null && report.BuiltByTproSignedAndEncryptedReportXml.Length != 0;
						if (flag4)
						{
							this.UpdateClientReportBuiltByTpro(report, report.BuiltByTproSignedAndEncryptedReportXml);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00013D1C File Offset: 0x00011F1C
		public Report CreateReportClone(int ReportId)
		{
			Report report = this.LoadReport(ReportId);
			report.IsBuiltByTpro = false;
			report.BuiltByTproSignedAndEncryptedReportXml = null;
			report.CreatedByLocation = null;
			report.Title = "Clone of " + (report.Title ?? "");
			return report;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00013D70 File Offset: 0x00011F70
		public int CloneReport(int ReportId)
		{
			IDictionary<int, int> dictionary = this.CloneReports(new int[]
			{
				ReportId
			});
			return (dictionary == null || dictionary.Count < 1 || !dictionary.ContainsKey(ReportId)) ? 0 : dictionary[ReportId];
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00013DB4 File Offset: 0x00011FB4
		public IDictionary<int, int> CloneReports(params int[] ReportIds)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (int num in ReportIds)
			{
				Report report = this.CreateReportClone(num);
				int value = this.CreateReport(report);
				dictionary.Add(num, value);
			}
			return dictionary;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00013E04 File Offset: 0x00012004
		public string ExportReportToXmlForUser(params int[] ReportIds)
		{
			ReportCollection reportCollection = this.LoadReports(new ReportContext
			{
				ReportIds = ReportIds,
				ReportSource = eReportSource.All,
				ReturnReportDisplayInformationOnly = false
			});
			return this.ExportReportsToXmlForUser(reportCollection);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00013E44 File Offset: 0x00012044
		public string ExportReportsToXmlForUser(ReportCollection reportCollection)
		{
			bool flag = reportCollection.Reports == null;
			if (flag)
			{
				reportCollection.Reports = new List<Report>();
			}
			bool flag2 = reportCollection.ReportGroups == null;
			if (flag2)
			{
				reportCollection.ReportGroups = new List<ReportGroup>();
			}
			foreach (Report report in reportCollection.Reports)
			{
				report.IsBuiltByTpro = false;
				report.BuiltByTproSignedAndEncryptedReportXml = null;
			}
			this.RemoveGroupsOutOfContext(ref reportCollection);
			ReportCollectionForExport reportCollectionForExport = new ReportCollectionForExport
			{
				ReportCollection = reportCollection
			};
			return reportCollectionForExport.ConvertReportsToNewXml();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00013EF8 File Offset: 0x000120F8
		private void RemoveGroupsOutOfContext(ref ReportCollection reportCollection)
		{
			IList<Report> reports = reportCollection.Reports;
			IList<ReportGroup> reportGroups = reportCollection.ReportGroups;
			List<ReportGroup> list = (from g in reportGroups
			where reports.FirstOrDefault((Report h) => h.GroupId == g.GroupId) != null
			select g).ToList<ReportGroup>();
			List<int> list2 = new List<int>();
			foreach (ReportGroup reportGroup in list)
			{
				bool flag = reportGroup.ParentGroupId < 1;
				if (!flag)
				{
					IList<int> list3 = this.FindDependantParentGroupIds(reportGroups, reportGroup.ParentGroupId);
					using (IEnumerator<int> enumerator2 = list3.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int d = enumerator2.Current;
							bool flag2 = !list2.Contains(d) && list.FirstOrDefault((ReportGroup g) => g.GroupId == d) == null && reportGroups.FirstOrDefault((ReportGroup h) => h.GroupId == d) != null;
							if (flag2)
							{
								list2.Add(d);
							}
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator3 = list2.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					int dg = enumerator3.Current;
					bool flag3 = list.FirstOrDefault((ReportGroup g) => g.GroupId == dg) != null;
					if (!flag3)
					{
						ReportGroup reportGroup2 = reportGroups.FirstOrDefault((ReportGroup g) => g.GroupId == dg);
						bool flag4 = reportGroup2 != null;
						if (flag4)
						{
							list.Add(reportGroup2);
						}
					}
				}
			}
			reportCollection.ReportGroups = list;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000140D8 File Offset: 0x000122D8
		private IList<int> FindDependantParentGroupIds(IList<ReportGroup> groups, int firstParentGroupId)
		{
			IList<int> result = new List<int>();
			this.FindDependantParentGroupIds(ref result, groups, firstParentGroupId);
			return result;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000140FC File Offset: 0x000122FC
		private void FindDependantParentGroupIds(ref IList<int> gids, IList<ReportGroup> groups, int firstParentGroupId)
		{
			bool flag = firstParentGroupId < 1 || gids.Contains(firstParentGroupId);
			if (!flag)
			{
				gids.Add(firstParentGroupId);
				for (ReportGroup reportGroup = groups.FirstOrDefault((ReportGroup g) => g.GroupId == firstParentGroupId); reportGroup != null; reportGroup = groups.FirstOrDefault((ReportGroup g) => g.GroupId == pgid))
				{
					int pgid = reportGroup.ParentGroupId;
					bool flag2 = pgid < 1 || gids.Contains(pgid);
					if (flag2)
					{
						break;
					}
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000141AC File Offset: 0x000123AC
		public string ExportReportToXmlForUpdatingSystem(params int[] ReportIds)
		{
			ReportCollection reportCollection = this.LoadReports(new ReportContext
			{
				ReportIds = ReportIds,
				ReportSource = eReportSource.All,
				ReturnReportDisplayInformationOnly = false
			});
			foreach (Report report in reportCollection.Reports)
			{
				bool isBuiltByTpro = report.IsBuiltByTpro;
				if (isBuiltByTpro)
				{
					report.BuiltByTproSignedAndEncryptedReportXml = this.dao.LoadBuiltByTpro(report.ReportId);
				}
			}
			this.RemoveGroupsOutOfContext(ref reportCollection);
			ReportCollectionForExport reportCollectionForExport = new ReportCollectionForExport
			{
				ReportCollection = reportCollection
			};
			return reportCollectionForExport.ConvertReportsToNewXml();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014264 File Offset: 0x00012464
		private void UpdateReportOrderNum(int ReportId, int NewOrderNum)
		{
			this.dao.UpdateReportOrderNum(ReportId, NewOrderNum);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00014275 File Offset: 0x00012475
		private void UpdateReportGroupOrderNum(int ReportGroupId, int NewOrderNum)
		{
			this.dao.UpdateGroupOrderNum(ReportGroupId, NewOrderNum);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00014286 File Offset: 0x00012486
		private void UpdateReportGroup(int ReportId, int NewGroupId)
		{
			this.dao.UpdateReportGroup(ReportId, NewGroupId);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00014297 File Offset: 0x00012497
		private void UpdateGroupParent(int ReportGroupId, int NewGroupId)
		{
			this.dao.UpdateGroupParent(ReportGroupId, NewGroupId);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000142A8 File Offset: 0x000124A8
		public int ChangeReportOrderInSameReportGroup(int ReportIdToMove, int ReportIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			Report report = this.LoadReport(ReportIdToMove);
			bool flag = report == null;
			if (flag)
			{
				throw new InvalidParameterException("ReportManager:ChangeReportOrderInSameReportGroup:Can't find report by id:rid=" + ReportIdToMove.ToString());
			}
			bool isTechnoProReport = report.IsTechnoProReport;
			if (isTechnoProReport)
			{
				throw new InvalidParameterException("ReportManager:ChangeReportOrderInSameReportGroup:Can't change order for tpro report:rid=" + ReportIdToMove.ToString());
			}
			int groupId = report.GroupId;
			List<Report> list = (from g in this.LoadReportsInAGroup(new int[]
			{
				groupId
			}).Reports
			where g.ReportId != ReportIdToMove
			select g).ToList<Report>();
			bool flag2 = list.Count < 1;
			int orderNum;
			if (flag2)
			{
				orderNum = report.OrderNum;
			}
			else
			{
				int num = (ReportIdToMoveBeforeOrAfter == 0) ? ((list.Count > 0) ? 0 : -1) : list.FindIndex((Report g) => g.ReportId == ReportIdToMoveBeforeOrAfter);
				bool flag3 = num < 0;
				if (flag3)
				{
					throw new NullParameterException("ReportManager:ChangeReportOrderInSameReportGroup:Can't find report to move after id in reports list by group by id:report to move before or after id=" + ReportIdToMoveBeforeOrAfter.ToString());
				}
				if (moveAfter)
				{
					report.OrderNum = list[num].OrderNum + 1;
					this.UpdateReportOrderNum(report.ReportId, report.OrderNum);
					list.Insert(num + 1, report);
				}
				else
				{
					report.OrderNum = list[num].OrderNum;
					this.UpdateReportOrderNum(report.ReportId, report.OrderNum);
					list[num].OrderNum = report.OrderNum + 1;
					this.UpdateReportOrderNum(list[num].ReportId, list[num].OrderNum);
					list.Insert(num, report);
				}
				for (int i = 1; i < list.Count; i++)
				{
					bool flag4 = list[i].OrderNum > list[i - 1].OrderNum;
					if (!flag4)
					{
						list[i].OrderNum = list[i - 1].OrderNum + 1;
						this.UpdateReportOrderNum(list[i].ReportId, list[i].OrderNum);
					}
				}
				orderNum = report.OrderNum;
			}
			return orderNum;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000144FC File Offset: 0x000126FC
		private int MoveReportToLastReportInGroup(int ReportIdToMove)
		{
			Report report = this.LoadReport(ReportIdToMove);
			bool flag = report == null;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = this.MoveReportToLastReportInGroup(ReportIdToMove, report.GroupId);
			}
			return result;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014530 File Offset: 0x00012730
		private int MoveReportToLastReportInGroup(int ReportIdToMove, int ReportGroupId)
		{
			ReportCollection reportCollection = this.LoadReportsInAGroup(new int[]
			{
				ReportGroupId
			});
			int num;
			if (reportCollection.Reports.Count >= 1)
			{
				num = reportCollection.Reports.Max((Report g) => g.OrderNum);
			}
			else
			{
				num = 0;
			}
			int num2 = num;
			int num3 = num2 + 1;
			this.UpdateReportOrderNum(ReportIdToMove, num3);
			return num3;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000145A0 File Offset: 0x000127A0
		private int MoveGroupToLastGroupInGroup(int ReportGroupIdToMove, int ParentGroupId)
		{
			IList<ReportGroup> list = this.LoadGroupsInAGroup(ParentGroupId);
			int num;
			if (list.Count >= 1)
			{
				num = list.Max((ReportGroup g) => g.OrderNum);
			}
			else
			{
				num = 0;
			}
			int num2 = num;
			int num3 = num2 + 1;
			this.UpdateReportGroupOrderNum(ReportGroupIdToMove, num3);
			return num3;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000145FC File Offset: 0x000127FC
		private ReportGroup LoadReportGroup(int ReportGroupId)
		{
			ReportContext reportContext = new ReportContext
			{
				ReportIds = new List<int>()
			};
			ReportGroup reportGroup = this.dao.LoadClientReportGroupById(ReportGroupId);
			bool flag = reportGroup != null;
			ReportGroup result;
			if (flag)
			{
				result = reportGroup;
			}
			else
			{
				ReportCollection reportCollection = this.dao.LoadTproReports(reportContext);
				bool flag2 = reportCollection != null && reportCollection.ReportGroups != null;
				if (flag2)
				{
					ReportGroup reportGroup2 = reportCollection.ReportGroups.FirstOrDefault((ReportGroup g) => g.GroupId == ReportGroupId);
					bool flag3 = reportGroup2 != null;
					if (flag3)
					{
						return reportGroup2;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000146A0 File Offset: 0x000128A0
		public IList<ReportGroup> LoadGroupsInAGroup(int ReportGroupId)
		{
			return this.dao.LoadGroupsInAGroup(ReportGroupId);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000146C0 File Offset: 0x000128C0
		public int ChangeReportGroupOrderInSameReportGroup(int ReportGroupIdToMove, int ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			ReportGroup reportGroup = this.LoadReportGroup(ReportGroupIdToMove);
			bool flag = reportGroup == null;
			if (flag)
			{
				throw new InvalidParameterException("ReportManager:ChangeReportGroupOrderInSameReportGroup:Can't find group by id:gid=" + ReportGroupIdToMove.ToString());
			}
			bool isTechnoProGroup = reportGroup.IsTechnoProGroup;
			if (isTechnoProGroup)
			{
				throw new InvalidParameterException("ReportManager:ChangeReportGroupOrderInSameReportGroup:Can't change order for tpro group:gid=" + ReportGroupIdToMove.ToString());
			}
			int parentGroupId = reportGroup.ParentGroupId;
			List<ReportGroup> list = (from g in this.LoadGroupsInAGroup(parentGroupId)
			where g.GroupId != ReportGroupIdToMove
			select g).ToList<ReportGroup>();
			bool flag2 = list.Count < 1;
			int orderNum;
			if (flag2)
			{
				orderNum = reportGroup.OrderNum;
			}
			else
			{
				int num = list.FindIndex((ReportGroup g) => g.GroupId == ReportGroupIdToMoveBeforeOrAfter);
				bool flag3 = num < 0;
				if (flag3)
				{
					throw new NullParameterException("ReportManager:ChangeReportGroupOrderInSameReportGroup:Can't find group to move after id in reports list by group by id:group to move before or after id=" + ReportGroupIdToMoveBeforeOrAfter.ToString());
				}
				if (moveAfter)
				{
					reportGroup.OrderNum = list[num].OrderNum + 1;
					this.UpdateReportGroupOrderNum(reportGroup.GroupId, reportGroup.OrderNum);
					list.Insert(num + 1, reportGroup);
				}
				else
				{
					reportGroup.OrderNum = list[num].OrderNum;
					this.UpdateReportGroupOrderNum(reportGroup.GroupId, reportGroup.OrderNum);
					list[num].OrderNum = reportGroup.OrderNum + 1;
					this.UpdateReportGroupOrderNum(list[num].GroupId, list[num].OrderNum);
					list.Insert(num, reportGroup);
				}
				for (int i = 1; i < list.Count; i++)
				{
					bool flag4 = list[i].OrderNum > list[i - 1].OrderNum;
					if (!flag4)
					{
						list[i].OrderNum = list[i - 1].OrderNum + 1;
						this.UpdateReportGroupOrderNum(list[i].GroupId, list[i].OrderNum);
					}
				}
				orderNum = reportGroup.OrderNum;
			}
			return orderNum;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000148F0 File Offset: 0x00012AF0
		public Report MoveReport(int ReportIdToMove, int NewReportParentGroupId, int? ReportIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			this.UpdateReportGroup(ReportIdToMove, NewReportParentGroupId);
			bool flag = ReportIdToMoveBeforeOrAfter == null;
			Report result;
			if (flag)
			{
				this.MoveReportToLastReportInGroup(ReportIdToMove, NewReportParentGroupId);
				result = this.LoadReport(ReportIdToMove);
			}
			else
			{
				this.ChangeReportOrderInSameReportGroup(ReportIdToMove, ReportIdToMoveBeforeOrAfter.Value, moveAfter);
				result = this.LoadReport(ReportIdToMove);
			}
			return result;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00014948 File Offset: 0x00012B48
		public ReportGroup MoveGroup(int ReportGroupIdToMove, int NewReportParentGroupId, int? ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			this.UpdateGroupParent(ReportGroupIdToMove, NewReportParentGroupId);
			bool flag = ReportGroupIdToMoveBeforeOrAfter == null;
			ReportGroup result;
			if (flag)
			{
				this.MoveGroupToLastGroupInGroup(ReportGroupIdToMove, NewReportParentGroupId);
				result = this.LoadReportGroup(ReportGroupIdToMove);
			}
			else
			{
				this.ChangeReportGroupOrderInSameReportGroup(ReportGroupIdToMove, ReportGroupIdToMoveBeforeOrAfter.Value, moveAfter);
				result = this.LoadReportGroup(ReportGroupIdToMove);
			}
			return result;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000149A0 File Offset: 0x00012BA0
		public void SortReportGroupMembersAlphabetically(int ParentReportGroupId)
		{
			ReportCollection reportCollection = this.LoadReportsInAGroup(new int[]
			{
				ParentReportGroupId
			});
			List<Report> list = reportCollection.Reports.ToList<Report>();
			list.Sort((Report g1, Report g2) => (g1.Title ?? "").CompareTo(g2.Title ?? ""));
			int num = 0;
			foreach (Report report in list)
			{
				report.OrderNum = num;
				num += 10;
				this.dao.UpdateReportOrderNum(report.ReportId, report.OrderNum);
			}
			List<ReportGroup> list2 = this.LoadGroupsInAGroup(ParentReportGroupId).ToList<ReportGroup>();
			list2.Sort((ReportGroup g1, ReportGroup g2) => (g1.Title ?? "").CompareTo(g2.Title ?? ""));
			num = 0;
			foreach (ReportGroup reportGroup in list2)
			{
				reportGroup.OrderNum = num;
				num += 10;
				this.dao.UpdateGroupOrderNum(reportGroup.GroupId, reportGroup.OrderNum);
			}
		}
	}
}
