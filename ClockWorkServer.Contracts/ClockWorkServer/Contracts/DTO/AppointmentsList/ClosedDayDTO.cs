using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AC1 RID: 2753
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClosedDayDTO
	{
		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x0001C514 File Offset: 0x0001A714
		// (set) Token: 0x06003A61 RID: 14945 RVA: 0x0001C51C File Offset: 0x0001A71C
		[DataMember]
		public int Availability2ItemsClosedDaysId { get; set; }

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x0001C525 File Offset: 0x0001A725
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x0001C52D File Offset: 0x0001A72D
		[DataMember]
		public DateTime DateClosed { get; set; }

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x0001C536 File Offset: 0x0001A736
		// (set) Token: 0x06003A65 RID: 14949 RVA: 0x0001C53E File Offset: 0x0001A73E
		[DataMember]
		public string Note { get; set; }

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x0001C547 File Offset: 0x0001A747
		// (set) Token: 0x06003A67 RID: 14951 RVA: 0x0001C54F File Offset: 0x0001A74F
		[DataMember]
		public PersonBaseDTO Staff { get; set; }
	}
}
