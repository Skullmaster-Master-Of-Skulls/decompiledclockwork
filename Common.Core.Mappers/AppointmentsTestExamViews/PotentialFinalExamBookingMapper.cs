using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestExamViews
{
	// Token: 0x020001BA RID: 442
	public static class PotentialFinalExamBookingMapper
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00020B54 File Offset: 0x0001ED54
		static PotentialFinalExamBookingMapper()
		{
			BasicPersonMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<PotentialFinalExamBookingDTO, PotentialFinalExamBooking>().ForMember((PotentialFinalExamBooking pb) => pb.Student, delegate(IMemberConfigurationExpression<PotentialFinalExamBookingDTO> m)
			{
				m.MapFrom<BasicPerson>((PotentialFinalExamBookingDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((PotentialFinalExamBooking pb) => pb.Course, delegate(IMemberConfigurationExpression<PotentialFinalExamBookingDTO> m)
			{
				m.MapFrom<LookupCourseBase>((PotentialFinalExamBookingDTO pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDomainObject());
			});
			Mapper.CreateMap<PotentialFinalExamBooking, PotentialFinalExamBookingDTO>().ForMember((PotentialFinalExamBookingDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<PotentialFinalExamBooking> m)
			{
				m.MapFrom<BasicPersonDTO>((PotentialFinalExamBooking pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((PotentialFinalExamBookingDTO pb) => pb.Course, delegate(IMemberConfigurationExpression<PotentialFinalExamBooking> m)
			{
				m.MapFrom<LookupCourseBaseDTO>((PotentialFinalExamBooking pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDTO());
			});
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		public static PotentialFinalExamBooking ToDomainObject(this PotentialFinalExamBookingDTO appointmentWorkshopInfoDTO)
		{
			return Mapper.Map<PotentialFinalExamBookingDTO, PotentialFinalExamBooking>(appointmentWorkshopInfoDTO);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00020CCC File Offset: 0x0001EECC
		public static PotentialFinalExamBookingDTO ToDTO(this PotentialFinalExamBooking appointmentWorkshopInfo)
		{
			return Mapper.Map<PotentialFinalExamBooking, PotentialFinalExamBookingDTO>(appointmentWorkshopInfo);
		}
	}
}
