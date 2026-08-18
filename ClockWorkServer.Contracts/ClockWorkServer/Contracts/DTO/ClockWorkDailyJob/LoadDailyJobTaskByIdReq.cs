using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x0200088F RID: 2191
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDailyJobTaskByIdReq : BaseReportMessageReq
	{
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x00014F91 File Offset: 0x00013191
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x00014F99 File Offset: 0x00013199
		[DataMember]
		public int WindowsTaskJobId { get; set; }
	}
}
