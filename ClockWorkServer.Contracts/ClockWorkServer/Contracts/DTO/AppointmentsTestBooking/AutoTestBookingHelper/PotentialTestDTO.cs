using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA2 RID: 2722
	[DataContract(Namespace = "http://tpro.ca")]
	public class PotentialTestDTO
	{
		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x0001BF80 File Offset: 0x0001A180
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x0001BF88 File Offset: 0x0001A188
		[DataMember]
		public List<PotentialTestMethodFoundNoteDTO> MethodFoundNotes { get; set; }

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x0001BF91 File Offset: 0x0001A191
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x0001BF99 File Offset: 0x0001A199
		[DataMember]
		public int Id { get; set; }

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x0001BFA2 File Offset: 0x0001A1A2
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x0001BFAA File Offset: 0x0001A1AA
		[DataMember]
		public bool OkToDoubleBook { get; set; }

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x0001BFB3 File Offset: 0x0001A1B3
		// (set) Token: 0x060039A0 RID: 14752 RVA: 0x0001BFBB File Offset: 0x0001A1BB
		[DataMember]
		public DateTime? PotentialTestDate { get; set; }

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		// (set) Token: 0x060039A2 RID: 14754 RVA: 0x0001BFCC File Offset: 0x0001A1CC
		[DataMember]
		public DateTime? PotentialTestStartTime { get; set; }

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x0001BFD5 File Offset: 0x0001A1D5
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x0001BFDD File Offset: 0x0001A1DD
		[DataMember]
		public DateTime? PotentialTestEndTime { get; set; }

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		// (set) Token: 0x060039A6 RID: 14758 RVA: 0x0001BFEE File Offset: 0x0001A1EE
		[DataMember]
		public TestDTO Test { get; set; }
	}
}
