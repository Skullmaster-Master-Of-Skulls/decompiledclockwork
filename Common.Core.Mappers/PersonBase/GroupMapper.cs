using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A4 RID: 164
	public static class GroupMapper
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000ED70 File Offset: 0x0000CF70
		static GroupMapper()
		{
			Mapper.CreateMap<Group, GroupDTO>();
			Mapper.CreateMap<GroupDTO, Group>().ForMember((Group pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<GroupDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		public static Group ToDomainObject(this GroupDTO groupDTO)
		{
			return Mapper.Map<GroupDTO, Group>(groupDTO);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000EE04 File Offset: 0x0000D004
		public static GroupDTO ToDTO(this Group group)
		{
			return Mapper.Map<Group, GroupDTO>(group);
		}
	}
}
