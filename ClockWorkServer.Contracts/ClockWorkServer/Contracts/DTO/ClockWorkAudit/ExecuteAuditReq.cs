using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit
{
	// Token: 0x02000896 RID: 2198
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteAuditReq : BaseReportMessageReq
	{
		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000151C3 File Offset: 0x000133C3
		// (set) Token: 0x06002C93 RID: 11411 RVA: 0x000151CB File Offset: 0x000133CB
		[DataMember]
		public eClockWorkAuditType AuditType { get; set; }
	}
}
