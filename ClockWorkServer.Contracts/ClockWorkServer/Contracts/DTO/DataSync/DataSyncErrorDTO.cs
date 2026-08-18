using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000703 RID: 1795
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncErrorDTO
	{
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x00010A8F File Offset: 0x0000EC8F
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x00010A97 File Offset: 0x0000EC97
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}
