using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000628 RID: 1576
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAppointmentIdsWithNotesResp
	{
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0000E8F6 File Offset: 0x0000CAF6
		// (set) Token: 0x06002013 RID: 8211 RVA: 0x0000E8FE File Offset: 0x0000CAFE
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
