using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x02000923 RID: 2339
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePointOfContactFromMessageReq : BaseMessageReq
	{
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x0001688A File Offset: 0x00014A8A
		// (set) Token: 0x06002F5D RID: 12125 RVA: 0x00016892 File Offset: 0x00014A92
		[DataMember]
		public ePointOfContactContext PocContext { get; set; }

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x0001689B File Offset: 0x00014A9B
		// (set) Token: 0x06002F5F RID: 12127 RVA: 0x000168A3 File Offset: 0x00014AA3
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000168AC File Offset: 0x00014AAC
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x000168B4 File Offset: 0x00014AB4
		[DataMember]
		public string PlainTextMessage { get; set; }
	}
}
