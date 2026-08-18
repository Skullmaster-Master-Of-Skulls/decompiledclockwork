using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A6 RID: 934
	[DataContract(Namespace = "http://tpro.ca")]
	public class StaffCommonInfoDTO
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00009D0E File Offset: 0x00007F0E
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x00009D16 File Offset: 0x00007F16
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00009D1F File Offset: 0x00007F1F
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x00009D27 File Offset: 0x00007F27
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00009D30 File Offset: 0x00007F30
		// (set) Token: 0x060014EA RID: 5354 RVA: 0x00009D38 File Offset: 0x00007F38
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00009D41 File Offset: 0x00007F41
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00009D49 File Offset: 0x00007F49
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00009D52 File Offset: 0x00007F52
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x00009D5A File Offset: 0x00007F5A
		[DataMember]
		public int SignatureDataId { get; set; }
	}
}
