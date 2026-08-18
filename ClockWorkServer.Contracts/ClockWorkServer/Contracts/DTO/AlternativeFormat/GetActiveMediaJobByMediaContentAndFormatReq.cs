using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BAC RID: 2988
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobByMediaContentAndFormatReq : BaseMessageReq
	{
		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x0001F218 File Offset: 0x0001D418
		// (set) Token: 0x06003F3B RID: 16187 RVA: 0x0001F220 File Offset: 0x0001D420
		[DataMember]
		public string MediaContentId { get; set; }

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x0001F229 File Offset: 0x0001D429
		// (set) Token: 0x06003F3D RID: 16189 RVA: 0x0001F231 File Offset: 0x0001D431
		[DataMember]
		public MediaContentFormat ContentFormat { get; set; }

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x0001F23A File Offset: 0x0001D43A
		// (set) Token: 0x06003F3F RID: 16191 RVA: 0x0001F242 File Offset: 0x0001D442
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
