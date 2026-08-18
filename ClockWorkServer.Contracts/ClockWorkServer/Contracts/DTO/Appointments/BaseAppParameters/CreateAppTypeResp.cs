using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000984 RID: 2436
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppTypeResp
	{
		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000181E2 File Offset: 0x000163E2
		// (set) Token: 0x06003192 RID: 12690 RVA: 0x000181EA File Offset: 0x000163EA
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
