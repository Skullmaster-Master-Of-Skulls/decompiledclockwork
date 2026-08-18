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
	// Token: 0x02000040 RID: 64
	public static class VetsBenefitApplicationBenAppMapper
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00007E4C File Offset: 0x0000604C
		static VetsBenefitApplicationBenAppMapper()
		{
			VetsBenefitApplicationMapper.CreateMap();
			Mapper.CreateMap<VetsBenefitApplicationBenApp, VetsBenefitApplicationBenAppDTO>().ForMember((VetsBenefitApplicationBenAppDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationBenApp> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplicationBenApp pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((VetsBenefitApplicationBenAppDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationBenApp> m)
			{
				m.MapFrom<SemesterDTO>((VetsBenefitApplicationBenApp pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			}).ForMember((VetsBenefitApplicationBenAppDTO pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationBenApp> m)
			{
				m.MapFrom<VetsChapterDTO>((VetsBenefitApplicationBenApp pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDTO());
			});
			Mapper.CreateMap<VetsBenefitApplicationBenAppDTO, VetsBenefitApplicationBenApp>().ForMember((VetsBenefitApplicationBenApp pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsBenefitApplicationBenAppDTO> m)
			{
				m.Ignore();
			}).ForMember((VetsBenefitApplicationBenApp pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationBenAppDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationBenAppDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((VetsBenefitApplicationBenApp pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationBenAppDTO> m)
			{
				m.MapFrom<Semester>((VetsBenefitApplicationBenAppDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			}).ForMember((VetsBenefitApplicationBenApp pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationBenAppDTO> m)
			{
				m.MapFrom<VetsChapter>((VetsBenefitApplicationBenAppDTO pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDomainObject());
			});
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000080A4 File Offset: 0x000062A4
		public static VetsBenefitApplicationBenApp ToDomainObject(this VetsBenefitApplicationBenAppDTO dto)
		{
			return Mapper.Map<VetsBenefitApplicationBenAppDTO, VetsBenefitApplicationBenApp>(dto);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000080BC File Offset: 0x000062BC
		public static VetsBenefitApplicationBenAppDTO ToDTO(this VetsBenefitApplicationBenApp item)
		{
			return Mapper.Map<VetsBenefitApplicationBenApp, VetsBenefitApplicationBenAppDTO>(item);
		}
	}
}
