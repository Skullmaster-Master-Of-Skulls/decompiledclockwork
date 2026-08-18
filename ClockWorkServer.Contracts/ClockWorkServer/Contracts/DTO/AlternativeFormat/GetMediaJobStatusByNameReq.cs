using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE4 RID: 3044
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobStatusByNameReq : BaseMessageReq
	{
		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x0001F8BC File Offset: 0x0001DABC
		// (set) Token: 0x0600403B RID: 16443 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		[DataMember]
		public string MediaJobStatusName { get; set; }
	}
}
