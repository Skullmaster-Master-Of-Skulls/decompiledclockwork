using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.Academic;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Vets
{
	// Token: 0x02000012 RID: 18
	public static class VetsStudentCardInfoItemMapper
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000039F8 File Offset: 0x00001BF8
		static VetsStudentCardInfoItemMapper()
		{
			SemesterMapper.CreateMap();
			Mapper.CreateMap<VetsStudentCardInfoItem, VetsStudentCardInfoItemDTO>().ForMember((VetsStudentCardInfoItemDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsStudentCardInfoItem> m)
			{
				m.MapFrom<SemesterDTO>((VetsStudentCardInfoItem pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			});
			Mapper.CreateMap<VetsStudentCardInfoItemDTO, VetsStudentCardInfoItem>().ForMember((VetsStudentCardInfoItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsStudentCardInfoItemDTO> m)
			{
				m.Ignore();
			}).ForMember((VetsStudentCardInfoItem pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsStudentCardInfoItemDTO> m)
			{
				m.MapFrom<Semester>((VetsStudentCardInfoItemDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			});
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003B18 File Offset: 0x00001D18
		public static VetsStudentCardInfoItem ToDomainObject(this VetsStudentCardInfoItemDTO surveyDTO)
		{
			return Mapper.Map<VetsStudentCardInfoItemDTO, VetsStudentCardInfoItem>(surveyDTO);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003B30 File Offset: 0x00001D30
		public static VetsStudentCardInfoItemDTO ToDTO(this VetsStudentCardInfoItem survey)
		{
			return Mapper.Map<VetsStudentCardInfoItem, VetsStudentCardInfoItemDTO>(survey);
		}
	}
}
