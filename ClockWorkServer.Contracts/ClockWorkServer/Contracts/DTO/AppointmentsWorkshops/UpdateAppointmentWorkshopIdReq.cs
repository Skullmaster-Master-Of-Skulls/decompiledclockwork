using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000915 RID: 2325
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAppointmentWorkshopIdReq : BaseMessageReq
	{
		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x00016629 File Offset: 0x00014829
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x00016631 File Offset: 0x00014831
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x0001663A File Offset: 0x0001483A
		// (set) Token: 0x06002F26 RID: 12070 RVA: 0x00016642 File Offset: 0x00014842
		[DataMember]
		public int NewWorkshopId { get; set; }
	}
}
