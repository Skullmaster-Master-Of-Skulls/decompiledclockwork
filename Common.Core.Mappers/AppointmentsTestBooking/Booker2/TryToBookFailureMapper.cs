using System;
using AutoMapper;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2
{
	// Token: 0x020001DC RID: 476
	public static class TryToBookFailureMapper
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x000229A4 File Offset: 0x00020BA4
		static TryToBookFailureMapper()
		{
			Mapper.CreateMap<TryToBookFailureDTO, TryToBookFailure>();
			Mapper.CreateMap<TryToBookFailure, TryToBookFailureDTO>();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000229B4 File Offset: 0x00020BB4
		public static TryToBookFailure ToDomainObject(this TryToBookFailureDTO accommodationForTestDTO)
		{
			return Mapper.Map<TryToBookFailureDTO, TryToBookFailure>(accommodationForTestDTO);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000229CC File Offset: 0x00020BCC
		public static TryToBookFailureDTO ToDTO(this TryToBookFailure accommodationForTest)
		{
			return Mapper.Map<TryToBookFailure, TryToBookFailureDTO>(accommodationForTest);
		}
	}
}
