using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000ABC RID: 2748
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRecurringWorkshopAppointmentInstancesResp
	{
		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x0001C47B File Offset: 0x0001A67B
		// (set) Token: 0x06003A4A RID: 14922 RVA: 0x0001C483 File Offset: 0x0001A683
		[DataMember]
		public IList<RecurringInstanceDTO> RecurringInstances { get; set; }
	}
}
