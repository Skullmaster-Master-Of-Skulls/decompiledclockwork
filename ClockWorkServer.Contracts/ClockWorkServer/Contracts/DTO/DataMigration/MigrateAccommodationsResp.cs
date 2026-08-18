using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000735 RID: 1845
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateAccommodationsResp
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x00011584 File Offset: 0x0000F784
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x0001158C File Offset: 0x0000F78C
		[DataMember]
		public IList<MigrationDataItemResultDTO> Results { get; set; }
	}
}
