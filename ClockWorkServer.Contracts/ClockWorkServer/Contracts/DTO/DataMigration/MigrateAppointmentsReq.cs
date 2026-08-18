using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000732 RID: 1842
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrateAppointmentsReq : BaseMessageReq
	{
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000114DA File Offset: 0x0000F6DA
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x000114E2 File Offset: 0x0000F6E2
		[DataMember]
		public bool PreviewMode { get; set; }

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000114EB File Offset: 0x0000F6EB
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x000114F3 File Offset: 0x0000F6F3
		[DataMember]
		public IList<MigrationMapperDataItemDTO> DataMappers { get; set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000114FC File Offset: 0x0000F6FC
		// (set) Token: 0x060025EE RID: 9710 RVA: 0x00011504 File Offset: 0x0000F704
		[DataMember]
		public IList<MigrationAppointmentDTO> AppointmentsWithPerAppData { get; set; }

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x0001150D File Offset: 0x0000F70D
		// (set) Token: 0x060025F0 RID: 9712 RVA: 0x00011515 File Offset: 0x0000F715
		[DataMember]
		public bool ClearExistingDataWhenMigrationDataIsEmpty { get; set; }

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x0001151E File Offset: 0x0000F71E
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x00011526 File Offset: 0x0000F726
		[DataMember]
		public bool AllowDuplicateAppointmentsToBeCreated { get; set; }
	}
}
