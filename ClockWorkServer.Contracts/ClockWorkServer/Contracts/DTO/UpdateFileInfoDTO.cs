using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F9 RID: 249
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateFileInfoDTO
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00002BD0 File Offset: 0x00000DD0
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00002BD8 File Offset: 0x00000DD8
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00002BE1 File Offset: 0x00000DE1
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x00002BE9 File Offset: 0x00000DE9
		[DataMember]
		public FileTypeDTO FileType { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00002BF2 File Offset: 0x00000DF2
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00002BFA File Offset: 0x00000DFA
		[DataMember]
		public int AddressSize { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00002C03 File Offset: 0x00000E03
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00002C0B File Offset: 0x00000E0B
		[DataMember]
		public string Version { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00002C14 File Offset: 0x00000E14
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00002C1C File Offset: 0x00000E1C
		[DataMember]
		public eUpdateStatusDTO Status { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00002C25 File Offset: 0x00000E25
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00002C2D File Offset: 0x00000E2D
		[DataMember]
		public DateTime LastModifiedTime { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00002C36 File Offset: 0x00000E36
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00002C3E File Offset: 0x00000E3E
		[DataMember]
		public bool IsPublic { get; set; }
	}
}
