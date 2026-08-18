using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger
{
	// Token: 0x02000C79 RID: 3193
	[DataContract(Namespace = "http://tpro.ca")]
	public class CheckForTriggerAlertsResp
	{
		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x00020810 File Offset: 0x0001EA10
		// (set) Token: 0x0600428C RID: 17036 RVA: 0x00020818 File Offset: 0x0001EA18
		[DataMember]
		public AlertTriggerForUserSetDTO AlertTriggerForUserSet { get; set; }
	}
}
