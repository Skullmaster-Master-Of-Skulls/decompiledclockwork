using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Core.DynamicCompile;
using TechnoPro.Common.Core.Mappers.Reports;
using TechnoPro.Common.Core.Mappers.Reports.RunReportResults;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.DynamicCompile;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Exceptions;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200007C RID: 124
	public class ReportServiceManager : IReport, IService
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x000157F4 File Offset: 0x000139F4
		private ReportExecutionPlanDTO CreateExecutionPlan(Report report, IList<int> onlyRunFunctionIds, IList<eFunctionType> functionTypesToSkip, bool isRunningOnServer)
		{
			ReportServiceManager.<>c__DisplayClass0_0 CS$<>8__locals1 = new ReportServiceManager.<>c__DisplayClass0_0();
			bool flag = !isRunningOnServer;
			ReportExecutionPlanDTO result;
			if (flag)
			{
				ReportExecutionPlanDTO reportExecutionPlanDTO = new ReportExecutionPlanDTO();
				ReportExecutionPlanDTO reportExecutionPlanDTO2 = reportExecutionPlanDTO;
				ExecuteReportPlanItemDTO[] array = new ExecuteReportPlanItemDTO[1];
				int num = 0;
				ExecuteReportPlanItemDTO executeReportPlanItemDTO = new ExecuteReportPlanItemDTO();
				executeReportPlanItemDTO.ReportFunctionIdsToRun = (from g in report.Functions
				select g.ReportFunctionId).ToList<int>();
				executeReportPlanItemDTO.RunOnClient = true;
				executeReportPlanItemDTO.HasCompleted = false;
				array[num] = executeReportPlanItemDTO;
				reportExecutionPlanDTO2.ExecutionSteps = array;
				result = reportExecutionPlanDTO;
			}
			else
			{
				ReportExecutionPlanDTO reportExecutionPlanDTO3 = new ReportExecutionPlanDTO
				{
					ExecutionSteps = new List<ExecuteReportPlanItemDTO>()
				};
				ReportServiceManager.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
				IList<int> idsToRun = onlyRunFunctionIds;
				if (onlyRunFunctionIds == null)
				{
					idsToRun = (from g in report.Functions
					select g.ReportFunctionId).ToList<int>();
				}
				CS$<>8__locals2.idsToRun = idsToRun;
				CS$<>8__locals1.typesToSkip = (functionTypesToSkip ?? new List<eFunctionType>());
				List<ReportFunction> list = (from g in report.Functions
				where CS$<>8__locals1.idsToRun.Contains(g.ReportFunctionId) && !CS$<>8__locals1.typesToSkip.Contains(g.FunctionCode)
				select g).ToList<ReportFunction>();
				int j;
				for (int i = 0; i < list.Count; i = j)
				{
					ReportFunction reportFunction = list[i];
					bool flag2 = !reportFunction.ExecuteThisFunctionOnClientIfPossible;
					for (j = i + 1; j < list.Count; j++)
					{
						ReportFunction reportFunction2 = list[j];
						bool flag3 = !reportFunction2.ExecuteThisFunctionOnClientIfPossible;
						bool flag4 = flag3 != flag2;
						if (flag4)
						{
							break;
						}
					}
					ICollection<ExecuteReportPlanItemDTO> executionSteps = reportExecutionPlanDTO3.ExecutionSteps;
					ExecuteReportPlanItemDTO executeReportPlanItemDTO2 = new ExecuteReportPlanItemDTO();
					executeReportPlanItemDTO2.HasCompleted = false;
					executeReportPlanItemDTO2.ReportFunctionIdsToRun = (from g in list.GetRange(i, j - i)
					select g.ReportFunctionId).ToList<int>();
					executeReportPlanItemDTO2.RunOnClient = !flag2;
					executionSteps.Add(executeReportPlanItemDTO2);
				}
				result = reportExecutionPlanDTO3;
			}
			return result;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000159D8 File Offset: 0x00013BD8
		public ExecuteReportResp ExecuteReport(ExecuteReportReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			ReportDTO reportDTO = (Request.PreviousRunReportResult != null) ? Request.PreviousRunReportResult.Report : Request.Report;
			Report report = (reportDTO == null) ? reportManager.LoadReport(Request.ReportId) : reportDTO.ToDomainObject();
			bool flag = report == null;
			if (flag)
			{
				throw new InvalidParameterException(string.Format("ReportServiceManager:ExecuteReport:Wasn't provided with a report to run and can't load report with rid={0}", Request.ReportId));
			}
			ReportExecutionPlanDTO reportExecutionPlanDTO = Request.ExecutionPlan ?? this.CreateExecutionPlan(report, Request.OnlyRunFunctionIds, Request.FunctionTypesToSkip, Request.RunningOnServer);
			bool flag2 = reportExecutionPlanDTO.ExecutionSteps == null;
			if (flag2)
			{
				reportExecutionPlanDTO.ExecutionSteps = new List<ExecuteReportPlanItemDTO>();
			}
			CWLogger.Logger.Trace("ReportServiceManager:ExecuteReport:BeforeRunningReport:RunningOnServer={0}:ReportId={1}:ExecutionPlan={2}", Request.RunningOnServer.ToString(), report.ReportId, string.Join(", ", reportExecutionPlanDTO.ExecutionSteps.Select(delegate(ExecuteReportPlanItemDTO g)
			{
				string[] array = new string[6];
				array[0] = "ids=";
				array[1] = string.Join(".", (from h in g.ReportFunctionIdsToRun
				select h.ToString()).ToArray<string>());
				array[2] = "; hasCompleted=";
				array[3] = g.HasCompleted.ToString();
				array[4] = "; runOnClient=";
				array[5] = g.RunOnClient.ToString();
				return string.Concat(array);
			}).ToArray<string>()));
			List<ExecuteReportPlanItemDTO> list = new List<ExecuteReportPlanItemDTO>();
			foreach (ExecuteReportPlanItemDTO executeReportPlanItemDTO in reportExecutionPlanDTO.ExecutionSteps)
			{
				bool hasCompleted = executeReportPlanItemDTO.HasCompleted;
				if (!hasCompleted)
				{
					bool flag3 = executeReportPlanItemDTO.RunOnClient == Request.RunningOnServer;
					if (flag3)
					{
						break;
					}
					list.Add(executeReportPlanItemDTO);
				}
			}
			ReportExecutionPlanDTO reportExecutionPlanDTO2 = reportExecutionPlanDTO;
			int numIterations = reportExecutionPlanDTO2.NumIterations;
			reportExecutionPlanDTO2.NumIterations = numIterations + 1;
			bool flag4 = reportExecutionPlanDTO.NumIterations > 10000;
			if (flag4)
			{
				throw new OperationOverflowException(string.Format("ReportServiceManager:ExecuteReport:ExecutionPlanIterationsOverflow:ExecutionPlan.NumIterations={0}", reportExecutionPlanDTO.NumIterations));
			}
			List<int> list2 = (Request.OnlyRunFunctionIds == null) ? null : Request.OnlyRunFunctionIds.ToList<int>();
			bool flag5 = reportExecutionPlanDTO.ExecutionSteps.Count != list.Count;
			if (flag5)
			{
				bool flag6 = list2 == null;
				if (flag6)
				{
					list2 = new List<int>();
				}
				foreach (ExecuteReportPlanItemDTO executeReportPlanItemDTO2 in list)
				{
					list2.AddRange(executeReportPlanItemDTO2.ReportFunctionIdsToRun);
				}
				list2 = list2.Distinct<int>().ToList<int>();
			}
			RunReportResult runReportResult;
			if (Request.PreviousRunReportResult != null)
			{
				runReportResult = reportManager.ExecuteReport2(Request.PreviousRunReportResult.ToDomainObject(), list2, null);
			}
			else
			{
				runReportResult = reportManager.ExecuteReport2(report, list2, null, Request.ReportParameters.ToList<ReportParameterDTO>().ConvertAll<ReportParameter>((ReportParameterDTO f) => f.ToDomainObject()).ToArray());
			}
			RunReportResult runReportResult2 = runReportResult;
			bool flag7 = runReportResult2 == null;
			ExecuteReportResp result;
			if (flag7)
			{
				result = null;
			}
			else
			{
				foreach (ExecuteReportPlanItemDTO executeReportPlanItemDTO3 in list)
				{
					executeReportPlanItemDTO3.HasCompleted = true;
				}
				RunReportResultDTO runReportResultDTO;
				try
				{
					runReportResultDTO = runReportResult2.ToDTO();
					this.RecordReportExecution(new RecordReportExecutionReq
					{
						ExectedFrom = Request.ExecutedFromLocation,
						ReportId = ((report != null) ? report.ReportId : Request.ReportId),
						WhoAmI = Request.WhoAmI
					});
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("ReportServiceManager.ExecuteReport2: {0}", ex.ToString());
					runReportResultDTO = null;
				}
				bool flag8 = runReportResultDTO != null;
				if (flag8)
				{
					runReportResultDTO.ExecutionPlan = reportExecutionPlanDTO;
				}
				result = new ExecuteReportResp
				{
					ReportResult = runReportResultDTO,
					ExecutionPlan = reportExecutionPlanDTO
				};
			}
			return result;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00015DA8 File Offset: 0x00013FA8
		public LoadReportsResp LoadReports(LoadReportsReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			ReportCollection reportCollection = reportManager.LoadReports((Request.ReportContext == null) ? null : Request.ReportContext.ToDomainObject());
			return new LoadReportsResp
			{
				Reports = ((reportCollection == null) ? null : reportCollection.ToDTO())
			};
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00015DFC File Offset: 0x00013FFC
		public LoadReportResp LoadReport(LoadReportReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			Report report = reportManager.LoadReport(Request.ReportId);
			return new LoadReportResp
			{
				Report = ((report == null) ? null : report.ToDTO())
			};
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00015E40 File Offset: 0x00014040
		public CreateReportGroupResp CreateReportGroup(CreateReportGroupReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			int reportGroupId = reportManager.CreateReportGroup(Request.Group.ToDomainObject());
			return new CreateReportGroupResp
			{
				ReportGroupId = reportGroupId
			};
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015E80 File Offset: 0x00014080
		public CreateReportResp CreateReport(CreateReportReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			int reportId = reportManager.CreateReport(Request.Report.ToDomainObject());
			return new CreateReportResp
			{
				ReportId = reportId
			};
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00015EC0 File Offset: 0x000140C0
		public LoadReportForestBySourceResp LoadReportForestBySource(LoadReportForestBySourceReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			Forest<ReportOrGroup> forest = reportManager.LoadReportForestBySource(Request.Xml, (Request.ReportContext == null) ? null : Request.ReportContext.ToDomainObject());
			return new LoadReportForestBySourceResp
			{
				ReportForest = ((forest == null) ? null : forest.ToDTO())
			};
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00015F1C File Offset: 0x0001411C
		public LoadReportsInAGroupResp LoadReportsInAGroup(LoadReportsInAGroupReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			ReportCollection reportCollection = reportManager.LoadReportsInAGroup(Request.ReportGroupTitles.ToArray<string>());
			return new LoadReportsInAGroupResp
			{
				ReportCollection = ((reportCollection == null) ? null : reportCollection.ToDTO())
			};
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00015F64 File Offset: 0x00014164
		public void DeleteReport(DeleteReportReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			reportManager.DeleteReport(Request.ReportId);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015F8C File Offset: 0x0001418C
		public void UpdateReport(UpdateReportReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			reportManager.UpdateReport(Request.Report.ToDomainObject());
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00015FB8 File Offset: 0x000141B8
		public void RecordReportExecution(RecordReportExecutionReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			ReportExecutionContext context = new ReportExecutionContext
			{
				WhoExecutedPersonId = Request.WhoAmI,
				ExecutionLocation = Request.ExectedFrom,
				ExecutionTimestamp = DateTime.Now,
				ReportId = Request.ReportId
			};
			reportManager.RecordReportExecution(context);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00016014 File Offset: 0x00014214
		public void DeleteClientReportGroup(DeleteClientReportGroupReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			reportManager.DeleteClientReportGroup(Request.ReportGroupId);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001603C File Offset: 0x0001423C
		public void SaveReportTechnoProNote(SaveReportTechnoProNoteReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			reportManager.SaveReportTechnoProNote(Request.ReportId, Request.Rtf);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001606C File Offset: 0x0001426C
		public LoadReportTechnoProNoteResp LoadReportTechnoProNote(LoadReportTechnoProNoteReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			string rtf = reportManager.LoadReportTechnoProNote(Request.ReportId);
			return new LoadReportTechnoProNoteResp
			{
				Rtf = rtf
			};
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000160A4 File Offset: 0x000142A4
		public CompileCSharpScript2Resp CompileCSharpScript2(CompileCSharpScript2Req Request)
		{
			IDynamicCompileManager dynamicCompileManager = new DynamicCompileManager(Request.GetOperationContext());
			IList<ReportCompileLineWarningOrError> list = dynamicCompileManager.TryCompileCode(Request.Code);
			CompileCSharpScript2Resp compileCSharpScript2Resp = new CompileCSharpScript2Resp();
			compileCSharpScript2Resp.CompileSucceeded = (list == null);
			IList<ReportCompileLineWarningOrErrorDTO> warningsOrErrors;
			if (list != null)
			{
				warningsOrErrors = list.ToList<ReportCompileLineWarningOrError>().ConvertAll<ReportCompileLineWarningOrErrorDTO>((ReportCompileLineWarningOrError g) => g.ToDTO());
			}
			else
			{
				warningsOrErrors = null;
			}
			compileCSharpScript2Resp.WarningsOrErrors = warningsOrErrors;
			return compileCSharpScript2Resp;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00016118 File Offset: 0x00014318
		public TryToCompileCSharpResp TryToCompileCSharp(TryToCompileCSharpReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			bool compileSucceeded;
			IList<ReportCompileLineWarningOrError> list = reportManager.TryToCompileCSharp(Request.Code, Request.Imports, out compileSucceeded);
			TryToCompileCSharpResp tryToCompileCSharpResp = new TryToCompileCSharpResp();
			tryToCompileCSharpResp.CompileSucceeded = compileSucceeded;
			IList<ReportCompileLineWarningOrErrorDTO> warningsOrErrors;
			if (list != null)
			{
				warningsOrErrors = list.ToList<ReportCompileLineWarningOrError>().ConvertAll<ReportCompileLineWarningOrErrorDTO>((ReportCompileLineWarningOrError g) => g.ToDTO());
			}
			else
			{
				warningsOrErrors = null;
			}
			tryToCompileCSharpResp.WarningsOrErrors = warningsOrErrors;
			return tryToCompileCSharpResp;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00016190 File Offset: 0x00014390
		public ExecuteReportFunctionResp ExecuteReportFunction(ExecuteReportFunctionReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			IReportManager reportManager2 = reportManager;
			eFunctionType functionToExecute = Request.FunctionToExecute;
			IList<ReportParameter> functionParameters;
			if (Request.FunctionParameters != null)
			{
				functionParameters = (from g in Request.FunctionParameters
				select g.ToDomainObject()).ToList<ReportParameter>();
			}
			else
			{
				functionParameters = null;
			}
			RunFunctionData runFunctionData = reportManager2.ExecuteReportFunction(functionToExecute, functionParameters, (Request.CurrentData == null) ? null : Request.CurrentData.ToDomainObject());
			return new ExecuteReportFunctionResp
			{
				ExecuteFunctionResult = ((runFunctionData == null) ? null : runFunctionData.ToDTO())
			};
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016224 File Offset: 0x00014424
		public UpdateClientReportBuiltByTproResp UpdateClientReportBuiltByTpro(UpdateClientReportBuiltByTproReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			reportManager.UpdateClientReportBuiltByTpro(Request.Report.ToDomainObject(), Request.BuiltByTproSignedAndEncrypted);
			return new UpdateClientReportBuiltByTproResp();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00016260 File Offset: 0x00014460
		public ValidateClientReportBuiltByTproIsNotTamperedWithResp ValidateClientReportBuiltByTproIsNotTamperedWith(ValidateClientReportBuiltByTproIsNotTamperedWithReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			bool isValidated = reportManager.ValidateClientReportBuiltByTproIsNotTamperedWith(Request.ReportId);
			return new ValidateClientReportBuiltByTproIsNotTamperedWithResp
			{
				IsValidated = isValidated
			};
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00016298 File Offset: 0x00014498
		public RevertClientReportBuiltByTproToLastTproChangeResp RevertClientReportBuiltByTproToLastTproChange(RevertClientReportBuiltByTproToLastTproChangeReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			bool wasReverted = reportManager.RevertClientReportBuiltByTproToLastTproChange(Request.ReportId);
			return new RevertClientReportBuiltByTproToLastTproChangeResp
			{
				WasReverted = wasReverted
			};
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000162D0 File Offset: 0x000144D0
		public CreateClientReportBuiltByTproResp CreateClientReportBuiltByTpro(CreateClientReportBuiltByTproReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			int reportId = reportManager.CreateClientReportBuiltByTpro(Request.Report.ToDomainObject(), Request.BuiltByTproSignedAndEncrypted);
			return new CreateClientReportBuiltByTproResp
			{
				ReportId = reportId
			};
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016314 File Offset: 0x00014514
		public CreateReportCloneResp CreateReportClone(CreateReportCloneReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			Report report = reportManager.CreateReportClone(Request.ReportId);
			return new CreateReportCloneResp
			{
				Report = ((report == null) ? null : report.ToDTO())
			};
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00016358 File Offset: 0x00014558
		public ExportReportToXmlForUserResp ExportReportToXmlForUser(ExportReportToXmlForUserReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			return new ExportReportToXmlForUserResp
			{
				Xml = reportManager.ExportReportToXmlForUser((Request.ReportIds == null) ? null : Request.ReportIds.ToArray<int>())
			};
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000163A0 File Offset: 0x000145A0
		public ExportReportToXmlForUpdatingSystemResp ExportReportToXmlForUpdatingSystem(ExportReportToXmlForUpdatingSystemReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			return new ExportReportToXmlForUpdatingSystemResp
			{
				Xml = reportManager.ExportReportToXmlForUpdatingSystem((Request.ReportIds == null) ? null : Request.ReportIds.ToArray<int>())
			};
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000163E8 File Offset: 0x000145E8
		public CloneReportsResp CloneReports(CloneReportsReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			IDictionary<int, int> oldAndNewReportIds = reportManager.CloneReports((Request.ReportIds == null) ? null : Request.ReportIds.ToArray<int>());
			return new CloneReportsResp
			{
				OldAndNewReportIds = oldAndNewReportIds
			};
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016430 File Offset: 0x00014630
		public CloneReportResp CloneReport(CloneReportReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			return new CloneReportResp
			{
				ReportId = reportManager.CloneReport(Request.ReportId)
			};
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00016468 File Offset: 0x00014668
		public ImportReportFromXmlForUserResp ImportReportFromXmlForUser(ImportReportFromXmlForUserReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			return new ImportReportFromXmlForUserResp
			{
				UniqueIdsAndNewReportIds = reportManager.ImportReportFromXmlForUser(Request.Xml, Request.ParentGroupId)
			};
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000164A4 File Offset: 0x000146A4
		public ExportReportToXmlForUserFromReportsResp ExportReportToXmlForUserFromReports(ExportReportToXmlForUserFromReportsReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			return new ExportReportToXmlForUserFromReportsResp
			{
				Xml = reportManager.ExportReportsToXmlForUser((Request.ReportCollection == null) ? null : Request.ReportCollection.ToDomainObject())
			};
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000164EC File Offset: 0x000146EC
		public LoadReportForestResp LoadReportForest(LoadReportForestReq Request)
		{
			LoadReportForestResp result;
			try
			{
				IReportManager reportManager = new ReportManager(Request.GetOperationContext());
				Forest<ReportOrGroup> forest = reportManager.LoadReportForest((Request.ReportContext == null) ? null : Request.ReportContext.ToDomainObject());
				result = new LoadReportForestResp
				{
					ReportForest = ((forest == null) ? null : forest.ToDTO())
				};
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ReportServiceManager:LoadReportForest:Error={0}", ex.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001656C File Offset: 0x0001476C
		public LoadReportGroupForestResp LoadReportGroupForest(LoadReportGroupForestReq Request)
		{
			LoadReportGroupForestResp result;
			try
			{
				IReportManager reportManager = new ReportManager(Request.GetOperationContext());
				Forest<ReportGroup> forest = reportManager.LoadReportGroupForest((Request.ReportContext == null) ? null : Request.ReportContext.ToDomainObject());
				LoadReportGroupForestResp loadReportGroupForestResp = new LoadReportGroupForestResp();
				Forest<ReportGroupDTO> reportGroups;
				if (forest != null)
				{
					reportGroups = forest.ConvertAll<ReportGroupDTO>((ReportGroup g) => g.ToDTO());
				}
				else
				{
					reportGroups = null;
				}
				loadReportGroupForestResp.ReportGroups = reportGroups;
				result = loadReportGroupForestResp;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ReportServiceManager:LoadReportGroupForest:Error={0}", ex.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001660C File Offset: 0x0001480C
		public ChangeReportOrderInSameReportGroupResp ChangeReportOrderInSameReportGroup(ChangeReportOrderInSameReportGroupReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			return new ChangeReportOrderInSameReportGroupResp
			{
				NewReportOrderNum = reportManager.ChangeReportOrderInSameReportGroup(Request.ReportIdToMove, Request.ReportIdToMoveBeforeOrAfter, Request.moveAfter)
			};
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00016650 File Offset: 0x00014850
		public ChangeReportGroupOrderInSameReportGroupResp ChangeReportGroupOrderInSameReportGroup(ChangeReportGroupOrderInSameReportGroupReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			return new ChangeReportGroupOrderInSameReportGroupResp
			{
				NewGroupOrderNum = reportManager.ChangeReportGroupOrderInSameReportGroup(Request.ReportGroupIdToMove, Request.ReportGroupIdToMoveBeforeOrAfter, Request.MoveAfter)
			};
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00016694 File Offset: 0x00014894
		public MoveReportResp MoveReport(MoveReportReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			Report report = reportManager.MoveReport(Request.ReportIdToMove, Request.NewReportParentGroupId, Request.ReportIdToMoveBeforeOrAfter, Request.MoveAfter);
			return new MoveReportResp
			{
				Report = ((report == null) ? null : report.ToDTO())
			};
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000166EC File Offset: 0x000148EC
		public MoveReportGroupResp MoveReportGroup(MoveReportGroupReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			ReportGroup reportGroup = reportManager.MoveGroup(Request.ReportGroupIdToMove, Request.NewReportParentGroupId, Request.ReportGroupIdToMoveBeforeOrAfter, Request.MoveAfter);
			return new MoveReportGroupResp
			{
				ReportGroup = ((reportGroup == null) ? null : reportGroup.ToDTO())
			};
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00016744 File Offset: 0x00014944
		public void SortReportGroupMembersAlphabetically(SortReportGroupMembersAlphabeticallyReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			reportManager.SortReportGroupMembersAlphabetically(Request.ParentReportGroupId);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001676C File Offset: 0x0001496C
		public LoadReportsInAGroupByGroupIdResp LoadReportsInAGroupByGroupId(LoadReportsInAGroupByGroupIdReq Request)
		{
			IReportManager reportManager = new ReportManager(Request.GetOperationContext());
			ReportCollection reportCollection = reportManager.LoadReportsInAGroup(new int[]
			{
				Request.GroupId
			});
			return new LoadReportsInAGroupByGroupIdResp
			{
				ReportCollection = ((reportCollection == null) ? null : reportCollection.ToDTO())
			};
		}
	}
}
