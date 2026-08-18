using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000053 RID: 83
	public static class StudentFileCategoryMapper
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00009B40 File Offset: 0x00007D40
		static StudentFileCategoryMapper()
		{
			StudentFileCategoryFieldMapper.CreateMap();
			Mapper.CreateMap<StudentFileCategoryDTO, StudentFileCategory>().ForMember((StudentFileCategory pb) => pb.Id, delegate(IMemberConfigurationExpression<StudentFileCategoryDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentFileCategory pb) => pb.Fields, delegate(IMemberConfigurationExpression<StudentFileCategoryDTO> m)
			{
				m.MapFrom<List<StudentFileCategoryField>>((StudentFileCategoryDTO pbdto) => (pbdto.Fields == null) ? null : (from g in pbdto.Fields
				select g.ToDomainObject()).ToList<StudentFileCategoryField>());
			});
			Mapper.CreateMap<StudentFileCategory, StudentFileCategoryDTO>().ForMember((StudentFileCategoryDTO pb) => pb.Fields, delegate(IMemberConfigurationExpression<StudentFileCategory> m)
			{
				m.MapFrom<List<StudentFileCategoryFieldDTO>>((StudentFileCategory pbdto) => (pbdto.Fields == null) ? null : (from g in pbdto.Fields
				select g.ToDTO()).ToList<StudentFileCategoryFieldDTO>());
			});
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009C50 File Offset: 0x00007E50
		public static StudentFileCategory ToDomainObject(this StudentFileCategoryDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFileCategoryDTO, StudentFileCategory>(dynamicDataDTO);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009C68 File Offset: 0x00007E68
		public static StudentFileCategoryDTO ToDTO(this StudentFileCategory dynamicData)
		{
			return Mapper.Map<StudentFileCategory, StudentFileCategoryDTO>(dynamicData);
		}
	}
}
