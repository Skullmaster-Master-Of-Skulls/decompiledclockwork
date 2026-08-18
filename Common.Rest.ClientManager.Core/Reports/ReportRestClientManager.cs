using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Reports
{
	// Token: 0x02000021 RID: 33
	public class ReportRestClientManager : BearerTokenRestProxy<IReportClientManager>, IReportClientManager, IWebService
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00004722 File Offset: 0x00002922
		public ReportRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000472C File Offset: 0x0000292C
		public ReportRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004737 File Offset: 0x00002937
		public Forest<ReportOrGroupDTO> LoadReportForest(ReportContextDTO ReportContext)
		{
			return base.Post<ReportContextDTO, LoadReportForestResp>(ReportContext, "reports/loadreportforest").ReportForest;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000474A File Offset: 0x0000294A
		public ReportCollectionDTO LoadReports(ReportContextDTO ReportContext)
		{
			return base.Post<ReportContextDTO, ReportCollectionDTO>(ReportContext, "reports/loadreports");
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004758 File Offset: 0x00002958
		public Forest<ReportOrGroupDTO> LoadReportForestBySource(string ReportXml, ReportContextDTO ReportContext)
		{
			LoadReportForestBySourceReq loadReportForestBySourceReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadReportForestBySourceReq>();
			loadReportForestBySourceReq.ReportContext = ReportContext;
			loadReportForestBySourceReq.Xml = ReportXml;
			BaseReportMessageReq baseReportMessageReq = loadReportForestBySourceReq;
			ApplicationContext applicationContext = loadReportForestBySourceReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<LoadReportForestBySourceReq, LoadReportForestBySourceResp>(loadReportForestBySourceReq, "reports/loadreportforestbysourse").ReportForest;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000047A7 File Offset: 0x000029A7
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, params ReportParameterDTO[] parameters)
		{
			return this.ExecuteReport(reportId, Location, null, parameters.ToList<ReportParameterDTO>());
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000047B8 File Offset: 0x000029B8
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, params ReportParameterDTO[] parameters)
		{
			return this.ExecuteReport(reportId, Location, FunctionTypesToSkip, parameters.ToList<ReportParameterDTO>());
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000047CA File Offset: 0x000029CA
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			return this.ExecuteReport(reportId, Location, null, FunctionTypesToSkip, parameters);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000047D8 File Offset: 0x000029D8
		public RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, params ReportParameterDTO[] parameters)
		{
			return this.ExecuteReport(reportId, Location, OnlyRunFunctionIds, FunctionTypesToSkip, parameters.ToList<ReportParameterDTO>());
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000047EC File Offset: 0x000029EC
		public RunReportResultDTO ExecuteReport(ReportDTO report, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			return this.ExecuteReport(report, Location, null, FunctionTypesToSkip, parameters);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000047FC File Offset: 0x000029FC
		public RunReportResultDTO ExecuteReport(ReportDTO report, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			ExecuteReportReq executeReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportReq>();
			executeReportReq.FunctionTypesToSkip = FunctionTypesToSkip;
			BaseReportMessageReq baseReportMessageReq = executeReportReq;
			ApplicationContext applicationContext = executeReportReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			executeReportReq.ReportParameters = parameters;
			executeReportReq.Report = report;
			executeReportReq.OnlyRunFunctionIds = OnlyRunFunctionIds;
			executeReportReq.ExecutedFromLocation = Location;
			RunReportResultDTO runReportResultDTO = base.Post<ExecuteReportReq, ExecuteReportResp>(executeReportReq, "reports/execute").ReportResult;
			ReportExecutionPlanDTO executionPlan = runReportResultDTO.ExecutionPlan;
			if (((executionPlan != null) ? executionPlan.ExecutionSteps : null) != null)
			{
				if (runReportResultDTO.ExecutionPlan.ExecutionSteps.Count((ExecuteReportPlanItemDTO g) => !g.HasCompleted) > 0)
				{
					runReportResultDTO = this.FinishReportExecutionPlan(runReportResultDTO, runReportResultDTO.ExecutionPlan, Location, OnlyRunFunctionIds, FunctionTypesToSkip);
				}
			}
			return runReportResultDTO;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000048BC File Offset: 0x00002ABC
		public RunReportResultDTO FinishReportExecutionPlan(RunReportResultDTO reportResult, ReportExecutionPlanDTO executionPlan, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip)
		{
			if (executionPlan.ExecutionSteps.All((ExecuteReportPlanItemDTO g) => g.HasCompleted))
			{
				return reportResult;
			}
			ReportExecutionPlanDTO reportExecutionPlanDTO = executionPlan;
			RunReportResultDTO runReportResultDTO = reportResult;
			int num = 0;
			for (;;)
			{
				num++;
				if (num > 10000)
				{
					break;
				}
				ExecuteReportPlanItemDTO executeReportPlanItemDTO = reportExecutionPlanDTO.ExecutionSteps.FirstOrDefault((ExecuteReportPlanItemDTO g) => !g.HasCompleted);
				if (executeReportPlanItemDTO == null)
				{
					return runReportResultDTO;
				}
				runReportResultDTO = this.ExecuteReportWithExecutionPlanOneStep(!executeReportPlanItemDTO.RunOnClient, reportExecutionPlanDTO, runReportResultDTO, Location, OnlyRunFunctionIds, FunctionTypesToSkip);
				if (runReportResultDTO.ExecutionPlan == null)
				{
					return runReportResultDTO;
				}
				if (runReportResultDTO.ExecutionPlan.ExecutionSteps.All((ExecuteReportPlanItemDTO g) => g.HasCompleted) || runReportResultDTO.ReportStatus == null || runReportResultDTO.ReportStatus.LastStatusStep != eRunStatusStepDTO.CompletedSuccessfully)
				{
					return runReportResultDTO;
				}
				reportExecutionPlanDTO = runReportResultDTO.ExecutionPlan;
			}
			return runReportResultDTO;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000049AC File Offset: 0x00002BAC
		public int CreateReportGroup(ReportGroupDTO Group)
		{
			return base.Post<ReportGroupDTO, int>(Group, "report/createreportgroup");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000049BA File Offset: 0x00002BBA
		public int CreateReport(ReportDTO Report)
		{
			return base.Post<ReportDTO, int>(Report, "reports/createreport");
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000049C8 File Offset: 0x00002BC8
		public ReportDTO LoadReport(int ReportId)
		{
			return base.Get<ReportDTO>(string.Format("reports/reportid/{0}", ReportId), true);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000049E1 File Offset: 0x00002BE1
		public ReportCollectionDTO LoadReportsInAGroup(params string[] GroupTitles)
		{
			return base.Get<ReportCollectionDTO>(string.Format("reports/grouptitles/{0}", GroupTitles.CommaSeparatedValuesWithoutSpace<string>()), true);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000049FA File Offset: 0x00002BFA
		public ReportCollectionDTO LoadReportsInAGroup(int groupId)
		{
			return base.Get<ReportCollectionDTO>(string.Format("reports/inagroup/groupid/{0}", groupId), true);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004A13 File Offset: 0x00002C13
		public void DeleteReport(int ReportId)
		{
			base.Delete(string.Format("reports/reportid/{0}", ReportId));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004A2B File Offset: 0x00002C2B
		public void UpdateReport(ReportDTO Report)
		{
			base.Put<ReportDTO>(Report, "reports");
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004A3C File Offset: 0x00002C3C
		public void RecordReportExecution(int ReportId, eReportExecutedFromLocation Location)
		{
			RecordReportExecutionReq recordReportExecutionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RecordReportExecutionReq>();
			recordReportExecutionReq.BinPath = ((recordReportExecutionReq.ApplicationContext != null) ? recordReportExecutionReq.ApplicationContext.ExecutingPath : null);
			recordReportExecutionReq.ReportId = ReportId;
			recordReportExecutionReq.ExectedFrom = Location;
			base.Post<RecordReportExecutionReq>(recordReportExecutionReq, "reports/recordexecution");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004A8A File Offset: 0x00002C8A
		public void DeleteClientReportGroup(int ReportGroupid)
		{
			base.Delete(string.Format("reports/clientreportgroup/reportgroupid/{0}", ReportGroupid));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004AA2 File Offset: 0x00002CA2
		public string LoadReportTechnoProNote(int ReportId)
		{
			return base.Get<string>(string.Format("reports/technopronote/reportid/{0}", ReportId), true);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004ABC File Offset: 0x00002CBC
		public void SaveReportTechnoProNote(int ReportId, string Rtf)
		{
			SaveReportTechnoProNoteReq saveReportTechnoProNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveReportTechnoProNoteReq>();
			BaseReportMessageReq baseReportMessageReq = saveReportTechnoProNoteReq;
			ApplicationContext applicationContext = saveReportTechnoProNoteReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			saveReportTechnoProNoteReq.ReportId = ReportId;
			saveReportTechnoProNoteReq.Rtf = Rtf;
			base.Post<SaveReportTechnoProNoteReq>(saveReportTechnoProNoteReq, "reports/technopronote");
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004B08 File Offset: 0x00002D08
		public bool TryToCompileCSharpLegacy(string code, out IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors)
		{
			CompileCSharpScript2Resp compileCSharpScript2Resp = base.Post<string, CompileCSharpScript2Resp>(code, "reports/compilecsharpscript2");
			WarningsOrErrors = compileCSharpScript2Resp.WarningsOrErrors;
			return compileCSharpScript2Resp.CompileSucceeded;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004B30 File Offset: 0x00002D30
		public bool TryToCompileCSharp(string code, IList<string> imports, out IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors)
		{
			TryToCompileCSharpReq tryToCompileCSharpReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToCompileCSharpReq>();
			BaseReportMessageReq baseReportMessageReq = tryToCompileCSharpReq;
			ApplicationContext applicationContext = tryToCompileCSharpReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			tryToCompileCSharpReq.Code = code;
			tryToCompileCSharpReq.Imports = imports;
			TryToCompileCSharpResp tryToCompileCSharpResp = base.Post<TryToCompileCSharpReq, TryToCompileCSharpResp>(tryToCompileCSharpReq, "reports/trytocompilecsharp");
			if (tryToCompileCSharpResp == null)
			{
				WarningsOrErrors = null;
				return false;
			}
			WarningsOrErrors = tryToCompileCSharpResp.WarningsOrErrors;
			return tryToCompileCSharpResp.CompileSucceeded;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004B94 File Offset: 0x00002D94
		public RunFunctionDataDTO ExecuteReportFunction(eFunctionType FunctionToExecute, IList<ReportParameterDTO> FunctionParameters, RunFunctionDataDTO CurrentData)
		{
			ExecuteReportFunctionReq executeReportFunctionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportFunctionReq>();
			BaseReportMessageReq baseReportMessageReq = executeReportFunctionReq;
			ApplicationContext applicationContext = executeReportFunctionReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			executeReportFunctionReq.FunctionToExecute = FunctionToExecute;
			executeReportFunctionReq.FunctionParameters = FunctionParameters;
			executeReportFunctionReq.CurrentData = CurrentData;
			return base.Post<ExecuteReportFunctionReq, RunFunctionDataDTO>(executeReportFunctionReq, "reports/executereportfunction");
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public int CreateClientReportBuiltByTpro(ReportDTO Report, byte[] BuiltByTproSignedAndEncrypted)
		{
			CreateClientReportBuiltByTproReq createClientReportBuiltByTproReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateClientReportBuiltByTproReq>();
			createClientReportBuiltByTproReq.BinPath = ((createClientReportBuiltByTproReq.ApplicationContext != null) ? createClientReportBuiltByTproReq.ApplicationContext.ExecutingPath : null);
			createClientReportBuiltByTproReq.Report = Report;
			createClientReportBuiltByTproReq.BuiltByTproSignedAndEncrypted = BuiltByTproSignedAndEncrypted;
			return base.Post<CreateClientReportBuiltByTproReq, int>(createClientReportBuiltByTproReq, "reports/createclientreportbuiltbytpro");
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004C38 File Offset: 0x00002E38
		public void UpdateClientReportBuiltByTpro(ReportDTO Report, byte[] BuiltByTproSignedAndEncrypted)
		{
			UpdateClientReportBuiltByTproReq updateClientReportBuiltByTproReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateClientReportBuiltByTproReq>();
			updateClientReportBuiltByTproReq.BinPath = ((updateClientReportBuiltByTproReq.ApplicationContext != null) ? updateClientReportBuiltByTproReq.ApplicationContext.ExecutingPath : null);
			updateClientReportBuiltByTproReq.Report = Report;
			updateClientReportBuiltByTproReq.BuiltByTproSignedAndEncrypted = BuiltByTproSignedAndEncrypted;
			base.Put<UpdateClientReportBuiltByTproReq>(updateClientReportBuiltByTproReq, "reports/clientreportbuiltbytpro");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004C86 File Offset: 0x00002E86
		public bool ValidateClientReportBuiltByTproIsNotTamperedWith(int ReportId)
		{
			return base.Get<bool>(string.Format("reports/validateclientreportbuiltbytproisnottamperedwith/reportid/{0}", ReportId), true);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public bool RevertClientReportBuiltByTproToLastTproChange(int ReportId)
		{
			RevertClientReportBuiltByTproToLastTproChangeReq revertClientReportBuiltByTproToLastTproChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RevertClientReportBuiltByTproToLastTproChangeReq>();
			revertClientReportBuiltByTproToLastTproChangeReq.BinPath = ((revertClientReportBuiltByTproToLastTproChangeReq.ApplicationContext != null) ? revertClientReportBuiltByTproToLastTproChangeReq.ApplicationContext.ExecutingPath : null);
			revertClientReportBuiltByTproToLastTproChangeReq.ReportId = ReportId;
			return base.Post<RevertClientReportBuiltByTproToLastTproChangeReq, bool>(revertClientReportBuiltByTproToLastTproChangeReq, "reports/revertclientreportbuiltbytprotolasttprochange");
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public ReportDTO CreateReportClone(int ReportId)
		{
			CreateReportCloneReq createReportCloneReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateReportCloneReq>();
			createReportCloneReq.BinPath = ((createReportCloneReq.ApplicationContext != null) ? createReportCloneReq.ApplicationContext.ExecutingPath : null);
			createReportCloneReq.ReportId = ReportId;
			return base.Post<CreateReportCloneReq, ReportDTO>(createReportCloneReq, "reports/createreportclone");
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004D30 File Offset: 0x00002F30
		public string ExportReportToXmlForUser(params int[] ReportIds)
		{
			ExportReportToXmlForUserReq exportReportToXmlForUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExportReportToXmlForUserReq>();
			BaseReportMessageReq baseReportMessageReq = exportReportToXmlForUserReq;
			ApplicationContext applicationContext = exportReportToXmlForUserReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			exportReportToXmlForUserReq.ReportIds = ReportIds;
			return base.Post<ExportReportToXmlForUserReq, string>(exportReportToXmlForUserReq, "reports/exportreporttoxmlforuser");
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004D74 File Offset: 0x00002F74
		public string ExportReportToXmlForUpdatingSystem(params int[] ReportIds)
		{
			ExportReportToXmlForUpdatingSystemReq exportReportToXmlForUpdatingSystemReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExportReportToXmlForUpdatingSystemReq>();
			exportReportToXmlForUpdatingSystemReq.BinPath = ((exportReportToXmlForUpdatingSystemReq.ApplicationContext != null) ? exportReportToXmlForUpdatingSystemReq.ApplicationContext.ExecutingPath : null);
			exportReportToXmlForUpdatingSystemReq.ReportIds = ReportIds;
			return base.Post<ExportReportToXmlForUpdatingSystemReq, string>(exportReportToXmlForUpdatingSystemReq, "reports/exportreporttoxmlforupdatingsystem");
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004DBC File Offset: 0x00002FBC
		public int CloneReport(int ReportId)
		{
			CloneReportReq cloneReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CloneReportReq>();
			BaseReportMessageReq baseReportMessageReq = cloneReportReq;
			ApplicationContext applicationContext = cloneReportReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			cloneReportReq.ReportId = ReportId;
			return base.Post<CloneReportReq, int>(cloneReportReq, "reports/clonereport");
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004E00 File Offset: 0x00003000
		public IDictionary<string, int> ImportReportFromXmlForUser(string Xml, int ParentGroupId = 0)
		{
			ImportReportFromXmlForUserReq importReportFromXmlForUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportReportFromXmlForUserReq>();
			importReportFromXmlForUserReq.BinPath = ((importReportFromXmlForUserReq.ApplicationContext != null) ? importReportFromXmlForUserReq.ApplicationContext.ExecutingPath : null);
			importReportFromXmlForUserReq.Xml = Xml;
			importReportFromXmlForUserReq.ParentGroupId = ParentGroupId;
			return base.Post<ImportReportFromXmlForUserReq, ImportReportFromXmlForUserResp>(importReportFromXmlForUserReq, "reports/importreportfromxmlforuser").UniqueIdsAndNewReportIds;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004E53 File Offset: 0x00003053
		public string ExportReportsToXmlForUserFromReports(ReportCollectionDTO reportCollection)
		{
			return base.Post<ReportCollectionDTO, string>(reportCollection, "reports/exportreporttoxmlforuserfromreports");
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004E61 File Offset: 0x00003061
		public Forest<ReportGroupDTO> LoadReportGroupForest(ReportContextDTO reportContext)
		{
			return base.Post<ReportContextDTO, LoadReportGroupForestResp>(reportContext, "reports/loadreportgroupforest").ReportGroups;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004E74 File Offset: 0x00003074
		public int ChangeReportOrderInSameReportGroup(int ReportIdToMove, int ReportIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			ChangeReportOrderInSameReportGroupReq changeReportOrderInSameReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeReportOrderInSameReportGroupReq>();
			changeReportOrderInSameReportGroupReq.BinPath = ((changeReportOrderInSameReportGroupReq.ApplicationContext != null) ? changeReportOrderInSameReportGroupReq.ApplicationContext.ExecutingPath : null);
			changeReportOrderInSameReportGroupReq.moveAfter = moveAfter;
			changeReportOrderInSameReportGroupReq.ReportIdToMove = ReportIdToMove;
			changeReportOrderInSameReportGroupReq.ReportIdToMoveBeforeOrAfter = ReportIdToMoveBeforeOrAfter;
			return base.Post<ChangeReportOrderInSameReportGroupReq, int>(changeReportOrderInSameReportGroupReq, "reports/changereportorderinsamereportgroup");
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004ECC File Offset: 0x000030CC
		public int ChangeReportGroupOrderInSameReportGroup(int ReportGroupIdToMove, int ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			ChangeReportGroupOrderInSameReportGroupReq changeReportGroupOrderInSameReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeReportGroupOrderInSameReportGroupReq>();
			BaseReportMessageReq baseReportMessageReq = changeReportGroupOrderInSameReportGroupReq;
			ApplicationContext applicationContext = changeReportGroupOrderInSameReportGroupReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			changeReportGroupOrderInSameReportGroupReq.MoveAfter = moveAfter;
			changeReportGroupOrderInSameReportGroupReq.ReportGroupIdToMove = ReportGroupIdToMove;
			changeReportGroupOrderInSameReportGroupReq.ReportGroupIdToMoveBeforeOrAfter = ReportGroupIdToMoveBeforeOrAfter;
			return base.Post<ChangeReportGroupOrderInSameReportGroupReq, int>(changeReportGroupOrderInSameReportGroupReq, "reports/changereportgrouporderinsamereportgroup");
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004F20 File Offset: 0x00003120
		public ReportDTO MoveReport(int ReportIdToMove, int NewReportParentGroupId, int? ReportIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			MoveReportReq moveReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MoveReportReq>();
			moveReportReq.BinPath = ((moveReportReq.ApplicationContext != null) ? moveReportReq.ApplicationContext.ExecutingPath : null);
			moveReportReq.MoveAfter = moveAfter;
			moveReportReq.ReportIdToMove = ReportIdToMove;
			moveReportReq.NewReportParentGroupId = NewReportParentGroupId;
			moveReportReq.ReportIdToMoveBeforeOrAfter = ReportIdToMoveBeforeOrAfter;
			return base.Post<MoveReportReq, ReportDTO>(moveReportReq, "reports/movereport");
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004F80 File Offset: 0x00003180
		public ReportGroupDTO MoveReportGroup(int ReportGroupIdToMove, int NewReportParentGroupId, int? ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true)
		{
			MoveReportGroupReq moveReportGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MoveReportGroupReq>();
			moveReportGroupReq.BinPath = ((moveReportGroupReq.ApplicationContext != null) ? moveReportGroupReq.ApplicationContext.ExecutingPath : null);
			moveReportGroupReq.MoveAfter = moveAfter;
			moveReportGroupReq.ReportGroupIdToMove = ReportGroupIdToMove;
			moveReportGroupReq.ReportGroupIdToMoveBeforeOrAfter = ReportGroupIdToMoveBeforeOrAfter;
			moveReportGroupReq.NewReportParentGroupId = NewReportParentGroupId;
			return base.Post<MoveReportGroupReq, ReportGroupDTO>(moveReportGroupReq, "reports/movereportgroup");
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004FE0 File Offset: 0x000031E0
		public void SortReportGroupMembersAlphabetically(int ParentReportGroupId)
		{
			SortReportGroupMembersAlphabeticallyReq sortReportGroupMembersAlphabeticallyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SortReportGroupMembersAlphabeticallyReq>();
			sortReportGroupMembersAlphabeticallyReq.BinPath = ((sortReportGroupMembersAlphabeticallyReq.ApplicationContext != null) ? sortReportGroupMembersAlphabeticallyReq.ApplicationContext.ExecutingPath : null);
			sortReportGroupMembersAlphabeticallyReq.ParentReportGroupId = ParentReportGroupId;
			base.Post<SortReportGroupMembersAlphabeticallyReq>(sortReportGroupMembersAlphabeticallyReq, "reports/sortreportgroupmembersalphabetically");
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005028 File Offset: 0x00003228
		private RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters)
		{
			ExecuteReportReq executeReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportReq>();
			executeReportReq.FunctionTypesToSkip = FunctionTypesToSkip;
			executeReportReq.BinPath = ((executeReportReq.ApplicationContext != null) ? executeReportReq.ApplicationContext.ExecutingPath : null);
			executeReportReq.ReportParameters = parameters;
			executeReportReq.ReportId = reportId;
			executeReportReq.OnlyRunFunctionIds = OnlyRunFunctionIds;
			executeReportReq.ExecutedFromLocation = Location;
			RunReportResultDTO runReportResultDTO = base.Post<ExecuteReportReq, ExecuteReportResp>(executeReportReq, "reports/execute").ReportResult;
			ReportExecutionPlanDTO executionPlan = runReportResultDTO.ExecutionPlan;
			if (((executionPlan != null) ? executionPlan.ExecutionSteps : null) != null)
			{
				if (runReportResultDTO.ExecutionPlan.ExecutionSteps.Count((ExecuteReportPlanItemDTO g) => !g.HasCompleted) > 0)
				{
					runReportResultDTO = this.FinishReportExecutionPlan(runReportResultDTO, runReportResultDTO.ExecutionPlan, Location, OnlyRunFunctionIds, FunctionTypesToSkip);
				}
			}
			return runReportResultDTO;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000050EC File Offset: 0x000032EC
		private RunReportResultDTO ExecuteReportWithExecutionPlanOneStep(bool runOnServer, ReportExecutionPlanDTO executionPlan, RunReportResultDTO reportResult, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip)
		{
			ExecuteReportReq executeReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportReq>();
			executeReportReq.FunctionTypesToSkip = FunctionTypesToSkip;
			executeReportReq.BinPath = ((executeReportReq.ApplicationContext != null) ? executeReportReq.ApplicationContext.ExecutingPath : null);
			executeReportReq.OnlyRunFunctionIds = OnlyRunFunctionIds;
			executeReportReq.ExecutedFromLocation = Location;
			executeReportReq.ExecutionPlan = executionPlan;
			executeReportReq.PreviousRunReportResult = reportResult;
			ExecuteReportResp executeReportResp = base.Post<ExecuteReportReq, ExecuteReportResp>(executeReportReq, "reports/execute");
			if (((executeReportResp != null) ? executeReportResp.ReportResult : null) != null)
			{
				executeReportResp.ReportResult.ExecutionPlan = executeReportResp.ExecutionPlan;
			}
			if (executeReportResp == null)
			{
				return null;
			}
			return executeReportResp.ReportResult;
		}
	}
}
