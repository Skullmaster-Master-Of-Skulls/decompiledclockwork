using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.Labels
{
	// Token: 0x020004BA RID: 1210
	[DataContract(Namespace = "http://tpro.ca")]
	public class LabelTemplateDTO
	{
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0000BECC File Offset: 0x0000A0CC
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0000BEDD File Offset: 0x0000A0DD
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
		[DataMember]
		public MailMergeTemplateDTO Template { get; set; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0000BEEE File Offset: 0x0000A0EE
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x0000BEF6 File Offset: 0x0000A0F6
		[DataMember]
		public MailMergeDefaultPrinterSettingsDTO DefaultPrinterSettings { get; set; }
	}
}
