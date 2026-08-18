using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003ED RID: 1005
	[DataContract(Namespace = "http://tpro.ca")]
	public class OnlineFormForDisplayDTO
	{
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0000A4E4 File Offset: 0x000086E4
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x0000A4EC File Offset: 0x000086EC
		[DataMember]
		public int OnlineFormId { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0000A4F5 File Offset: 0x000086F5
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x0000A4FD File Offset: 0x000086FD
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0000A506 File Offset: 0x00008706
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x0000A50E File Offset: 0x0000870E
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0000A517 File Offset: 0x00008717
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x0000A51F File Offset: 0x0000871F
		[DataMember]
		public string ShortCode { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0000A528 File Offset: 0x00008728
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x0000A530 File Offset: 0x00008730
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
