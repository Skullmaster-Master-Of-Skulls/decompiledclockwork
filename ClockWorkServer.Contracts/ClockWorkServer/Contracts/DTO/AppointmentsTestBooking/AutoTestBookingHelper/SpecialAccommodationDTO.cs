using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA6 RID: 2726
	[DataContract(Namespace = "http://tpro.ca")]
	public class SpecialAccommodationDTO
	{
		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x0001C090 File Offset: 0x0001A290
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x0001C098 File Offset: 0x0001A298
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x0001C0A1 File Offset: 0x0001A2A1
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x0001C0A9 File Offset: 0x0001A2A9
		[DataMember]
		public string ControlIdSpecificValue { get; set; }

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x0001C0B2 File Offset: 0x0001A2B2
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x0001C0BA File Offset: 0x0001A2BA
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x0001C0C3 File Offset: 0x0001A2C3
		// (set) Token: 0x060039C4 RID: 14788 RVA: 0x0001C0CB File Offset: 0x0001A2CB
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		// (set) Token: 0x060039C6 RID: 14790 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		[DataMember]
		public StringDictionary Args { get; set; }

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x0001C0E5 File Offset: 0x0001A2E5
		// (set) Token: 0x060039C8 RID: 14792 RVA: 0x0001C0ED File Offset: 0x0001A2ED
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x0001C0F6 File Offset: 0x0001A2F6
		// (set) Token: 0x060039CA RID: 14794 RVA: 0x0001C0FE File Offset: 0x0001A2FE
		[DataMember]
		public SpecialAccommodationType SpecialAccommodationType { get; set; }
	}
}
