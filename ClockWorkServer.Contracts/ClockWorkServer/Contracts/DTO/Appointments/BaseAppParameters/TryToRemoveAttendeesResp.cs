using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200096E RID: 2414
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToRemoveAttendeesResp
	{
		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x0001806C File Offset: 0x0001626C
		// (set) Token: 0x06003150 RID: 12624 RVA: 0x00018074 File Offset: 0x00016274
		[DataMember]
		public IList<int> NotAllowToDeletePersonIdList { get; set; }
	}
}
