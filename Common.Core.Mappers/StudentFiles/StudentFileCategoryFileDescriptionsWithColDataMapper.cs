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
	// Token: 0x02000051 RID: 81
	public static class StudentFileCategoryFileDescriptionsWithColDataMapper
	{
		// Token: 0x0600014C RID: 332 RVA: 0x000098C0 File Offset: 0x00007AC0
		static StudentFileCategoryFileDescriptionsWithColDataMapper()
		{
			DynamicFileDescriptionMapper.CreateMap();
			Mapper.CreateMap<StudentFileCategoryFileDescriptionsWithColDataDTO, StudentFileCategoryFileDescriptionsWithColData>().ForMember((StudentFileCategoryFileDescriptionsWithColData pb) => pb.Id, delegate(IMemberConfigurationExpression<StudentFileCategoryFileDescriptionsWithColDataDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentFileCategoryFileDescriptionsWithColData pb) => pb.FileDescriptions, delegate(IMemberConfigurationExpression<StudentFileCategoryFileDescriptionsWithColDataDTO> m)
			{
				m.MapFrom<DynamicFileDescriptionWithColData[]>((StudentFileCategoryFileDescriptionsWithColDataDTO pbdto) => (pbdto.FileDescriptions == null) ? null : (from g in pbdto.FileDescriptions
				select g.ToDomainObject()).ToArray<DynamicFileDescriptionWithColData>());
			});
			Mapper.CreateMap<StudentFileCategoryFileDescriptionsWithColData, StudentFileCategoryFileDescriptionsWithColDataDTO>().ForMember((StudentFileCategoryFileDescriptionsWithColDataDTO pb) => pb.FileDescriptions, delegate(IMemberConfigurationExpression<StudentFileCategoryFileDescriptionsWithColData> m)
			{
				m.MapFrom<DynamicFileDescriptionWithColDataDTO[]>((StudentFileCategoryFileDescriptionsWithColData pbdto) => (pbdto.FileDescriptions == null) ? null : (from g in pbdto.FileDescriptions
				select g.ToDTO()).ToArray<DynamicFileDescriptionWithColDataDTO>());
			});
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000099D0 File Offset: 0x00007BD0
		public static StudentFileCategoryFileDescriptionsWithColData ToDomainObject(this StudentFileCategoryFileDescriptionsWithColDataDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFileCategoryFileDescriptionsWithColDataDTO, StudentFileCategoryFileDescriptionsWithColData>(dynamicDataDTO);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000099E8 File Offset: 0x00007BE8
		public static StudentFileCategoryFileDescriptionsWithColDataDTO ToDTO(this StudentFileCategoryFileDescriptionsWithColData dynamicData)
		{
			return Mapper.Map<StudentFileCategoryFileDescriptionsWithColData, StudentFileCategoryFileDescriptionsWithColDataDTO>(dynamicData);
		}
	}
}
