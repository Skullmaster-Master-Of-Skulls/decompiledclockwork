using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student
{
	// Token: 0x0200072A RID: 1834
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentDataSyncPreviewDataDTO
	{
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x00011342 File Offset: 0x0000F542
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x0001134A File Offset: 0x0000F54A
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00011353 File Offset: 0x0000F553
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x0001135B File Offset: 0x0000F55B
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x00011364 File Offset: 0x0000F564
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x0001136C File Offset: 0x0000F56C
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x00011375 File Offset: 0x0000F575
		// (set) Token: 0x060025B8 RID: 9656 RVA: 0x0001137D File Offset: 0x0000F57D
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x00011386 File Offset: 0x0000F586
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x0001138E File Offset: 0x0000F58E
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x00011397 File Offset: 0x0000F597
		// (set) Token: 0x060025BC RID: 9660 RVA: 0x0001139F File Offset: 0x0000F59F
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x000113A8 File Offset: 0x0000F5A8
		// (set) Token: 0x060025BE RID: 9662 RVA: 0x000113B0 File Offset: 0x0000F5B0
		[DataMember]
		public IList<DataSyncExternalDataDTO> ExternalDataItems { get; set; }
	}
}
