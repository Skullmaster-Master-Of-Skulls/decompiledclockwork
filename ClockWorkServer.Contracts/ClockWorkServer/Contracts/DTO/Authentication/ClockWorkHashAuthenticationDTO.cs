using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008DF RID: 2271
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkHashAuthenticationDTO
	{
		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x00015C93 File Offset: 0x00013E93
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x00015C9B File Offset: 0x00013E9B
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x00015CA4 File Offset: 0x00013EA4
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x00015CAC File Offset: 0x00013EAC
		[DataMember]
		public DateTime StampTime { get; set; }

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x00015CB5 File Offset: 0x00013EB5
		// (set) Token: 0x06002E0D RID: 11789 RVA: 0x00015CBD File Offset: 0x00013EBD
		[DataMember]
		public int Seed { get; set; }

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x00015CC6 File Offset: 0x00013EC6
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x00015CCE File Offset: 0x00013ECE
		[DataMember]
		public string HashValue { get; set; }
	}
}
