using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsList;

namespace TechnoPro.Common.Core.Mappers.AppointmentsList
{
	// Token: 0x020001FD RID: 509
	public static class ListAppointmentMapper
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x00024BD4 File Offset: 0x00022DD4
		static ListAppointmentMapper()
		{
			BaseBasicAppointmentMapper.CreateMap();
			AppTypeMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<ListAppointmentDTO, ListAppointment>().ForMember((ListAppointment pb) => pb.Staff, delegate(IMemberConfigurationExpression<ListAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((ListAppointment pb) => pb.Student, delegate(IMemberConfigurationExpression<ListAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((ListAppointment pb) => (object)pb.IsIn, delegate(IMemberConfigurationExpression<ListAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((ListAppointment pb) => (object)pb.IsConfirmed, delegate(IMemberConfigurationExpression<ListAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((ListAppointment pb) => (object)pb.IsNoShow, delegate(IMemberConfigurationExpression<ListAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((ListAppointment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ListAppointmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ListAppointment, ListAppointmentDTO>().ForMember((ListAppointmentDTO pb) => pb.Staff, delegate(IMemberConfigurationExpression<ListAppointment> m)
			{
				m.Ignore();
			}).ForMember((ListAppointmentDTO pb) => (object)pb.StartDate, delegate(IMemberConfigurationExpression<ListAppointment> m)
			{
				m.Ignore();
			}).ForMember((ListAppointmentDTO pb) => (object)pb.EndDate, delegate(IMemberConfigurationExpression<ListAppointment> m)
			{
				m.Ignore();
			}).ForMember((ListAppointmentDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<ListAppointment> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00024F6C File Offset: 0x0002316C
		public static ListAppointment ToDomainObject(this ListAppointmentDTO listAppointmentDTO)
		{
			return Mapper.Map<ListAppointmentDTO, ListAppointment>(listAppointmentDTO);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00024F84 File Offset: 0x00023184
		public static ListAppointmentDTO ToDTO(this ListAppointment listAppointment)
		{
			return Mapper.Map<ListAppointment, ListAppointmentDTO>(listAppointment);
		}
	}
}
