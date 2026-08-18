using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000142 RID: 322
	public static class DataSyncExternalDataMapper
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x00019FD8 File Offset: 0x000181D8
		static DataSyncExternalDataMapper()
		{
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<DataSyncExternalDataDTO, DataSyncExternalData>().ForMember((DataSyncExternalData pb) => pb.MapItem, delegate(IMemberConfigurationExpression<DataSyncExternalDataDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DataSyncExternalData, DataSyncExternalDataDTO>();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001A048 File Offset: 0x00018248
		public static DataSyncExternalData ToDomainObject(this DataSyncExternalDataDTO dto)
		{
			return Mapper.Map<DataSyncExternalDataDTO, DataSyncExternalData>(dto);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001A060 File Offset: 0x00018260
		public static DataSyncExternalDataDTO ToDTO(this DataSyncExternalData item)
		{
			return Mapper.Map<DataSyncExternalData, DataSyncExternalDataDTO>(item);
		}
	}
}
