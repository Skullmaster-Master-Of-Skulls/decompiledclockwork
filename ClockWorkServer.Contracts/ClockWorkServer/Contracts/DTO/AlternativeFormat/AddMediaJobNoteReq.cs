using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA4 RID: 2980
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaJobNoteReq : BaseMessageReq
	{
		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x0001F190 File Offset: 0x0001D390
		// (set) Token: 0x06003F23 RID: 16163 RVA: 0x0001F198 File Offset: 0x0001D398
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x0001F1A1 File Offset: 0x0001D3A1
		// (set) Token: 0x06003F25 RID: 16165 RVA: 0x0001F1A9 File Offset: 0x0001D3A9
		[DataMember]
		public MediaJobRunningNoteDTO Note { get; set; }
	}
}
