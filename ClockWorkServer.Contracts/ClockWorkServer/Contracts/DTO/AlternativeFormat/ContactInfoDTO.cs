using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B46 RID: 2886
	[DataContract(Namespace = "http://tpro.ca")]
	public class ContactInfoDTO
	{
		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x0001D92B File Offset: 0x0001BB2B
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x0001D933 File Offset: 0x0001BB33
		[DataMember]
		public int ContactInfoId { get; set; }

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x0001D93C File Offset: 0x0001BB3C
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x0001D944 File Offset: 0x0001BB44
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x0001D94D File Offset: 0x0001BB4D
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x0001D955 File Offset: 0x0001BB55
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x0001D95E File Offset: 0x0001BB5E
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x0001D966 File Offset: 0x0001BB66
		[DataMember]
		public string CellPhone { get; set; }

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x0001D96F File Offset: 0x0001BB6F
		// (set) Token: 0x06003CFD RID: 15613 RVA: 0x0001D977 File Offset: 0x0001BB77
		[DataMember]
		public string Address { get; set; }

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x0001D980 File Offset: 0x0001BB80
		// (set) Token: 0x06003CFF RID: 15615 RVA: 0x0001D988 File Offset: 0x0001BB88
		[DataMember]
		public string Fax { get; set; }

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x0001D991 File Offset: 0x0001BB91
		// (set) Token: 0x06003D01 RID: 15617 RVA: 0x0001D999 File Offset: 0x0001BB99
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x0001D9A2 File Offset: 0x0001BBA2
		// (set) Token: 0x06003D03 RID: 15619 RVA: 0x0001D9AA File Offset: 0x0001BBAA
		[DataMember]
		public string Website { get; set; }

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x0001D9B3 File Offset: 0x0001BBB3
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x0001D9BB File Offset: 0x0001BBBB
		[DataMember]
		public string Position { get; set; }

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x0001D9CC File Offset: 0x0001BBCC
		[DataMember]
		public string Notes { get; set; }
	}
}
