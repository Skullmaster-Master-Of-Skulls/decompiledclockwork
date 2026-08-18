using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F1 RID: 497
	public static class PotentialTestMethodFoundNoteMapper
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x0002438C File Offset: 0x0002258C
		static PotentialTestMethodFoundNoteMapper()
		{
			Mapper.CreateMap<PotentialTestMethodFoundNoteDTO, PotentialTestMethodFoundNote>();
			Mapper.CreateMap<PotentialTestMethodFoundNote, PotentialTestMethodFoundNoteDTO>();
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002439C File Offset: 0x0002259C
		public static PotentialTestMethodFoundNote ToDomainObject(this PotentialTestMethodFoundNoteDTO accommodationForTestDTO)
		{
			return Mapper.Map<PotentialTestMethodFoundNoteDTO, PotentialTestMethodFoundNote>(accommodationForTestDTO);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000243B4 File Offset: 0x000225B4
		public static PotentialTestMethodFoundNoteDTO ToDTO(this PotentialTestMethodFoundNote accommodationForTest)
		{
			return Mapper.Map<PotentialTestMethodFoundNote, PotentialTestMethodFoundNoteDTO>(accommodationForTest);
		}
	}
}
