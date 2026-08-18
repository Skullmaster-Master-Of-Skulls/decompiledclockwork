using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule2
{
	// Token: 0x02000187 RID: 391
	public static class Availability2ItemMapper
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0001E568 File Offset: 0x0001C768
		static Availability2ItemMapper()
		{
			Availability2NoteMapper.CreateMap();
			Mapper.CreateMap<Availability2ItemDTO, Availability2Item>().ForMember((Availability2Item pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<Availability2ItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Availability2Item, Availability2ItemDTO>();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001E5EC File Offset: 0x0001C7EC
		public static Availability2Item ToDomainObject(this Availability2ItemDTO availability2ItemDTO)
		{
			return Mapper.Map<Availability2ItemDTO, Availability2Item>(availability2ItemDTO);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001E604 File Offset: 0x0001C804
		public static Availability2ItemDTO ToDTO(this Availability2Item availability2Item)
		{
			return Mapper.Map<Availability2Item, Availability2ItemDTO>(availability2Item);
		}
	}
}
