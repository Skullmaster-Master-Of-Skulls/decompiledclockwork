using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E8 RID: 488
	public static class AutoRescheduleTestExamResultMapper
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x00023660 File Offset: 0x00021860
		static AutoRescheduleTestExamResultMapper()
		{
			PotentialTestMethodFoundNoteMapper.CreateMap();
			TestMapper.CreateMap();
			Mapper.CreateMap<AutoRescheduleTestExamResultDTO, AutoRescheduleTestExamResult>().ForMember((AutoRescheduleTestExamResult pb) => pb.PreviewResult, delegate(IMemberConfigurationExpression<AutoRescheduleTestExamResultDTO> m)
			{
				m.MapFrom<AutoBookTestExamPreviewResult>((AutoRescheduleTestExamResultDTO pbdto) => (pbdto.PreviewResult == null) ? null : pbdto.PreviewResult.ToDomainObject());
			});
			Mapper.CreateMap<AutoRescheduleTestExamResult, AutoRescheduleTestExamResultDTO>().ForMember((AutoRescheduleTestExamResultDTO pb) => pb.PreviewResult, delegate(IMemberConfigurationExpression<AutoRescheduleTestExamResult> m)
			{
				m.MapFrom<AutoBookTestExamPreviewResultDTO>((AutoRescheduleTestExamResult pbdto) => (pbdto.PreviewResult == null) ? null : pbdto.PreviewResult.ToDTO());
			});
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00023724 File Offset: 0x00021924
		public static AutoRescheduleTestExamResult ToDomainObject(this AutoRescheduleTestExamResultDTO accommodationForTestDTO)
		{
			return Mapper.Map<AutoRescheduleTestExamResultDTO, AutoRescheduleTestExamResult>(accommodationForTestDTO);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002373C File Offset: 0x0002193C
		public static AutoRescheduleTestExamResultDTO ToDTO(this AutoRescheduleTestExamResult accommodationForTest)
		{
			return Mapper.Map<AutoRescheduleTestExamResult, AutoRescheduleTestExamResultDTO>(accommodationForTest);
		}
	}
}
