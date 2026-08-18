using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.Labels
{
	// Token: 0x020004BB RID: 1211
	public static class LabelTemplateParameters
	{
		// Token: 0x02000CA1 RID: 3233
		[DataContract(Namespace = "http://tpro.ca")]
		public class ParseLabelTemplatesResp
		{
			// Token: 0x170018B9 RID: 6329
			// (get) Token: 0x06004374 RID: 17268 RVA: 0x00024762 File Offset: 0x00022962
			// (set) Token: 0x06004375 RID: 17269 RVA: 0x0002476A File Offset: 0x0002296A
			[DataMember]
			public IList<LabelTemplateDTO> Labels { get; set; }
		}

		// Token: 0x02000CA2 RID: 3234
		[DataContract(Namespace = "http://tpro.ca")]
		public class ParseLabelTemplatesReq : BaseMessageReq
		{
			// Token: 0x170018BA RID: 6330
			// (get) Token: 0x06004377 RID: 17271 RVA: 0x00024773 File Offset: 0x00022973
			// (set) Token: 0x06004378 RID: 17272 RVA: 0x0002477B File Offset: 0x0002297B
			[DataMember]
			public string Xml { get; set; }
		}

		// Token: 0x02000CA3 RID: 3235
		[DataContract(Namespace = "http://tpro.ca")]
		public class LabelTemplatesToXmlResp
		{
			// Token: 0x170018BB RID: 6331
			// (get) Token: 0x0600437A RID: 17274 RVA: 0x00024784 File Offset: 0x00022984
			// (set) Token: 0x0600437B RID: 17275 RVA: 0x0002478C File Offset: 0x0002298C
			[DataMember]
			public string Xml { get; set; }
		}

		// Token: 0x02000CA4 RID: 3236
		[DataContract(Namespace = "http://tpro.ca")]
		public class LabelTemplatesToXmlReq : BaseMessageReq
		{
			// Token: 0x170018BC RID: 6332
			// (get) Token: 0x0600437D RID: 17277 RVA: 0x00024795 File Offset: 0x00022995
			// (set) Token: 0x0600437E RID: 17278 RVA: 0x0002479D File Offset: 0x0002299D
			[DataMember]
			public IList<LabelTemplateDTO> Templates { get; set; }
		}
	}
}
