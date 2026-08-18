using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000473 RID: 1139
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeExamSheetsResp
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0000B57F File Offset: 0x0000977F
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x0000B587 File Offset: 0x00009787
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
