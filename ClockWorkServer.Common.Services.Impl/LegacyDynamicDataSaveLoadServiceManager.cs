using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.Core.DynamicForms.Legacy;
using TechnoPro.Common.Core.Mappers.DynamicForms.Legacy;
using TechnoPro.Common.ICore.DynamicForms.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000043 RID: 67
	public class LegacyDynamicDataSaveLoadServiceManager : ILegacyDynamicDataSaveLoad, IService
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public SaveDataPSResp SaveDataPS(SaveDataPSReq Request)
		{
			ILegacyDynamicDataSaveLoadManager legacyDynamicDataSaveLoadManager = new LegacyDynamicDataSaveLoadManager(Request.GetOperationContext());
			ILegacyDynamicDataSaveLoadManager legacyDynamicDataSaveLoadManager2 = legacyDynamicDataSaveLoadManager;
			LegacyDynamicDataRowDatasDTO legacyData = Request.LegacyData;
			IList<LegacySaveDataResult> list = legacyDynamicDataSaveLoadManager2.SaveDataPS((legacyData != null) ? legacyData.ToDomainObject() : null, Request.TableName, Request.ScreenNum, Request.StudentPid, Request.WhoModifiedPid, Request.TablesStoreScreenNum);
			SaveDataPSResp saveDataPSResp = new SaveDataPSResp();
			IList<LegacySaveDataResultDTO> saveDataResults;
			if (list == null)
			{
				saveDataResults = null;
			}
			else
			{
				saveDataResults = (from g in list
				select g.ToDTO()).ToList<LegacySaveDataResultDTO>();
			}
			saveDataPSResp.SaveDataResults = saveDataResults;
			return saveDataPSResp;
		}
	}
}
