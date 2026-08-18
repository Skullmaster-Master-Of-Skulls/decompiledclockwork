using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D3 RID: 2515
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoBookTestOrExamPreviewReq : BaseMessageReq
	{
		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x0001963F File Offset: 0x0001783F
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x00019647 File Offset: 0x00017847
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x00019650 File Offset: 0x00017850
		// (set) Token: 0x0600343F RID: 13375 RVA: 0x00019658 File Offset: 0x00017858
		[DataMember]
		public eAutoTestBookingContext TestBookingContext { get; set; }

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x00019661 File Offset: 0x00017861
		// (set) Token: 0x06003441 RID: 13377 RVA: 0x00019669 File Offset: 0x00017869
		[DataMember]
		public int Pid { get; set; }

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x00019672 File Offset: 0x00017872
		// (set) Token: 0x06003443 RID: 13379 RVA: 0x0001967A File Offset: 0x0001787A
		[DataMember]
		public int Lucid { get; set; }

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x00019683 File Offset: 0x00017883
		// (set) Token: 0x06003445 RID: 13381 RVA: 0x0001968B File Offset: 0x0001788B
		[DataMember]
		public DateTime ClassStartDateTime { get; set; }

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003446 RID: 13382 RVA: 0x00019694 File Offset: 0x00017894
		// (set) Token: 0x06003447 RID: 13383 RVA: 0x0001969C File Offset: 0x0001789C
		[DataMember]
		public DateTime ClassEndDateTime { get; set; }

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000196A5 File Offset: 0x000178A5
		// (set) Token: 0x06003449 RID: 13385 RVA: 0x000196AD File Offset: 0x000178AD
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
