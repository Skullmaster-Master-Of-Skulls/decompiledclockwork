using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000707 RID: 1799
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseFinalExamInfoDTO
	{
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x00010CE2 File Offset: 0x0000EEE2
		// (set) Token: 0x060024D0 RID: 9424 RVA: 0x00010CEA File Offset: 0x0000EEEA
		[DataMember]
		public string ExternalId { get; set; }

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x00010CF3 File Offset: 0x0000EEF3
		// (set) Token: 0x060024D2 RID: 9426 RVA: 0x00010CFB File Offset: 0x0000EEFB
		[DataMember]
		public DateTime? StartDateTime { get; set; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x00010D04 File Offset: 0x0000EF04
		// (set) Token: 0x060024D4 RID: 9428 RVA: 0x00010D0C File Offset: 0x0000EF0C
		[DataMember]
		public DateTime? EndDateTime { get; set; }

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x00010D15 File Offset: 0x0000EF15
		// (set) Token: 0x060024D6 RID: 9430 RVA: 0x00010D1D File Offset: 0x0000EF1D
		[DataMember]
		public string Location { get; set; }
	}
}
