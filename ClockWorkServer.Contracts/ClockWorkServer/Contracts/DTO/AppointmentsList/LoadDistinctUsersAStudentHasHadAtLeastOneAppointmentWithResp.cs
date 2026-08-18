using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF0 RID: 2800
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp
	{
		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x0001CCEE File Offset: 0x0001AEEE
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x0001CCF6 File Offset: 0x0001AEF6
		[DataMember]
		public IList<PersonBaseDTO> Staff { get; set; }
	}
}
