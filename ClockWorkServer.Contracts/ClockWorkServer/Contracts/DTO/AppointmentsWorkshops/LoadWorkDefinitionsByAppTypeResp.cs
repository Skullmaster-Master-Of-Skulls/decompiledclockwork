using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000912 RID: 2322
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkDefinitionsByAppTypeResp
	{
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x000165D4 File Offset: 0x000147D4
		// (set) Token: 0x06002F17 RID: 12055 RVA: 0x000165DC File Offset: 0x000147DC
		[DataMember]
		public IList<WorkshopDefinitionDTO> WorkshopDefinitions { get; set; }
	}
}
