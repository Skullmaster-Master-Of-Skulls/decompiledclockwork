using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent
{
	// Token: 0x0200020B RID: 523
	public static class CutoffTimeMapper
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x000261C4 File Offset: 0x000243C4
		static CutoffTimeMapper()
		{
			Mapper.CreateMap<CutoffTimeDTO, CutoffTime>();
			Mapper.CreateMap<CutoffTime, CutoffTimeDTO>();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000261D4 File Offset: 0x000243D4
		public static CutoffTime ToDomainObject(this CutoffTimeDTO dto)
		{
			return Mapper.Map<CutoffTimeDTO, CutoffTime>(dto);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000261EC File Offset: 0x000243EC
		public static CutoffTimeDTO ToDTO(this CutoffTime item)
		{
			return Mapper.Map<CutoffTime, CutoffTimeDTO>(item);
		}
	}
}
