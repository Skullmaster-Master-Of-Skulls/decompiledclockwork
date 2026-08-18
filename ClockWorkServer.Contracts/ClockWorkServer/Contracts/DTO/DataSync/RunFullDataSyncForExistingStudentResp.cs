using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000714 RID: 1812
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunFullDataSyncForExistingStudentResp
	{
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x000111AA File Offset: 0x0000F3AA
		// (set) Token: 0x0600256D RID: 9581 RVA: 0x000111B2 File Offset: 0x0000F3B2
		[DataMember]
		public DataSyncResultDTO Result { get; set; }
	}
}
