using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x0200003C RID: 60
	public static class AgreementFormConsentMapper
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00007534 File Offset: 0x00005734
		static AgreementFormConsentMapper()
		{
			DynamicFormMapper.CreateMap();
			GroupMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<AgreementFormConsent, AgreementFormConsentDTO>();
			Mapper.CreateMap<AgreementFormConsentDTO, AgreementFormConsent>();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007558 File Offset: 0x00005758
		public static AgreementFormConsent ToDomainObject(this AgreementFormConsentDTO surveyDTO)
		{
			return Mapper.Map<AgreementFormConsentDTO, AgreementFormConsent>(surveyDTO);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007570 File Offset: 0x00005770
		public static AgreementFormConsentDTO ToDTO(this AgreementFormConsent survey)
		{
			return Mapper.Map<AgreementFormConsent, AgreementFormConsentDTO>(survey);
		}
	}
}
