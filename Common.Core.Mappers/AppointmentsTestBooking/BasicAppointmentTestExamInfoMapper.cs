using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001BC RID: 444
	internal static class BasicAppointmentTestExamInfoMapper
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00020D2C File Offset: 0x0001EF2C
		static BasicAppointmentTestExamInfoMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<BasicAppointmentTestExamInfo, BasicAppointmentTestExamInfoDTO>().ForMember((BasicAppointmentTestExamInfoDTO pb) => pb.Course, delegate(IMemberConfigurationExpression<BasicAppointmentTestExamInfo> m)
			{
				m.MapFrom<LookupCourseBaseDTO>((BasicAppointmentTestExamInfo pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDTO());
			});
			Mapper.CreateMap<BasicAppointmentTestExamInfoDTO, BasicAppointmentTestExamInfo>().ForMember((BasicAppointmentTestExamInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<BasicAppointmentTestExamInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((BasicAppointmentTestExamInfo pb) => pb.Course, delegate(IMemberConfigurationExpression<BasicAppointmentTestExamInfoDTO> m)
			{
				m.MapFrom<LookupCourseBase>((BasicAppointmentTestExamInfoDTO pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDomainObject());
			});
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00020E4C File Offset: 0x0001F04C
		public static BasicAppointmentTestExamInfo ToDomainObject(this BasicAppointmentTestExamInfoDTO appointmentDTO)
		{
			return Mapper.Map<BasicAppointmentTestExamInfoDTO, BasicAppointmentTestExamInfo>(appointmentDTO);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00020E64 File Offset: 0x0001F064
		public static BasicAppointmentTestExamInfoDTO ToDTO(this BasicAppointmentTestExamInfo appointment)
		{
			return Mapper.Map<BasicAppointmentTestExamInfo, BasicAppointmentTestExamInfoDTO>(appointment);
		}
	}
}
