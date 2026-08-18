using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.DAO.DataSync
{
	// Token: 0x0200008E RID: 142
	public interface IDataSyncDAO : IBaseOperationContext<DataSyncOperationContext>
	{
		// Token: 0x060003A8 RID: 936
		void DeleteAllCustomData(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);

		// Token: 0x060003A9 RID: 937
		IList<string> GetDatabaseCustomColumnNames(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);

		// Token: 0x060003AA RID: 938
		void WriteCustomDataRow(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, IList<string> tableColumnNames, string[] row, int StudentNumberColIndex, params int[] cellIndicesToNotEncrypt);

		// Token: 0x060003AB RID: 939
		DataTable LoadCustomData(string StudentNumber, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string ClockWorkColumnNameForStudentNumber, IList<ExternalInternalColumnMapping> ColumnMappings);

		// Token: 0x060003AC RID: 940
		DataTable LoadCustomDataByEncryptedLookupField(string LookupFieldPlainText, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string LookupFieldClockWorkColName, IList<ExternalInternalColumnMapping> ColumnMappings, IList<ExternalInternalColumnMapping> mapping_fieldsToReturn);

		// Token: 0x060003AD RID: 941
		void WriteCustomDataMappings(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, List<ExternalInternalColumnMapping> ExternalToInternalMappings);

		// Token: 0x060003AE RID: 942
		IList<ExternalInternalColumnMapping> LoadCustomDataMappings(string ClockWorkTableNameWithoutCustomPrefix);

		// Token: 0x060003AF RID: 943
		IList<ExternalInternalColumnMapping> LoadCustomDataMappingsForMultipleTables(params string[] ClockWorkTableNamesWithoutCustomPrefix);

		// Token: 0x060003B0 RID: 944
		DataTable LoadCustomData(string Sql, string StudentNumber);

		// Token: 0x060003B1 RID: 945
		int GetNewBatchDataSyncLogId(int attemptedStudentCount);

		// Token: 0x060003B2 RID: 946
		void UpdateBatchSync(int batchDataSyncLogId, int successfulStudentCount, string errorMessage);
	}
}
