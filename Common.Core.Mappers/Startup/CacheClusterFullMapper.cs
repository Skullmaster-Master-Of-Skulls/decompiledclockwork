using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Core.Mappers.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.Startup;

namespace TechnoPro.Common.Core.Mappers.Startup
{
	// Token: 0x02000066 RID: 102
	public static class CacheClusterFullMapper
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000AB10 File Offset: 0x00008D10
		static CacheClusterFullMapper()
		{
			OldUserSettingMapper.CreateMap();
			UserPermissionMapper.CreateMap();
			DynamicFormWithExtendedInfoMapper.CreateMap();
			AppTypeWithExtendedInfoMapper.CreateMap();
			GroupMapper.CreateMap();
			WorkshopDefinitionMapper.CreateMap();
			AppointmentIconMapper.CreateMap();
			SessionMapper.CreateMap();
			Mapper.CreateMap<CacheClusterFull, CacheClusterFullDTO>();
			Mapper.CreateMap<CacheClusterFullDTO, CacheClusterFull>().ForMember((CacheClusterFull pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CacheClusterFullDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000ABBC File Offset: 0x00008DBC
		public static CacheClusterFull ToDomainObject(this CacheClusterFullDTO dto)
		{
			return Mapper.Map<CacheClusterFullDTO, CacheClusterFull>(dto);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		public static CacheClusterFullDTO ToDTO(this CacheClusterFull val)
		{
			return Mapper.Map<CacheClusterFull, CacheClusterFullDTO>(val);
		}
	}
}
