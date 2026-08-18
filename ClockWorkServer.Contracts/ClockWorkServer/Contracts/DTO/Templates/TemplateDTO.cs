using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C1 RID: 449
	[DataContract(Namespace = "http://tpro.ca")]
	public class TemplateDTO : BaseTemplateDTO
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00004B7F File Offset: 0x00002D7F
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00004B87 File Offset: 0x00002D87
		[DataMember]
		public BinaryFileDTO Document { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00004B90 File Offset: 0x00002D90
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x00004B98 File Offset: 0x00002D98
		[DataMember]
		public TPMailMessageDTO EmailBehindDocumentTemplate { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00004BA1 File Offset: 0x00002DA1
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00004BA9 File Offset: 0x00002DA9
		[DataMember]
		public TPMailMessageDTO EmailTemplate { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00004BB2 File Offset: 0x00002DB2
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00004BBA File Offset: 0x00002DBA
		[DataMember]
		public IDictionary<string, string> FieldMappings { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public bool IsEmpty
		{
			get
			{
				return this.Document == null && this.EmailBehindDocumentTemplate == null && this.EmailTemplate == null;
			}
		}
	}
}
