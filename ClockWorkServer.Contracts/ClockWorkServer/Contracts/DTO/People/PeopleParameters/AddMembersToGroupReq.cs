using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003D2 RID: 978
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMembersToGroupReq : BaseMessageReq
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0000A1C5 File Offset: 0x000083C5
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x0000A1CD File Offset: 0x000083CD
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0000A1D6 File Offset: 0x000083D6
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0000A1DE File Offset: 0x000083DE
		[DataMember]
		public int[] PersonIds { get; set; }
	}
}
