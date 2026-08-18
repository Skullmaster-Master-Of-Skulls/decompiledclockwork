using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A6A RID: 2666
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionsSmallResp
	{
		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x0001B2E8 File Offset: 0x000194E8
		// (set) Token: 0x060037F4 RID: 14324 RVA: 0x0001B2F0 File Offset: 0x000194F0
		[DataMember]
		public IList<ClassTestDefinitionSmallDTO> ClassTestDefinitionsSmall { get; set; }

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x0001B2F9 File Offset: 0x000194F9
		// (set) Token: 0x060037F6 RID: 14326 RVA: 0x0001B301 File Offset: 0x00019501
		[DataMember]
		public IList<string> ExtendedColumnNames { get; set; }
	}
}
