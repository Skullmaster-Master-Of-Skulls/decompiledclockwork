using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000ABB RID: 2747
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRecurringWorkshopAppointmentInstancesReq : BaseMessageReq
	{
		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x0001C448 File Offset: 0x0001A648
		// (set) Token: 0x06003A43 RID: 14915 RVA: 0x0001C450 File Offset: 0x0001A650
		[DataMember]
		public WorkshopAppointmentDTO WorkshopApp { get; set; }

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x0001C459 File Offset: 0x0001A659
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x0001C461 File Offset: 0x0001A661
		[DataMember]
		public IList<RecurringInstanceDTO> RecurringInstances { get; set; }

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x0001C46A File Offset: 0x0001A66A
		// (set) Token: 0x06003A47 RID: 14919 RVA: 0x0001C472 File Offset: 0x0001A672
		[DataMember]
		public RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour { get; set; }
	}
}
