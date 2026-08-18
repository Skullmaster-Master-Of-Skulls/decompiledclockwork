using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200072F RID: 1839
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateStudentDataResp
	{
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x00011452 File Offset: 0x0000F652
		// (set) Token: 0x060025D7 RID: 9687 RVA: 0x0001145A File Offset: 0x0000F65A
		[DataMember]
		public IList<MigrationDataItemResultDTO> Results { get; set; }
	}
}
