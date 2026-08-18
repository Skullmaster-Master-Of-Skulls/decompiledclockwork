using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger
{
	// Token: 0x02000C77 RID: 3191
	[DataContract(Namespace = "http://tpro.ca")]
	public class AlertTriggerForUserGroupDTO
	{
		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x000207DD File Offset: 0x0001E9DD
		// (set) Token: 0x06004284 RID: 17028 RVA: 0x000207E5 File Offset: 0x0001E9E5
		[DataMember]
		public virtual eAlertTriggerType TriggerType { get; set; }

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x000207EE File Offset: 0x0001E9EE
		// (set) Token: 0x06004286 RID: 17030 RVA: 0x000207F6 File Offset: 0x0001E9F6
		[DataMember]
		public AlertTriggerForUserDTO[] Triggers { get; set; }
	}
}
