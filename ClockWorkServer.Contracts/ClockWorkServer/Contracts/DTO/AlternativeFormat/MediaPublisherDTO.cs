using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C11 RID: 3089
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaPublisherDTO
	{
		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x0001FCA7 File Offset: 0x0001DEA7
		// (set) Token: 0x060040DE RID: 16606 RVA: 0x0001FCAF File Offset: 0x0001DEAF
		[DataMember]
		public int PublisherId { get; set; }

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		// (set) Token: 0x060040E0 RID: 16608 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x0001FCC9 File Offset: 0x0001DEC9
		// (set) Token: 0x060040E2 RID: 16610 RVA: 0x0001FCD1 File Offset: 0x0001DED1
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x0001FCDA File Offset: 0x0001DEDA
		// (set) Token: 0x060040E4 RID: 16612 RVA: 0x0001FCE2 File Offset: 0x0001DEE2
		[DataMember]
		public string Address { get; set; }

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x0001FCEB File Offset: 0x0001DEEB
		// (set) Token: 0x060040E6 RID: 16614 RVA: 0x0001FCF3 File Offset: 0x0001DEF3
		[DataMember]
		public string Fax { get; set; }

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x0001FCFC File Offset: 0x0001DEFC
		// (set) Token: 0x060040E8 RID: 16616 RVA: 0x0001FD04 File Offset: 0x0001DF04
		[DataMember]
		public string Email { get; set; }

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x0001FD0D File Offset: 0x0001DF0D
		// (set) Token: 0x060040EA RID: 16618 RVA: 0x0001FD15 File Offset: 0x0001DF15
		[DataMember]
		public string Website { get; set; }

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x0001FD1E File Offset: 0x0001DF1E
		// (set) Token: 0x060040EC RID: 16620 RVA: 0x0001FD26 File Offset: 0x0001DF26
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		// (set) Token: 0x060040EE RID: 16622 RVA: 0x0001FD37 File Offset: 0x0001DF37
		[DataMember]
		public string Notes { get; set; }
	}
}
