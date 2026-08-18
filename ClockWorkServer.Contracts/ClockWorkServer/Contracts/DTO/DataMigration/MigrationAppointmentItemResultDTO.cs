using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000737 RID: 1847
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationAppointmentItemResultDTO
	{
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x00011672 File Offset: 0x0000F872
		// (set) Token: 0x0600261F RID: 9759 RVA: 0x0001167A File Offset: 0x0000F87A
		[DataMember]
		public eMigrationAppointmentItemStatus Status { get; set; }

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x00011683 File Offset: 0x0000F883
		// (set) Token: 0x06002621 RID: 9761 RVA: 0x0001168B File Offset: 0x0000F88B
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x00011694 File Offset: 0x0000F894
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x0001169C File Offset: 0x0000F89C
		[DataMember]
		public MigrationAppointmentDTO ExternalAppointment { get; set; }

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x000116A5 File Offset: 0x0000F8A5
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x000116AD File Offset: 0x0000F8AD
		[DataMember]
		public IList<MigrationDataItemResultDTO> DataItemResults { get; set; }
	}
}
