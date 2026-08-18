using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006B4 RID: 1716
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicListGroupDTO
	{
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0000FE4E File Offset: 0x0000E04E
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x0000FE56 File Offset: 0x0000E056
		[DataMember]
		public int LookupGroupId { get; set; }

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x0000FE5F File Offset: 0x0000E05F
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x0000FE67 File Offset: 0x0000E067
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x0000FE70 File Offset: 0x0000E070
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x0000FE78 File Offset: 0x0000E078
		[DataMember]
		public string ChildList { get; set; }

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0000FE81 File Offset: 0x0000E081
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x0000FE89 File Offset: 0x0000E089
		[DataMember]
		public int SortBy { get; set; }
	}
}
