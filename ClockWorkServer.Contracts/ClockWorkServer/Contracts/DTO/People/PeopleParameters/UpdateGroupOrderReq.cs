using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003CC RID: 972
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateGroupOrderReq : BaseMessageReq
	{
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0000A170 File Offset: 0x00008370
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x0000A178 File Offset: 0x00008378
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0000A181 File Offset: 0x00008381
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x0000A189 File Offset: 0x00008389
		[DataMember]
		public int NewOrderNum { get; set; }
	}
}
