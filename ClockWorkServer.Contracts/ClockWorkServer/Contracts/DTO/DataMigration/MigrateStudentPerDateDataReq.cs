using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000730 RID: 1840
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateStudentPerDateDataReq : BaseMessageReq
	{
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00011463 File Offset: 0x0000F663
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x0001146B File Offset: 0x0000F66B
		[DataMember]
		public bool PreviewMode { get; set; }

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x00011474 File Offset: 0x0000F674
		// (set) Token: 0x060025DC RID: 9692 RVA: 0x0001147C File Offset: 0x0000F67C
		[DataMember]
		public int PerDateScreenNum { get; set; }

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x00011485 File Offset: 0x0000F685
		// (set) Token: 0x060025DE RID: 9694 RVA: 0x0001148D File Offset: 0x0000F68D
		[DataMember]
		public string TitleKeyName { get; set; }

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x00011496 File Offset: 0x0000F696
		// (set) Token: 0x060025E0 RID: 9696 RVA: 0x0001149E File Offset: 0x0000F69E
		[DataMember]
		public IList<MigrationMapperDataItemDTO> DataMappers { get; set; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x000114A7 File Offset: 0x0000F6A7
		// (set) Token: 0x060025E2 RID: 9698 RVA: 0x000114AF File Offset: 0x0000F6AF
		[DataMember]
		public IList<MigrationStudentWithPerDateDataDTO> StudentsWithPerDateData { get; set; }

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000114B8 File Offset: 0x0000F6B8
		// (set) Token: 0x060025E4 RID: 9700 RVA: 0x000114C0 File Offset: 0x0000F6C0
		[DataMember]
		public bool ClearExistingDataWhenMigrationDataIsEmpty { get; set; }
	}
}
