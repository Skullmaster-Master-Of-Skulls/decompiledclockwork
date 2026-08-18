using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Legacy.Appointment;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Legacy.Appointment
{
	// Token: 0x020000EC RID: 236
	public static class AppointmentModifiedHistoryItemMapper
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x00012AEC File Offset: 0x00010CEC
		static AppointmentModifiedHistoryItemMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<AppointmentModifiedHistoryItemDTO, AppointmentModifiedHistoryItem>().ForMember((AppointmentModifiedHistoryItem pb) => pb.ActionBy, delegate(IMemberConfigurationExpression<AppointmentModifiedHistoryItemDTO> m)
			{
				m.MapFrom<PersonBase>((AppointmentModifiedHistoryItemDTO pbdto) => (pbdto.ActionBy == null) ? null : pbdto.ActionBy.ToDomainObject());
			});
			Mapper.CreateMap<AppointmentModifiedHistoryItem, AppointmentModifiedHistoryItemDTO>().ForMember((AppointmentModifiedHistoryItemDTO pb) => pb.ActionBy, delegate(IMemberConfigurationExpression<AppointmentModifiedHistoryItem> m)
			{
				m.MapFrom<PersonBaseDTO>((AppointmentModifiedHistoryItem pbdto) => (pbdto.ActionBy == null) ? null : pbdto.ActionBy.ToDTO());
			});
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00012BA8 File Offset: 0x00010DA8
		public static AppointmentModifiedHistoryItem ToDomainObject(this AppointmentModifiedHistoryItemDTO dynamicDataDTO)
		{
			return Mapper.Map<AppointmentModifiedHistoryItemDTO, AppointmentModifiedHistoryItem>(dynamicDataDTO);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00012BC0 File Offset: 0x00010DC0
		public static AppointmentModifiedHistoryItemDTO ToDTO(this AppointmentModifiedHistoryItem dynamicData)
		{
			return Mapper.Map<AppointmentModifiedHistoryItem, AppointmentModifiedHistoryItemDTO>(dynamicData);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00012BD8 File Offset: 0x00010DD8
		public static IList<AppointmentModifiedHistoryItem> ToDomainObject(this IList<AppointmentModifiedHistoryItemDTO> daos)
		{
			IList<AppointmentModifiedHistoryItem> result;
			if (daos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in daos
				select g.ToDomainObject()).ToList<AppointmentModifiedHistoryItem>();
			}
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00012C1C File Offset: 0x00010E1C
		public static IList<AppointmentModifiedHistoryItemDTO> ToDTO(this IList<AppointmentModifiedHistoryItem> entities)
		{
			IList<AppointmentModifiedHistoryItemDTO> result;
			if (entities == null)
			{
				result = null;
			}
			else
			{
				result = (from g in entities
				select g.ToDTO()).ToList<AppointmentModifiedHistoryItemDTO>();
			}
			return result;
		}
	}
}
