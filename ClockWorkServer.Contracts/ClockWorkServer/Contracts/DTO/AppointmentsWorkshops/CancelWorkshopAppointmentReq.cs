using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008FB RID: 2299
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelWorkshopAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000164B3 File Offset: 0x000146B3
		// (set) Token: 0x06002EDE RID: 11998 RVA: 0x000164BB File Offset: 0x000146BB
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000164C4 File Offset: 0x000146C4
		// (set) Token: 0x06002EE0 RID: 12000 RVA: 0x000164CC File Offset: 0x000146CC
		[DataMember]
		public AppCancelInfoDTO CancelInfo { get; set; }
	}
}
