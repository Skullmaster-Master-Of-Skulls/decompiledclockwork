using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000733 RID: 1843
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateAppointmentsResp
	{
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x0001152F File Offset: 0x0000F72F
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x00011537 File Offset: 0x0000F737
		[DataMember]
		public IList<MigrationAppointmentItemResultDTO> Results { get; set; }
	}
}
