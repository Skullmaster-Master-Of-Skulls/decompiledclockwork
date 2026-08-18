using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Cases;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Cases
{
	// Token: 0x02000176 RID: 374
	public static class CaseClientMapper
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0001D47C File Offset: 0x0001B67C
		static CaseClientMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<CaseClientDTO, CaseClient>().ForMember((CaseClient pb) => pb.Client, delegate(IMemberConfigurationExpression<CaseClientDTO> m)
			{
				m.MapFrom<PersonBase>((CaseClientDTO pbdto) => (pbdto.Client == null) ? null : pbdto.Client.ToDomainObject());
			});
			Mapper.CreateMap<CaseClient, CaseClientDTO>().ForMember((CaseClientDTO pb) => pb.Client, delegate(IMemberConfigurationExpression<CaseClient> m)
			{
				m.MapFrom<PersonBaseDTO>((CaseClient pbdto) => (pbdto.Client == null) ? null : pbdto.Client.ToDTO());
			});
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001D538 File Offset: 0x0001B738
		public static CaseClient ToDomainObject(this CaseClientDTO lookupCourseDTO)
		{
			return Mapper.Map<CaseClientDTO, CaseClient>(lookupCourseDTO);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001D550 File Offset: 0x0001B750
		public static CaseClientDTO ToDTO(this CaseClient lookupCourse)
		{
			return Mapper.Map<CaseClient, CaseClientDTO>(lookupCourse);
		}
	}
}
