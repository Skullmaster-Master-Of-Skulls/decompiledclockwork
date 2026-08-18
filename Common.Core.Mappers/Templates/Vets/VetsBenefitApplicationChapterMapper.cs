using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.Academic;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x0200003F RID: 63
	public static class VetsBenefitApplicationChapterMapper
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00007BC4 File Offset: 0x00005DC4
		static VetsBenefitApplicationChapterMapper()
		{
			VetsBenefitApplicationMapper.CreateMap();
			Mapper.CreateMap<VetsBenefitApplicationChapter, VetsBenefitApplicationChapterDTO>().ForMember((VetsBenefitApplicationChapterDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationChapter> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplicationChapter pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((VetsBenefitApplicationChapterDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationChapter> m)
			{
				m.MapFrom<SemesterDTO>((VetsBenefitApplicationChapter pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			}).ForMember((VetsBenefitApplicationChapterDTO pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationChapter> m)
			{
				m.MapFrom<VetsChapterDTO>((VetsBenefitApplicationChapter pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDTO());
			});
			Mapper.CreateMap<VetsBenefitApplicationChapterDTO, VetsBenefitApplicationChapter>().ForMember((VetsBenefitApplicationChapter pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsBenefitApplicationChapterDTO> m)
			{
				m.Ignore();
			}).ForMember((VetsBenefitApplicationChapter pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationChapterDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationChapterDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((VetsBenefitApplicationChapter pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationChapterDTO> m)
			{
				m.MapFrom<Semester>((VetsBenefitApplicationChapterDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			}).ForMember((VetsBenefitApplicationChapter pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationChapterDTO> m)
			{
				m.MapFrom<VetsChapter>((VetsBenefitApplicationChapterDTO pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDomainObject());
			});
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007E1C File Offset: 0x0000601C
		public static VetsBenefitApplicationChapter ToDomainObject(this VetsBenefitApplicationChapterDTO dto)
		{
			return Mapper.Map<VetsBenefitApplicationChapterDTO, VetsBenefitApplicationChapter>(dto);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007E34 File Offset: 0x00006034
		public static VetsBenefitApplicationChapterDTO ToDTO(this VetsBenefitApplicationChapter item)
		{
			return Mapper.Map<VetsBenefitApplicationChapter, VetsBenefitApplicationChapterDTO>(item);
		}
	}
}
