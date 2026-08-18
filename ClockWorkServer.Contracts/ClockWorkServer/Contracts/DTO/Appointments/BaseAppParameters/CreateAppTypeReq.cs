using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000983 RID: 2435
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppTypeReq : BaseMessageReq
	{
		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000181D1 File Offset: 0x000163D1
		// (set) Token: 0x0600318F RID: 12687 RVA: 0x000181D9 File Offset: 0x000163D9
		[DataMember]
		public AppTypeDTO AppointmentType { get; set; }
	}
}
