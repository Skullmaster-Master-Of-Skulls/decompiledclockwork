using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.Cases;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Cases
{
	// Token: 0x02000177 RID: 375
	public static class CaseForDisplayMapper
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0001D568 File Offset: 0x0001B768
		static CaseForDisplayMapper()
		{
			CaseBaseMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<CaseForDisplayDTO, CaseForDisplay>().ForMember((CaseForDisplay pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CaseForDisplayDTO> m)
			{
				m.Ignore();
			}).ForMember((CaseForDisplay pb) => pb.DynamicFormDataSummary, delegate(IMemberConfigurationExpression<CaseForDisplayDTO> m)
			{
				m.MapFrom<List<DynamicData>>((CaseForDisplayDTO pbdto) => (pbdto.DynamicFormDataSummary == null) ? null : (from g in pbdto.DynamicFormDataSummary
				select g.ToDomainObject()).ToList<DynamicData>());
			}).ForMember((CaseForDisplay pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<CaseForDisplayDTO> m)
			{
				m.MapFrom<PersonBase>((CaseForDisplayDTO pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDomainObject());
			});
			Mapper.CreateMap<CaseForDisplay, CaseForDisplayDTO>().ForMember((CaseForDisplayDTO pb) => pb.DynamicFormDataSummary, delegate(IMemberConfigurationExpression<CaseForDisplay> m)
			{
				m.MapFrom<List<DynamicDataDTO>>((CaseForDisplay pbdto) => (pbdto.DynamicFormDataSummary == null) ? null : (from g in pbdto.DynamicFormDataSummary
				select g.ToDTO()).ToList<DynamicDataDTO>());
			}).ForMember((CaseForDisplayDTO pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<CaseForDisplay> m)
			{
				m.MapFrom<PersonBaseDTO>((CaseForDisplay pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDTO());
			});
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001D730 File Offset: 0x0001B930
		public static CaseForDisplay ToDomainObject(this CaseForDisplayDTO lookupCourseDTO)
		{
			return Mapper.Map<CaseForDisplayDTO, CaseForDisplay>(lookupCourseDTO);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001D748 File Offset: 0x0001B948
		public static CaseForDisplayDTO ToDTO(this CaseForDisplay lookupCourse)
		{
			return Mapper.Map<CaseForDisplay, CaseForDisplayDTO>(lookupCourse);
		}
	}
}
