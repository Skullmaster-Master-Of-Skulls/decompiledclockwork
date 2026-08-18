using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E6 RID: 486
	[DataContract(Namespace = "http://tpro.ca")]
	public class TaskGroupDTO
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00005176 File Offset: 0x00003376
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0000517E File Offset: 0x0000337E
		[DataMember]
		public int TaskGroupId { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00005187 File Offset: 0x00003387
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0000518F File Offset: 0x0000338F
		[DataMember]
		public PersonBaseDTO Owner { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00005198 File Offset: 0x00003398
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x000051A0 File Offset: 0x000033A0
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x000051A9 File Offset: 0x000033A9
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x000051B1 File Offset: 0x000033B1
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x000051BA File Offset: 0x000033BA
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x000051C2 File Offset: 0x000033C2
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000051CB File Offset: 0x000033CB
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x000051D3 File Offset: 0x000033D3
		[DataMember]
		public bool IsPrivate { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x000051DC File Offset: 0x000033DC
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x000051E4 File Offset: 0x000033E4
		[DataMember]
		public int ParentTaskGroupId { get; set; }
	}
}
