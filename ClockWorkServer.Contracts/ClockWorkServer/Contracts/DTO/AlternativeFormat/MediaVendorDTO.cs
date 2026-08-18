using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C1E RID: 3102
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaVendorDTO
	{
		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x0001FDFB File Offset: 0x0001DFFB
		// (set) Token: 0x06004113 RID: 16659 RVA: 0x0001FE03 File Offset: 0x0001E003
		[DataMember]
		public int VendorId { get; set; }

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x0001FE0C File Offset: 0x0001E00C
		// (set) Token: 0x06004115 RID: 16661 RVA: 0x0001FE14 File Offset: 0x0001E014
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x0001FE1D File Offset: 0x0001E01D
		// (set) Token: 0x06004117 RID: 16663 RVA: 0x0001FE25 File Offset: 0x0001E025
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x0001FE2E File Offset: 0x0001E02E
		// (set) Token: 0x06004119 RID: 16665 RVA: 0x0001FE36 File Offset: 0x0001E036
		[DataMember]
		public string Cellphone { get; set; }

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x0001FE3F File Offset: 0x0001E03F
		// (set) Token: 0x0600411B RID: 16667 RVA: 0x0001FE47 File Offset: 0x0001E047
		[DataMember]
		public string Address { get; set; }

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x0001FE50 File Offset: 0x0001E050
		// (set) Token: 0x0600411D RID: 16669 RVA: 0x0001FE58 File Offset: 0x0001E058
		[DataMember]
		public string Fax { get; set; }

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x0001FE61 File Offset: 0x0001E061
		// (set) Token: 0x0600411F RID: 16671 RVA: 0x0001FE69 File Offset: 0x0001E069
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x0001FE72 File Offset: 0x0001E072
		// (set) Token: 0x06004121 RID: 16673 RVA: 0x0001FE7A File Offset: 0x0001E07A
		[DataMember]
		public string Website { get; set; }

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x0001FE83 File Offset: 0x0001E083
		// (set) Token: 0x06004123 RID: 16675 RVA: 0x0001FE8B File Offset: 0x0001E08B
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x0001FE94 File Offset: 0x0001E094
		// (set) Token: 0x06004125 RID: 16677 RVA: 0x0001FE9C File Offset: 0x0001E09C
		[DataMember]
		public string Notes { get; set; }
	}
}
