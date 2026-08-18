using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000731 RID: 1841
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateStudentPerDateDataResp
	{
		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000114C9 File Offset: 0x0000F6C9
		// (set) Token: 0x060025E7 RID: 9703 RVA: 0x000114D1 File Offset: 0x0000F6D1
		[DataMember]
		public IList<MigrationDataItemResultDTO> Results { get; set; }
	}
}
