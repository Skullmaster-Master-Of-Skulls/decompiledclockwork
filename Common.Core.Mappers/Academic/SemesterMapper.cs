using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.Common.Public.Entities.Academic;

namespace TechnoPro.Common.Core.Mappers.Academic
{
	// Token: 0x02000230 RID: 560
	public static class SemesterMapper
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0002B970 File Offset: 0x00029B70
		static SemesterMapper()
		{
			Mapper.CreateMap<SemesterDTO, Semester>().ForMember((Semester pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SemesterDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Semester, SemesterDTO>();
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002B9EC File Offset: 0x00029BEC
		public static Semester ToDomainObject(this SemesterDTO dto)
		{
			return Mapper.Map<SemesterDTO, Semester>(dto);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002BA04 File Offset: 0x00029C04
		public static SemesterDTO ToDTO(this Semester item)
		{
			return Mapper.Map<Semester, SemesterDTO>(item);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002BA1C File Offset: 0x00029C1C
		public static IList<Semester> ToDomainObject(this IList<SemesterDTO> dtos)
		{
			IList<Semester> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<Semester>();
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002BA60 File Offset: 0x00029C60
		public static IList<SemesterDTO> ToDTO(this IList<Semester> items)
		{
			IList<SemesterDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<SemesterDTO>();
			}
			return result;
		}
	}
}
