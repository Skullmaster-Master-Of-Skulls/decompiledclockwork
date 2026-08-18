using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD7 RID: 2775
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClosedDaysReq : BaseMessageReq
	{
		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x0001C9AD File Offset: 0x0001ABAD
		// (set) Token: 0x06003AB6 RID: 15030 RVA: 0x0001C9B5 File Offset: 0x0001ABB5
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x0001C9BE File Offset: 0x0001ABBE
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x0001C9C6 File Offset: 0x0001ABC6
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x0001C9D7 File Offset: 0x0001ABD7
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
