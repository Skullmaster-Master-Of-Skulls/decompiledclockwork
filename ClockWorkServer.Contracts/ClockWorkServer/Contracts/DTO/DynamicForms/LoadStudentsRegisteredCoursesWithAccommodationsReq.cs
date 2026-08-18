using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000621 RID: 1569
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsRegisteredCoursesWithAccommodationsReq : BaseMessageReq
	{
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0000E791 File Offset: 0x0000C991
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x0000E799 File Offset: 0x0000C999
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0000E7A2 File Offset: 0x0000C9A2
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0000E7B3 File Offset: 0x0000C9B3
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0000E7BB File Offset: 0x0000C9BB
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		[DataMember]
		public bool LoadAccommodations { get; set; }

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0000E7D5 File Offset: 0x0000C9D5
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x0000E7DD File Offset: 0x0000C9DD
		[DataMember]
		public bool IncludeOfflineAccommodations { get; set; }
	}
}
