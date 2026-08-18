using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C5 RID: 197
	public class QueryContext
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00025AA1 File Offset: 0x00023CA1
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00025AA9 File Offset: 0x00023CA9
		public IDictionary<string, object> SelectParameters { get; private set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00025AB2 File Offset: 0x00023CB2
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00025ABA File Offset: 0x00023CBA
		public IOrderedDictionary OrderByParameters { get; private set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00025AC3 File Offset: 0x00023CC3
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00025ACB File Offset: 0x00023CCB
		public IDictionary<string, object> GroupByParameters { get; private set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00025AD4 File Offset: 0x00023CD4
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00025ADC File Offset: 0x00023CDC
		public IDictionary<string, object> OrderGroupsByParameters { get; private set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00025AE5 File Offset: 0x00023CE5
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00025AED File Offset: 0x00023CED
		public IDictionary<string, object> WhereParameters { get; private set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00025AF6 File Offset: 0x00023CF6
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00025AFE File Offset: 0x00023CFE
		public DataSourceSelectArguments Arguments { get; private set; }

		// Token: 0x060009E0 RID: 2528 RVA: 0x00025B07 File Offset: 0x00023D07
		public QueryContext(IDictionary<string, object> whereParameters, IDictionary<string, object> orderGroupsByParameters, IOrderedDictionary orderByParameters, IDictionary<string, object> groupByParameters, IDictionary<string, object> selectParameters, DataSourceSelectArguments arguments)
		{
			this.WhereParameters = whereParameters;
			this.OrderByParameters = orderByParameters;
			this.OrderGroupsByParameters = orderGroupsByParameters;
			this.SelectParameters = selectParameters;
			this.GroupByParameters = groupByParameters;
			this.Arguments = arguments;
		}
	}
}
