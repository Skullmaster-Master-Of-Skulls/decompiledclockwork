using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000DF RID: 223
	public static class LookupSubjectMapper
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00011FCC File Offset: 0x000101CC
		static LookupSubjectMapper()
		{
			Mapper.CreateMap<LookupSubjectDTO, LookupSubject>().ForMember((LookupSubject pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupSubjectDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LookupSubject, LookupSubjectDTO>();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012048 File Offset: 0x00010248
		public static LookupSubject ToDomainObject(this LookupSubjectDTO lookupSubjectDTO)
		{
			return Mapper.Map<LookupSubjectDTO, LookupSubject>(lookupSubjectDTO);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012060 File Offset: 0x00010260
		public static LookupSubjectDTO ToDTO(this LookupSubject lookupSubject)
		{
			return Mapper.Map<LookupSubject, LookupSubjectDTO>(lookupSubject);
		}
	}
}
