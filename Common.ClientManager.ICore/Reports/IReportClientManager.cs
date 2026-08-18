using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.ClientManager.ICore.Reports
{
	// Token: 0x02000025 RID: 37
	public interface IReportClientManager : IWebService
	{
		// Token: 0x060000D3 RID: 211
		Forest<ReportOrGroupDTO> LoadReportForest(ReportContextDTO ReportContext);

		// Token: 0x060000D4 RID: 212
		ReportCollectionDTO LoadReports(ReportContextDTO ReportContext);

		// Token: 0x060000D5 RID: 213
		Forest<ReportOrGroupDTO> LoadReportForestBySource(string ReportXml, ReportContextDTO ReportContext);

		// Token: 0x060000D6 RID: 214
		RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, params ReportParameterDTO[] parameters);

		// Token: 0x060000D7 RID: 215
		RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, params ReportParameterDTO[] parameters);

		// Token: 0x060000D8 RID: 216
		RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters);

		// Token: 0x060000D9 RID: 217
		RunReportResultDTO ExecuteReport(int reportId, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, params ReportParameterDTO[] parameters);

		// Token: 0x060000DA RID: 218
		RunReportResultDTO ExecuteReport(ReportDTO report, eReportExecutedFromLocation Location, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters);

		// Token: 0x060000DB RID: 219
		RunReportResultDTO ExecuteReport(ReportDTO report, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> parameters);

		// Token: 0x060000DC RID: 220
		RunReportResultDTO FinishReportExecutionPlan(RunReportResultDTO reportResult, ReportExecutionPlanDTO executionPlan, eReportExecutedFromLocation Location, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip);

		// Token: 0x060000DD RID: 221
		int CreateReportGroup(ReportGroupDTO Group);

		// Token: 0x060000DE RID: 222
		int CreateReport(ReportDTO Report);

		// Token: 0x060000DF RID: 223
		ReportDTO LoadReport(int ReportId);

		// Token: 0x060000E0 RID: 224
		ReportCollectionDTO LoadReportsInAGroup(params string[] GroupTitles);

		// Token: 0x060000E1 RID: 225
		ReportCollectionDTO LoadReportsInAGroup(int groupId);

		// Token: 0x060000E2 RID: 226
		void DeleteReport(int ReportId);

		// Token: 0x060000E3 RID: 227
		void UpdateReport(ReportDTO Report);

		// Token: 0x060000E4 RID: 228
		void RecordReportExecution(int ReportId, eReportExecutedFromLocation Location);

		// Token: 0x060000E5 RID: 229
		void DeleteClientReportGroup(int ReportGroupid);

		// Token: 0x060000E6 RID: 230
		string LoadReportTechnoProNote(int ReportId);

		// Token: 0x060000E7 RID: 231
		void SaveReportTechnoProNote(int ReportId, string Rtf);

		// Token: 0x060000E8 RID: 232
		bool TryToCompileCSharpLegacy(string code, out IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors);

		// Token: 0x060000E9 RID: 233
		bool TryToCompileCSharp(string code, IList<string> imports, out IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors);

		// Token: 0x060000EA RID: 234
		RunFunctionDataDTO ExecuteReportFunction(eFunctionType FunctionToExecute, IList<ReportParameterDTO> FunctionParameters, RunFunctionDataDTO CurrentData);

		// Token: 0x060000EB RID: 235
		int CreateClientReportBuiltByTpro(ReportDTO Report, byte[] BuiltByTproSignedAndEncrypted);

		// Token: 0x060000EC RID: 236
		void UpdateClientReportBuiltByTpro(ReportDTO Report, byte[] BuiltByTproSignedAndEncrypted);

		// Token: 0x060000ED RID: 237
		bool ValidateClientReportBuiltByTproIsNotTamperedWith(int ReportId);

		// Token: 0x060000EE RID: 238
		bool RevertClientReportBuiltByTproToLastTproChange(int ReportId);

		// Token: 0x060000EF RID: 239
		ReportDTO CreateReportClone(int ReportId);

		// Token: 0x060000F0 RID: 240
		string ExportReportToXmlForUser(params int[] ReportIds);

		// Token: 0x060000F1 RID: 241
		string ExportReportToXmlForUpdatingSystem(params int[] ReportIds);

		// Token: 0x060000F2 RID: 242
		int CloneReport(int ReportId);

		// Token: 0x060000F3 RID: 243
		IDictionary<string, int> ImportReportFromXmlForUser(string Xml, int ParentGroupId = 0);

		// Token: 0x060000F4 RID: 244
		string ExportReportsToXmlForUserFromReports(ReportCollectionDTO reportCollection);

		// Token: 0x060000F5 RID: 245
		Forest<ReportGroupDTO> LoadReportGroupForest(ReportContextDTO reportContext);

		// Token: 0x060000F6 RID: 246
		int ChangeReportOrderInSameReportGroup(int ReportIdToMove, int ReportIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x060000F7 RID: 247
		int ChangeReportGroupOrderInSameReportGroup(int ReportGroupIdToMove, int ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x060000F8 RID: 248
		ReportDTO MoveReport(int ReportIdToMove, int NewReportParentGroupId, int? ReportIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x060000F9 RID: 249
		ReportGroupDTO MoveReportGroup(int ReportGroupIdToMove, int NewReportParentGroupId, int? ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x060000FA RID: 250
		void SortReportGroupMembersAlphabetically(int ParentReportGroupId);
	}
}
