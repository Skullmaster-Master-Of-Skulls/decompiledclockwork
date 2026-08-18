using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ActionPlan
{
	// Token: 0x020004EA RID: 1258
	[DataContract(Namespace = "http://tpro.ca")]
	public class ActionPlanTaskDTO
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0000C53D File Offset: 0x0000A73D
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0000C545 File Offset: 0x0000A745
		[DataMember]
		public int TaskId { get; set; }

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0000C54E File Offset: 0x0000A74E
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x0000C556 File Offset: 0x0000A756
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0000C55F File Offset: 0x0000A75F
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0000C567 File Offset: 0x0000A767
		[DataMember]
		public int WhoResponsibleCode { get; set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0000C570 File Offset: 0x0000A770
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x0000C578 File Offset: 0x0000A778
		[DataMember]
		public DateTime? DateLastModified { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0000C581 File Offset: 0x0000A781
		// (set) Token: 0x06001ABA RID: 6842 RVA: 0x0000C589 File Offset: 0x0000A789
		[DataMember]
		public int WhoAdded { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x0000C592 File Offset: 0x0000A792
		// (set) Token: 0x06001ABC RID: 6844 RVA: 0x0000C59A File Offset: 0x0000A79A
		[DataMember]
		public int WhoLastModified { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0000C5AB File Offset: 0x0000A7AB
		[DataMember]
		public string Group { get; set; }

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0000C5C5 File Offset: 0x0000A7C5
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		[DataMember]
		public int? CompletedId { get; set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x0000C5DE File Offset: 0x0000A7DE
		[DataMember]
		public bool MeansComplete { get; set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0000C5E7 File Offset: 0x0000A7E7
		// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x0000C5EF File Offset: 0x0000A7EF
		[DataMember]
		public string StaffNotes { get; set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x0000C600 File Offset: 0x0000A800
		[DataMember]
		public string StudentNotes { get; set; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0000C609 File Offset: 0x0000A809
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x0000C611 File Offset: 0x0000A811
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0000C61A File Offset: 0x0000A81A
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x0000C622 File Offset: 0x0000A822
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0000C62B File Offset: 0x0000A82B
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x0000C633 File Offset: 0x0000A833
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0000C63C File Offset: 0x0000A83C
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x0000C644 File Offset: 0x0000A844
		[DataMember]
		public DateTime DateAdded { get; set; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0000C64D File Offset: 0x0000A84D
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x0000C655 File Offset: 0x0000A855
		[DataMember]
		public string Completed { get; set; }
	}
}
