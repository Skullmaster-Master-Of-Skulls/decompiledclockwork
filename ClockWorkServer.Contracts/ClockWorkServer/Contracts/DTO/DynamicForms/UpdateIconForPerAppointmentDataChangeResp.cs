using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000661 RID: 1633
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateIconForPerAppointmentDataChangeResp
	{
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0000F13C File Offset: 0x0000D33C
		// (set) Token: 0x06002135 RID: 8501 RVA: 0x0000F144 File Offset: 0x0000D344
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
