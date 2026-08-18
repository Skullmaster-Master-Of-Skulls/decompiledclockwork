using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x0200017D RID: 381
	public static class AddAvailabilitiesActionResultMapper
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x0001DC54 File Offset: 0x0001BE54
		static AddAvailabilitiesActionResultMapper()
		{
			AddAvailabilityActionResultMapper.CreateMap();
			Mapper.CreateMap<AddAvailabilitiesActionResultDTO, AddAvailabilitiesActionResult>().ForMember((AddAvailabilitiesActionResult pb) => pb.Results, delegate(IMemberConfigurationExpression<AddAvailabilitiesActionResultDTO> m)
			{
				m.MapFrom<IEnumerable<AddAvailabilityActionResult>>((AddAvailabilitiesActionResultDTO pbdto) => (pbdto.Results == null) ? null : (from g in pbdto.Results
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<AddAvailabilitiesActionResult, AddAvailabilitiesActionResultDTO>().ForMember((AddAvailabilitiesActionResultDTO pb) => pb.Results, delegate(IMemberConfigurationExpression<AddAvailabilitiesActionResult> m)
			{
				m.MapFrom<IEnumerable<AddAvailabilityActionResultDTO>>((AddAvailabilitiesActionResult pbdto) => (pbdto.Results == null) ? null : (from g in pbdto.Results
				select g.ToDTO()));
			});
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001DD10 File Offset: 0x0001BF10
		public static AddAvailabilitiesActionResult ToDomainObject(this AddAvailabilitiesActionResultDTO appTypeGroupDTO)
		{
			return Mapper.Map<AddAvailabilitiesActionResultDTO, AddAvailabilitiesActionResult>(appTypeGroupDTO);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001DD28 File Offset: 0x0001BF28
		public static AddAvailabilitiesActionResultDTO ToDTO(this AddAvailabilitiesActionResult appTypeGroup)
		{
			return Mapper.Map<AddAvailabilitiesActionResult, AddAvailabilitiesActionResultDTO>(appTypeGroup);
		}
	}
}
