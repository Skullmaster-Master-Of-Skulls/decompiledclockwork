using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001EF RID: 495
	public static class PotentialRoomMapper
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x000241B4 File Offset: 0x000223B4
		static PotentialRoomMapper()
		{
			RoomMapper.CreateMap();
			Mapper.CreateMap<PotentialRoomDTO, PotentialRoom>();
			Mapper.CreateMap<PotentialRoom, PotentialRoomDTO>();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000241CC File Offset: 0x000223CC
		public static PotentialRoom ToDomainObject(this PotentialRoomDTO accommodationForTestDTO)
		{
			return Mapper.Map<PotentialRoomDTO, PotentialRoom>(accommodationForTestDTO);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000241E4 File Offset: 0x000223E4
		public static PotentialRoomDTO ToDTO(this PotentialRoom accommodationForTest)
		{
			return Mapper.Map<PotentialRoom, PotentialRoomDTO>(accommodationForTest);
		}
	}
}
