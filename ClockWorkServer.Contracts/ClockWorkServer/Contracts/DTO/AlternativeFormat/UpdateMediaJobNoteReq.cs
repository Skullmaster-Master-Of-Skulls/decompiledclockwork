using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA6 RID: 2982
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaJobNoteReq : BaseMessageReq
	{
		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x0001F1C3 File Offset: 0x0001D3C3
		// (set) Token: 0x06003F2B RID: 16171 RVA: 0x0001F1CB File Offset: 0x0001D3CB
		[DataMember]
		public MediaJobRunningNoteDTO Note { get; set; }
	}
}
