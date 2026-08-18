using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000917 RID: 2327
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypesWithWorkshopDefinitionsResp
	{
		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x0001664B File Offset: 0x0001484B
		// (set) Token: 0x06002F2A RID: 12074 RVA: 0x00016653 File Offset: 0x00014853
		[DataMember]
		public Forest<WorkshopDefinitionOrAppTypeDTO> WorkshopAppTypesWithDefinitions { get; set; }
	}
}
