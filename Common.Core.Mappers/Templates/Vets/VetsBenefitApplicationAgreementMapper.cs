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
	// Token: 0x02000041 RID: 65
	public static class VetsBenefitApplicationAgreementMapper
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000080D4 File Offset: 0x000062D4
		static VetsBenefitApplicationAgreementMapper()
		{
			VetsBenefitApplicationMapper.CreateMap();
			Mapper.CreateMap<VetsBenefitApplicationAgreement, VetsBenefitApplicationAgreementDTO>().ForMember((VetsBenefitApplicationAgreementDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationAgreement> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplicationAgreement pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((VetsBenefitApplicationAgreementDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationAgreement> m)
			{
				m.MapFrom<SemesterDTO>((VetsBenefitApplicationAgreement pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			}).ForMember((VetsBenefitApplicationAgreementDTO pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationAgreement> m)
			{
				m.MapFrom<VetsChapterDTO>((VetsBenefitApplicationAgreement pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDTO());
			});
			Mapper.CreateMap<VetsBenefitApplicationAgreementDTO, VetsBenefitApplicationAgreement>().ForMember((VetsBenefitApplicationAgreement pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsBenefitApplicationAgreementDTO> m)
			{
				m.Ignore();
			}).ForMember((VetsBenefitApplicationAgreement pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationAgreementDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationAgreementDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((VetsBenefitApplicationAgreement pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationAgreementDTO> m)
			{
				m.MapFrom<Semester>((VetsBenefitApplicationAgreementDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			}).ForMember((VetsBenefitApplicationAgreement pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationAgreementDTO> m)
			{
				m.MapFrom<VetsChapter>((VetsBenefitApplicationAgreementDTO pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDomainObject());
			});
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000832C File Offset: 0x0000652C
		public static VetsBenefitApplicationAgreement ToDomainObject(this VetsBenefitApplicationAgreementDTO dto)
		{
			return Mapper.Map<VetsBenefitApplicationAgreementDTO, VetsBenefitApplicationAgreement>(dto);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008344 File Offset: 0x00006544
		public static VetsBenefitApplicationAgreementDTO ToDTO(this VetsBenefitApplicationAgreement item)
		{
			return Mapper.Map<VetsBenefitApplicationAgreement, VetsBenefitApplicationAgreementDTO>(item);
		}
	}
}
