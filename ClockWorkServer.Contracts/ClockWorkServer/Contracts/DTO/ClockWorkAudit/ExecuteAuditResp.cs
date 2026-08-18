using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit
{
	// Token: 0x02000897 RID: 2199
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteAuditResp
	{
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000151D4 File Offset: 0x000133D4
		// (set) Token: 0x06002C96 RID: 11414 RVA: 0x000151DC File Offset: 0x000133DC
		[DataMember]
		public AuditResultDTO Result { get; set; }
	}
}
