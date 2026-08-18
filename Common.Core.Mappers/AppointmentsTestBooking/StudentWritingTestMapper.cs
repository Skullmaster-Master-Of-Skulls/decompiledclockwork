using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001CA RID: 458
	public static class StudentWritingTestMapper
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00021B84 File Offset: 0x0001FD84
		static StudentWritingTestMapper()
		{
			PersonBaseMapper.CreateMap();
			AppTypeMapper.CreateMap();
			Mapper.CreateMap<StudentWritingTestDTO, StudentWritingTest>().ForMember((StudentWritingTest pb) => pb.Student, delegate(IMemberConfigurationExpression<StudentWritingTestDTO> m)
			{
				m.MapFrom<PersonBase>((StudentWritingTestDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((StudentWritingTest pb) => pb.AppointmentType, delegate(IMemberConfigurationExpression<StudentWritingTestDTO> m)
			{
				m.MapFrom<AppType>((StudentWritingTestDTO pbdto) => (pbdto.AppointmentType == null) ? null : pbdto.AppointmentType.ToDomainObject());
			});
			Mapper.CreateMap<StudentWritingTest, StudentWritingTestDTO>().ForMember((StudentWritingTestDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<StudentWritingTest> m)
			{
				m.MapFrom<PersonBaseDTO>((StudentWritingTest pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((StudentWritingTestDTO pb) => pb.AppointmentType, delegate(IMemberConfigurationExpression<StudentWritingTest> m)
			{
				m.MapFrom<AppTypeDTO>((StudentWritingTest pbdto) => (pbdto.AppointmentType == null) ? null : pbdto.AppointmentType.ToDTO());
			});
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		public static StudentWritingTest ToDomainObject(this StudentWritingTestDTO dto)
		{
			return Mapper.Map<StudentWritingTestDTO, StudentWritingTest>(dto);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00021CFC File Offset: 0x0001FEFC
		public static StudentWritingTestDTO ToDTO(this StudentWritingTest item)
		{
			return Mapper.Map<StudentWritingTest, StudentWritingTestDTO>(item);
		}
	}
}
