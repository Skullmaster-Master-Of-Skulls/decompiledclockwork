using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ActionPlan;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.Legacy.ActionPlan;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Legacy.ActionPlan;
using TechnoPro.Common.Public.Entities.Legacy.ServiceProviders;

namespace TechnoPro.Common.Core.Mappers.Legacy.ServiceProvider
{
	// Token: 0x020000E7 RID: 231
	public static class LegacyServiceProviderRequestDetailMapper
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00012654 File Offset: 0x00010854
		static LegacyServiceProviderRequestDetailMapper()
		{
			BinaryFileMapper.CreateMap();
			Mapper.CreateMap<LegacyServiceProviderRequestDetailDTO, LegacyServiceProviderRequestDetail>().ForMember((LegacyServiceProviderRequestDetail pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LegacyServiceProviderRequestDetailDTO> m)
			{
				m.Ignore();
			}).ForMember((LegacyServiceProviderRequestDetail pb) => pb.FsFirstNationsLetterOfApprovalFile, delegate(IMemberConfigurationExpression<LegacyServiceProviderRequestDetailDTO> m)
			{
				m.MapFrom<BinaryFile>((LegacyServiceProviderRequestDetailDTO pbdto) => (pbdto.FsFirstNationsLetterOfApprovalFile == null) ? null : pbdto.FsFirstNationsLetterOfApprovalFile.ToDomainObject());
			}).ForMember((LegacyServiceProviderRequestDetail pb) => pb.FsWsibLetterOfApprovalFile, delegate(IMemberConfigurationExpression<LegacyServiceProviderRequestDetailDTO> m)
			{
				m.MapFrom<BinaryFile>((LegacyServiceProviderRequestDetailDTO pbdto) => (pbdto.FsWsibLetterOfApprovalFile == null) ? null : pbdto.FsWsibLetterOfApprovalFile.ToDomainObject());
			}).ForMember((LegacyServiceProviderRequestDetail pb) => pb.FsOtherFile, delegate(IMemberConfigurationExpression<LegacyServiceProviderRequestDetailDTO> m)
			{
				m.MapFrom<BinaryFile>((LegacyServiceProviderRequestDetailDTO pbdto) => (pbdto.FsOtherFile == null) ? null : pbdto.FsOtherFile.ToDomainObject());
			});
			Mapper.CreateMap<LegacyServiceProviderRequestDetail, LegacyServiceProviderRequestDetailDTO>().ForMember((LegacyServiceProviderRequestDetailDTO pb) => pb.FsFirstNationsLetterOfApprovalFile, delegate(IMemberConfigurationExpression<LegacyServiceProviderRequestDetail> m)
			{
				m.MapFrom<BinaryFileDTO>((LegacyServiceProviderRequestDetail pbdto) => (pbdto.FsFirstNationsLetterOfApprovalFile == null) ? null : pbdto.FsFirstNationsLetterOfApprovalFile.ToDTO());
			}).ForMember((LegacyServiceProviderRequestDetailDTO pb) => pb.FsWsibLetterOfApprovalFile, delegate(IMemberConfigurationExpression<LegacyServiceProviderRequestDetail> m)
			{
				m.MapFrom<BinaryFileDTO>((LegacyServiceProviderRequestDetail pbdto) => (pbdto.FsWsibLetterOfApprovalFile == null) ? null : pbdto.FsWsibLetterOfApprovalFile.ToDTO());
			}).ForMember((LegacyServiceProviderRequestDetailDTO pb) => pb.FsOtherFile, delegate(IMemberConfigurationExpression<LegacyServiceProviderRequestDetail> m)
			{
				m.MapFrom<BinaryFileDTO>((LegacyServiceProviderRequestDetail pbdto) => (pbdto.FsOtherFile == null) ? null : pbdto.FsOtherFile.ToDTO());
			});
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000128AC File Offset: 0x00010AAC
		public static LegacyServiceProviderRequestDetail ToDomainObject(this LegacyServiceProviderRequestDetailDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacyServiceProviderRequestDetailDTO, LegacyServiceProviderRequestDetail>(dynamicDataDTO);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000128C4 File Offset: 0x00010AC4
		public static LegacyServiceProviderRequestDetailDTO ToDTO(this LegacyServiceProviderRequestDetail dynamicData)
		{
			return Mapper.Map<LegacyServiceProviderRequestDetail, LegacyServiceProviderRequestDetailDTO>(dynamicData);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000128DC File Offset: 0x00010ADC
		public static IList<ActionPlanNote> ToDomainObject(this IList<ActionPlanNoteDTO> daos)
		{
			IList<ActionPlanNote> result;
			if (daos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in daos
				select g.ToDomainObject()).ToList<ActionPlanNote>();
			}
			return result;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012920 File Offset: 0x00010B20
		public static IList<ActionPlanNoteDTO> ToDTO(this IList<ActionPlanNote> entities)
		{
			IList<ActionPlanNoteDTO> result;
			if (entities == null)
			{
				result = null;
			}
			else
			{
				result = (from g in entities
				select g.ToDTO()).ToList<ActionPlanNoteDTO>();
			}
			return result;
		}
	}
}
