using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200073B RID: 1851
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationMapperDataItemDTO
	{
		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x00011B93 File Offset: 0x0000FD93
		// (set) Token: 0x06002645 RID: 9797 RVA: 0x00011B9B File Offset: 0x0000FD9B
		[DataMember]
		public IList<string> DataNamesOrdered { get; set; }

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		// (set) Token: 0x06002647 RID: 9799 RVA: 0x00011BAC File Offset: 0x0000FDAC
		[DataMember]
		public int ClockWorkCid { get; set; }
	}
}
