using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC0 RID: 3008
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedMediaJobByMediaContentAndFormatReq : BaseMessageReq
	{
		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x0001F3D2 File Offset: 0x0001D5D2
		// (set) Token: 0x06003F83 RID: 16259 RVA: 0x0001F3DA File Offset: 0x0001D5DA
		[DataMember]
		public string MediaContentId { get; set; }

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x0001F3E3 File Offset: 0x0001D5E3
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x0001F3EB File Offset: 0x0001D5EB
		[DataMember]
		public MediaContentFormat ContentFormat { get; set; }

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
