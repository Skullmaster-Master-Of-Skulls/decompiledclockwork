using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000728 RID: 1832
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncResultDTO
	{
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x00011320 File Offset: 0x0000F520
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x00011328 File Offset: 0x0000F528
		[DataMember]
		public eDataSyncStatusDTO Status { get; set; }

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x00011331 File Offset: 0x0000F531
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x00011339 File Offset: 0x0000F539
		[DataMember]
		public DataSyncErrorDTO SyncError { get; set; }
	}
}
