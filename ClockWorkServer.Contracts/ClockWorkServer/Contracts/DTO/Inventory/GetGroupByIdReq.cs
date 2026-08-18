using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200052B RID: 1323
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetGroupByIdReq : BaseMessageReq
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0000CB59 File Offset: 0x0000AD59
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x0000CB61 File Offset: 0x0000AD61
		[DataMember]
		public int GroupId { get; set; }
	}
}
