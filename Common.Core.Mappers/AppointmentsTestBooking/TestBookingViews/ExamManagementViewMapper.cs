using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x020001D2 RID: 466
	public static class ExamManagementViewMapper
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x000222C0 File Offset: 0x000204C0
		static ExamManagementViewMapper()
		{
			Mapper.CreateMap<ExamManagementViewDTO, ExamManagementView>();
			Mapper.CreateMap<ExamManagementView, ExamManagementViewDTO>();
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000222D0 File Offset: 0x000204D0
		public static ExamManagementView ToDomainObject(this ExamManagementViewDTO dto)
		{
			return Mapper.Map<ExamManagementViewDTO, ExamManagementView>(dto);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000222E8 File Offset: 0x000204E8
		public static ExamManagementViewDTO ToDTO(this ExamManagementView item)
		{
			return Mapper.Map<ExamManagementView, ExamManagementViewDTO>(item);
		}
	}
}
