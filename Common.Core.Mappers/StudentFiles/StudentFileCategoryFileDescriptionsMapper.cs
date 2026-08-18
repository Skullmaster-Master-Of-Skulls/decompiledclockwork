using System;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000052 RID: 82
	public static class StudentFileCategoryFileDescriptionsMapper
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00009A00 File Offset: 0x00007C00
		static StudentFileCategoryFileDescriptionsMapper()
		{
			DynamicFileDescriptionMapper.CreateMap();
			Mapper.CreateMap<StudentFileCategoryFileDescriptionsDTO, StudentFileCategoryFileDescriptions>().ForMember((StudentFileCategoryFileDescriptions pb) => pb.Id, delegate(IMemberConfigurationExpression<StudentFileCategoryFileDescriptionsDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentFileCategoryFileDescriptions pb) => pb.FileDescriptions, delegate(IMemberConfigurationExpression<StudentFileCategoryFileDescriptionsDTO> m)
			{
				m.MapFrom<DynamicFileDescription[]>((StudentFileCategoryFileDescriptionsDTO pbdto) => (pbdto.FileDescriptions == null) ? null : (from g in pbdto.FileDescriptions
				select g.ToDomainObject()).ToArray<DynamicFileDescription>());
			});
			Mapper.CreateMap<StudentFileCategoryFileDescriptions, StudentFileCategoryFileDescriptionsDTO>().ForMember((StudentFileCategoryFileDescriptionsDTO pb) => pb.FileDescriptions, delegate(IMemberConfigurationExpression<StudentFileCategoryFileDescriptions> m)
			{
				m.MapFrom<DynamicFileDescriptionDTO[]>((StudentFileCategoryFileDescriptions pbdto) => (pbdto.FileDescriptions == null) ? null : (from g in pbdto.FileDescriptions
				select g.ToDTO()).ToArray<DynamicFileDescriptionDTO>());
			});
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009B10 File Offset: 0x00007D10
		public static StudentFileCategoryFileDescriptions ToDomainObject(this StudentFileCategoryFileDescriptionsDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFileCategoryFileDescriptionsDTO, StudentFileCategoryFileDescriptions>(dynamicDataDTO);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009B28 File Offset: 0x00007D28
		public static StudentFileCategoryFileDescriptionsDTO ToDTO(this StudentFileCategoryFileDescriptions dynamicData)
		{
			return Mapper.Map<StudentFileCategoryFileDescriptions, StudentFileCategoryFileDescriptionsDTO>(dynamicData);
		}
	}
}
