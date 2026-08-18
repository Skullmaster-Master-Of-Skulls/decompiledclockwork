using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E7 RID: 487
	public static class AutoBookTestExamResultMapper
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00023618 File Offset: 0x00021818
		static AutoBookTestExamResultMapper()
		{
			AutoBookTestExamPreviewResultMapper.CreateMap();
			Mapper.CreateMap<AutoBookTestExamResultDTO, AutoBookTestExamResult>();
			Mapper.CreateMap<AutoBookTestExamResult, AutoBookTestExamResultDTO>();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00023630 File Offset: 0x00021830
		public static AutoBookTestExamResult ToDomainObject(this AutoBookTestExamResultDTO accommodationForTestDTO)
		{
			return Mapper.Map<AutoBookTestExamResultDTO, AutoBookTestExamResult>(accommodationForTestDTO);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00023648 File Offset: 0x00021848
		public static AutoBookTestExamResultDTO ToDTO(this AutoBookTestExamResult accommodationForTest)
		{
			return Mapper.Map<AutoBookTestExamResult, AutoBookTestExamResultDTO>(accommodationForTest);
		}
	}
}
