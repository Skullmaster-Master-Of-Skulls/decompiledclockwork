using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegEmail
{
	// Token: 0x020001AE RID: 430
	public class SelfRegEmailLogicRule
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00013D90 File Offset: 0x00011F90
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00013D98 File Offset: 0x00011F98
		public eSelfRegEmailLogicType LogicType { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00013DA1 File Offset: 0x00011FA1
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00013DA9 File Offset: 0x00011FA9
		public string Title { get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00013DB2 File Offset: 0x00011FB2
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00013DBA File Offset: 0x00011FBA
		public IList<SelfRegDataFieldMatchingRule> DataMatchingRules { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00013DC3 File Offset: 0x00011FC3
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00013DCB File Offset: 0x00011FCB
		public int EmailTemplateId { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00013DD4 File Offset: 0x00011FD4
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00013DDC File Offset: 0x00011FDC
		public int LetterTemplateId { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00013DE5 File Offset: 0x00011FE5
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x00013DED File Offset: 0x00011FED
		public bool IsDisabled { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00013DF6 File Offset: 0x00011FF6
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00013DFE File Offset: 0x00011FFE
		public bool CancelProfEmail { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00013E07 File Offset: 0x00012007
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x00013E0F File Offset: 0x0001200F
		public int AuthorizedGroupId { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00013E18 File Offset: 0x00012018
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00013E20 File Offset: 0x00012020
		public IList<string> NotificationEmails { get; set; }
	}
}
