using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000738 RID: 1848
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationCreateStudentResultDTO
	{
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000116B6 File Offset: 0x0000F8B6
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x000116BE File Offset: 0x0000F8BE
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000116C7 File Offset: 0x0000F8C7
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x000116CF File Offset: 0x0000F8CF
		[DataMember]
		public eMigrationCreateStudentStatus Status { get; set; }

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000116D8 File Offset: 0x0000F8D8
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x000116E0 File Offset: 0x0000F8E0
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000116E9 File Offset: 0x0000F8E9
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x000116F1 File Offset: 0x0000F8F1
		[DataMember]
		public int PersonId { get; set; }
	}
}
