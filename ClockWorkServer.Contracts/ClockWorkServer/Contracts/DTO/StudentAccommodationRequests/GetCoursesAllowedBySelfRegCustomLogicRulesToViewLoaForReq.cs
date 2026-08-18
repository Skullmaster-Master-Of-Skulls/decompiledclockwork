using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000243 RID: 579
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq : BaseMessageReq
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x000060C0 File Offset: 0x000042C0
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x000060C8 File Offset: 0x000042C8
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x000060D1 File Offset: 0x000042D1
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x000060D9 File Offset: 0x000042D9
		[DataMember]
		public string StudentPersonIdHash { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x000060E2 File Offset: 0x000042E2
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x000060EA File Offset: 0x000042EA
		[DataMember]
		public string StudentPersonIdHashPlainText { get; set; }
	}
}
