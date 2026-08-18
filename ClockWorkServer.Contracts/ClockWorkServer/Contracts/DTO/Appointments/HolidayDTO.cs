using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000939 RID: 2361
	[DataContract(Namespace = "http://tpro.ca")]
	public class HolidayDTO
	{
		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x00017A69 File Offset: 0x00015C69
		// (set) Token: 0x06003070 RID: 12400 RVA: 0x00017A71 File Offset: 0x00015C71
		[DataMember]
		public int HolidayId { get; set; }

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x00017A7A File Offset: 0x00015C7A
		// (set) Token: 0x06003072 RID: 12402 RVA: 0x00017A82 File Offset: 0x00015C82
		[DataMember]
		public DateTime Date { get; set; }

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x00017A8B File Offset: 0x00015C8B
		// (set) Token: 0x06003074 RID: 12404 RVA: 0x00017A93 File Offset: 0x00015C93
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x00017A9C File Offset: 0x00015C9C
		// (set) Token: 0x06003076 RID: 12406 RVA: 0x00017AA4 File Offset: 0x00015CA4
		[DataMember]
		public string Description { get; set; }
	}
}
