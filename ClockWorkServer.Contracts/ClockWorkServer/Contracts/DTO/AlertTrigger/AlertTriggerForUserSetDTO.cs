using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger
{
	// Token: 0x02000C76 RID: 3190
	[DataContract(Namespace = "http://tpro.ca")]
	public class AlertTriggerForUserSetDTO
	{
		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x000207BB File Offset: 0x0001E9BB
		// (set) Token: 0x0600427F RID: 17023 RVA: 0x000207C3 File Offset: 0x0001E9C3
		[DataMember]
		public virtual int StudentPersonId { get; set; }

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000207CC File Offset: 0x0001E9CC
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x000207D4 File Offset: 0x0001E9D4
		[DataMember]
		public AlertTriggerForUserGroupDTO[] AlertTriggerGroups { get; set; }
	}
}
