using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.Common.ICore.DynamicForms.Legacy
{
	// Token: 0x0200009E RID: 158
	public interface ILegacyDynamicDataSaveLoadManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004B2 RID: 1202
		IList<LegacySaveDataResult> SaveDataPS(LegacyDynamicDataRowDatas legacyData, string tableName, int screenNum, int studentPid, int whoModifiedPid, bool tablesStoreScreenNum);

		// Token: 0x060004B3 RID: 1203
		Pair<eDynamicFormType, DynamicDataContext> GetFormTypeAndDynamicDataContextFromDataIdAndControlId(int dataId, int controlId);
	}
}
