using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000074 RID: 116
	public static class SPRequestEventMapper
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		static SPRequestEventMapper()
		{
			SPRequestMapper.CreateMap();
			SPRequestStatusTypeMapper.CreateMap();
			SPRequestAssignmentStatusTypeMapper.CreateMap();
			SPUrgencyLevelTypeMapper.CreateMap();
			SPRequestEventAssignmentMapper.CreateMap();
			Mapper.CreateMap<SPRequestEvent, SPRequestEventDTO>();
			Mapper.CreateMap<SPRequestEventDTO, SPRequestEvent>().ForMember((SPRequestEvent pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRequestEventDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000BE44 File Offset: 0x0000A044
		public static SPRequestEvent ToDomainObject(this SPRequestEventDTO dto)
		{
			return Mapper.Map<SPRequestEventDTO, SPRequestEvent>(dto);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public static SPRequestEventDTO ToDTO(this SPRequestEvent item)
		{
			return Mapper.Map<SPRequestEvent, SPRequestEventDTO>(item);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000BE74 File Offset: 0x0000A074
		public static IList<SPRequestEvent> ToDomainObject(this IList<SPRequestEventDTO> list)
		{
			IList<SPRequestEvent> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequestEvent>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public static IList<SPRequestEventDTO> ToDTO(this IList<SPRequestEvent> list)
		{
			IList<SPRequestEventDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestEventDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
