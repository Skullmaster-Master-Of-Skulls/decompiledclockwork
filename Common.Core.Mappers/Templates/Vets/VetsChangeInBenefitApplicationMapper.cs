using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x02000043 RID: 67
	public static class VetsChangeInBenefitApplicationMapper
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00008854 File Offset: 0x00006A54
		static VetsChangeInBenefitApplicationMapper()
		{
			CustomDataSetMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<VetsChangeInBenefitApplication, VetsChangeInBenefitApplicationDTO>().ForMember((VetsChangeInBenefitApplicationDTO pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<VetsChangeInBenefitApplication> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsChangeInBenefitApplication pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDTO());
			}).ForMember((VetsChangeInBenefitApplicationDTO pb) => pb.ChangeInBenefitFormCustomData, delegate(IMemberConfigurationExpression<VetsChangeInBenefitApplication> m)
			{
				m.MapFrom<CustomDataSetDTO>((VetsChangeInBenefitApplication pbdto) => (pbdto.ChangeInBenefitFormCustomData == null) ? null : pbdto.ChangeInBenefitFormCustomData.ToDTO());
			});
			Mapper.CreateMap<VetsChangeInBenefitApplicationDTO, VetsChangeInBenefitApplication>().ForMember((VetsChangeInBenefitApplication pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<VetsChangeInBenefitApplicationDTO> m)
			{
				m.MapFrom<PersonBase>((VetsChangeInBenefitApplicationDTO pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDomainObject());
			}).ForMember((VetsChangeInBenefitApplication pb) => pb.ChangeInBenefitFormCustomData, delegate(IMemberConfigurationExpression<VetsChangeInBenefitApplicationDTO> m)
			{
				m.MapFrom<CustomDataSet>((VetsChangeInBenefitApplicationDTO pbdto) => (pbdto.ChangeInBenefitFormCustomData == null) ? null : pbdto.ChangeInBenefitFormCustomData.ToDomainObject());
			});
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000089B4 File Offset: 0x00006BB4
		public static VetsChangeInBenefitApplication ToDomainObject(this VetsChangeInBenefitApplicationDTO surveyDTO)
		{
			return Mapper.Map<VetsChangeInBenefitApplicationDTO, VetsChangeInBenefitApplication>(surveyDTO);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000089CC File Offset: 0x00006BCC
		public static VetsChangeInBenefitApplicationDTO ToDTO(this VetsChangeInBenefitApplication survey)
		{
			return Mapper.Map<VetsChangeInBenefitApplication, VetsChangeInBenefitApplicationDTO>(survey);
		}
	}
}
