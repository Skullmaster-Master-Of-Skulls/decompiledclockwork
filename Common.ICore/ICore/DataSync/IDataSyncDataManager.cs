using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncData;

namespace TechnoPro.Common.ICore.DataSync
{
	// Token: 0x020000A7 RID: 167
	public interface IDataSyncDataManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004E6 RID: 1254
		DataSyncDataResult DataSyncData(string studentNumber, DataTable t);

		// Token: 0x060004E7 RID: 1255
		BatchDataSyncDataResult DataSyncBatchDataAndCourses(IList<string> studentNumbers);

		// Token: 0x060004E8 RID: 1256
		void DataSyncDataLegacy(string studentNumber);

		// Token: 0x060004E9 RID: 1257
		void DataSyncIntakeData(string studentNumber, bool deleteIntakeEntry = true);
	}
}
