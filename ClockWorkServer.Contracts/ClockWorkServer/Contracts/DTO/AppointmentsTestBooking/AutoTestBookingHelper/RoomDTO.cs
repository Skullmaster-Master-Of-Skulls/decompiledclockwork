using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA5 RID: 2725
	[DataContract(Namespace = "http://tpro.ca")]
	public class RoomDTO
	{
		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x0001C019 File Offset: 0x0001A219
		// (set) Token: 0x060039AF RID: 14767 RVA: 0x0001C021 File Offset: 0x0001A221
		[DataMember]
		public RoomType RoomType { get; set; }

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x0001C02A File Offset: 0x0001A22A
		// (set) Token: 0x060039B1 RID: 14769 RVA: 0x0001C032 File Offset: 0x0001A232
		[DataMember]
		public int RoomId { get; set; }

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x0001C03B File Offset: 0x0001A23B
		// (set) Token: 0x060039B3 RID: 14771 RVA: 0x0001C043 File Offset: 0x0001A243
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x0001C04C File Offset: 0x0001A24C
		// (set) Token: 0x060039B5 RID: 14773 RVA: 0x0001C054 File Offset: 0x0001A254
		[DataMember]
		public int PriorityNumber { get; set; }

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x0001C05D File Offset: 0x0001A25D
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x0001C065 File Offset: 0x0001A265
		[DataMember]
		public IList<AccommodationDTO> GivePriorityToStudentsWithTheseAccommodations { get; set; }

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x0001C06E File Offset: 0x0001A26E
		// (set) Token: 0x060039B9 RID: 14777 RVA: 0x0001C076 File Offset: 0x0001A276
		[DataMember]
		public List<string> Campuses { get; set; }

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x0001C07F File Offset: 0x0001A27F
		// (set) Token: 0x060039BB RID: 14779 RVA: 0x0001C087 File Offset: 0x0001A287
		[DataMember]
		public List<AssetDTO> Assets { get; set; }
	}
}
