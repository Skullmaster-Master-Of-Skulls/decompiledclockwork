using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000711 RID: 1809
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunCourseDataSyncByIdReq : BaseReportMessageReq
	{
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x00011155 File Offset: 0x0000F355
		// (set) Token: 0x06002560 RID: 9568 RVA: 0x0001115D File Offset: 0x0000F35D
		[DataMember]
		public int PersonId { get; set; }
	}
}
