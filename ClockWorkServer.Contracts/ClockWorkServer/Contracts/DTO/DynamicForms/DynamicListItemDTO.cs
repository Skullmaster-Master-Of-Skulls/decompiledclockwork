using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006B5 RID: 1717
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicListItemDTO
	{
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0000FE92 File Offset: 0x0000E092
		// (set) Token: 0x060022D5 RID: 8917 RVA: 0x0000FE9A File Offset: 0x0000E09A
		[DataMember]
		public int LookupListId { get; set; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0000FEA3 File Offset: 0x0000E0A3
		// (set) Token: 0x060022D7 RID: 8919 RVA: 0x0000FEAB File Offset: 0x0000E0AB
		[DataMember]
		public string LookupText { get; set; }

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		// (set) Token: 0x060022D9 RID: 8921 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		[DataMember]
		public string LookupValue { get; set; }

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x0000FEC5 File Offset: 0x0000E0C5
		// (set) Token: 0x060022DB RID: 8923 RVA: 0x0000FECD File Offset: 0x0000E0CD
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x0000FED6 File Offset: 0x0000E0D6
		// (set) Token: 0x060022DD RID: 8925 RVA: 0x0000FEDE File Offset: 0x0000E0DE
		[DataMember]
		public string Children { get; set; }

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x0000FEE7 File Offset: 0x0000E0E7
		// (set) Token: 0x060022DF RID: 8927 RVA: 0x0000FEEF File Offset: 0x0000E0EF
		[DataMember]
		public DynamicListGroupDTO Group { get; set; }
	}
}
