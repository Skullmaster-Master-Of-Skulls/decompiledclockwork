using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.ClockWorkServer.Contracts.DTO.General;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.Academic;
using TechnoPro.Common.Core.Mappers.General;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.General;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x0200003D RID: 61
	public static class VetsBenefitApplicationMapper
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00007588 File Offset: 0x00005788
		static VetsBenefitApplicationMapper()
		{
			PersonBaseMapper.CreateMap();
			SemesterMapper.CreateMap();
			VetsChapterMapper.CreateMap();
			ModificationHistoryItemBaseMapper.CreateMap();
			VetsBenefitApplicationRegistrationMapper.CreateMap();
			VetsBenefitApplicationChapterMapper.CreateMap();
			VetsBenefitApplicationBenAppMapper.CreateMap();
			VetsBenefitApplicationAgreementMapper.CreateMap();
			VetsBenefitApplicationStatusMapper.CreateMap();
			Mapper.CreateMap<VetsBenefitApplication, VetsBenefitApplicationDTO>().Include<VetsBenefitApplicationRegistration, VetsBenefitApplicationRegistrationDTO>().Include<VetsBenefitApplicationChapter, VetsBenefitApplicationChapterDTO>().Include<VetsBenefitApplicationBenApp, VetsBenefitApplicationBenAppDTO>().Include<VetsBenefitApplicationAgreement, VetsBenefitApplicationAgreementDTO>().Include<VetsBenefitApplicationStatus, VetsBenefitApplicationStatusDTO>().ForMember((VetsBenefitApplicationDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplication> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplication pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((VetsBenefitApplicationDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplication> m)
			{
				m.MapFrom<SemesterDTO>((VetsBenefitApplication pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			}).ForMember((VetsBenefitApplicationDTO pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplication> m)
			{
				m.MapFrom<VetsChapterDTO>((VetsBenefitApplication pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDTO());
			}).ForMember((VetsBenefitApplicationDTO pb) => pb.ModificationHistoryItem, delegate(IMemberConfigurationExpression<VetsBenefitApplication> m)
			{
				m.MapFrom<ModificationHistoryItemBaseDTO>((VetsBenefitApplication pbdto) => (pbdto.ModificationHistoryItem == null) ? null : pbdto.ModificationHistoryItem.ToDTO());
			});
			Mapper.CreateMap<VetsBenefitApplicationDTO, VetsBenefitApplication>().Include<VetsBenefitApplicationRegistrationDTO, VetsBenefitApplicationRegistration>().Include<VetsBenefitApplicationChapterDTO, VetsBenefitApplicationChapter>().Include<VetsBenefitApplicationBenAppDTO, VetsBenefitApplicationBenApp>().Include<VetsBenefitApplicationAgreementDTO, VetsBenefitApplicationAgreement>().Include<VetsBenefitApplicationStatusDTO, VetsBenefitApplicationStatus>().ForMember((VetsBenefitApplication pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsBenefitApplicationDTO> m)
			{
				m.Ignore();
			}).ForMember((VetsBenefitApplication pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((VetsBenefitApplication pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationDTO> m)
			{
				m.MapFrom<Semester>((VetsBenefitApplicationDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			}).ForMember((VetsBenefitApplication pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationDTO> m)
			{
				m.MapFrom<VetsChapter>((VetsBenefitApplicationDTO pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDomainObject());
			}).ForMember((VetsBenefitApplication pb) => pb.ModificationHistoryItem, delegate(IMemberConfigurationExpression<VetsBenefitApplicationDTO> m)
			{
				m.MapFrom<ModificationHistoryItemBase>((VetsBenefitApplicationDTO pbdto) => (pbdto.ModificationHistoryItem == null) ? null : pbdto.ModificationHistoryItem.ToDomainObject());
			});
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000078DC File Offset: 0x00005ADC
		public static VetsBenefitApplication ToDomainObject(this VetsBenefitApplicationDTO dto)
		{
			return (VetsBenefitApplication)Mapper.Map(dto, dto.GetType(), typeof(VetsBenefitApplication));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000790C File Offset: 0x00005B0C
		public static VetsBenefitApplicationDTO ToDTO(this VetsBenefitApplication bo)
		{
			return (VetsBenefitApplicationDTO)Mapper.Map(bo, bo.GetType(), typeof(VetsBenefitApplicationDTO));
		}
	}
}
