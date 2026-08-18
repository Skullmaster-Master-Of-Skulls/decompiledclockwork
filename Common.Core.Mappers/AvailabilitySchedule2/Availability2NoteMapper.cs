using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule2
{
	// Token: 0x02000189 RID: 393
	public static class Availability2NoteMapper
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		static Availability2NoteMapper()
		{
			Mapper.CreateMap<Availability2NoteDTO, Availability2Note>();
			Mapper.CreateMap<Availability2Note, Availability2NoteDTO>();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
		public static Availability2Note ToDomainObject(this Availability2NoteDTO availability2NoteDTO)
		{
			return Mapper.Map<Availability2NoteDTO, Availability2Note>(availability2NoteDTO);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
		public static Availability2NoteDTO ToDTO(this Availability2Note availability2Note)
		{
			return Mapper.Map<Availability2Note, Availability2NoteDTO>(availability2Note);
		}
	}
}
