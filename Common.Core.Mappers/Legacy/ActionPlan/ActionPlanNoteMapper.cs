using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ActionPlan;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Legacy.ActionPlan;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Legacy.ActionPlan
{
	// Token: 0x020000ED RID: 237
	public static class ActionPlanNoteMapper
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00012C60 File Offset: 0x00010E60
		static ActionPlanNoteMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<ActionPlanNoteDTO, ActionPlanNote>().ForMember((ActionPlanNote pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ActionPlanNoteDTO> m)
			{
				m.Ignore();
			}).ForMember((ActionPlanNote pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<ActionPlanNoteDTO> m)
			{
				m.MapFrom<PersonBase>((ActionPlanNoteDTO pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDomainObject());
			});
			Mapper.CreateMap<ActionPlanNote, ActionPlanNoteDTO>().ForMember((ActionPlanNoteDTO pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<ActionPlanNote> m)
			{
				m.MapFrom<PersonBaseDTO>((ActionPlanNote pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDTO());
			});
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00012D80 File Offset: 0x00010F80
		public static ActionPlanNote ToDomainObject(this ActionPlanNoteDTO dynamicDataDTO)
		{
			return Mapper.Map<ActionPlanNoteDTO, ActionPlanNote>(dynamicDataDTO);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00012D98 File Offset: 0x00010F98
		public static ActionPlanNoteDTO ToDTO(this ActionPlanNote dynamicData)
		{
			return Mapper.Map<ActionPlanNote, ActionPlanNoteDTO>(dynamicData);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00012DB0 File Offset: 0x00010FB0
		public static IList<ActionPlanNote> ToDomainObject(this IList<ActionPlanNoteDTO> daos)
		{
			IList<ActionPlanNote> result;
			if (daos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in daos
				select g.ToDomainObject()).ToList<ActionPlanNote>();
			}
			return result;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00012DF4 File Offset: 0x00010FF4
		public static IList<ActionPlanNoteDTO> ToDTO(this IList<ActionPlanNote> entities)
		{
			IList<ActionPlanNoteDTO> result;
			if (entities == null)
			{
				result = null;
			}
			else
			{
				result = (from g in entities
				select g.ToDTO()).ToList<ActionPlanNoteDTO>();
			}
			return result;
		}
	}
}
