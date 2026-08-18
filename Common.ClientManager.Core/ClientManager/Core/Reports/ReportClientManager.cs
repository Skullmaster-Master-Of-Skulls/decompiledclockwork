using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Reports
{
	// Token: 0x02000029 RID: 41
	public class ReportClientManager : IReportClientManager, IWebService
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000068C8 File Offset: 0x00004AC8
		public Forest<ReportOrGroupDTO> LoadReportForest(ReportContextDTO ReportContext)
		{
			LoadReportForestReq loadReportForestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportForestReq>();
			loadReportForestReq.ReportContext = ReportContext;
			loadReportForestReq.BinPath = ((loadReportForestReq.ApplicationContext != null) ? loadReportForestReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReportForest(loadReportForestReq).ReportForest;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000691C File Offset: 0x00004B1C
		public ReportCollectionDTO LoadReports(ReportContextDTO ReportContext)
		{
			LoadReportsReq loadReportsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportsReq>();
			loadReportsReq.ReportContext = ReportContext;
			loadReportsReq.BinPath = ((loadReportsReq.ApplicationContext != null) ? loadReportsReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReports(loadReportsReq).Reports;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006970 File Offset: 0x00004B70
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, params ReportParameterDTO[] parameters)
		{
			return this.ExecuteReport(reportId, Location, null, parameters.ToList<ReportParameterDTO>());
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006994 File Offset: 0x00004B94
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, params ReportParameterDTO[] parameters)
		{
			return this.ExecuteReport(reportId, Location, FunctionTypesToSkip, parameters.ToList<ReportParameterDTO>());
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000069B8 File Offset: 0x00004BB8
		public int CreateReportGroup(ReportGroupDTO Group)
		{
			CreateReportGroupReq createReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateReportGroupReq>();
			createReportGroupReq.Group = Group;
			createReportGroupReq.BinPath = ((createReportGroupReq.ApplicationContext != null) ? createReportGroupReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().CreateReportGroup(createReportGroupReq).ReportGroupId;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006A0C File Offset: 0x00004C0C
		public int CreateReport(ReportDTO Report)
		{
			CreateReportReq createReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateReportReq>();
			createReportReq.Report = Report;
			createReportReq.BinPath = ((createReportReq.ApplicationContext != null) ? createReportReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().CreateReport(createReportReq).ReportId;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006A60 File Offset: 0x00004C60
		public ReportDTO LoadReport(int ReportId)
		{
			LoadReportReq loadReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportReq>();
			loadReportReq.ReportId = ReportId;
			loadReportReq.BinPath = ((loadReportReq.ApplicationContext != null) ? loadReportReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReport(loadReportReq).Report;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public Forest<ReportOrGroupDTO> LoadReportForestBySource(string ReportXml, ReportContextDTO ReportContext)
		{
			LoadReportForestBySourceReq loadReportForestBySourceReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportForestBySourceReq>();
			loadReportForestBySourceReq.ReportContext = ReportContext;
			loadReportForestBySourceReq.Xml = ReportXml;
			loadReportForestBySourceReq.BinPath = ((loadReportForestBySourceReq.ApplicationContext != null) ? loadReportForestBySourceReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReportForestBySource(loadReportForestBySourceReq).ReportForest;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006B10 File Offset: 0x00004D10
		public ReportCollectionDTO LoadReportsInAGroup(params string[] GroupTitles)
		{
			LoadReportsInAGroupReq loadReportsInAGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportsInAGroupReq>();
			loadReportsInAGroupReq.ReportGroupTitles = GroupTitles;
			loadReportsInAGroupReq.BinPath = ((loadReportsInAGroupReq.ApplicationContext != null) ? loadReportsInAGroupReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReportsInAGroup(loadReportsInAGroupReq).ReportCollection;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006B64 File Offset: 0x00004D64
		public ReportCollectionDTO LoadReportsInAGroup(int groupId)
		{
			LoadReportsInAGroupByGroupIdReq loadReportsInAGroupByGroupIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportsInAGroupByGroupIdReq>();
			loadReportsInAGroupByGroupIdReq.GroupId = groupId;
			loadReportsInAGroupByGroupIdReq.BinPath = ((loadReportsInAGroupByGroupIdReq.ApplicationContext != null) ? loadReportsInAGroupByGroupIdReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReportsInAGroupByGroupId(loadReportsInAGroupByGroupIdReq).ReportCollection;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			ExecuteReportReq executeReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportReq>();
			executeReportReq.FunctionTypesToSkip = FunctionTypesToSkip;
			executeReportReq.BinPath = ((executeReportReq.ApplicationContext != null) ? executeReportReq.ApplicationContext.ExecutingPath : null);
			executeReportReq.ReportParameters = parameters;
			executeReportReq.ReportId = reportId;
			executeReportReq.OnlyRunFunctionIds = OnlyRunFunctionIds;
			executeReportReq.ExecutedFromLocation = Location;
			RunReportResultDTO runReportResultDTO = ClientServiceFactory.GetClientInstance<IReport>().ExecuteReport(executeReportReq).ReportResult;
			bool flag;
			if (runReportResultDTO.ExecutionPlan != null && runReportResultDTO.ExecutionPlan.ExecutionSteps != null)
			{
				flag = (runReportResultDTO.ExecutionPlan.ExecutionSteps.Count((ExecuteReportPlanItemDTO g) => !g.HasCompleted) > 0);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				runReportResultDTO = this.FinishReportExecutionPlan(runReportResultDTO, runReportResultDTO.ExecutionPlan, Location, OnlyRunFunctionIds, FunctionTypesToSkip);
			}
			return runReportResultDTO;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006C90 File Offset: 0x00004E90
		private RunReportResultDTO ExecuteReportWithExecutionPlanOneStep(bool runOnServer, ReportExecutionPlanDTO executionPlan, RunReportResultDTO reportResult, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip)
		{
			ExecuteReportReq executeReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportReq>();
			executeReportReq.FunctionTypesToSkip = FunctionTypesToSkip;
			executeReportReq.BinPath = ((executeReportReq.ApplicationContext != null) ? executeReportReq.ApplicationContext.ExecutingPath : null);
			executeReportReq.OnlyRunFunctionIds = OnlyRunFunctionIds;
			executeReportReq.ExecutedFromLocation = Location;
			executeReportReq.ExecutionPlan = executionPlan;
			executeReportReq.PreviousRunReportResult = reportResult;
			IReport report = runOnServer ? ClientServiceFactory.GetClientInstance<IReport>() : ClientServiceFactory.GetDirectClientInstanceNoServer<IReport>();
			ExecuteReportResp executeReportResp = report.ExecuteReport(executeReportReq);
			bool flag = executeReportResp != null && executeReportResp.ReportResult != null;
			if (flag)
			{
				executeReportResp.ReportResult.ExecutionPlan = executeReportResp.ExecutionPlan;
			}
			return (executeReportResp != null) ? executeReportResp.ReportResult : null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006D40 File Offset: 0x00004F40
		public RunReportResultDTO FinishReportExecutionPlan(RunReportResultDTO reportResult, ReportExecutionPlanDTO executionPlan, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip)
		{
			bool flag = executionPlan.ExecutionSteps.All((ExecuteReportPlanItemDTO g) => g.HasCompleted);
			RunReportResultDTO result;
			if (flag)
			{
				result = reportResult;
			}
			else
			{
				ReportExecutionPlanDTO reportExecutionPlanDTO = executionPlan;
				RunReportResultDTO runReportResultDTO = reportResult;
				int num = 0;
				for (;;)
				{
					num++;
					bool flag2 = num > 10000;
					if (flag2)
					{
						break;
					}
					ExecuteReportPlanItemDTO executeReportPlanItemDTO = reportExecutionPlanDTO.ExecutionSteps.FirstOrDefault((ExecuteReportPlanItemDTO g) => !g.HasCompleted);
					bool flag3 = executeReportPlanItemDTO == null;
					if (flag3)
					{
						goto Block_5;
					}
					runReportResultDTO = this.ExecuteReportWithExecutionPlanOneStep(!executeReportPlanItemDTO.RunOnClient, reportExecutionPlanDTO, runReportResultDTO, Location, OnlyRunFunctionIds, FunctionTypesToSkip);
					bool flag4;
					if (runReportResultDTO.ExecutionPlan != null)
					{
						flag4 = runReportResultDTO.ExecutionPlan.ExecutionSteps.All((ExecuteReportPlanItemDTO g) => g.HasCompleted);
					}
					else
					{
						flag4 = true;
					}
					bool flag5 = flag4;
					if (flag5)
					{
						goto Block_8;
					}
					bool flag6 = runReportResultDTO.ReportStatus == null || runReportResultDTO.ReportStatus.LastStatusStep != eRunStatusStepDTO.CompletedSuccessfully;
					if (flag6)
					{
						goto Block_10;
					}
					reportExecutionPlanDTO = runReportResultDTO.ExecutionPlan;
				}
				return runReportResultDTO;
				Block_5:
				Block_8:
				Block_10:
				result = runReportResultDTO;
			}
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006E7C File Offset: 0x0000507C
		public RunReportResultDTO ExecuteReport(ReportDTO report, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			ExecuteReportReq executeReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportReq>();
			executeReportReq.FunctionTypesToSkip = FunctionTypesToSkip;
			executeReportReq.BinPath = ((executeReportReq.ApplicationContext != null) ? executeReportReq.ApplicationContext.ExecutingPath : null);
			executeReportReq.ReportParameters = parameters;
			executeReportReq.Report = report;
			executeReportReq.OnlyRunFunctionIds = OnlyRunFunctionIds;
			executeReportReq.ExecutedFromLocation = Location;
			RunReportResultDTO runReportResultDTO = ClientServiceFactory.GetClientInstance<IReport>().ExecuteReport(executeReportReq).ReportResult;
			bool flag;
			if (runReportResultDTO.ExecutionPlan != null && runReportResultDTO.ExecutionPlan.ExecutionSteps != null)
			{
				flag = (runReportResultDTO.ExecutionPlan.ExecutionSteps.Count((ExecuteReportPlanItemDTO g) => !g.HasCompleted) > 0);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				runReportResultDTO = this.FinishReportExecutionPlan(runReportResultDTO, runReportResultDTO.ExecutionPlan, Location, OnlyRunFunctionIds, FunctionTypesToSkip);
			}
			return runReportResultDTO;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006F54 File Offset: 0x00005154
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, params ReportParameterDTO[] parameters)
		{
			return this.ExecuteReport(reportId, Location, OnlyRunFunctionIds, FunctionTypesToSkip, parameters.ToList<ReportParameterDTO>());
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006F78 File Offset: 0x00005178
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			return this.ExecuteReport(reportId, Location, null, FunctionTypesToSkip, parameters);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006F98 File Offset: 0x00005198
		public RunReportResultDTO ExecuteReport(ReportDTO report, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			return this.ExecuteReport(report, Location, null, FunctionTypesToSkip, parameters);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006FB8 File Offset: 0x000051B8
		public void DeleteReport(int ReportId)
		{
			DeleteReportReq deleteReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteReportReq>();
			deleteReportReq.BinPath = ((deleteReportReq.ApplicationContext != null) ? deleteReportReq.ApplicationContext.ExecutingPath : null);
			deleteReportReq.ReportId = ReportId;
			ClientServiceFactory.GetClientInstance<IReport>().DeleteReport(deleteReportReq);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007004 File Offset: 0x00005204
		public void UpdateReport(ReportDTO Report)
		{
			UpdateReportReq updateReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateReportReq>();
			updateReportReq.BinPath = ((updateReportReq.ApplicationContext != null) ? updateReportReq.ApplicationContext.ExecutingPath : null);
			updateReportReq.Report = Report;
			ClientServiceFactory.GetClientInstance<IReport>().UpdateReport(updateReportReq);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007050 File Offset: 0x00005250
		public void RecordReportExecution(int ReportId, eReportExecutedFromLocation Location)
		{
			RecordReportExecutionReq recordReportExecutionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RecordReportExecutionReq>();
			recordReportExecutionReq.BinPath = ((recordReportExecutionReq.ApplicationContext != null) ? recordReportExecutionReq.ApplicationContext.ExecutingPath : null);
			recordReportExecutionReq.ReportId = ReportId;
			recordReportExecutionReq.ExectedFrom = Location;
			ClientServiceFactory.GetClientInstance<IReport>().RecordReportExecution(recordReportExecutionReq);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000070A4 File Offset: 0x000052A4
		public void DeleteClientReportGroup(int ReportGroupid)
		{
			DeleteClientReportGroupReq deleteClientReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteClientReportGroupReq>();
			deleteClientReportGroupReq.BinPath = ((deleteClientReportGroupReq.ApplicationContext != null) ? deleteClientReportGroupReq.ApplicationContext.ExecutingPath : null);
			deleteClientReportGroupReq.ReportGroupId = ReportGroupid;
			ClientServiceFactory.GetClientInstance<IReport>().DeleteClientReportGroup(deleteClientReportGroupReq);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000070F0 File Offset: 0x000052F0
		public string LoadReportTechnoProNote(int ReportId)
		{
			LoadReportTechnoProNoteReq loadReportTechnoProNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportTechnoProNoteReq>();
			loadReportTechnoProNoteReq.BinPath = ((loadReportTechnoProNoteReq.ApplicationContext != null) ? loadReportTechnoProNoteReq.ApplicationContext.ExecutingPath : null);
			loadReportTechnoProNoteReq.ReportId = ReportId;
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReportTechnoProNote(loadReportTechnoProNoteReq).Rtf;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007144 File Offset: 0x00005344
		public void SaveReportTechnoProNote(int ReportId, string Rtf)
		{
			SaveReportTechnoProNoteReq saveReportTechnoProNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveReportTechnoProNoteReq>();
			saveReportTechnoProNoteReq.BinPath = ((saveReportTechnoProNoteReq.ApplicationContext != null) ? saveReportTechnoProNoteReq.ApplicationContext.ExecutingPath : null);
			saveReportTechnoProNoteReq.ReportId = ReportId;
			saveReportTechnoProNoteReq.Rtf = Rtf;
			ClientServiceFactory.GetClientInstance<IReport>().SaveReportTechnoProNote(saveReportTechnoProNoteReq);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007198 File Offset: 0x00005398
		public bool TryToCompileCSharpLegacy(string code, out IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors)
		{
			CompileCSharpScript2Req compileCSharpScript2Req = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CompileCSharpScript2Req>();
			compileCSharpScript2Req.BinPath = ((compileCSharpScript2Req.ApplicationContext != null) ? compileCSharpScript2Req.ApplicationContext.ExecutingPath : null);
			compileCSharpScript2Req.Code = code;
			CompileCSharpScript2Resp compileCSharpScript2Resp = ClientServiceFactory.GetClientInstance<IReport>().CompileCSharpScript2(compileCSharpScript2Req);
			WarningsOrErrors = compileCSharpScript2Resp.WarningsOrErrors;
			return compileCSharpScript2Resp.CompileSucceeded;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000071F4 File Offset: 0x000053F4
		public bool TryToCompileCSharp(string code, IList<string> imports, out IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors)
		{
			TryToCompileCSharpReq tryToCompileCSharpReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToCompileCSharpReq>();
			tryToCompileCSharpReq.BinPath = ((tryToCompileCSharpReq.ApplicationContext != null) ? tryToCompileCSharpReq.ApplicationContext.ExecutingPath : null);
			tryToCompileCSharpReq.Code = code;
			tryToCompileCSharpReq.Imports = imports;
			TryToCompileCSharpResp tryToCompileCSharpResp = ClientServiceFactory.GetClientInstance<IReport>().TryToCompileCSharp(tryToCompileCSharpReq);
			bool flag = tryToCompileCSharpResp == null;
			bool result;
			if (flag)
			{
				WarningsOrErrors = null;
				result = false;
			}
			else
			{
				WarningsOrErrors = tryToCompileCSharpResp.WarningsOrErrors;
				result = tryToCompileCSharpResp.CompileSucceeded;
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007268 File Offset: 0x00005468
		public RunFunctionDataDTO ExecuteReportFunction(eFunctionType FunctionToExecute, IList<ReportParameterDTO> FunctionParameters, RunFunctionDataDTO CurrentData)
		{
			ExecuteReportFunctionReq executeReportFunctionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportFunctionReq>();
			executeReportFunctionReq.BinPath = ((executeReportFunctionReq.ApplicationContext != null) ? executeReportFunctionReq.ApplicationContext.ExecutingPath : null);
			executeReportFunctionReq.FunctionToExecute = FunctionToExecute;
			executeReportFunctionReq.FunctionParameters = FunctionParameters;
			executeReportFunctionReq.CurrentData = CurrentData;
			return ClientServiceFactory.GetClientInstance<IReport>().ExecuteReportFunction(executeReportFunctionReq).ExecuteFunctionResult;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000072CC File Offset: 0x000054CC
		public void UpdateClientReportBuiltByTpro(ReportDTO Report, byte[] BuiltByTproSignedAndEncrypted)
		{
			UpdateClientReportBuiltByTproReq updateClientReportBuiltByTproReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateClientReportBuiltByTproReq>();
			updateClientReportBuiltByTproReq.BinPath = ((updateClientReportBuiltByTproReq.ApplicationContext != null) ? updateClientReportBuiltByTproReq.ApplicationContext.ExecutingPath : null);
			updateClientReportBuiltByTproReq.Report = Report;
			updateClientReportBuiltByTproReq.BuiltByTproSignedAndEncrypted = BuiltByTproSignedAndEncrypted;
			ClientServiceFactory.GetClientInstance<IReport>().UpdateClientReportBuiltByTpro(updateClientReportBuiltByTproReq);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007320 File Offset: 0x00005520
		public bool ValidateClientReportBuiltByTproIsNotTamperedWith(int ReportId)
		{
			ValidateClientReportBuiltByTproIsNotTamperedWithReq validateClientReportBuiltByTproIsNotTamperedWithReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ValidateClientReportBuiltByTproIsNotTamperedWithReq>();
			validateClientReportBuiltByTproIsNotTamperedWithReq.BinPath = ((validateClientReportBuiltByTproIsNotTamperedWithReq.ApplicationContext != null) ? validateClientReportBuiltByTproIsNotTamperedWithReq.ApplicationContext.ExecutingPath : null);
			validateClientReportBuiltByTproIsNotTamperedWithReq.ReportId = ReportId;
			return ClientServiceFactory.GetClientInstance<IReport>().ValidateClientReportBuiltByTproIsNotTamperedWith(validateClientReportBuiltByTproIsNotTamperedWithReq).IsValidated;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007374 File Offset: 0x00005574
		public bool RevertClientReportBuiltByTproToLastTproChange(int ReportId)
		{
			RevertClientReportBuiltByTproToLastTproChangeReq revertClientReportBuiltByTproToLastTproChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RevertClientReportBuiltByTproToLastTproChangeReq>();
			revertClientReportBuiltByTproToLastTproChangeReq.BinPath = ((revertClientReportBuiltByTproToLastTproChangeReq.ApplicationContext != null) ? revertClientReportBuiltByTproToLastTproChangeReq.ApplicationContext.ExecutingPath : null);
			revertClientReportBuiltByTproToLastTproChangeReq.ReportId = ReportId;
			return ClientServiceFactory.GetClientInstance<IReport>().RevertClientReportBuiltByTproToLastTproChange(revertClientReportBuiltByTproToLastTproChangeReq).WasReverted;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000073C8 File Offset: 0x000055C8
		public int CreateClientReportBuiltByTpro(ReportDTO Report, byte[] BuiltByTproSignedAndEncrypted)
		{
			CreateClientReportBuiltByTproReq createClientReportBuiltByTproReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateClientReportBuiltByTproReq>();
			createClientReportBuiltByTproReq.BinPath = ((createClientReportBuiltByTproReq.ApplicationContext != null) ? createClientReportBuiltByTproReq.ApplicationContext.ExecutingPath : null);
			createClientReportBuiltByTproReq.Report = Report;
			createClientReportBuiltByTproReq.BuiltByTproSignedAndEncrypted = BuiltByTproSignedAndEncrypted;
			return ClientServiceFactory.GetClientInstance<IReport>().CreateClientReportBuiltByTpro(createClientReportBuiltByTproReq).ReportId;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007424 File Offset: 0x00005624
		public ReportDTO CreateReportClone(int ReportId)
		{
			CreateReportCloneReq createReportCloneReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateReportCloneReq>();
			createReportCloneReq.BinPath = ((createReportCloneReq.ApplicationContext != null) ? createReportCloneReq.ApplicationContext.ExecutingPath : null);
			createReportCloneReq.ReportId = ReportId;
			return ClientServiceFactory.GetClientInstance<IReport>().CreateReportClone(createReportCloneReq).Report;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007478 File Offset: 0x00005678
		public string ExportReportToXmlForUser(params int[] ReportIds)
		{
			ExportReportToXmlForUserReq exportReportToXmlForUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExportReportToXmlForUserReq>();
			exportReportToXmlForUserReq.BinPath = ((exportReportToXmlForUserReq.ApplicationContext != null) ? exportReportToXmlForUserReq.ApplicationContext.ExecutingPath : null);
			exportReportToXmlForUserReq.ReportIds = ReportIds;
			return ClientServiceFactory.GetClientInstance<IReport>().ExportReportToXmlForUser(exportReportToXmlForUserReq).Xml;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000074CC File Offset: 0x000056CC
		public string ExportReportToXmlForUpdatingSystem(params int[] ReportIds)
		{
			ExportReportToXmlForUpdatingSystemReq exportReportToXmlForUpdatingSystemReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExportReportToXmlForUpdatingSystemReq>();
			exportReportToXmlForUpdatingSystemReq.BinPath = ((exportReportToXmlForUpdatingSystemReq.ApplicationContext != null) ? exportReportToXmlForUpdatingSystemReq.ApplicationContext.ExecutingPath : null);
			exportReportToXmlForUpdatingSystemReq.ReportIds = ReportIds;
			return ClientServiceFactory.GetClientInstance<IReport>().ExportReportToXmlForUpdatingSystem(exportReportToXmlForUpdatingSystemReq).Xml;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007520 File Offset: 0x00005720
		public int CloneReport(int ReportId)
		{
			CloneReportReq cloneReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CloneReportReq>();
			cloneReportReq.BinPath = ((cloneReportReq.ApplicationContext != null) ? cloneReportReq.ApplicationContext.ExecutingPath : null);
			cloneReportReq.ReportId = ReportId;
			return ClientServiceFactory.GetClientInstance<IReport>().CloneReport(cloneReportReq).ReportId;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007574 File Offset: 0x00005774
		public IDictionary<string, int> ImportReportFromXmlForUser(string Xml, int ParentGroupId = 0)
		{
			ImportReportFromXmlForUserReq importReportFromXmlForUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportReportFromXmlForUserReq>();
			importReportFromXmlForUserReq.BinPath = ((importReportFromXmlForUserReq.ApplicationContext != null) ? importReportFromXmlForUserReq.ApplicationContext.ExecutingPath : null);
			importReportFromXmlForUserReq.Xml = Xml;
			importReportFromXmlForUserReq.ParentGroupId = ParentGroupId;
			return ClientServiceFactory.GetClientInstance<IReport>().ImportReportFromXmlForUser(importReportFromXmlForUserReq).UniqueIdsAndNewReportIds;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000075D0 File Offset: 0x000057D0
		public string ExportReportsToXmlForUserFromReports(ReportCollectionDTO reportCollection)
		{
			ExportReportToXmlForUserFromReportsReq exportReportToXmlForUserFromReportsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExportReportToXmlForUserFromReportsReq>();
			exportReportToXmlForUserFromReportsReq.BinPath = ((exportReportToXmlForUserFromReportsReq.ApplicationContext != null) ? exportReportToXmlForUserFromReportsReq.ApplicationContext.ExecutingPath : null);
			exportReportToXmlForUserFromReportsReq.ReportCollection = reportCollection;
			return ClientServiceFactory.GetClientInstance<IReport>().ExportReportToXmlForUserFromReports(exportReportToXmlForUserFromReportsReq).Xml;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007624 File Offset: 0x00005824
		public Forest<ReportGroupDTO> LoadReportGroupForest(ReportContextDTO reportContext)
		{
			LoadReportGroupForestReq loadReportGroupForestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportGroupForestReq>();
			loadReportGroupForestReq.BinPath = ((loadReportGroupForestReq.ApplicationContext != null) ? loadReportGroupForestReq.ApplicationContext.ExecutingPath : null);
			loadReportGroupForestReq.ReportContext = reportContext;
			return ClientServiceFactory.GetClientInstance<IReport>().LoadReportGroupForest(loadReportGroupForestReq).ReportGroups;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007678 File Offset: 0x00005878
		public int ChangeReportOrderInSameReportGroup(int ReportIdToMove, int ReportIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			ChangeReportOrderInSameReportGroupReq changeReportOrderInSameReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeReportOrderInSameReportGroupReq>();
			changeReportOrderInSameReportGroupReq.BinPath = ((changeReportOrderInSameReportGroupReq.ApplicationContext != null) ? changeReportOrderInSameReportGroupReq.ApplicationContext.ExecutingPath : null);
			changeReportOrderInSameReportGroupReq.moveAfter = moveAfter;
			changeReportOrderInSameReportGroupReq.ReportIdToMove = ReportIdToMove;
			changeReportOrderInSameReportGroupReq.ReportIdToMoveBeforeOrAfter = ReportIdToMoveBeforeOrAfter;
			return ClientServiceFactory.GetClientInstance<IReport>().ChangeReportOrderInSameReportGroup(changeReportOrderInSameReportGroupReq).NewReportOrderNum;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000076DC File Offset: 0x000058DC
		public int ChangeReportGroupOrderInSameReportGroup(int ReportGroupIdToMove, int ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			ChangeReportGroupOrderInSameReportGroupReq changeReportGroupOrderInSameReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeReportGroupOrderInSameReportGroupReq>();
			changeReportGroupOrderInSameReportGroupReq.BinPath = ((changeReportGroupOrderInSameReportGroupReq.ApplicationContext != null) ? changeReportGroupOrderInSameReportGroupReq.ApplicationContext.ExecutingPath : null);
			changeReportGroupOrderInSameReportGroupReq.MoveAfter = moveAfter;
			changeReportGroupOrderInSameReportGroupReq.ReportGroupIdToMove = ReportGroupIdToMove;
			changeReportGroupOrderInSameReportGroupReq.ReportGroupIdToMoveBeforeOrAfter = ReportGroupIdToMoveBeforeOrAfter;
			return ClientServiceFactory.GetClientInstance<IReport>().ChangeReportGroupOrderInSameReportGroup(changeReportGroupOrderInSameReportGroupReq).NewGroupOrderNum;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007740 File Offset: 0x00005940
		public ReportDTO MoveReport(int ReportIdToMove, int NewReportParentGroupId, int? ReportIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			MoveReportReq moveReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MoveReportReq>();
			moveReportReq.BinPath = ((moveReportReq.ApplicationContext != null) ? moveReportReq.ApplicationContext.ExecutingPath : null);
			moveReportReq.MoveAfter = moveAfter;
			moveReportReq.ReportIdToMove = ReportIdToMove;
			moveReportReq.NewReportParentGroupId = NewReportParentGroupId;
			moveReportReq.ReportIdToMoveBeforeOrAfter = ReportIdToMoveBeforeOrAfter;
			return ClientServiceFactory.GetClientInstance<IReport>().MoveReport(moveReportReq).Report;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000077AC File Offset: 0x000059AC
		public ReportGroupDTO MoveReportGroup(int ReportGroupIdToMove, int NewReportParentGroupId, int? ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			MoveReportGroupReq moveReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MoveReportGroupReq>();
			moveReportGroupReq.BinPath = ((moveReportGroupReq.ApplicationContext != null) ? moveReportGroupReq.ApplicationContext.ExecutingPath : null);
			moveReportGroupReq.MoveAfter = moveAfter;
			moveReportGroupReq.ReportGroupIdToMove = ReportGroupIdToMove;
			moveReportGroupReq.ReportGroupIdToMoveBeforeOrAfter = ReportGroupIdToMoveBeforeOrAfter;
			moveReportGroupReq.NewReportParentGroupId = NewReportParentGroupId;
			return ClientServiceFactory.GetClientInstance<IReport>().MoveReportGroup(moveReportGroupReq).ReportGroup;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007818 File Offset: 0x00005A18
		public void SortReportGroupMembersAlphabetically(int ParentReportGroupId)
		{
			SortReportGroupMembersAlphabeticallyReq sortReportGroupMembersAlphabeticallyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SortReportGroupMembersAlphabeticallyReq>();
			sortReportGroupMembersAlphabeticallyReq.BinPath = ((sortReportGroupMembersAlphabeticallyReq.ApplicationContext != null) ? sortReportGroupMembersAlphabeticallyReq.ApplicationContext.ExecutingPath : null);
			sortReportGroupMembersAlphabeticallyReq.ParentReportGroupId = ParentReportGroupId;
			ClientServiceFactory.GetClientInstance<IReport>().SortReportGroupMembersAlphabetically(sortReportGroupMembersAlphabeticallyReq);
		}
	}
}
