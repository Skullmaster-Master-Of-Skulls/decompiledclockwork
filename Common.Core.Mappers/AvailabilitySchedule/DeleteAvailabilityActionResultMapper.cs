using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x02000186 RID: 390
	public static class DeleteAvailabilityActionResultMapper
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0001E474 File Offset: 0x0001C674
		static DeleteAvailabilityActionResultMapper()
		{
			AvailabilityScheduleItemActionResultMapper.CreateMap();
			AvailabilityScheduleTimeMapper.CreateMap();
			Mapper.CreateMap<DeleteAvailabilityActionResultDTO, DeleteAvailabilityActionResult>().ForMember((DeleteAvailabilityActionResult pb) => pb.Time, delegate(IMemberConfigurationExpression<DeleteAvailabilityActionResultDTO> m)
			{
				m.MapFrom<AvailabilityScheduleTime>((DeleteAvailabilityActionResultDTO pbdto) => (pbdto.Time == null) ? null : pbdto.Time.ToDomainObject());
			}).ForMember((DeleteAvailabilityActionResult pb) => pb.Status, delegate(IMemberConfigurationExpression<DeleteAvailabilityActionResultDTO> m)
			{
				m.MapFrom<AvailabilityScheduleItemActionResult>((DeleteAvailabilityActionResultDTO pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDomainObject());
			});
			Mapper.CreateMap<DeleteAvailabilityActionResult, DeleteAvailabilityActionResultDTO>();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001E538 File Offset: 0x0001C738
		public static DeleteAvailabilityActionResult ToDomainObject(this DeleteAvailabilityActionResultDTO appTypeGroupDTO)
		{
			return Mapper.Map<DeleteAvailabilityActionResultDTO, DeleteAvailabilityActionResult>(appTypeGroupDTO);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001E550 File Offset: 0x0001C750
		public static DeleteAvailabilityActionResultDTO ToDTO(this DeleteAvailabilityActionResult appTypeGroup)
		{
			return Mapper.Map<DeleteAvailabilityActionResult, DeleteAvailabilityActionResultDTO>(appTypeGroup);
		}
	}
}
