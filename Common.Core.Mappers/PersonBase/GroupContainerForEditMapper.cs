using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A1 RID: 161
	public static class GroupContainerForEditMapper
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000EC42 File Offset: 0x0000CE42
		static GroupContainerForEditMapper()
		{
			Mapper.CreateMap<GroupContainerForEdit, GroupContainerForEditDTO>();
			Mapper.CreateMap<GroupContainerForEditDTO, GroupContainerForEdit>();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public static GroupContainerForEdit ToDomainObject(this GroupContainerForEditDTO groupDTO)
		{
			return Mapper.Map<GroupContainerForEditDTO, GroupContainerForEdit>(groupDTO);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public static GroupContainerForEditDTO ToDTO(this GroupContainerForEdit group)
		{
			return Mapper.Map<GroupContainerForEdit, GroupContainerForEditDTO>(group);
		}
	}
}
