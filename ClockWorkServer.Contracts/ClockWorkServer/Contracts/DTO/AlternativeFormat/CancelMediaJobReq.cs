using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BDA RID: 3034
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelMediaJobReq : BaseMessageReq
	{
		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x0001F6CF File Offset: 0x0001D8CF
		// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x0001F6D7 File Offset: 0x0001D8D7
		[DataMember]
		public MediaJobDTO MediaJob { get; set; }

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x0001F6E8 File Offset: 0x0001D8E8
		[DataMember]
		public string CancelNotes { get; set; }
	}
}
