using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000D6 RID: 214
	public static class LookupCourseBaseMapper
	{
		// Token: 0x0600038C RID: 908 RVA: 0x000117D8 File Offset: 0x0000F9D8
		static LookupCourseBaseMapper()
		{
			LookupSubjectMapper.CreateMap();
			Mapper.CreateMap<LookupCourseBaseDTO, LookupCourseBase>().ForMember((LookupCourseBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupCourseBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LookupCourseBase, LookupCourseBaseDTO>();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001185C File Offset: 0x0000FA5C
		public static LookupCourseBase ToDomainObject(this LookupCourseBaseDTO dto)
		{
			return Mapper.Map<LookupCourseBaseDTO, LookupCourseBase>(dto);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00011874 File Offset: 0x0000FA74
		public static LookupCourseBaseDTO ToDTO(this LookupCourseBase item)
		{
			return Mapper.Map<LookupCourseBase, LookupCourseBaseDTO>(item);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001188C File Offset: 0x0000FA8C
		public static IList<LookupCourseBase> ToDomainObject(this IList<LookupCourseBaseDTO> list)
		{
			IList<LookupCourseBase> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<LookupCourseBase>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000118D0 File Offset: 0x0000FAD0
		public static IList<LookupCourseBaseDTO> ToDTO(this IList<LookupCourseBase> list)
		{
			IList<LookupCourseBaseDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<LookupCourseBaseDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
