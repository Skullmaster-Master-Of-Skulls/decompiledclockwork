using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000AC RID: 172
	public static class UserGroupObjectMapper
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		static UserGroupObjectMapper()
		{
			PersonBaseMapper.CreateMap();
			UserGroupObjectIdMapper.CreateMap();
			Mapper.CreateMap<UserGroupObject, UserGroupObjectDTO>();
			Mapper.CreateMap<UserGroupObjectDTO, UserGroupObject>().ForMember((UserGroupObject pb) => pb.Id, delegate(IMemberConfigurationExpression<UserGroupObjectDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000F41C File Offset: 0x0000D61C
		public static UserGroupObjectDTO ToDTO(this UserGroupObject item)
		{
			return Mapper.Map<UserGroupObject, UserGroupObjectDTO>(item);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000F434 File Offset: 0x0000D634
		public static UserGroupObject ToDomainObject(this UserGroupObjectDTO dto)
		{
			return Mapper.Map<UserGroupObjectDTO, UserGroupObject>(dto);
		}
	}
}
