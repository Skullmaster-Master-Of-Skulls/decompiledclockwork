using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000718 RID: 1816
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDataSyncInfoResp
	{
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x000111DD File Offset: 0x0000F3DD
		// (set) Token: 0x06002577 RID: 9591 RVA: 0x000111E5 File Offset: 0x0000F3E5
		[DataMember]
		public DataSyncInfoDTO DataSyncInfo { get; set; }
	}
}
