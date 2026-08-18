using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200047B RID: 1147
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationLetterResp
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0000B6C2 File Offset: 0x000098C2
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x0000B6CA File Offset: 0x000098CA
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
