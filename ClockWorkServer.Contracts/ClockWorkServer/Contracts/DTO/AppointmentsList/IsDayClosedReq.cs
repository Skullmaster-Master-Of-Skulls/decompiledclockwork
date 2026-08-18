using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD9 RID: 2777
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsDayClosedReq : BaseMessageReq
	{
		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x0001CA02 File Offset: 0x0001AC02
		// (set) Token: 0x06003AC2 RID: 15042 RVA: 0x0001CA0A File Offset: 0x0001AC0A
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x0001CA13 File Offset: 0x0001AC13
		// (set) Token: 0x06003AC4 RID: 15044 RVA: 0x0001CA1B File Offset: 0x0001AC1B
		[DataMember]
		public DateTime Date { get; set; }
	}
}
