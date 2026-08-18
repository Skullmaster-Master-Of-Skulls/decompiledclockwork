using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x0200015F RID: 351
	public static class CourseRegistrationStatusMapper
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		static CourseRegistrationStatusMapper()
		{
			Mapper.CreateMap<CourseRegistrationStatus, CourseRegistrationStatusDTO>();
			Mapper.CreateMap<CourseRegistrationStatusDTO, CourseRegistrationStatus>().ForMember((CourseRegistrationStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CourseRegistrationStatusDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001BE64 File Offset: 0x0001A064
		public static CourseRegistrationStatus ToDomainObject(this CourseRegistrationStatusDTO dto)
		{
			return Mapper.Map<CourseRegistrationStatusDTO, CourseRegistrationStatus>(dto);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001BE7C File Offset: 0x0001A07C
		public static CourseRegistrationStatusDTO ToDTO(this CourseRegistrationStatus item)
		{
			return Mapper.Map<CourseRegistrationStatus, CourseRegistrationStatusDTO>(item);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001BE94 File Offset: 0x0001A094
		public static IList<CourseRegistrationStatus> ToDomainObject(this IList<CourseRegistrationStatusDTO> list)
		{
			IList<CourseRegistrationStatus> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<CourseRegistrationStatus>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		public static IList<CourseRegistrationStatusDTO> ToDTO(this IList<CourseRegistrationStatus> list)
		{
			IList<CourseRegistrationStatusDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<CourseRegistrationStatusDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
