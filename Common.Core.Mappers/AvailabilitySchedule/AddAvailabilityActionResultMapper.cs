using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x0200017E RID: 382
	public static class AddAvailabilityActionResultMapper
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x0001DD40 File Offset: 0x0001BF40
		static AddAvailabilityActionResultMapper()
		{
			AvailabilityScheduleItemActionResultMapper.CreateMap();
			AvailabilityScheduleTimeMapper.CreateMap();
			Mapper.CreateMap<AddAvailabilityActionResultDTO, AddAvailabilityActionResult>().ForMember((AddAvailabilityActionResult pb) => pb.Time, delegate(IMemberConfigurationExpression<AddAvailabilityActionResultDTO> m)
			{
				m.MapFrom<AvailabilityScheduleTime>((AddAvailabilityActionResultDTO pbdto) => (pbdto.Time == null) ? null : pbdto.Time.ToDomainObject());
			}).ForMember((AddAvailabilityActionResult pb) => pb.Status, delegate(IMemberConfigurationExpression<AddAvailabilityActionResultDTO> m)
			{
				m.MapFrom<AvailabilityScheduleItemActionResult>((AddAvailabilityActionResultDTO pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDomainObject());
			});
			Mapper.CreateMap<AddAvailabilityActionResult, AddAvailabilityActionResultDTO>().ForMember((AddAvailabilityActionResultDTO pb) => pb.Time, delegate(IMemberConfigurationExpression<AddAvailabilityActionResult> m)
			{
				m.MapFrom<AvailabilityScheduleTimeDTO>((AddAvailabilityActionResult pbdto) => (pbdto.Time == null) ? null : pbdto.Time.ToDTO());
			}).ForMember((AddAvailabilityActionResultDTO pb) => pb.Status, delegate(IMemberConfigurationExpression<AddAvailabilityActionResult> m)
			{
				m.MapFrom<AvailabilityScheduleItemActionResultDTO>((AddAvailabilityActionResult pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDTO());
			});
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		public static AddAvailabilityActionResult ToDomainObject(this AddAvailabilityActionResultDTO appTypeGroupDTO)
		{
			return Mapper.Map<AddAvailabilityActionResultDTO, AddAvailabilityActionResult>(appTypeGroupDTO);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		public static AddAvailabilityActionResultDTO ToDTO(this AddAvailabilityActionResult appTypeGroup)
		{
			return Mapper.Map<AddAvailabilityActionResult, AddAvailabilityActionResultDTO>(appTypeGroup);
		}
	}
}
