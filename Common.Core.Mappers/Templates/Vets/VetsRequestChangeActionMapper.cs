using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x02000045 RID: 69
	public static class VetsRequestChangeActionMapper
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00008A90 File Offset: 0x00006C90
		static VetsRequestChangeActionMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<VetsRequestChangeAction, VetsRequestChangeActionDTO>().ForMember((VetsRequestChangeActionDTO pb) => pb.WhoChanged, delegate(IMemberConfigurationExpression<VetsRequestChangeAction> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsRequestChangeAction pbdto) => (pbdto.WhoChanged == null) ? null : pbdto.WhoChanged.ToDTO());
			});
			Mapper.CreateMap<VetsRequestChangeActionDTO, VetsRequestChangeAction>().ForMember((VetsRequestChangeAction pb) => pb.WhoChanged, delegate(IMemberConfigurationExpression<VetsRequestChangeActionDTO> m)
			{
				m.MapFrom<AppointmentRoom>((VetsRequestChangeActionDTO pbdto) => (pbdto.WhoChanged == null) ? null : pbdto.WhoChanged.ToDomainObject());
			});
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008B4C File Offset: 0x00006D4C
		public static VetsRequestChangeAction ToDomainObject(this VetsRequestChangeActionDTO surveyDTO)
		{
			return Mapper.Map<VetsRequestChangeActionDTO, VetsRequestChangeAction>(surveyDTO);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008B64 File Offset: 0x00006D64
		public static VetsRequestChangeActionDTO ToDTO(this VetsRequestChangeAction survey)
		{
			return Mapper.Map<VetsRequestChangeAction, VetsRequestChangeActionDTO>(survey);
		}
	}
}
