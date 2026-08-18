using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD0 RID: 3024
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsByStudentAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x0001F59D File Offset: 0x0001D79D
		// (set) Token: 0x06003FC9 RID: 16329 RVA: 0x0001F5A5 File Offset: 0x0001D7A5
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x0001F5AE File Offset: 0x0001D7AE
		// (set) Token: 0x06003FCB RID: 16331 RVA: 0x0001F5B6 File Offset: 0x0001D7B6
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x0001F5BF File Offset: 0x0001D7BF
		// (set) Token: 0x06003FCD RID: 16333 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		// (set) Token: 0x06003FCF RID: 16335 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		[DataMember]
		public int CampusId { get; set; }
	}
}
