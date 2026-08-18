using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.Core.Mappers.Legacy.DynamicData
{
	// Token: 0x020000EB RID: 235
	public static class LegacyStudentNoteMapper
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x00012A24 File Offset: 0x00010C24
		static LegacyStudentNoteMapper()
		{
			Mapper.CreateMap<LegacyStudentNoteDTO, LegacyStudentNote>();
			Mapper.CreateMap<LegacyStudentNote, LegacyStudentNoteDTO>();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00012A34 File Offset: 0x00010C34
		public static LegacyStudentNote ToDomainObject(this LegacyStudentNoteDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacyStudentNoteDTO, LegacyStudentNote>(dynamicDataDTO);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012A4C File Offset: 0x00010C4C
		public static LegacyStudentNoteDTO ToDTO(this LegacyStudentNote dynamicData)
		{
			return Mapper.Map<LegacyStudentNote, LegacyStudentNoteDTO>(dynamicData);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012A64 File Offset: 0x00010C64
		public static IList<LegacyStudentNote> ToDomainObject(this IList<LegacyStudentNoteDTO> daos)
		{
			IList<LegacyStudentNote> result;
			if (daos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in daos
				select g.ToDomainObject()).ToList<LegacyStudentNote>();
			}
			return result;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00012AA8 File Offset: 0x00010CA8
		public static IList<LegacyStudentNoteDTO> ToDTO(this IList<LegacyStudentNote> entities)
		{
			IList<LegacyStudentNoteDTO> result;
			if (entities == null)
			{
				result = null;
			}
			else
			{
				result = (from g in entities
				select g.ToDTO()).ToList<LegacyStudentNoteDTO>();
			}
			return result;
		}
	}
}
