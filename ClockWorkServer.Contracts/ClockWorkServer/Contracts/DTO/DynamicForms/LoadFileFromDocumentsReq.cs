using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000650 RID: 1616
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFileFromDocumentsReq : BaseMessageReq
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0000EF4F File Offset: 0x0000D14F
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x0000EF57 File Offset: 0x0000D157
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x0000EF60 File Offset: 0x0000D160
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x0000EF68 File Offset: 0x0000D168
		[DataMember]
		public int FileId { get; set; }
	}
}
