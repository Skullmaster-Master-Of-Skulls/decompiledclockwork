using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B5F RID: 2911
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentFileMatchingReq : BaseMessageReq
	{
		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x0001E532 File Offset: 0x0001C732
		// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x0001E53A File Offset: 0x0001C73A
		[DataMember]
		public string SearchText { get; set; }

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x0001E543 File Offset: 0x0001C743
		// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x0001E54B File Offset: 0x0001C74B
		[DataMember]
		public int LuCourseid { get; set; }
	}
}
