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
	// Token: 0x0200003E RID: 62
	public static class VetsBenefitApplicationRegistrationMapper
	{
		// Token: 0x06000100 RID: 256 RVA: 0x0000793C File Offset: 0x00005B3C
		static VetsBenefitApplicationRegistrationMapper()
		{
			VetsBenefitApplicationMapper.CreateMap();
			Mapper.CreateMap<VetsBenefitApplicationRegistration, VetsBenefitApplicationRegistrationDTO>().ForMember((VetsBenefitApplicationRegistrationDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationRegistration> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplicationRegistration pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((VetsBenefitApplicationRegistrationDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationRegistration> m)
			{
				m.MapFrom<SemesterDTO>((VetsBenefitApplicationRegistration pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			}).ForMember((VetsBenefitApplicationRegistrationDTO pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationRegistration> m)
			{
				m.MapFrom<VetsChapterDTO>((VetsBenefitApplicationRegistration pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDTO());
			});
			Mapper.CreateMap<VetsBenefitApplicationRegistrationDTO, VetsBenefitApplicationRegistration>().ForMember((VetsBenefitApplicationRegistration pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsBenefitApplicationRegistrationDTO> m)
			{
				m.Ignore();
			}).ForMember((VetsBenefitApplicationRegistration pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationRegistrationDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationRegistrationDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((VetsBenefitApplicationRegistration pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationRegistrationDTO> m)
			{
				m.MapFrom<Semester>((VetsBenefitApplicationRegistrationDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			}).ForMember((VetsBenefitApplicationRegistration pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationRegistrationDTO> m)
			{
				m.MapFrom<VetsChapter>((VetsBenefitApplicationRegistrationDTO pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDomainObject());
			});
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007B94 File Offset: 0x00005D94
		public static VetsBenefitApplicationRegistration ToDomainObject(this VetsBenefitApplicationRegistrationDTO dto)
		{
			return Mapper.Map<VetsBenefitApplicationRegistrationDTO, VetsBenefitApplicationRegistration>(dto);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007BAC File Offset: 0x00005DAC
		public static VetsBenefitApplicationRegistrationDTO ToDTO(this VetsBenefitApplicationRegistration item)
		{
			return Mapper.Map<VetsBenefitApplicationRegistration, VetsBenefitApplicationRegistrationDTO>(item);
		}
	}
}
