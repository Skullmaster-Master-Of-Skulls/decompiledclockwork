using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000139 RID: 313
	public static class DataSyncExternalCourseAltContactMapper
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x00019ADC File Offset: 0x00017CDC
		static DataSyncExternalCourseAltContactMapper()
		{
			Mapper.CreateMap<DataSyncExternalCourseAltContactDTO, DataSyncExternalCourseAltContact>().ForMember((DataSyncExternalCourseAltContact pb) => pb.Id, delegate(IMemberConfigurationExpression<DataSyncExternalCourseAltContactDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DataSyncExternalCourseAltContact, DataSyncExternalCourseAltContactDTO>();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00019B4C File Offset: 0x00017D4C
		public static DataSyncExternalCourseAltContact ToDomainObject(this DataSyncExternalCourseAltContactDTO dataSyncExternalCourseAltContactDTO)
		{
			return Mapper.Map<DataSyncExternalCourseAltContactDTO, DataSyncExternalCourseAltContact>(dataSyncExternalCourseAltContactDTO);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00019B64 File Offset: 0x00017D64
		public static DataSyncExternalCourseAltContactDTO ToDTO(this DataSyncExternalCourseAltContact dataSyncExternalCourseAltContact)
		{
			return Mapper.Map<DataSyncExternalCourseAltContact, DataSyncExternalCourseAltContactDTO>(dataSyncExternalCourseAltContact);
		}
	}
}
