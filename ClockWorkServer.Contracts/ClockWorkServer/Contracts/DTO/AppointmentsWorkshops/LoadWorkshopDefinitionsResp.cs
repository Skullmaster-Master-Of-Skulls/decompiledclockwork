using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008F6 RID: 2294
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopDefinitionsResp
	{
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x0001641A File Offset: 0x0001461A
		// (set) Token: 0x06002EC7 RID: 11975 RVA: 0x00016422 File Offset: 0x00014622
		[DataMember]
		public IList<WorkshopDefinitionDTO> WorkshopDefinitions { get; set; }
	}
}
