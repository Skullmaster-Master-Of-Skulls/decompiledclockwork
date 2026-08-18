using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B68 RID: 2920
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentMatchingReq : BaseMessageReq
	{
		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06003DEE RID: 15854 RVA: 0x0001E664 File Offset: 0x0001C864
		// (set) Token: 0x06003DEF RID: 15855 RVA: 0x0001E66C File Offset: 0x0001C86C
		[DataMember]
		public string SearchText { get; set; }

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x0001E675 File Offset: 0x0001C875
		// (set) Token: 0x06003DF1 RID: 15857 RVA: 0x0001E67D File Offset: 0x0001C87D
		[DataMember]
		public int LUCourseId { get; set; }
	}
}
