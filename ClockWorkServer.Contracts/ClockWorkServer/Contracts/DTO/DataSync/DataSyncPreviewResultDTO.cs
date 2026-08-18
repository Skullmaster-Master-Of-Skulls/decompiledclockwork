using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000727 RID: 1831
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncPreviewResultDTO
	{
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000112ED File Offset: 0x0000F4ED
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x000112F5 File Offset: 0x0000F4F5
		[DataMember]
		public eDataSyncStatusDTO Status { get; set; }

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000112FE File Offset: 0x0000F4FE
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x00011306 File Offset: 0x0000F506
		[DataMember]
		public DataSyncErrorDTO SyncError { get; set; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x0001130F File Offset: 0x0000F50F
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x00011317 File Offset: 0x0000F517
		[DataMember]
		public IList<DataSyncExternalDataDTO> Data { get; set; }
	}
}
