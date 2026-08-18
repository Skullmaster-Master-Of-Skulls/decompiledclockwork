using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ADA RID: 2778
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateClosedDayReq : BaseMessageReq
	{
		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x0001CA24 File Offset: 0x0001AC24
		// (set) Token: 0x06003AC7 RID: 15047 RVA: 0x0001CA2C File Offset: 0x0001AC2C
		[DataMember]
		public IList<ClosedDayDTO> ClosedDays { get; set; }
	}
}
