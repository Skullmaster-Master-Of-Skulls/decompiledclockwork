using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000D4 RID: 212
	public static class AcademicTermMapper
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00011680 File Offset: 0x0000F880
		static AcademicTermMapper()
		{
			Mapper.CreateMap<AcademicTermDTO, AcademicTerm>().ForMember((AcademicTerm pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AcademicTermDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AcademicTerm, AcademicTermDTO>();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000116FC File Offset: 0x0000F8FC
		public static AcademicTerm ToDomainObject(this AcademicTermDTO sessionDTO)
		{
			return Mapper.Map<AcademicTermDTO, AcademicTerm>(sessionDTO);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011714 File Offset: 0x0000F914
		public static AcademicTermDTO ToDTO(this AcademicTerm session)
		{
			return Mapper.Map<AcademicTerm, AcademicTermDTO>(session);
		}
	}
}
