using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB0 RID: 2992
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCountActiveMediaJobByMediaContentAndFormatReq : BaseMessageReq
	{
		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x0001F28F File Offset: 0x0001D48F
		// (set) Token: 0x06003F4D RID: 16205 RVA: 0x0001F297 File Offset: 0x0001D497
		[DataMember]
		public string MediaContentId { get; set; }

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x0001F2A0 File Offset: 0x0001D4A0
		// (set) Token: 0x06003F4F RID: 16207 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		[DataMember]
		public MediaContentFormat ContentFormat { get; set; }

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x0001F2B1 File Offset: 0x0001D4B1
		// (set) Token: 0x06003F51 RID: 16209 RVA: 0x0001F2B9 File Offset: 0x0001D4B9
		[DataMember]
		public int StudentId { get; set; }
	}
}
