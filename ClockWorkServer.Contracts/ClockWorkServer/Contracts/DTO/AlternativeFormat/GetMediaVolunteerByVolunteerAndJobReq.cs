using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF9 RID: 3065
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteerByVolunteerAndJobReq : BaseMessageReq
	{
		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x0600408F RID: 16527 RVA: 0x0001FADC File Offset: 0x0001DCDC
		// (set) Token: 0x06004090 RID: 16528 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		[DataMember]
		public int VolunteerId { get; set; }

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x0001FAED File Offset: 0x0001DCED
		// (set) Token: 0x06004092 RID: 16530 RVA: 0x0001FAF5 File Offset: 0x0001DCF5
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
