using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B8 RID: 2488
	[DataContract(Namespace = "http://tpro.ca")]
	public class AccommodationForTestDTO
	{
		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x00019078 File Offset: 0x00017278
		// (set) Token: 0x06003378 RID: 13176 RVA: 0x00019080 File Offset: 0x00017280
		[DataMember]
		public DynamicDataDTO DynamicFieldData { get; set; }

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x00019089 File Offset: 0x00017289
		// (set) Token: 0x0600337A RID: 13178 RVA: 0x00019091 File Offset: 0x00017291
		[DataMember]
		public bool UseForTest { get; set; }

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x0001909A File Offset: 0x0001729A
		// (set) Token: 0x0600337C RID: 13180 RVA: 0x000190A2 File Offset: 0x000172A2
		[DataMember]
		public bool Discrepency { get; set; }

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000190AB File Offset: 0x000172AB
		// (set) Token: 0x0600337E RID: 13182 RVA: 0x000190B3 File Offset: 0x000172B3
		[DataMember]
		public string DiscrepencyMessage { get; set; }
	}
}
