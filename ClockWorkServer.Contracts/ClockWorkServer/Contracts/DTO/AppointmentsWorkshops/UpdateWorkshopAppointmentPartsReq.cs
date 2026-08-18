using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000913 RID: 2323
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateWorkshopAppointmentPartsReq : BaseMessageReq
	{
		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06002F19 RID: 12057 RVA: 0x000165E5 File Offset: 0x000147E5
		// (set) Token: 0x06002F1A RID: 12058 RVA: 0x000165ED File Offset: 0x000147ED
		[DataMember]
		public WorkshopAppointmentDTO WorkshopAppointment { get; set; }

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000165F6 File Offset: 0x000147F6
		// (set) Token: 0x06002F1C RID: 12060 RVA: 0x000165FE File Offset: 0x000147FE
		[DataMember]
		public eAppointmentPart PartsToUpdate { get; set; }
	}
}
