using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004AA RID: 1194
	[DataContract(Namespace = "http://tpro.ca")]
	public class OutputFileResp
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0000BC34 File Offset: 0x00009E34
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x0000BC3C File Offset: 0x00009E3C
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
