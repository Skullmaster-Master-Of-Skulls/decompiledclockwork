using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.DataSync
{
	// Token: 0x020000A8 RID: 168
	public interface IDataSyncInfoManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004EA RID: 1258
		DataSyncInfo LoadDataSyncInfo();

		// Token: 0x060004EB RID: 1259
		IList<DataSyncInfoActionResult> DataSyncInfo(DataSyncInfoSettings Settings, string MapXml, DataTable ExternalDataTable);

		// Token: 0x060004EC RID: 1260
		IList<DataSyncInfoActionResult> DataSyncInfo(DataSyncInfoSettings Settings, IList<DataSyncInfoMapItem> Map, ref IList<DataSyncExternalData> ExternalDataItems);

		// Token: 0x060004ED RID: 1261
		DataTable LoadOnlineIntakeFormData(int ScreenNum, string StudentNumber, out PersonBase StudentInfo);

		// Token: 0x060004EE RID: 1262
		DataTable LoadOnlineIntakeFormDataAndMergeWithExternalData(DataTable existingStudentDataToMergeWithResults, int ScreenNum, string StudentNumber, out PersonBase StudentInfo);

		// Token: 0x060004EF RID: 1263
		DataTable LoadOnlineIntakeFormDataAndMergeWithExternalData(DataTable existingStudentDataToMergeWithResults, int ScreenNum, string StudentNumber);

		// Token: 0x060004F0 RID: 1264
		void DataSyncIntakeData(string student_no);
	}
}
