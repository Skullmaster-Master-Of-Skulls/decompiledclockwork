using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000623 RID: 1571
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationChangesReq : BaseMessageReq
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0000E7F7 File Offset: 0x0000C9F7
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x0000E7FF File Offset: 0x0000C9FF
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0000E808 File Offset: 0x0000CA08
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x0000E810 File Offset: 0x0000CA10
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0000E819 File Offset: 0x0000CA19
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x0000E821 File Offset: 0x0000CA21
		[DataMember]
		public DateTime SinceDate { get; set; }
	}
}
