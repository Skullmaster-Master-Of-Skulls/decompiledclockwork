using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD8 RID: 2776
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsDayClosedResp
	{
		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		// (set) Token: 0x06003ABD RID: 15037 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		[DataMember]
		public bool IsClosed { get; set; }

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x0001C9F1 File Offset: 0x0001ABF1
		// (set) Token: 0x06003ABF RID: 15039 RVA: 0x0001C9F9 File Offset: 0x0001ABF9
		[DataMember]
		public ClosedDayDTO DayClosed { get; set; }
	}
}
