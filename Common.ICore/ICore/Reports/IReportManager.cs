using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.ICore.Reports
{
	// Token: 0x02000026 RID: 38
	public interface IReportManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000F8 RID: 248
		Forest<ReportOrGroup> LoadReportForest(ReportContext ReportContext);

		// Token: 0x060000F9 RID: 249
		ReportCollection LoadReports(ReportContext ReportContext);

		// Token: 0x060000FA RID: 250
		Report LoadReport(int ReportId);

		// Token: 0x060000FB RID: 251
		Forest<ReportOrGroup> LoadReportForestBySource(string ReportXml, ReportContext ReportContext);

		// Token: 0x060000FC RID: 252
		ReportCollection LoadReportsInAGroup(params string[] GroupTitles);

		// Token: 0x060000FD RID: 253
		RunReportResult ExecuteReport2(int reportId, params ReportParameter[] parameters);

		// Token: 0x060000FE RID: 254
		RunReportResult ExecuteReport2(int reportId, IList<eFunctionType> FunctionTypesToSkip, params ReportParameter[] parameters);

		// Token: 0x060000FF RID: 255
		RunReportResult ExecuteReport2(int reportId, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, params ReportParameter[] parameters);

		// Token: 0x06000100 RID: 256
		RunReportResult ExecuteReport2(Report Report, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip, params ReportParameter[] parameters);

		// Token: 0x06000101 RID: 257
		int CreateReportGroup(ReportGroup Group);

		// Token: 0x06000102 RID: 258
		int CreateReport(Report Report);

		// Token: 0x06000103 RID: 259
		void UpdateReport(Report Report);

		// Token: 0x06000104 RID: 260
		void DeleteReport(int ReportId);

		// Token: 0x06000105 RID: 261
		void RecordReportExecution(ReportExecutionContext Context);

		// Token: 0x06000106 RID: 262
		void DeleteClientReportGroup(int ReportGroupId);

		// Token: 0x06000107 RID: 263
		string LoadReportTechnoProNote(int ReportId);

		// Token: 0x06000108 RID: 264
		void SaveReportTechnoProNote(int ReportId, string Rtf);

		// Token: 0x06000109 RID: 265
		IList<ReportCompileLineWarningOrError> TryToCompileCSharp(string Code, IList<string> Imports, out bool Successful);

		// Token: 0x0600010A RID: 266
		RunReportResult ExecuteReport2(RunReportResult PreviousRunReportResult, IList<int> OnlyRunFunctionIds, IList<eFunctionType> FunctionTypesToSkip);

		// Token: 0x0600010B RID: 267
		RunFunctionData ExecuteReportFunction(eFunctionType FunctionToExecute, IList<ReportParameter> FunctionParameters, RunFunctionData CurrentData);

		// Token: 0x0600010C RID: 268
		int CreateClientReportBuiltByTpro(Report Report, byte[] BuiltByTproSignedAndEncrypted);

		// Token: 0x0600010D RID: 269
		void UpdateClientReportBuiltByTpro(Report Report, byte[] BuiltByTproSignedAndEncrypted);

		// Token: 0x0600010E RID: 270
		bool ValidateClientReportBuiltByTproIsNotTamperedWith(int ReportId);

		// Token: 0x0600010F RID: 271
		bool RevertClientReportBuiltByTproToLastTproChange(int ReportId);

		// Token: 0x06000110 RID: 272
		Report CreateReportClone(int ReportId);

		// Token: 0x06000111 RID: 273
		string ExportReportToXmlForUser(params int[] ReportIds);

		// Token: 0x06000112 RID: 274
		string ExportReportToXmlForUpdatingSystem(params int[] ReportIds);

		// Token: 0x06000113 RID: 275
		IDictionary<int, int> CloneReports(params int[] ReportIds);

		// Token: 0x06000114 RID: 276
		int CloneReport(int ReportId);

		// Token: 0x06000115 RID: 277
		IDictionary<string, int> ImportReportFromXmlForUser(string Xml, int ParentGroupId = 0);

		// Token: 0x06000116 RID: 278
		IDictionary<string, int> ImportReportsFromXmlForUpdatingSystem(string Xml, int OverrideParentGroupId = 2000000033);

		// Token: 0x06000117 RID: 279
		string ExportReportsToXmlForUser(ReportCollection reportCollection);

		// Token: 0x06000118 RID: 280
		Forest<ReportGroup> LoadReportGroupForest(ReportContext ReportContext);

		// Token: 0x06000119 RID: 281
		int ChangeReportOrderInSameReportGroup(int ReportIdToMove, int ReportIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x0600011A RID: 282
		int ChangeReportGroupOrderInSameReportGroup(int ReportGroupIdToMove, int ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x0600011B RID: 283
		Report MoveReport(int ReportIdToMove, int NewReportParentGroupId, int? ReportIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x0600011C RID: 284
		ReportGroup MoveGroup(int ReportGroupIdToMove, int NewReportParentGroupId, int? ReportGroupIdToMoveBeforeOrAfter, bool moveAfter = true);

		// Token: 0x0600011D RID: 285
		void SortReportGroupMembersAlphabetically(int ParentReportGroupId);

		// Token: 0x0600011E RID: 286
		ReportCollection LoadReportsInAGroup(params int[] GroupIds);
	}
}
