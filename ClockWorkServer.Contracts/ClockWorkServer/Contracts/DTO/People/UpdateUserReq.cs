using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200038E RID: 910
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateUserReq : BaseMessageReq
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x00009B0B File Offset: 0x00007D0B
		public UpdateUserReq()
		{
			this.UpdateGroupMemberships = true;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00009B1D File Offset: 0x00007D1D
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00009B25 File Offset: 0x00007D25
		[DataMember]
		public PersonBaseDTO User { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00009B2E File Offset: 0x00007D2E
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x00009B36 File Offset: 0x00007D36
		[DataMember]
		public bool UpdateGroupMemberships { get; set; }
	}
}
