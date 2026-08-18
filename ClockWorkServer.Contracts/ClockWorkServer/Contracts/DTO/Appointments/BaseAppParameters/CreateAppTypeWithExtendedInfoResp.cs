using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200099F RID: 2463
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppTypeWithExtendedInfoResp
	{
		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x00018369 File Offset: 0x00016569
		// (set) Token: 0x060031DB RID: 12763 RVA: 0x00018371 File Offset: 0x00016571
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
