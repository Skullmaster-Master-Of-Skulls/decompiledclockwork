using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000734 RID: 1844
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateAccommodationsReq : BaseMessageReq
	{
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x00011540 File Offset: 0x0000F740
		// (set) Token: 0x060025F8 RID: 9720 RVA: 0x00011548 File Offset: 0x0000F748
		[DataMember]
		public bool PreviewMode { get; set; }

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x00011551 File Offset: 0x0000F751
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x00011559 File Offset: 0x0000F759
		[DataMember]
		public IList<MigrationMapperDataItemDTO> DataMappers { get; set; }

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00011562 File Offset: 0x0000F762
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x0001156A File Offset: 0x0000F76A
		[DataMember]
		public IList<MigrationStudentWithDataDTO> StudentsWithAccommodationData { get; set; }

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00011573 File Offset: 0x0000F773
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x0001157B File Offset: 0x0000F77B
		[DataMember]
		public bool ClearExistingDataWhenMigrationDataIsEmpty { get; set; }
	}
}
