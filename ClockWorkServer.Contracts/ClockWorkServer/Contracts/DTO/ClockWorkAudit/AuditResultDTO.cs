using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit
{
	// Token: 0x02000895 RID: 2197
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuditResultDTO
	{
		// Token: 0x06002C8C RID: 11404 RVA: 0x0001513A File Offset: 0x0001333A
		public AuditResultDTO()
		{
			this.Checks = new List<AuditCheckDTO>();
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x00015150 File Offset: 0x00013350
		public eAuditStatus Status
		{
			get
			{
				eAuditStatus result;
				if (this.Checks != null)
				{
					result = (from g in this.Checks
					select g.Status).ToList<eAuditStatus>().GetStatus();
				}
				else
				{
					result = eAuditStatus.Failed;
				}
				return result;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000151A1 File Offset: 0x000133A1
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x000151A9 File Offset: 0x000133A9
		public eClockWorkAuditType AuditType { get; set; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000151B2 File Offset: 0x000133B2
		// (set) Token: 0x06002C91 RID: 11409 RVA: 0x000151BA File Offset: 0x000133BA
		public IList<AuditCheckDTO> Checks { get; set; }
	}
}
