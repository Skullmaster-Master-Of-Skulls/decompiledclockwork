using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000393 RID: 915
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteUserReq : BaseMessageReq
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00009B83 File Offset: 0x00007D83
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x00009B8B File Offset: 0x00007D8B
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00009B94 File Offset: 0x00007D94
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x00009B9C File Offset: 0x00007D9C
		[DataMember]
		public bool JustDeactivate { get; set; }
	}
}
