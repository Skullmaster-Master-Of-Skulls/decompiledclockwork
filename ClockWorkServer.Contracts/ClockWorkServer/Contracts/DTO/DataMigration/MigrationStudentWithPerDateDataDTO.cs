using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200073E RID: 1854
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationStudentWithPerDateDataDTO : MigrationStudentWithDataDTO
	{
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00011C2C File Offset: 0x0000FE2C
		// (set) Token: 0x0600265A RID: 9818 RVA: 0x00011C34 File Offset: 0x0000FE34
		[DataMember]
		public DateTime DateKey { get; set; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x00011C3D File Offset: 0x0000FE3D
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x00011C45 File Offset: 0x0000FE45
		[DataMember]
		public string WhoEnteredStudent_no { get; set; }

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x00011C4E File Offset: 0x0000FE4E
		// (set) Token: 0x0600265E RID: 9822 RVA: 0x00011C56 File Offset: 0x0000FE56
		[DataMember]
		public int WhoEnterePersonId { get; set; }
	}
}
