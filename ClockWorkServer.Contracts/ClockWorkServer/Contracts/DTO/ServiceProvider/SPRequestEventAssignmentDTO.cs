using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x0200027A RID: 634
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestEventAssignmentDTO
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00007238 File Offset: 0x00005438
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00007240 File Offset: 0x00005440
		[DataMember]
		public int SPRequestEventAssignmentId { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00007249 File Offset: 0x00005449
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x00007251 File Offset: 0x00005451
		[DataMember]
		public SPProviderDTO AssignedProvider { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0000725A File Offset: 0x0000545A
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00007262 File Offset: 0x00005462
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0000726B File Offset: 0x0000546B
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x00007273 File Offset: 0x00005473
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0000727C File Offset: 0x0000547C
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x00007284 File Offset: 0x00005484
		[DataMember]
		public DateTime? DateCancelled { get; set; }
	}
}
