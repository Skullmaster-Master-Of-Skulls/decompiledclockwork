using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000736 RID: 1846
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationAppointmentDTO
	{
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x00011595 File Offset: 0x0000F795
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x0001159D File Offset: 0x0000F79D
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000115A6 File Offset: 0x0000F7A6
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x000115AE File Offset: 0x0000F7AE
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000115B7 File Offset: 0x0000F7B7
		// (set) Token: 0x06002608 RID: 9736 RVA: 0x000115BF File Offset: 0x0000F7BF
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000115C8 File Offset: 0x0000F7C8
		// (set) Token: 0x0600260A RID: 9738 RVA: 0x000115D0 File Offset: 0x0000F7D0
		[DataMember]
		public string Memo { get; set; }

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000115D9 File Offset: 0x0000F7D9
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x000115E1 File Offset: 0x0000F7E1
		[DataMember]
		public IList<MigrationDataItemDTO> DataItems { get; set; }

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000115EA File Offset: 0x0000F7EA
		// (set) Token: 0x0600260E RID: 9742 RVA: 0x000115F2 File Offset: 0x0000F7F2
		[DataMember]
		public string StudentId { get; set; }

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000115FB File Offset: 0x0000F7FB
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x00011603 File Offset: 0x0000F803
		[DataMember]
		public string StaffId { get; set; }

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x0001160C File Offset: 0x0000F80C
		// (set) Token: 0x06002612 RID: 9746 RVA: 0x00011614 File Offset: 0x0000F814
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x0001161D File Offset: 0x0000F81D
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x00011625 File Offset: 0x0000F825
		[DataMember]
		public bool IsCancelled { get; set; }

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x0001162E File Offset: 0x0000F82E
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x00011636 File Offset: 0x0000F836
		[DataMember]
		public bool IsTentative { get; set; }

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x0001163F File Offset: 0x0000F83F
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x00011647 File Offset: 0x0000F847
		[DataMember]
		public bool IsNoShow { get; set; }

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x00011650 File Offset: 0x0000F850
		// (set) Token: 0x0600261A RID: 9754 RVA: 0x00011658 File Offset: 0x0000F858
		[DataMember]
		public bool IsPrivate { get; set; }

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x00011661 File Offset: 0x0000F861
		// (set) Token: 0x0600261C RID: 9756 RVA: 0x00011669 File Offset: 0x0000F869
		[DataMember]
		public string ExternalAppId { get; set; }
	}
}
