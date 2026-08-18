using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F4 RID: 244
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteCampusReq : BaseMessageReq
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000298C File Offset: 0x00000B8C
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00002994 File Offset: 0x00000B94
		[DataMember]
		public int CampusId { get; set; }
	}
}
