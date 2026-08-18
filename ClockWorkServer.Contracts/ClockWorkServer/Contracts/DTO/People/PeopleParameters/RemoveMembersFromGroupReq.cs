using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003D4 RID: 980
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveMembersFromGroupReq : BaseMessageReq
	{
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0000A1E7 File Offset: 0x000083E7
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0000A1EF File Offset: 0x000083EF
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0000A1F8 File Offset: 0x000083F8
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x0000A200 File Offset: 0x00008400
		[DataMember]
		public int[] PersonIds { get; set; }
	}
}
