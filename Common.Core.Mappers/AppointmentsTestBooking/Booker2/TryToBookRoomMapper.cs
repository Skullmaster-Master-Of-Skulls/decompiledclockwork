using System;
using AutoMapper;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2
{
	// Token: 0x020001DF RID: 479
	public static class TryToBookRoomMapper
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x00022D88 File Offset: 0x00020F88
		static TryToBookRoomMapper()
		{
			Mapper.CreateMap<TryToBookRoomDTO, TryToBookRoom>();
			Mapper.CreateMap<TryToBookRoom, TryToBookRoomDTO>();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00022D98 File Offset: 0x00020F98
		public static TryToBookRoom ToDomainObject(this TryToBookRoomDTO accommodationForTestDTO)
		{
			return Mapper.Map<TryToBookRoomDTO, TryToBookRoom>(accommodationForTestDTO);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00022DB0 File Offset: 0x00020FB0
		public static TryToBookRoomDTO ToDTO(this TryToBookRoom accommodationForTest)
		{
			return Mapper.Map<TryToBookRoom, TryToBookRoomDTO>(accommodationForTest);
		}
	}
}
