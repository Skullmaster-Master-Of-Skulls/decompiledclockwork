using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000716 RID: 1814
	[DataContract(Namespace = "http://tpro.ca")]
	public class PreviewDataSyncDataResp
	{
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x000111CC File Offset: 0x0000F3CC
		// (set) Token: 0x06002573 RID: 9587 RVA: 0x000111D4 File Offset: 0x0000F3D4
		[DataMember]
		public DataSyncPreviewResultDTO Result { get; set; }
	}
}
