using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001FA RID: 506
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeTaskCompletedStatusReq : BaseMessageReq
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x000054D9 File Offset: 0x000036D9
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x000054E1 File Offset: 0x000036E1
		[DataMember]
		public int TaskId { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x000054EA File Offset: 0x000036EA
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x000054F2 File Offset: 0x000036F2
		[DataMember]
		public bool NewCompletedStatus { get; set; }
	}
}
