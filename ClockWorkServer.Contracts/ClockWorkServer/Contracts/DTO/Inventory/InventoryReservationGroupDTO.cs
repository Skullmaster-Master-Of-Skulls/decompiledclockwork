using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B1 RID: 1457
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryReservationGroupDTO
	{
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x0000DB3F File Offset: 0x0000BD3F
		// (set) Token: 0x06001E0B RID: 7691 RVA: 0x0000DB47 File Offset: 0x0000BD47
		[DataMember]
		public int ReservationGroupId { get; set; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0000DB50 File Offset: 0x0000BD50
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x0000DB58 File Offset: 0x0000BD58
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0000DB61 File Offset: 0x0000BD61
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x0000DB69 File Offset: 0x0000BD69
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0000DB72 File Offset: 0x0000BD72
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x0000DB7A File Offset: 0x0000BD7A
		[DataMember]
		public string ReservationNotes { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0000DB83 File Offset: 0x0000BD83
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x0000DB8B File Offset: 0x0000BD8B
		[DataMember]
		public PersonBaseDTO WhoMadeReservation { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x0000DB94 File Offset: 0x0000BD94
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		[DataMember]
		public PersonBaseDTO WhoReservedStaffPerson { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0000DBA5 File Offset: 0x0000BDA5
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x0000DBAD File Offset: 0x0000BDAD
		[DataMember]
		public DateTime CreationDate { get; set; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0000DBB6 File Offset: 0x0000BDB6
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		[DataMember]
		public IList<string> NotificationEmails { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0000DBC7 File Offset: 0x0000BDC7
		// (set) Token: 0x06001E1B RID: 7707 RVA: 0x0000DBCF File Offset: 0x0000BDCF
		[DataMember]
		public bool BeNotified { get; set; }
	}
}
