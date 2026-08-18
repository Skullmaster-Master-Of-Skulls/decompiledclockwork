using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A2 RID: 1186
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupCodeValuesReq : BaseReportMessageReq
	{
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0000BB68 File Offset: 0x00009D68
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x0000BB70 File Offset: 0x00009D70
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0000BB79 File Offset: 0x00009D79
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x0000BB81 File Offset: 0x00009D81
		[DataMember]
		[Obsolete]
		public IList<MailMergeCodeDTO> Codes { get; set; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0000BB8A File Offset: 0x00009D8A
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x0000BB92 File Offset: 0x00009D92
		[DataMember]
		public IList<string> CodesNoTags { get; set; }
	}
}
