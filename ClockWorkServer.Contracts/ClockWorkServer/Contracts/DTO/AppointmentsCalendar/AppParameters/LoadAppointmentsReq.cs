using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B1D RID: 2845
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsReq : BaseMessageReq
	{
		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x0001D232 File Offset: 0x0001B432
		// (set) Token: 0x06003BFA RID: 15354 RVA: 0x0001D23A File Offset: 0x0001B43A
		[DataMember]
		public List<int> PersonIds { get; set; }

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x0001D243 File Offset: 0x0001B443
		// (set) Token: 0x06003BFC RID: 15356 RVA: 0x0001D24B File Offset: 0x0001B44B
		[DataMember]
		public List<int> AppTypeIds { get; set; }

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x0001D254 File Offset: 0x0001B454
		// (set) Token: 0x06003BFE RID: 15358 RVA: 0x0001D25C File Offset: 0x0001B45C
		[DataMember]
		public bool HideCancelled { get; set; }

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x0001D265 File Offset: 0x0001B465
		// (set) Token: 0x06003C00 RID: 15360 RVA: 0x0001D26D File Offset: 0x0001B46D
		[DataMember]
		public bool LoadPerStudentDataIcons { get; set; }

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06003C01 RID: 15361 RVA: 0x0001D276 File Offset: 0x0001B476
		// (set) Token: 0x06003C02 RID: 15362 RVA: 0x0001D27E File Offset: 0x0001B47E
		[DataMember]
		public bool LoadPerAnonymousDataIcons { get; set; }

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06003C03 RID: 15363 RVA: 0x0001D287 File Offset: 0x0001B487
		// (set) Token: 0x06003C04 RID: 15364 RVA: 0x0001D28F File Offset: 0x0001B48F
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x0001D298 File Offset: 0x0001B498
		// (set) Token: 0x06003C06 RID: 15366 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		[DataMember]
		public DateTime EndDateTime { get; set; }
	}
}
