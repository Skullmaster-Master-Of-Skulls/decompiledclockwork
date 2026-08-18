using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger
{
	// Token: 0x02000C78 RID: 3192
	[DataContract(Namespace = "http://tpro.ca")]
	public class CheckForTriggerAlertsReq : BaseMessageReq
	{
		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x000207FF File Offset: 0x0001E9FF
		// (set) Token: 0x06004289 RID: 17033 RVA: 0x00020807 File Offset: 0x0001EA07
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
