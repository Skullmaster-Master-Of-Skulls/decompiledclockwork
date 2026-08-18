using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D9 RID: 2009
	[DataContract(Namespace = "http://tpro.ca")]
	public class SetSessionChooserDefaultValueReq : BaseMessageReq
	{
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x00013658 File Offset: 0x00011858
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x00013660 File Offset: 0x00011860
		[DataMember]
		public DateTime DtpNowAdjusted { get; set; }
	}
}
