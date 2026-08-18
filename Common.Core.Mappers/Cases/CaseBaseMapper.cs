using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.Public.Entities.Cases;

namespace TechnoPro.Common.Core.Mappers.Cases
{
	// Token: 0x02000175 RID: 373
	public static class CaseBaseMapper
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		static CaseBaseMapper()
		{
			Mapper.CreateMap<CaseBaseDTO, CaseBase>().ForMember((CaseBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CaseBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<CaseBase, CaseBaseDTO>();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001D44C File Offset: 0x0001B64C
		public static CaseBase ToDomainObject(this CaseBaseDTO lookupCourseDTO)
		{
			return Mapper.Map<CaseBaseDTO, CaseBase>(lookupCourseDTO);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001D464 File Offset: 0x0001B664
		public static CaseBaseDTO ToDTO(this CaseBase lookupCourse)
		{
			return Mapper.Map<CaseBase, CaseBaseDTO>(lookupCourse);
		}
	}
}
