using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000364 RID: 868
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeUserPasswordReq : BaseMessageReq
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0000951E File Offset: 0x0000771E
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00009526 File Offset: 0x00007726
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0000952F File Offset: 0x0000772F
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x00009537 File Offset: 0x00007737
		[DataMember]
		public string CurrentPassword { get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00009540 File Offset: 0x00007740
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x00009548 File Offset: 0x00007748
		[DataMember]
		public string NewPassword { get; set; }
	}
}
