using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.ClockWorkServer.Contracts.DTO.Workflows;
using TechnoPro.Common.Core.Mappers.Academic;
using TechnoPro.Common.Core.Mappers.Workflows;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Vets;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x02000042 RID: 66
	public static class VetsBenefitApplicationStatusMapper
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000835C File Offset: 0x0000655C
		static VetsBenefitApplicationStatusMapper()
		{
			VetsBenefitApplicationMapper.CreateMap();
			Mapper.CreateMap<VetsBenefitApplicationStatus, VetsBenefitApplicationStatusDTO>().ForMember((VetsBenefitApplicationStatusDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatus> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplicationStatus pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((VetsBenefitApplicationStatusDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatus> m)
			{
				m.MapFrom<SemesterDTO>((VetsBenefitApplicationStatus pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			}).ForMember((VetsBenefitApplicationStatusDTO pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatus> m)
			{
				m.MapFrom<VetsChapterDTO>((VetsBenefitApplicationStatus pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDTO());
			}).ForMember((VetsBenefitApplicationStatusDTO pb) => pb.Notes, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatus> m)
			{
				m.MapFrom<List<VetsRequestStatusNoteDTO>>((VetsBenefitApplicationStatus pbdto) => (pbdto.Notes == null) ? null : (from g in pbdto.Notes
				select g.ToDTO()).ToList<VetsRequestStatusNoteDTO>());
			}).ForMember((VetsBenefitApplicationStatusDTO pb) => pb.CurrentProgressStep, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatus> m)
			{
				m.MapFrom<ProgressStepDTO>((VetsBenefitApplicationStatus pbdto) => (pbdto.CurrentProgressStep == null) ? null : pbdto.CurrentProgressStep.ToDTO());
			}).ForMember((VetsBenefitApplicationStatusDTO pb) => pb.Screener, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatus> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplicationStatus pbdto) => (pbdto.Screener == null) ? null : pbdto.Screener.ToDTO());
			}).ForMember((VetsBenefitApplicationStatusDTO pb) => pb.Certifier, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatus> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsBenefitApplicationStatus pbdto) => (pbdto.Certifier == null) ? null : pbdto.Certifier.ToDTO());
			});
			Mapper.CreateMap<VetsBenefitApplicationStatusDTO, VetsBenefitApplicationStatus>().ForMember((VetsBenefitApplicationStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.Ignore();
			}).ForMember((VetsBenefitApplicationStatus pb) => pb.Student, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationStatusDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((VetsBenefitApplicationStatus pb) => pb.Semester, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.MapFrom<Semester>((VetsBenefitApplicationStatusDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			}).ForMember((VetsBenefitApplicationStatus pb) => pb.Chapter, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.MapFrom<VetsChapter>((VetsBenefitApplicationStatusDTO pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDomainObject());
			}).ForMember((VetsBenefitApplicationStatus pb) => pb.Notes, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.MapFrom<List<VetsRequestStatusNote>>((VetsBenefitApplicationStatusDTO pbdto) => (pbdto.Notes == null) ? null : (from g in pbdto.Notes
				select g.ToDomainObject()).ToList<VetsRequestStatusNote>());
			}).ForMember((VetsBenefitApplicationStatus pb) => pb.CurrentProgressStep, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.MapFrom<ProgressStep>((VetsBenefitApplicationStatusDTO pbdto) => (pbdto.CurrentProgressStep == null) ? null : pbdto.CurrentProgressStep.ToDomainObject());
			}).ForMember((VetsBenefitApplicationStatus pb) => pb.Screener, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationStatusDTO pbdto) => (pbdto.Screener == null) ? null : pbdto.Screener.ToDomainObject());
			}).ForMember((VetsBenefitApplicationStatus pb) => pb.Certifier, delegate(IMemberConfigurationExpression<VetsBenefitApplicationStatusDTO> m)
			{
				m.MapFrom<PersonBase>((VetsBenefitApplicationStatusDTO pbdto) => (pbdto.Certifier == null) ? null : pbdto.Certifier.ToDomainObject());
			});
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008824 File Offset: 0x00006A24
		public static VetsBenefitApplicationStatus ToDomainObject(this VetsBenefitApplicationStatusDTO dto)
		{
			return Mapper.Map<VetsBenefitApplicationStatusDTO, VetsBenefitApplicationStatus>(dto);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000883C File Offset: 0x00006A3C
		public static VetsBenefitApplicationStatusDTO ToDTO(this VetsBenefitApplicationStatus item)
		{
			return Mapper.Map<VetsBenefitApplicationStatus, VetsBenefitApplicationStatusDTO>(item);
		}
	}
}
