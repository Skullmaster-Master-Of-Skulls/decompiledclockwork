using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200073A RID: 1850
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationDataItemResultDTO
	{
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00011B3E File Offset: 0x0000FD3E
		// (set) Token: 0x0600263A RID: 9786 RVA: 0x00011B46 File Offset: 0x0000FD46
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00011B4F File Offset: 0x0000FD4F
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x00011B57 File Offset: 0x0000FD57
		[DataMember]
		public string DataItemName { get; set; }

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x00011B60 File Offset: 0x0000FD60
		// (set) Token: 0x0600263E RID: 9790 RVA: 0x00011B68 File Offset: 0x0000FD68
		[DataMember]
		public string DataItemValue { get; set; }

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00011B71 File Offset: 0x0000FD71
		// (set) Token: 0x06002640 RID: 9792 RVA: 0x00011B79 File Offset: 0x0000FD79
		[DataMember]
		public eMigrationDataItemStatus Status { get; set; }

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00011B82 File Offset: 0x0000FD82
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x00011B8A File Offset: 0x0000FD8A
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}
