using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x0200026F RID: 623
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPApplicationAvailabilityTypeDTO
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00006CB5 File Offset: 0x00004EB5
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x00006CBD File Offset: 0x00004EBD
		[DataMember]
		public int SPApplicationAvailabilityTypeId { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00006CC6 File Offset: 0x00004EC6
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x00006CCE File Offset: 0x00004ECE
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00006CD7 File Offset: 0x00004ED7
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x00006CDF File Offset: 0x00004EDF
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00006CE8 File Offset: 0x00004EE8
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00006CF0 File Offset: 0x00004EF0
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00006CF9 File Offset: 0x00004EF9
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x00006D01 File Offset: 0x00004F01
		[DataMember]
		public bool IsVisible { get; set; }
	}
}
