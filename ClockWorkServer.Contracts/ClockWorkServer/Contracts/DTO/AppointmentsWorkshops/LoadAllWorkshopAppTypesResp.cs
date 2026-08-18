using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000904 RID: 2308
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllWorkshopAppTypesResp
	{
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x00016508 File Offset: 0x00014708
		// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x00016510 File Offset: 0x00014710
		[DataMember]
		public IList<AppTypeDTO> WorkshopGroups { get; set; }
	}
}
