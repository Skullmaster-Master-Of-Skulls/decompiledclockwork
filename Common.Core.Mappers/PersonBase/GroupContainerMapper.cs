using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A2 RID: 162
	public static class GroupContainerMapper
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000EC84 File Offset: 0x0000CE84
		static GroupContainerMapper()
		{
			Mapper.CreateMap<GroupContainer, GroupContainerDTO>();
			Mapper.CreateMap<GroupContainerDTO, GroupContainer>();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public static GroupContainer ToDomainObject(this GroupContainerDTO dto)
		{
			return Mapper.Map<GroupContainerDTO, GroupContainer>(dto);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public static GroupContainerDTO ToDTO(this GroupContainer item)
		{
			return Mapper.Map<GroupContainer, GroupContainerDTO>(item);
		}
	}
}
