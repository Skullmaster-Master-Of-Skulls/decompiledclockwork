using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B34 RID: 2868
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsWithSpecialPermissionsReq : BaseMessageReq
	{
		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x0001D474 File Offset: 0x0001B674
		// (set) Token: 0x06003C55 RID: 15445 RVA: 0x0001D47C File Offset: 0x0001B67C
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x0001D485 File Offset: 0x0001B685
		// (set) Token: 0x06003C57 RID: 15447 RVA: 0x0001D48D File Offset: 0x0001B68D
		[DataMember]
		public IList<int> AppTypeIds { get; set; }

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x0001D496 File Offset: 0x0001B696
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x0001D49E File Offset: 0x0001B69E
		[DataMember]
		public bool HideCancelled { get; set; }

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x0001D4A7 File Offset: 0x0001B6A7
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x0001D4AF File Offset: 0x0001B6AF
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		// (set) Token: 0x06003C5D RID: 15453 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		[DataMember]
		public int NumDays { get; set; }
	}
}
