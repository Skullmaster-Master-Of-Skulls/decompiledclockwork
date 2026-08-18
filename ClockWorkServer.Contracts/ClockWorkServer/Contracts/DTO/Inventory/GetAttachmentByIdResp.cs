using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F0 RID: 1264
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAttachmentByIdResp
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0000C719 File Offset: 0x0000A919
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x0000C721 File Offset: 0x0000A921
		[DataMember]
		public InventoryAttachedFileDTO AttachedFile { get; set; }
	}
}
