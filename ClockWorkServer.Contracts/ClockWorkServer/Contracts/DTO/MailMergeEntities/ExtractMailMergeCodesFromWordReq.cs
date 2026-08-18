using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A7 RID: 1191
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExtractMailMergeCodesFromWordReq : BaseReportMessageReq
	{
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0000BBDF File Offset: 0x00009DDF
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x0000BBE7 File Offset: 0x00009DE7
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
