using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000251 RID: 593
	public class ReportFunctionLoadAppointmentsParameters
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000186B4 File Offset: 0x000168B4
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x000186BC File Offset: 0x000168BC
		public DateTime? StartDate { get; set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x000186C5 File Offset: 0x000168C5
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x000186CD File Offset: 0x000168CD
		public DateTime? EndDate { get; set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x000186D6 File Offset: 0x000168D6
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x000186DE File Offset: 0x000168DE
		public bool IncludeCancelled { get; set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x000186E7 File Offset: 0x000168E7
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x000186EF File Offset: 0x000168EF
		public IList<int> PersonIds { get; set; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000186F8 File Offset: 0x000168F8
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x00018700 File Offset: 0x00016900
		public IList<int> GroupIds { get; set; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x00018709 File Offset: 0x00016909
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x00018711 File Offset: 0x00016911
		public IList<int> AppTypeIds { get; set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0001871A File Offset: 0x0001691A
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x00018722 File Offset: 0x00016922
		public eLoadAppointmentsType LoadAppointmentsdMethod { get; set; }
	}
}
