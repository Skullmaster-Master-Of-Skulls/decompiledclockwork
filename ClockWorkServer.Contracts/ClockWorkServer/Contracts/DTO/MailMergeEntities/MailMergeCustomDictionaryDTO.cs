using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200046B RID: 1131
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeCustomDictionaryDTO
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x0000B43C File Offset: 0x0000963C
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x0000B444 File Offset: 0x00009644
		[DataMember]
		public Dictionary<string, string> Args { get; set; }
	}
}
