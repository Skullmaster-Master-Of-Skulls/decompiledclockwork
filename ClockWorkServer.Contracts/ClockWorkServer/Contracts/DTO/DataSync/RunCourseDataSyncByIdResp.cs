using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000712 RID: 1810
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunCourseDataSyncByIdResp
	{
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x00011166 File Offset: 0x0000F366
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x0001116E File Offset: 0x0000F36E
		[DataMember]
		public DataSyncResultDTO Result { get; set; }
	}
}
