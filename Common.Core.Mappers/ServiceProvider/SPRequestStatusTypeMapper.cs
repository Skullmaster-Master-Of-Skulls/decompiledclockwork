using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000076 RID: 118
	public static class SPRequestStatusTypeMapper
	{
		// Token: 0x060001FC RID: 508 RVA: 0x0000C050 File Offset: 0x0000A250
		static SPRequestStatusTypeMapper()
		{
			SPUrgencyLevelTypeMapper.CreateMap();
			Mapper.CreateMap<SPRequestStatusType, SPRequestStatusTypeDTO>();
			Mapper.CreateMap<SPRequestStatusTypeDTO, SPRequestStatusType>().ForMember((SPRequestStatusType pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRequestStatusTypeDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public static SPRequestStatusType ToDomainObject(this SPRequestStatusTypeDTO dto)
		{
			return Mapper.Map<SPRequestStatusTypeDTO, SPRequestStatusType>(dto);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public static SPRequestStatusTypeDTO ToDTO(this SPRequestStatusType item)
		{
			return Mapper.Map<SPRequestStatusType, SPRequestStatusTypeDTO>(item);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C104 File Offset: 0x0000A304
		public static IList<SPRequestStatusType> ToDomainObject(this IList<SPRequestStatusTypeDTO> list)
		{
			IList<SPRequestStatusType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequestStatusType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000C148 File Offset: 0x0000A348
		public static IList<SPRequestStatusTypeDTO> ToDTO(this IList<SPRequestStatusType> list)
		{
			IList<SPRequestStatusTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestStatusTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
