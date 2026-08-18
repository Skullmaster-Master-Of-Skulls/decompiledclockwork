using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200070D RID: 1805
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalDataDTO
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x00011078 File Offset: 0x0000F278
		// (set) Token: 0x06002542 RID: 9538 RVA: 0x00011080 File Offset: 0x0000F280
		[DataMember]
		public string FieldName { get; set; }

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x00011089 File Offset: 0x0000F289
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x00011091 File Offset: 0x0000F291
		[DataMember]
		public string FieldValue { get; set; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x0001109A File Offset: 0x0000F29A
		// (set) Token: 0x06002546 RID: 9542 RVA: 0x000110A2 File Offset: 0x0000F2A2
		[DataMember]
		public string Student_no { get; set; }

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000110AB File Offset: 0x0000F2AB
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x000110B3 File Offset: 0x0000F2B3
		[DataMember]
		public int ClockWorkPersonId { get; set; }

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000110BC File Offset: 0x0000F2BC
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x000110C4 File Offset: 0x0000F2C4
		[DataMember]
		public DynamicDataDTO MatchingClockWorkData { get; set; }
	}
}
