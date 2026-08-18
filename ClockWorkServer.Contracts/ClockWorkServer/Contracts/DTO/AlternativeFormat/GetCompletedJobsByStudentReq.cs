using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BCC RID: 3020
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByStudentReq : BaseMessageReq
	{
		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x0001F515 File Offset: 0x0001D715
		// (set) Token: 0x06003FB5 RID: 16309 RVA: 0x0001F51D File Offset: 0x0001D71D
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x0001F526 File Offset: 0x0001D726
		// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x0001F52E File Offset: 0x0001D72E
		[DataMember]
		public int CampusId { get; set; }
	}
}
