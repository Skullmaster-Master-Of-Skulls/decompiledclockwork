using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200073D RID: 1853
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationStudentWithDataDTO
	{
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00011C0A File Offset: 0x0000FE0A
		// (set) Token: 0x06002655 RID: 9813 RVA: 0x00011C12 File Offset: 0x0000FE12
		[DataMember]
		public MigrationStudentDTO Student { get; set; }

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x00011C1B File Offset: 0x0000FE1B
		// (set) Token: 0x06002657 RID: 9815 RVA: 0x00011C23 File Offset: 0x0000FE23
		[DataMember]
		public IList<MigrationDataItemDTO> DataItems { get; set; }
	}
}
