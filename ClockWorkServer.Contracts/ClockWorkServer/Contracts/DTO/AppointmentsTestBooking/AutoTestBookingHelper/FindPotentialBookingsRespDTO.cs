using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA0 RID: 2720
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindPotentialBookingsRespDTO
	{
		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x0001BED6 File Offset: 0x0001A0D6
		// (set) Token: 0x06003984 RID: 14724 RVA: 0x0001BEDE File Offset: 0x0001A0DE
		[DataMember]
		public string EmailBody { get; set; }

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x0001BEE7 File Offset: 0x0001A0E7
		// (set) Token: 0x06003986 RID: 14726 RVA: 0x0001BEEF File Offset: 0x0001A0EF
		[DataMember]
		public IList<int> IconIds { get; set; }

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		// (set) Token: 0x06003988 RID: 14728 RVA: 0x0001BF00 File Offset: 0x0001A100
		[DataMember]
		public IList<PrivateNoteDTO> PrivateNotes { get; set; }

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x0001BF09 File Offset: 0x0001A109
		// (set) Token: 0x0600398A RID: 14730 RVA: 0x0001BF11 File Offset: 0x0001A111
		[DataMember]
		public BookingResultsDTO BookingResults { get; set; }

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x0001BF1A File Offset: 0x0001A11A
		// (set) Token: 0x0600398C RID: 14732 RVA: 0x0001BF22 File Offset: 0x0001A122
		[DataMember]
		public IList<PotentialTestDTO> PotentialTests { get; set; }

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x0001BF2B File Offset: 0x0001A12B
		// (set) Token: 0x0600398E RID: 14734 RVA: 0x0001BF33 File Offset: 0x0001A133
		[DataMember]
		public IList<string> DebugNotes { get; set; }
	}
}
