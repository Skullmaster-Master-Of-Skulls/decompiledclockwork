using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.Core.Mappers.ClockWorkAudit
{
	// Token: 0x02000174 RID: 372
	public static class AuditResultMapper
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		static AuditResultMapper()
		{
			AuditCheckMapper.CreateMap();
			Mapper.CreateMap<AuditResult, AuditResultDTO>().ForMember((AuditResultDTO pb) => pb.Checks, delegate(IMemberConfigurationExpression<AuditResult> m)
			{
				m.MapFrom<IEnumerable<AuditCheckDTO>>((AuditResult pbdto) => (pbdto.Checks == null) ? null : (from g in pbdto.Checks
				select g.ToDTO()));
			});
			Mapper.CreateMap<AuditResultDTO, AuditResult>().ForMember((AuditResult pb) => pb.Checks, delegate(IMemberConfigurationExpression<AuditResultDTO> m)
			{
				m.MapFrom<IEnumerable<AuditCheck>>((AuditResultDTO pbdto) => (pbdto.Checks == null) ? null : (from g in pbdto.Checks
				select g.ToDomainObject()));
			});
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		public static AuditResult ToDomainObject(this AuditResultDTO departmentDTO)
		{
			return Mapper.Map<AuditResultDTO, AuditResult>(departmentDTO);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		public static AuditResultDTO ToDTO(this AuditResult department)
		{
			return Mapper.Map<AuditResult, AuditResultDTO>(department);
		}
	}
}
