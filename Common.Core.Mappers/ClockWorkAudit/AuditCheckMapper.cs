using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.Core.Mappers.ClockWorkAudit
{
	// Token: 0x02000173 RID: 371
	public static class AuditCheckMapper
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		static AuditCheckMapper()
		{
			Mapper.CreateMap<AuditCheck, AuditCheckDTO>();
			Mapper.CreateMap<AuditCheckDTO, AuditCheck>();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
		public static AuditCheck ToDomainObject(this AuditCheckDTO departmentDTO)
		{
			return Mapper.Map<AuditCheckDTO, AuditCheck>(departmentDTO);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		public static AuditCheckDTO ToDTO(this AuditCheck department)
		{
			return Mapper.Map<AuditCheck, AuditCheckDTO>(department);
		}
	}
}
