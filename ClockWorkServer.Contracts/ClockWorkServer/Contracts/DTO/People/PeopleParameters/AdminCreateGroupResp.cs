using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003C7 RID: 967
	[DataContract(Namespace = "http://tpro.ca")]
	public class AdminCreateGroupResp
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0000A13D File Offset: 0x0000833D
		// (set) Token: 0x06001585 RID: 5509 RVA: 0x0000A145 File Offset: 0x00008345
		[DataMember]
		public int GroupId { get; set; }
	}
}
