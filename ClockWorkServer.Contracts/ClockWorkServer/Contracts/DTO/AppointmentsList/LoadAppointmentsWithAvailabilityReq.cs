using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE6 RID: 2790
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsWithAvailabilityReq : BaseMessageReq
	{
		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x0001CB9A File Offset: 0x0001AD9A
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x0001CBA2 File Offset: 0x0001ADA2
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x0001CBAB File Offset: 0x0001ADAB
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x0001CBB3 File Offset: 0x0001ADB3
		[DataMember]
		public int NumDays { get; set; }

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		// (set) Token: 0x06003B03 RID: 15107 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x0001CBCD File Offset: 0x0001ADCD
		// (set) Token: 0x06003B05 RID: 15109 RVA: 0x0001CBD5 File Offset: 0x0001ADD5
		[DataMember]
		public bool LoadIsStudentsFirstAppointment { get; set; }

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x0001CBDE File Offset: 0x0001ADDE
		// (set) Token: 0x06003B07 RID: 15111 RVA: 0x0001CBE6 File Offset: 0x0001ADE6
		[DataMember]
		public bool HideCancelledAppointments { get; set; }
	}
}
