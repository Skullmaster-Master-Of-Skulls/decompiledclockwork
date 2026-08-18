using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit
{
	// Token: 0x02000894 RID: 2196
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuditCheckDTO
	{
		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000150F6 File Offset: 0x000132F6
		// (set) Token: 0x06002C84 RID: 11396 RVA: 0x000150FE File Offset: 0x000132FE
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x00015107 File Offset: 0x00013307
		// (set) Token: 0x06002C86 RID: 11398 RVA: 0x0001510F File Offset: 0x0001330F
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x00015118 File Offset: 0x00013318
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x00015120 File Offset: 0x00013320
		[DataMember]
		public string Note { get; set; }

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x00015129 File Offset: 0x00013329
		// (set) Token: 0x06002C8A RID: 11402 RVA: 0x00015131 File Offset: 0x00013331
		[DataMember]
		public eAuditStatus Status { get; set; }
	}
}
