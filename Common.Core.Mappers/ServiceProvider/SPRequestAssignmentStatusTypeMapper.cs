using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000070 RID: 112
	public static class SPRequestAssignmentStatusTypeMapper
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000B898 File Offset: 0x00009A98
		static SPRequestAssignmentStatusTypeMapper()
		{
			SPUrgencyLevelTypeMapper.CreateMap();
			Mapper.CreateMap<SPRequestAssignmentStatusType, SPRequestAssignmentStatusTypeDTO>();
			Mapper.CreateMap<SPRequestAssignmentStatusTypeDTO, SPRequestAssignmentStatusType>().ForMember((SPRequestAssignmentStatusType pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRequestAssignmentStatusTypeDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B91C File Offset: 0x00009B1C
		public static SPRequestAssignmentStatusType ToDomainObject(this SPRequestAssignmentStatusTypeDTO dto)
		{
			return Mapper.Map<SPRequestAssignmentStatusTypeDTO, SPRequestAssignmentStatusType>(dto);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B934 File Offset: 0x00009B34
		public static SPRequestAssignmentStatusTypeDTO ToDTO(this SPRequestAssignmentStatusType item)
		{
			return Mapper.Map<SPRequestAssignmentStatusType, SPRequestAssignmentStatusTypeDTO>(item);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B94C File Offset: 0x00009B4C
		public static IList<SPRequestAssignmentStatusType> ToDomainObject(this IList<SPRequestAssignmentStatusTypeDTO> list)
		{
			IList<SPRequestAssignmentStatusType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequestAssignmentStatusType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B990 File Offset: 0x00009B90
		public static IList<SPRequestAssignmentStatusTypeDTO> ToDTO(this IList<SPRequestAssignmentStatusType> list)
		{
			IList<SPRequestAssignmentStatusTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestAssignmentStatusTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
