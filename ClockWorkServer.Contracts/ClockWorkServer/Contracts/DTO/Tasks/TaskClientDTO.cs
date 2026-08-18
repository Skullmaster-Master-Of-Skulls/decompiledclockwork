using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E2 RID: 482
	[DataContract(Namespace = "http://tpro.ca")]
	public class TaskClientDTO
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00004FDE File Offset: 0x000031DE
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00004FE6 File Offset: 0x000031E6
		[DataMember]
		public int TaskClientId { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00004FEF File Offset: 0x000031EF
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x00004FF7 File Offset: 0x000031F7
		[DataMember]
		public PersonBaseDTO Client { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00005000 File Offset: 0x00003200
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00005008 File Offset: 0x00003208
		[DataMember]
		public string Notes { get; set; }
	}
}
