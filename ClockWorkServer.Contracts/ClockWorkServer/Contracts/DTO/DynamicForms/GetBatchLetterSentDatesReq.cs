using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000617 RID: 1559
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetBatchLetterSentDatesReq : BaseReportMessageReq
	{
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0000E65F File Offset: 0x0000C85F
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x0000E667 File Offset: 0x0000C867
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0000E670 File Offset: 0x0000C870
		// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x0000E678 File Offset: 0x0000C878
		[DataMember]
		public IList<int> LuCourseIds { get; set; }
	}
}
