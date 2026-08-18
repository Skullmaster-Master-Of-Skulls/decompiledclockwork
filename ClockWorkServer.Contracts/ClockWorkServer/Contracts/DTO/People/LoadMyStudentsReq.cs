using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003BC RID: 956
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMyStudentsReq : BaseMessageReq
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00009F94 File Offset: 0x00008194
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x00009F9C File Offset: 0x0000819C
		[DataMember]
		public int CounsellorPersonId { get; set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00009FA5 File Offset: 0x000081A5
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x00009FAD File Offset: 0x000081AD
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00009FB6 File Offset: 0x000081B6
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x00009FBE File Offset: 0x000081BE
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00009FC7 File Offset: 0x000081C7
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x00009FCF File Offset: 0x000081CF
		[DataMember]
		public bool ShowStudentsIHaveAppsWith { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00009FD8 File Offset: 0x000081D8
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x00009FE0 File Offset: 0x000081E0
		[DataMember]
		public bool ShowStudentsIAmAdvisorFor { get; set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00009FE9 File Offset: 0x000081E9
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x00009FF1 File Offset: 0x000081F1
		[DataMember]
		public bool IncludeCancelledAppointments { get; set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00009FFA File Offset: 0x000081FA
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x0000A002 File Offset: 0x00008202
		[DataMember]
		public bool IncludeNoShowAppointments { get; set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0000A00B File Offset: 0x0000820B
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x0000A013 File Offset: 0x00008213
		[DataMember]
		public int OverrideAssignedCounsellorControlId { get; set; }
	}
}
