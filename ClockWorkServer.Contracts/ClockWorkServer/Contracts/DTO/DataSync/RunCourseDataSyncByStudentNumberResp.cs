using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000710 RID: 1808
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunCourseDataSyncByStudentNumberResp
	{
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x00011144 File Offset: 0x0000F344
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x0001114C File Offset: 0x0000F34C
		[DataMember]
		public DataSyncResultDTO Result { get; set; }
	}
}
