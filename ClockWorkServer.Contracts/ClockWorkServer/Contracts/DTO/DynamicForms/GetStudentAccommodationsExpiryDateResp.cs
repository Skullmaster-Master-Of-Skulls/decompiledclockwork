using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200061E RID: 1566
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentAccommodationsExpiryDateResp
	{
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0000E71A File Offset: 0x0000C91A
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x0000E722 File Offset: 0x0000C922
		[DataMember]
		public DateTime? ExpiryDate { get; set; }
	}
}
