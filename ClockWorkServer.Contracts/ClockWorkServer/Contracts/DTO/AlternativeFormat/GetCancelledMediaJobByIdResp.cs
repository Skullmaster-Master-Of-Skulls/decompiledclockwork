using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BBF RID: 3007
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledMediaJobByIdResp
	{
		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x0001F3C1 File Offset: 0x0001D5C1
		// (set) Token: 0x06003F80 RID: 16256 RVA: 0x0001F3C9 File Offset: 0x0001D5C9
		[DataMember]
		public CancelledMediaJobDTO MediaJob { get; set; }
	}
}
