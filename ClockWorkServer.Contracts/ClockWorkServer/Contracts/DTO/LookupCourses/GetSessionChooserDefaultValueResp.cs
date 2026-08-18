using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007DA RID: 2010
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSessionChooserDefaultValueResp
	{
		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x00013669 File Offset: 0x00011869
		// (set) Token: 0x06002900 RID: 10496 RVA: 0x00013671 File Offset: 0x00011871
		[DataMember]
		public DateTime? DtpNowAdjusted { get; set; }
	}
}
