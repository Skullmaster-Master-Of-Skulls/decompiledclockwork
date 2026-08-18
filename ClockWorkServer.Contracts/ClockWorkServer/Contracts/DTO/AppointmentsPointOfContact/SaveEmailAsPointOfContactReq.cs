using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x0200091C RID: 2332
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveEmailAsPointOfContactReq : BaseMessageReq
	{
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x000167E0 File Offset: 0x000149E0
		// (set) Token: 0x06002F42 RID: 12098 RVA: 0x000167E8 File Offset: 0x000149E8
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000167F1 File Offset: 0x000149F1
		// (set) Token: 0x06002F44 RID: 12100 RVA: 0x000167F9 File Offset: 0x000149F9
		[DataMember]
		public int StaffPersonId { get; set; }

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x00016802 File Offset: 0x00014A02
		// (set) Token: 0x06002F46 RID: 12102 RVA: 0x0001680A File Offset: 0x00014A0A
		[DataMember]
		public TPMailMessageDTO MailMessage { get; set; }

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x00016813 File Offset: 0x00014A13
		// (set) Token: 0x06002F48 RID: 12104 RVA: 0x0001681B File Offset: 0x00014A1B
		[DataMember]
		public ePointOfContactContext PocContext { get; set; }
	}
}
