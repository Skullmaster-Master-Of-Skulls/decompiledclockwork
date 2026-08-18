using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006FF RID: 1791
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncLookupCoursesByTableReq : BaseReportMessageReq
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x00010A29 File Offset: 0x0000EC29
		// (set) Token: 0x06002476 RID: 9334 RVA: 0x00010A31 File Offset: 0x0000EC31
		[DataMember]
		public new int WhoAmI { get; set; }

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00010A3A File Offset: 0x0000EC3A
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x00010A42 File Offset: 0x0000EC42
		[DataMember]
		public DataTable Table { get; set; }
	}
}
