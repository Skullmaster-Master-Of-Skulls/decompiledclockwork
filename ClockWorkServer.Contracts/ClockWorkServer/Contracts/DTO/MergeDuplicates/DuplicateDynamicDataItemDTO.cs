using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x0200045E RID: 1118
	[DataContract(Namespace = "http://tpro.ca")]
	public class DuplicateDynamicDataItemDTO
	{
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0000B037 File Offset: 0x00009237
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0000B03F File Offset: 0x0000923F
		[DataMember]
		public DynamicDataDTO DataItem1 { get; set; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0000B048 File Offset: 0x00009248
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x0000B050 File Offset: 0x00009250
		[DataMember]
		public DynamicDataDTO DataItem2 { get; set; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0000B059 File Offset: 0x00009259
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0000B061 File Offset: 0x00009261
		[DataMember]
		public eDuplicateItemToUse DataItemToUse { get; set; }
	}
}
