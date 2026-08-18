using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200046E RID: 1134
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeTemplateDTO
	{
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0000B4D5 File Offset: 0x000096D5
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x0000B4DD File Offset: 0x000096DD
		[DataMember]
		public string Template { get; set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0000B4E6 File Offset: 0x000096E6
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x0000B4EE File Offset: 0x000096EE
		[DataMember]
		public string FontName { get; set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0000B4F7 File Offset: 0x000096F7
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0000B4FF File Offset: 0x000096FF
		[DataMember]
		public int FontSize { get; set; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0000B508 File Offset: 0x00009708
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0000B510 File Offset: 0x00009710
		[DataMember]
		public bool AllCaps { get; set; }
	}
}
