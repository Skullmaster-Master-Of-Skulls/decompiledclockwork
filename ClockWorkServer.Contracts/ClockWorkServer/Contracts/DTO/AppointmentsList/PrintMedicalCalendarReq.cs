using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE0 RID: 2784
	[DataContract(Namespace = "http://tpro.ca")]
	public class PrintMedicalCalendarReq : BaseReportMessageReq
	{
		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x0001CA9B File Offset: 0x0001AC9B
		// (set) Token: 0x06003ADB RID: 15067 RVA: 0x0001CAA3 File Offset: 0x0001ACA3
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		// (set) Token: 0x06003ADD RID: 15069 RVA: 0x0001CAB4 File Offset: 0x0001ACB4
		[DataMember]
		public int NumDays { get; set; }

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x0001CABD File Offset: 0x0001ACBD
		// (set) Token: 0x06003ADF RID: 15071 RVA: 0x0001CAC5 File Offset: 0x0001ACC5
		[DataMember]
		public IList<PersonBaseDTO> Staff { get; set; }

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x0001CACE File Offset: 0x0001ACCE
		// (set) Token: 0x06003AE1 RID: 15073 RVA: 0x0001CAD6 File Offset: 0x0001ACD6
		[DataMember]
		public eFileFormatDTO OutputFormat { get; set; }

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x0001CADF File Offset: 0x0001ACDF
		// (set) Token: 0x06003AE3 RID: 15075 RVA: 0x0001CAE7 File Offset: 0x0001ACE7
		[DataMember]
		public bool HideCancelled { get; set; }
	}
}
