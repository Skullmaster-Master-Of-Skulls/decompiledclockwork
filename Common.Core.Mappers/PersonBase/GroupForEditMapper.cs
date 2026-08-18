using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A3 RID: 163
	public static class GroupForEditMapper
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		static GroupForEditMapper()
		{
			Mapper.CreateMap<GroupForEdit, GroupForEditDTO>();
			Mapper.CreateMap<GroupForEditDTO, GroupForEdit>().ForMember((GroupForEdit pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<GroupForEditDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public static GroupForEdit ToDomainObject(this GroupForEditDTO groupDTO)
		{
			return Mapper.Map<GroupForEditDTO, GroupForEdit>(groupDTO);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000ED58 File Offset: 0x0000CF58
		public static GroupForEditDTO ToDTO(this GroupForEdit group)
		{
			return Mapper.Map<GroupForEdit, GroupForEditDTO>(group);
		}
	}
}
