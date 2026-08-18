using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.FullTest
{
	// Token: 0x020001DA RID: 474
	public static class TestForEdit2Mapper
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x000226D0 File Offset: 0x000208D0
		static TestForEdit2Mapper()
		{
			BaseExtendedAppointmentMapper.CreateMap();
			TestForEditBookingSpecificMapper.CreateMap();
			TestForEditClassDefinitionSpecificMapper.CreateMap();
			Mapper.CreateMap<TestForEdit2DTO, TestForEdit2>().ForMember((TestForEdit2 pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TestForEdit2DTO> m)
			{
				m.Ignore();
			}).ForMember((TestForEdit2 pb) => pb.BookingSpecificInfo, delegate(IMemberConfigurationExpression<TestForEdit2DTO> m)
			{
				m.MapFrom<TestForEditBookingSpecific>((TestForEdit2DTO pbdto) => (pbdto.BookingSpecificInfo == null) ? null : pbdto.BookingSpecificInfo.ToDomainObject());
			}).ForMember((TestForEdit2 pb) => pb.ClassTestDefinitionSpecificInfo, delegate(IMemberConfigurationExpression<TestForEdit2DTO> m)
			{
				m.MapFrom<TestForEditClassDefinitionSpecific>((TestForEdit2DTO pbdto) => (pbdto.ClassTestDefinitionSpecificInfo == null) ? null : pbdto.ClassTestDefinitionSpecificInfo.ToDomainObject());
			});
			Mapper.CreateMap<TestForEdit2, TestForEdit2DTO>().ForMember((TestForEdit2DTO pb) => pb.Sitting, delegate(IMemberConfigurationExpression<TestForEdit2> m)
			{
				m.Ignore();
			}).ForMember((TestForEdit2DTO pb) => pb.ExamFiles, delegate(IMemberConfigurationExpression<TestForEdit2> m)
			{
				m.Ignore();
			}).ForMember((TestForEdit2DTO pb) => pb.BookingSpecificInfo, delegate(IMemberConfigurationExpression<TestForEdit2> m)
			{
				m.MapFrom<TestForEditBookingSpecificDTO>((TestForEdit2 pbdto) => (pbdto.BookingSpecificInfo == null) ? null : pbdto.BookingSpecificInfo.ToDTO());
			}).ForMember((TestForEdit2DTO pb) => pb.ClassTestDefinitionSpecificInfo, delegate(IMemberConfigurationExpression<TestForEdit2> m)
			{
				m.MapFrom<TestForEditClassDefinitionSpecificDTO>((TestForEdit2 pbdto) => (pbdto.ClassTestDefinitionSpecificInfo == null) ? null : pbdto.ClassTestDefinitionSpecificInfo.ToDTO());
			});
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00022934 File Offset: 0x00020B34
		public static TestForEdit2 ToDomainObject(this TestForEdit2DTO classTestDTO)
		{
			return Mapper.Map<TestForEdit2DTO, TestForEdit2>(classTestDTO);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002294C File Offset: 0x00020B4C
		public static TestForEdit2DTO ToDTO(this TestForEdit2 classTest)
		{
			return Mapper.Map<TestForEdit2, TestForEdit2DTO>(classTest);
		}
	}
}
