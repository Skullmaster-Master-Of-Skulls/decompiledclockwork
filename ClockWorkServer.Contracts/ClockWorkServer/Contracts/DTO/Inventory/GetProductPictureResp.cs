using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004FE RID: 1278
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class GetProductPictureResp
	{
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0000C7E5 File Offset: 0x0000A9E5
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0000C7ED File Offset: 0x0000A9ED
		[DataMember]
		public Image Picture { get; set; }
	}
}
