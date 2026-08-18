using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C62 RID: 3170
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsMediaContentAlreadyRequestedReq : BaseReportMessageReq
	{
		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x00020417 File Offset: 0x0001E617
		// (set) Token: 0x0600420F RID: 16911 RVA: 0x0002041F File Offset: 0x0001E61F
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x00020428 File Offset: 0x0001E628
		// (set) Token: 0x06004211 RID: 16913 RVA: 0x00020430 File Offset: 0x0001E630
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }
	}
}
