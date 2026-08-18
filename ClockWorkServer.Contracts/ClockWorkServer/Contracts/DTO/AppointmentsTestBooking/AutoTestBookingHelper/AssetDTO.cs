using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A97 RID: 2711
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssetDTO
	{
		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06003908 RID: 14600 RVA: 0x0001BAEA File Offset: 0x00019CEA
		// (set) Token: 0x06003909 RID: 14601 RVA: 0x0001BAF2 File Offset: 0x00019CF2
		[DataMember]
		public string AssetId { get; set; }

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x0001BAFB File Offset: 0x00019CFB
		// (set) Token: 0x0600390B RID: 14603 RVA: 0x0001BB03 File Offset: 0x00019D03
		[DataMember]
		public int Score { get; set; }

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x0001BB0C File Offset: 0x00019D0C
		// (set) Token: 0x0600390D RID: 14605 RVA: 0x0001BB14 File Offset: 0x00019D14
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x0001BB1D File Offset: 0x00019D1D
		// (set) Token: 0x0600390F RID: 14607 RVA: 0x0001BB25 File Offset: 0x00019D25
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x0001BB2E File Offset: 0x00019D2E
		// (set) Token: 0x06003911 RID: 14609 RVA: 0x0001BB36 File Offset: 0x00019D36
		[DataMember]
		public IList<AccommodationDTO> AccommodationsSupported { get; set; }

		// Token: 0x040015B4 RID: 5556
		public const int DEFAULT_SCORE = 100;
	}
}
