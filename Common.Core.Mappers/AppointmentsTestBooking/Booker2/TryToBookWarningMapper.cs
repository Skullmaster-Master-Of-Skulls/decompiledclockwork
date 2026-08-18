using System;
using AutoMapper;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2
{
	// Token: 0x020001E0 RID: 480
	public static class TryToBookWarningMapper
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00022DC8 File Offset: 0x00020FC8
		static TryToBookWarningMapper()
		{
			Mapper.CreateMap<TryToBookWarningDTO, TryToBookWarning>();
			Mapper.CreateMap<TryToBookWarning, TryToBookWarningDTO>();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00022DD8 File Offset: 0x00020FD8
		public static TryToBookWarning ToDomainObject(this TryToBookWarningDTO accommodationForTestDTO)
		{
			return Mapper.Map<TryToBookWarningDTO, TryToBookWarning>(accommodationForTestDTO);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00022DF0 File Offset: 0x00020FF0
		public static TryToBookWarningDTO ToDTO(this TryToBookWarning accommodationForTest)
		{
			return Mapper.Map<TryToBookWarning, TryToBookWarningDTO>(accommodationForTest);
		}
	}
}
