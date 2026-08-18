using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x02000204 RID: 516
	public class ServiceRequest : ServiceRequestBase
	{
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00016E67 File Offset: 0x00015067
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00016E6F File Offset: 0x0001506F
		public string DateTimeRequestTitle { get; set; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00016E78 File Offset: 0x00015078
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00016E80 File Offset: 0x00015080
		public DateTime? StartDateTimeRequest { get; set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00016E89 File Offset: 0x00015089
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x00016E91 File Offset: 0x00015091
		public DateTime? EndDateTimeRequest { get; set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00016E9A File Offset: 0x0001509A
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x00016EA2 File Offset: 0x000150A2
		public ServiceProviderType ProviderType { get; set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00016EAB File Offset: 0x000150AB
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x00016EB3 File Offset: 0x000150B3
		public DateTime? DateEntered { get; set; }

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00016EBC File Offset: 0x000150BC
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x00016EC4 File Offset: 0x000150C4
		public DateTime? StartDate { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00016ECD File Offset: 0x000150CD
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x00016ED5 File Offset: 0x000150D5
		public DateTime? EndDate { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00016EDE File Offset: 0x000150DE
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x00016EE6 File Offset: 0x000150E6
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00016EEF File Offset: 0x000150EF
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00016EF7 File Offset: 0x000150F7
		public ServiceProviderRequestDetailBase RequestDetailBase { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00016F00 File Offset: 0x00015100
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00016F08 File Offset: 0x00015108
		public string Notes { get; set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00016F11 File Offset: 0x00015111
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00016F19 File Offset: 0x00015119
		public bool StudentRequested { get; set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00016F22 File Offset: 0x00015122
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x00016F2A File Offset: 0x0001512A
		public string StudentRequestedCancelNote { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00016F33 File Offset: 0x00015133
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x00016F3B File Offset: 0x0001513B
		public DateTime? DateAssigned { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00016F44 File Offset: 0x00015144
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x00016F4C File Offset: 0x0001514C
		public string SpecialInstructions { get; set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00016F55 File Offset: 0x00015155
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x00016F5D File Offset: 0x0001515D
		public IList<ServiceRequestPartBase> SubRequestParts { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00016F66 File Offset: 0x00015166
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x00016F6E File Offset: 0x0001516E
		public string PartsDescription { get; set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00016F77 File Offset: 0x00015177
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x00016F7F File Offset: 0x0001517F
		public bool IsActive { get; set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00016F88 File Offset: 0x00015188
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x00016F90 File Offset: 0x00015190
		public DateTime? DateInserted { get; set; }
	}
}
