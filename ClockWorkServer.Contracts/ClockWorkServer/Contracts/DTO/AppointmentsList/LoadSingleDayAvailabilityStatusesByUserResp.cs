using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsList;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AEC RID: 2796
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSingleDayAvailabilityStatusesByUserResp
	{
		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x0001CC88 File Offset: 0x0001AE88
		// (set) Token: 0x06003B21 RID: 15137 RVA: 0x0001CC90 File Offset: 0x0001AE90
		[DataMember]
		public Dictionary<DateTime, eAvailabilityCode> Items { get; set; }
	}
}
