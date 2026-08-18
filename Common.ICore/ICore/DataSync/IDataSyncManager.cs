using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.Notetaking;
using TechnoPro.Common.Public.Entities.DataSync.Student;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.ICore.DataSync
{
	// Token: 0x020000A9 RID: 169
	public interface IDataSyncManager : IBaseOperationContext<DataSyncOperationContext>
	{
		// Token: 0x060004F1 RID: 1265
		DataSyncResult RunBatchDataSync();

		// Token: 0x060004F2 RID: 1266
		void RunBatchDataSync(DataTable studentsTable, DataSyncBatchParameters batchSyncParameters);

		// Token: 0x060004F3 RID: 1267
		void RunBatchDataSyncForOldCourses(DataTable studentsTable, DataSyncBatchParameters batchSyncParameters);

		// Token: 0x060004F4 RID: 1268
		RunReportResult RunMoveDataIntoClockWork();

		// Token: 0x060004F5 RID: 1269
		DataSyncResult RunCourseDataSyncByStudentNumber(string Student_no);

		// Token: 0x060004F6 RID: 1270
		DataSyncResult RunCourseDataSyncById(int pid);

		// Token: 0x060004F7 RID: 1271
		DataSyncResult RunFullDataSyncForExistingStudent(string Student_no, bool DontSyncData, bool DontSyncCourses);

		// Token: 0x060004F8 RID: 1272
		DataSyncPreviewResult PreviewDataSyncData(string Student_no);

		// Token: 0x060004F9 RID: 1273
		DataTable LoadCustomDataByEncryptedLookupField(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string LookupFieldPlainText, string ExternalColumnNameForEncryptedLookupField, params string[] ExternalColumnsToReturnNullForAll);

		// Token: 0x060004FA RID: 1274
		DataTable LoadCustomData(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string StudentNumber, string ExternalColumnNameForStudentNumber);

		// Token: 0x060004FB RID: 1275
		DataTable LoadCustomDataWithCustomSql(string Sql, string StudentNumber);

		// Token: 0x060004FC RID: 1276
		NotetakerWithExternalCourses GetNotetakerPreviewData(string UserName);

		// Token: 0x060004FD RID: 1277
		IList<DataSyncExternalCourse> GetNotetakerPreviewExternalCoursesByUserName(string UserName);

		// Token: 0x060004FE RID: 1278
		IList<DataSyncExternalCourse> GetNotetakerPreviewExternalCoursesByStudentNumber(string StudentNumber);

		// Token: 0x060004FF RID: 1279
		NotetakerWithExternalCourses GetNotetakerPreviewDataByStudentNumber(string UserName, string StudentNumber);

		// Token: 0x06000500 RID: 1280
		void CopyCsvDataToCustomTable(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders);

		// Token: 0x06000501 RID: 1281
		void CopyTabDelimitedDataToCustomTable(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders);

		// Token: 0x06000502 RID: 1282
		void CopyCharacterDelimitedDataToCustomTable(char Delimiter, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInFile, bool FirstRowHasHeaders, params string[] FileColumnNamesIfNotFirstRowHasHeaders);

		// Token: 0x06000503 RID: 1283
		StudentDataSyncPreviewData GetStudentPreviewDataByStudentNumberOrUsername(string UserName, string StudentNumber);

		// Token: 0x06000504 RID: 1284
		void CopyXmlDataToCustomData<T>(string fileName, string[] headerRow, Func<T, string[][]> convertForStorage, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string colNameWithStudentNumber);

		// Token: 0x06000505 RID: 1285
		DataTable ConvertObjectToDataRows<T>() where T : class;

		// Token: 0x06000506 RID: 1286
		DataTable ConvertObjectToDataRows<T>(T item) where T : class;

		// Token: 0x06000507 RID: 1287
		string[] LoadCustomTableNames();

		// Token: 0x06000508 RID: 1288
		string[] LoadCustomExternalColumnNames(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
	}
}
