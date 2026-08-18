using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E6 RID: 486
	public static class AutoBookTestExamPreviewResultMapper
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00023344 File Offset: 0x00021544
		static AutoBookTestExamPreviewResultMapper()
		{
			TryToBookFailureMapper.CreateMap();
			BasicPersonMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			AppointmentRoomMapper.CreateMap();
			Mapper.CreateMap<AutoBookTestExamPreviewResultDTO, AutoBookTestExamPreviewResult>().ForMember((AutoBookTestExamPreviewResult pb) => pb.Student, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResultDTO> m)
			{
				m.MapFrom<BasicPerson>((AutoBookTestExamPreviewResultDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((AutoBookTestExamPreviewResult pb) => pb.Course, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResultDTO> m)
			{
				m.MapFrom<LookupCourseBase>((AutoBookTestExamPreviewResultDTO pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDomainObject());
			}).ForMember((AutoBookTestExamPreviewResult pb) => pb.Failures, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResultDTO> m)
			{
				m.MapFrom<List<TryToBookFailure>>((AutoBookTestExamPreviewResultDTO pbdto) => (pbdto.Failures == null) ? null : (from g in pbdto.Failures
				select g.ToDomainObject()).ToList<TryToBookFailure>());
			}).ForMember((AutoBookTestExamPreviewResult pb) => pb.PotentialRoom, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResultDTO> m)
			{
				m.MapFrom<AppointmentRoom>((AutoBookTestExamPreviewResultDTO pbdto) => (pbdto.PotentialRoom == null) ? null : pbdto.PotentialRoom.ToDomainObject());
			});
			Mapper.CreateMap<AutoBookTestExamPreviewResult, AutoBookTestExamPreviewResultDTO>().ForMember((AutoBookTestExamPreviewResultDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResult> m)
			{
				m.MapFrom<BasicPersonDTO>((AutoBookTestExamPreviewResult pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((AutoBookTestExamPreviewResultDTO pb) => pb.Course, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResult> m)
			{
				m.MapFrom<LookupCourseBaseDTO>((AutoBookTestExamPreviewResult pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDTO());
			}).ForMember((AutoBookTestExamPreviewResultDTO pb) => pb.Failures, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResult> m)
			{
				m.MapFrom<List<TryToBookFailureDTO>>((AutoBookTestExamPreviewResult pbdto) => (pbdto.Failures == null) ? null : (from g in pbdto.Failures
				select g.ToDTO()).ToList<TryToBookFailureDTO>());
			}).ForMember((AutoBookTestExamPreviewResultDTO pb) => pb.PotentialRoom, delegate(IMemberConfigurationExpression<AutoBookTestExamPreviewResult> m)
			{
				m.MapFrom<AppointmentRoomDTO>((AutoBookTestExamPreviewResult pbdto) => (pbdto.PotentialRoom == null) ? null : pbdto.PotentialRoom.ToDTO());
			});
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000235E8 File Offset: 0x000217E8
		public static AutoBookTestExamPreviewResult ToDomainObject(this AutoBookTestExamPreviewResultDTO accommodationForTestDTO)
		{
			return Mapper.Map<AutoBookTestExamPreviewResultDTO, AutoBookTestExamPreviewResult>(accommodationForTestDTO);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00023600 File Offset: 0x00021800
		public static AutoBookTestExamPreviewResultDTO ToDTO(this AutoBookTestExamPreviewResult accommodationForTest)
		{
			return Mapper.Map<AutoBookTestExamPreviewResult, AutoBookTestExamPreviewResultDTO>(accommodationForTest);
		}
	}
}
