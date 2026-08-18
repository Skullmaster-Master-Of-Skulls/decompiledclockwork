using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200099B RID: 2459
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeWithExtendedInfoIdByIdResp
	{
		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x00018336 File Offset: 0x00016536
		// (set) Token: 0x060031D1 RID: 12753 RVA: 0x0001833E File Offset: 0x0001653E
		[DataMember]
		public AppTypeWithExtendedInfoDTO AppType { get; set; }
	}
}
