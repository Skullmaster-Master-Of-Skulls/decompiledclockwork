using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200036B RID: 875
	[DataContract(Namespace = "http://tpro.ca")]
	public class GroupForEditDTO
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x000096C4 File Offset: 0x000078C4
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x000096CC File Offset: 0x000078CC
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000096D5 File Offset: 0x000078D5
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x000096DD File Offset: 0x000078DD
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x000096E6 File Offset: 0x000078E6
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x000096EE File Offset: 0x000078EE
		[DataMember]
		public bool IsPrimary { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x000096F7 File Offset: 0x000078F7
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x000096FF File Offset: 0x000078FF
		[DataMember]
		public bool ViewAppsVisible { get; set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00009708 File Offset: 0x00007908
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x00009710 File Offset: 0x00007910
		[DataMember]
		public string FullDescription { get; set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00009719 File Offset: 0x00007919
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x00009721 File Offset: 0x00007921
		[DataMember]
		public int OrderNum { get; set; }
	}
}
