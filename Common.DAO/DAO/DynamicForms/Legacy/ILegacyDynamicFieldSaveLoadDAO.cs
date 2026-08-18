using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.Common.DAO.DynamicForms.Legacy
{
	// Token: 0x02000087 RID: 135
	public interface ILegacyDynamicFieldSaveLoadDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000390 RID: 912
		void LogDataChange(bool deleteOldLogData, int screenNum, int studentPid);

		// Token: 0x06000391 RID: 913
		IList<LegacySaveDataResult> SaveLegacyDataPerStudent(IList<LegacyDynamicDataRowSaveData> todoList, string tableName, bool tablesStoreScreenNum, bool tablesHaveArchiveSets);

		// Token: 0x06000392 RID: 914
		DynamicDataContext LoadDataContext(eDynamicFormType formType, int dataId, int controlId);

		// Token: 0x06000393 RID: 915
		void UpdateStudentFileUploadStatusMarkers(int cid, IDictionary<int, bool> pidsWithHasAtLeastOneFileOpen);

		// Token: 0x06000394 RID: 916
		Task UpdateStudentFileUploadStatusMarkersAsync(int cid, IDictionary<int, bool> pidsWithHasAtLeastOneFileOpen);
	}
}
