using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000224 RID: 548
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFileCategoryDTO
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00005AB1 File Offset: 0x00003CB1
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x00005AB9 File Offset: 0x00003CB9
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00005AC2 File Offset: 0x00003CC2
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00005ACA File Offset: 0x00003CCA
		[DataMember]
		public StudentFileCategoryFieldDTO[] Fields { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00005AD3 File Offset: 0x00003CD3
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00005ADB File Offset: 0x00003CDB
		[DataMember]
		public bool IsDisabled { get; set; }
	}
}
