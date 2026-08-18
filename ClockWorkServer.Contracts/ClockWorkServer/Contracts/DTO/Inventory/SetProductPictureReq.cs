using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004FF RID: 1279
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class SetProductPictureReq : BaseMessageReq
	{
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0000C7F6 File Offset: 0x0000A9F6
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x0000C7FE File Offset: 0x0000A9FE
		[DataMember]
		public Guid ProductId { get; set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0000C807 File Offset: 0x0000AA07
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x0000C80F File Offset: 0x0000AA0F
		[DataMember]
		public Image Picture { get; set; }
	}
}
