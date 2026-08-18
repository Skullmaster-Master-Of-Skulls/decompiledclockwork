using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200061F RID: 1567
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq : BaseMessageReq
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0000E72B File Offset: 0x0000C92B
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0000E733 File Offset: 0x0000C933
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0000E73C File Offset: 0x0000C93C
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x0000E744 File Offset: 0x0000C944
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0000E74D File Offset: 0x0000C94D
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x0000E755 File Offset: 0x0000C955
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0000E75E File Offset: 0x0000C95E
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x0000E766 File Offset: 0x0000C966
		[DataMember]
		public bool LoadAccommodations { get; set; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0000E76F File Offset: 0x0000C96F
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x0000E777 File Offset: 0x0000C977
		[DataMember]
		public bool IncludeOfflineAccommodations { get; set; }
	}
}
