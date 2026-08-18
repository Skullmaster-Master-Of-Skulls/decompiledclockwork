using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.Common.Public.Entities.Legacy.ServiceProviders;

namespace TechnoPro.Common.Core.Mappers.Legacy.ServiceProvider
{
	// Token: 0x020000E6 RID: 230
	public static class LegacyRequestDetailNotesAndSpecialInstructionsMapper
	{
		// Token: 0x060003CD RID: 973 RVA: 0x00012518 File Offset: 0x00010718
		static LegacyRequestDetailNotesAndSpecialInstructionsMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<LegacyRequestDetailNotesAndSpecialInstructionsDTO, LegacyRequestDetailNotesAndSpecialInstructions>().ForMember((LegacyRequestDetailNotesAndSpecialInstructions pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LegacyRequestDetailNotesAndSpecialInstructionsDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LegacyRequestDetailNotesAndSpecialInstructions, LegacyRequestDetailNotesAndSpecialInstructionsDTO>();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001259C File Offset: 0x0001079C
		public static LegacyRequestDetailNotesAndSpecialInstructions ToDomainObject(this LegacyRequestDetailNotesAndSpecialInstructionsDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacyRequestDetailNotesAndSpecialInstructionsDTO, LegacyRequestDetailNotesAndSpecialInstructions>(dynamicDataDTO);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000125B4 File Offset: 0x000107B4
		public static LegacyRequestDetailNotesAndSpecialInstructionsDTO ToDTO(this LegacyRequestDetailNotesAndSpecialInstructions dynamicData)
		{
			return Mapper.Map<LegacyRequestDetailNotesAndSpecialInstructions, LegacyRequestDetailNotesAndSpecialInstructionsDTO>(dynamicData);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000125CC File Offset: 0x000107CC
		public static IList<LegacyRequestDetailNotesAndSpecialInstructions> ToDomainObject(this IList<LegacyRequestDetailNotesAndSpecialInstructionsDTO> daos)
		{
			IList<LegacyRequestDetailNotesAndSpecialInstructions> result;
			if (daos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in daos
				select g.ToDomainObject()).ToList<LegacyRequestDetailNotesAndSpecialInstructions>();
			}
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00012610 File Offset: 0x00010810
		public static IList<LegacyRequestDetailNotesAndSpecialInstructionsDTO> ToDTO(this IList<LegacyRequestDetailNotesAndSpecialInstructions> entities)
		{
			IList<LegacyRequestDetailNotesAndSpecialInstructionsDTO> result;
			if (entities == null)
			{
				result = null;
			}
			else
			{
				result = (from g in entities
				select g.ToDTO()).ToList<LegacyRequestDetailNotesAndSpecialInstructionsDTO>();
			}
			return result;
		}
	}
}
