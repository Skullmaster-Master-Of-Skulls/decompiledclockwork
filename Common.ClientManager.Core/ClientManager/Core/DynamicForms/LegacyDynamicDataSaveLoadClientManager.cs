using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x0200006B RID: 107
	public class LegacyDynamicDataSaveLoadClientManager : ILegacyDynamicDataSaveLoadClientManager, IWebService
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0001197C File Offset: 0x0000FB7C
		public IList<LegacySaveDataResultDTO> SaveDataPS(LegacyDynamicDataRowDatasDTO legacyData, string tableName, int screenNum, int studentPid, int whoModifiedPid, bool tablesStoreScreenNum)
		{
			SaveDataPSReq saveDataPSReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveDataPSReq>();
			saveDataPSReq.LegacyData = legacyData;
			saveDataPSReq.TableName = tableName;
			saveDataPSReq.ScreenNum = screenNum;
			saveDataPSReq.StudentPid = studentPid;
			saveDataPSReq.WhoModifiedPid = whoModifiedPid;
			saveDataPSReq.TablesStoreScreenNum = tablesStoreScreenNum;
			return ClientServiceFactory.GetClientInstance<ILegacyDynamicDataSaveLoad>().SaveDataPS(saveDataPSReq).SaveDataResults;
		}
	}
}
