using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200097D RID: 2429
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAppointmentTypeAssociatedPerAppScreenNumsResp
	{
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x0001816B File Offset: 0x0001636B
		// (set) Token: 0x0600317D RID: 12669 RVA: 0x00018173 File Offset: 0x00016373
		[DataMember]
		public IList<int> PerAppScreenNums { get; set; }
	}
}
