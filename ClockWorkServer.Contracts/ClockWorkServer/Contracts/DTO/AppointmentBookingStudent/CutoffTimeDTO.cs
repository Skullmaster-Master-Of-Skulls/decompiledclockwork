using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent
{
	// Token: 0x02000B42 RID: 2882
	[DataContract(Namespace = "http://tpro.ca")]
	public class CutoffTimeDTO
	{
		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x0001D74F File Offset: 0x0001B94F
		// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x0001D757 File Offset: 0x0001B957
		[DataMember]
		public bool Enabled { get; set; }

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x0001D760 File Offset: 0x0001B960
		// (set) Token: 0x06003CBB RID: 15547 RVA: 0x0001D768 File Offset: 0x0001B968
		[DataMember]
		public int Amount { get; set; }

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x0001D771 File Offset: 0x0001B971
		// (set) Token: 0x06003CBD RID: 15549 RVA: 0x0001D779 File Offset: 0x0001B979
		[DataMember]
		public eTimeInterval Interval { get; set; }
	}
}
