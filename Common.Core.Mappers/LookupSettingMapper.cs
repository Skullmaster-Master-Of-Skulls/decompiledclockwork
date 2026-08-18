using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000003 RID: 3
	internal static class LookupSettingMapper
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002180 File Offset: 0x00000380
		static LookupSettingMapper()
		{
			Mapper.CreateMap<LookupSetting, LookupSettingDTO>();
			Mapper.CreateMap<LookupSettingDTO, LookupSetting>().ForMember((LookupSetting pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => sett.Description, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => (object)sett.Group, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => sett.GroupName, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => (object)sett.HasDefaultValue, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => (object)sett.IsHidden, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => sett.Name, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => (object)sett.SemanticType, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => sett.SettingDataAttribute, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => sett.SubGroup, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupSetting sett) => sett.SystemType, delegate(IMemberConfigurationExpression<LookupSettingDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002544 File Offset: 0x00000744
		public static LookupSetting ToDomainObject(this LookupSettingDTO appSettingDTO)
		{
			return Mapper.Map<LookupSettingDTO, LookupSetting>(appSettingDTO);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000255C File Offset: 0x0000075C
		public static LookupSettingDTO ToDTO(this LookupSetting appSetting)
		{
			return Mapper.Map<LookupSetting, LookupSettingDTO>(appSetting);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002574 File Offset: 0x00000774
		public static IList<LookupSetting> ToDomainObject(this IList<LookupSettingDTO> list)
		{
			IList<LookupSetting> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<LookupSetting>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025B8 File Offset: 0x000007B8
		public static IList<LookupSettingDTO> ToDTO(this IList<LookupSetting> list)
		{
			IList<LookupSettingDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<LookupSettingDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
