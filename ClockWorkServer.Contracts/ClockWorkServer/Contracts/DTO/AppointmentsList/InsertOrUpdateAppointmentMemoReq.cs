using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF2 RID: 2802
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentMemoReq : BaseMessageReq
	{
		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x0001CD21 File Offset: 0x0001AF21
		// (set) Token: 0x06003B39 RID: 15161 RVA: 0x0001CD29 File Offset: 0x0001AF29
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x0001CD32 File Offset: 0x0001AF32
		// (set) Token: 0x06003B3B RID: 15163 RVA: 0x0001CD3A File Offset: 0x0001AF3A
		[DataMember]
		public string MemoText { get; set; }
	}
}
