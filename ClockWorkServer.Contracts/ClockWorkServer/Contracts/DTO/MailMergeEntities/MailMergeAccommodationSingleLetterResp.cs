using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000479 RID: 1145
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationSingleLetterResp
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0000B66D File Offset: 0x0000986D
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x0000B675 File Offset: 0x00009875
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
