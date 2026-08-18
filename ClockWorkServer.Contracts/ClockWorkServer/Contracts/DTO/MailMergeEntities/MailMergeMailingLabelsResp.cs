using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000472 RID: 1138
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMailingLabelsResp
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0000B56E File Offset: 0x0000976E
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x0000B576 File Offset: 0x00009776
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
