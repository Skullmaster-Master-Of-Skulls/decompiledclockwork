using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003CA RID: 970
	[DataContract(Namespace = "http://tpro.ca")]
	public class AdminDeleteGroupReq : BaseMessageReq
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0000A15F File Offset: 0x0000835F
		// (set) Token: 0x0600158C RID: 5516 RVA: 0x0000A167 File Offset: 0x00008367
		[DataMember]
		public int GroupId { get; set; }
	}
}
