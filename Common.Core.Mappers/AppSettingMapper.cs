using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000002 RID: 2
	public static class AppSettingMapper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		static AppSettingMapper()
		{
			LookupSettingMapper.CreateMap();
			Mapper.CreateMap<AppSetting, AppSettingDTO>();
			Mapper.CreateMap<AppSettingDTO, AppSetting>().ForMember((AppSetting pb) => pb.Id, delegate(IMemberConfigurationExpression<AppSettingDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C8 File Offset: 0x000002C8
		public static AppSetting ToDomainObject(this AppSettingDTO appSettingDTO)
		{
			return Mapper.Map<AppSettingDTO, AppSetting>(appSettingDTO);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E0 File Offset: 0x000002E0
		public static AppSettingDTO ToDTO(this AppSetting appSetting)
		{
			return Mapper.Map<AppSetting, AppSettingDTO>(appSetting);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F8 File Offset: 0x000002F8
		public static IList<AppSetting> ToDomainObject(this IList<AppSettingDTO> list)
		{
			IList<AppSetting> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<AppSetting>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
		public static IList<AppSettingDTO> ToDTO(this IList<AppSetting> list)
		{
			IList<AppSettingDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<AppSettingDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
