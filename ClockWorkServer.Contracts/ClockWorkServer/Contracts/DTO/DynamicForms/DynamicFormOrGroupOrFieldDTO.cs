using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000698 RID: 1688
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicFormOrGroupOrFieldDTO
	{
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0000FB1D File Offset: 0x0000DD1D
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0000FB25 File Offset: 0x0000DD25
		[DataMember]
		public string GroupName { get; set; }

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0000FB2E File Offset: 0x0000DD2E
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x0000FB36 File Offset: 0x0000DD36
		[DataMember]
		public DynamicFormDTO DynamicForm { get; set; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0000FB3F File Offset: 0x0000DD3F
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x0000FB47 File Offset: 0x0000DD47
		[DataMember]
		public DynamicFieldDTO Field { get; set; }
	}
}
