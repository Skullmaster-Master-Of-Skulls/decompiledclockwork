using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001CD RID: 461
	public static class TestMapper
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x00021EAC File Offset: 0x000200AC
		static TestMapper()
		{
			StudentClassTestBaseMapper.CreateMap();
			AccommodationForTestMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			AppTypeMapper.CreateMap();
			AppCancelInfoMapper.CreateMap();
			AppointmentIconMapper.CreateMap();
			AppointmentWorkshopInfoMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			AttendeeMapper.CreateMap();
			BaseExtendedAppointmentMapper.CreateMap();
			Mapper.CreateMap<TestDTO, Test>().ForMember((Test pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TestDTO> m)
			{
				m.Ignore();
			}).ForMember((Test pb) => pb.ClassTestInfo, delegate(IMemberConfigurationExpression<TestDTO> m)
			{
				m.MapFrom<ClassTestBase>((TestDTO pbdto) => (pbdto.ClassTestInfo == null) ? null : pbdto.ClassTestInfo.ToDomainObject());
			}).ForMember((Test pb) => pb.StudentClassTestInfo, delegate(IMemberConfigurationExpression<TestDTO> m)
			{
				m.MapFrom<StudentClassTestBase>((TestDTO pbdto) => (pbdto.StudentClassTestInfo == null) ? null : pbdto.StudentClassTestInfo.ToDomainObject());
			});
			Mapper.CreateMap<Test, TestDTO>().ForMember((TestDTO pb) => pb.ClassTestInfo, delegate(IMemberConfigurationExpression<Test> m)
			{
				m.MapFrom<ClassTestBaseDTO>((Test pbdto) => (pbdto.ClassTestInfo == null) ? null : pbdto.ClassTestInfo.ToDTO());
			}).ForMember((TestDTO pb) => pb.StudentClassTestInfo, delegate(IMemberConfigurationExpression<Test> m)
			{
				m.MapFrom<StudentClassTestBaseDTO>((Test pbdto) => (pbdto.StudentClassTestInfo == null) ? null : pbdto.StudentClassTestInfo.ToDTO());
			});
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002209C File Offset: 0x0002029C
		public static Test ToDomainObject(this TestDTO testDTO)
		{
			return Mapper.Map<TestDTO, Test>(testDTO);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000220B4 File Offset: 0x000202B4
		public static TestDTO ToDTO(this Test test)
		{
			return Mapper.Map<Test, TestDTO>(test);
		}
	}
}
