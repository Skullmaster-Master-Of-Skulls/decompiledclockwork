using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000078 RID: 120
	public static class SPUrgencyLevelTypeMapper
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000C264 File Offset: 0x0000A464
		static SPUrgencyLevelTypeMapper()
		{
			Mapper.CreateMap<SPUrgencyLevelType, SPUrgencyLevelTypeDTO>();
			Mapper.CreateMap<SPUrgencyLevelTypeDTO, SPUrgencyLevelType>().ForMember((SPUrgencyLevelType pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPUrgencyLevelTypeDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public static SPUrgencyLevelType ToDomainObject(this SPUrgencyLevelTypeDTO dto)
		{
			return Mapper.Map<SPUrgencyLevelTypeDTO, SPUrgencyLevelType>(dto);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public static SPUrgencyLevelTypeDTO ToDTO(this SPUrgencyLevelType item)
		{
			return Mapper.Map<SPUrgencyLevelType, SPUrgencyLevelTypeDTO>(item);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000C310 File Offset: 0x0000A510
		public static IList<SPUrgencyLevelType> ToDomainObject(this IList<SPUrgencyLevelTypeDTO> list)
		{
			IList<SPUrgencyLevelType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPUrgencyLevelType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C354 File Offset: 0x0000A554
		public static IList<SPUrgencyLevelTypeDTO> ToDTO(this IList<SPUrgencyLevelType> list)
		{
			IList<SPUrgencyLevelTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPUrgencyLevelTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
