using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F2 RID: 498
	public static class PrivateNoteMapper
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x000243CC File Offset: 0x000225CC
		static PrivateNoteMapper()
		{
			Mapper.CreateMap<PrivateNoteDTO, PrivateNote>();
			Mapper.CreateMap<PrivateNote, PrivateNoteDTO>();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000243DC File Offset: 0x000225DC
		public static PrivateNote ToDomainObject(this PrivateNoteDTO accommodationForTestDTO)
		{
			return Mapper.Map<PrivateNoteDTO, PrivateNote>(accommodationForTestDTO);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000243F4 File Offset: 0x000225F4
		public static PrivateNoteDTO ToDTO(this PrivateNote accommodationForTest)
		{
			return Mapper.Map<PrivateNote, PrivateNoteDTO>(accommodationForTest);
		}
	}
}
