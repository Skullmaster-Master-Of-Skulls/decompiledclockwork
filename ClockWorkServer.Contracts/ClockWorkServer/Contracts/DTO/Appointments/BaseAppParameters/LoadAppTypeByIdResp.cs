using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200097F RID: 2431
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeByIdResp
	{
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x0001818D File Offset: 0x0001638D
		// (set) Token: 0x06003183 RID: 12675 RVA: 0x00018195 File Offset: 0x00016395
		[DataMember]
		public AppTypeDTO AppointmentType { get; set; }
	}
}
