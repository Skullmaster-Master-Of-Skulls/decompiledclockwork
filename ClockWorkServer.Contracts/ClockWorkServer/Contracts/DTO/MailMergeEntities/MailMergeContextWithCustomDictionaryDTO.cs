using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200046A RID: 1130
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeContextWithCustomDictionaryDTO
	{
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0000B41A File Offset: 0x0000961A
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x0000B422 File Offset: 0x00009622
		[DataMember]
		public MailMergeContextDTO Context { get; set; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0000B42B File Offset: 0x0000962B
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0000B433 File Offset: 0x00009633
		[DataMember]
		public MailMergeCustomDictionaryDTO CustomDictionary { get; set; }
	}
}
