using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x02000064 RID: 100
	public interface ILegacyDynamicDataSaveLoadClientManager : IWebService
	{
		// Token: 0x06000305 RID: 773
		IList<LegacySaveDataResultDTO> SaveDataPS(LegacyDynamicDataRowDatasDTO legacyData, string tableName, int screenNum, int studentPid, int whoModifiedPid, bool tablesStoreScreenNum);
	}
}
