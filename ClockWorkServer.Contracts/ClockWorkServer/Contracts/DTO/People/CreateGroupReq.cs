using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200038F RID: 911
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateGroupReq : BaseMessageReq
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00009B3F File Offset: 0x00007D3F
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x00009B47 File Offset: 0x00007D47
		[DataMember]
		public GroupDTO Group { get; set; }
	}
}
