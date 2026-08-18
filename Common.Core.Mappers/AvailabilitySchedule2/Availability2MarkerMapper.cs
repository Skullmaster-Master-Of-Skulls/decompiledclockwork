using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule2
{
	// Token: 0x02000188 RID: 392
	public static class Availability2MarkerMapper
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0001E61C File Offset: 0x0001C81C
		static Availability2MarkerMapper()
		{
			Mapper.CreateMap<Availability2MarkerDTO, Availability2Marker>().ForMember((Availability2Marker pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<Availability2MarkerDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Availability2Marker, Availability2MarkerDTO>();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001E698 File Offset: 0x0001C898
		public static Availability2Marker ToDomainObject(this Availability2MarkerDTO dto)
		{
			return Mapper.Map<Availability2MarkerDTO, Availability2Marker>(dto);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		public static Availability2MarkerDTO ToDTO(this Availability2Marker item)
		{
			return Mapper.Map<Availability2Marker, Availability2MarkerDTO>(item);
		}
	}
}
