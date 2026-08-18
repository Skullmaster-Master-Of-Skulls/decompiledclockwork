using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000390 RID: 912
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateGroupResp
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00009B50 File Offset: 0x00007D50
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x00009B58 File Offset: 0x00007D58
		[DataMember]
		public int GroupId { get; set; }
	}
}
