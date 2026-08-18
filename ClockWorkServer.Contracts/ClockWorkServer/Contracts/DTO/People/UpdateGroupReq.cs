using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000391 RID: 913
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateGroupReq : BaseMessageReq
	{
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00009B61 File Offset: 0x00007D61
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x00009B69 File Offset: 0x00007D69
		[DataMember]
		public GroupDTO Group { get; set; }
	}
}
