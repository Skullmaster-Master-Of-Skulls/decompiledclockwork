using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000982 RID: 2434
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAppTypeReq : BaseMessageReq
	{
		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000181C0 File Offset: 0x000163C0
		// (set) Token: 0x0600318C RID: 12684 RVA: 0x000181C8 File Offset: 0x000163C8
		[DataMember]
		public AppTypeDTO AppointmentType { get; set; }
	}
}
