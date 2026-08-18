using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TechnoPro.Common.DAO.Entity.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.DAO.Reports
{
	// Token: 0x0200003B RID: 59
	public interface IReportDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000F5 RID: 245
		object[] GetReportCodeCompileParameters(RunReportResult CurrentRunReportResult);

		// Token: 0x060000F6 RID: 246
		ReportCollection LoadClientReports(ReportContext reportContext);

		// Token: 0x060000F7 RID: 247
		ReportCollection LoadTproReports(ReportContext reportContext);

		// Token: 0x060000F8 RID: 248
		ReportCollection LoadReportsFromXml(string Xml, ReportContext reportContext);

		// Token: 0x060000F9 RID: 249
		Report LoadClientReportById(int ReportId);

		// Token: 0x060000FA RID: 250
		ReportCollection LoadReportsInAGroup(params string[] GroupTitles);

		// Token: 0x060000FB RID: 251
		ReportCollection LoadReportsInAGroup(params int[] GroupIds);

		// Token: 0x060000FC RID: 252
		DataTable RunReportSql(IList<ReportParameter> reportParameters, string sql);

		// Token: 0x060000FD RID: 253
		DataTable RunReportSql(IList<ReportParameter> reportParameters1, string sql1, int overrideCommandTimeoutInSeconds);

		// Token: 0x060000FE RID: 254
		DataTable RunReportSqlExternal(eExternalQueryDatabaseType dbType, string providerType, string connectionString, string sql, IList<ReportParameter> reportParameters);

		// Token: 0x060000FF RID: 255
		DataTable DecryptData(DataTable t, params string[] colsToDecrypt);

		// Token: 0x06000100 RID: 256
		DataTable EncryptData(DataTable t, params string[] colsToEncrypt);

		// Token: 0x06000101 RID: 257
		DataTable ImportUserData(DataTable lastReportResultDataView, string parameters);

		// Token: 0x06000102 RID: 258
		DataTable ExecuteLegacyFunction(DataTable currentTable, ReportFunction function, string functionParameters);

		// Token: 0x06000103 RID: 259
		int CreateClientReportGroup(ReportGroup Group);

		// Token: 0x06000104 RID: 260
		int CreateClientReport(Report Report);

		// Token: 0x06000105 RID: 261
		int CreateClientReportFunction(int reportId, bool functionParametersAreEncrypted, ReportFunction function, DbTransaction transaction = null);

		// Token: 0x06000106 RID: 262
		void UpdateClientReport(Report Report);

		// Token: 0x06000107 RID: 263
		void UpdateClientReportFunction(ReportFunction ReportFunction, bool FunctionParametersAreEncrypted, DbTransaction transaction = null);

		// Token: 0x06000108 RID: 264
		void DeleteClientReportFunction(int ReportFunctionId, DbTransaction transaction = null);

		// Token: 0x06000109 RID: 265
		void DeleteClientReport(int ReportId);

		// Token: 0x0600010A RID: 266
		void DeleteClientFormattedReport(int FileId, DbTransaction transaction = null);

		// Token: 0x0600010B RID: 267
		void UpdateClientFormattedReport(FormattedReport FormattedReport, DbTransaction transaction = null);

		// Token: 0x0600010C RID: 268
		int CreateClientFormattedReport(int ReportId, FormattedReport FormattedReport, DbTransaction transaction = null);

		// Token: 0x0600010D RID: 269
		void RecordReportExecution(ReportExecutionContext Context);

		// Token: 0x0600010E RID: 270
		void DeleteClientReportGroup(int ReportGroupId);

		// Token: 0x0600010F RID: 271
		string LoadReportTechnoProNote(int ReportId);

		// Token: 0x06000110 RID: 272
		void SaveReportTechnoProNote(int ReportId, string Rtf);

		// Token: 0x06000111 RID: 273
		IList<ReportCompileLineWarningOrError> TryToCompileCSharp(string Code, IList<string> Imports, out bool Successful);

		// Token: 0x06000112 RID: 274
		void UpdateBuiltByTpro(int ReportId, byte[] BuiltByTproSignedAndEncrypted);

		// Token: 0x06000113 RID: 275
		byte[] LoadBuiltByTpro(int ReportId);

		// Token: 0x06000114 RID: 276
		Report LoadReportByUniqueId(string ReportUniqueId);

		// Token: 0x06000115 RID: 277
		void MarkReportChange(Report ReportAfterChange, int WhoChangedPersonId);

		// Token: 0x06000116 RID: 278
		void UpdateReportOrderNum(int ReportId, int NewOrderNum);

		// Token: 0x06000117 RID: 279
		void UpdateGroupOrderNum(int ReportGrouPId, int NewOrderNum);

		// Token: 0x06000118 RID: 280
		void UpdateReportGroup(int ReportId, int NewGroupId);

		// Token: 0x06000119 RID: 281
		void UpdateGroupParent(int ReportGroupId, int NewGroupId);

		// Token: 0x0600011A RID: 282
		ReportGroup LoadClientReportGroupById(int ReportGroupId);

		// Token: 0x0600011B RID: 283
		IList<ReportGroup> LoadGroupsInAGroup(int ReportGroupId);
	}
}
