using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008F4 RID: 2292
	[DataContract(Namespace = "http://tpro.ca")]
	public class WorkshopDefinitionOrAppTypeDTO
	{
		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x000163F8 File Offset: 0x000145F8
		// (set) Token: 0x06002EC1 RID: 11969 RVA: 0x00016400 File Offset: 0x00014600
		[DataMember]
		public AppTypeDTO AppType { get; set; }

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x00016409 File Offset: 0x00014609
		// (set) Token: 0x06002EC3 RID: 11971 RVA: 0x00016411 File Offset: 0x00014611
		[DataMember]
		public WorkshopDefinitionDTO WorkshopDefinition { get; set; }
	}
}
