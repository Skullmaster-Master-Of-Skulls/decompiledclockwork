using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200072E RID: 1838
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateStudentDataReq : BaseMessageReq
	{
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0001140E File Offset: 0x0000F60E
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x00011416 File Offset: 0x0000F616
		[DataMember]
		public bool PreviewMode { get; set; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0001141F File Offset: 0x0000F61F
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x00011427 File Offset: 0x0000F627
		[DataMember]
		public IList<MigrationMapperDataItemDTO> DataMappers { get; set; }

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x00011430 File Offset: 0x0000F630
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x00011438 File Offset: 0x0000F638
		[DataMember]
		public IList<MigrationStudentWithDataDTO> StudentsWithPerStudentData { get; set; }

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00011441 File Offset: 0x0000F641
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x00011449 File Offset: 0x0000F649
		[DataMember]
		public bool ClearExistingDataWhenMigrationDataIsEmpty { get; set; }
	}
}
