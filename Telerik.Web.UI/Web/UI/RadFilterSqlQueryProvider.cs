using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x020018F3 RID: 6387
	public class RadFilterSqlQueryProvider : RadFilterQueryProvider
	{
		// Token: 0x0600F609 RID: 62985 RVA: 0x0037D3F4 File Offset: 0x0037B5F4
		public RadFilterSqlQueryProvider() : this(new List<RadFilterFunction>(), new List<RadFilterGroupOperation>())
		{
		}

		// Token: 0x0600F60A RID: 62986 RVA: 0x0037D406 File Offset: 0x0037B606
		public RadFilterSqlQueryProvider(IList<RadFilterFunction> supportedFilterFunctions, IList<RadFilterGroupOperation> supportedGroupOperations)
		{
			this.Expression = new StringBuilder();
			this._supportedFilterFunctions = supportedFilterFunctions;
			this._supportedGroupOperations = supportedGroupOperations;
		}

		// Token: 0x17004A0C RID: 18956
		// (get) Token: 0x0600F60B RID: 62987 RVA: 0x0037D427 File Offset: 0x0037B627
		public override IList<RadFilterFunction> SupportedFilterFunctions
		{
			get
			{
				return this._supportedFilterFunctions;
			}
		}

		// Token: 0x17004A0D RID: 18957
		// (get) Token: 0x0600F60C RID: 62988 RVA: 0x0037D42F File Offset: 0x0037B62F
		public override IList<RadFilterGroupOperation> SupportedGroupOperations
		{
			get
			{
				return this._supportedGroupOperations;
			}
		}

		// Token: 0x0600F60D RID: 62989 RVA: 0x0037D437 File Offset: 0x0037B637
		public override void ProcessGroup(RadFilterGroupExpression rootGroup)
		{
			if (this.Expression != null && this.Expression.Length > 0)
			{
				this.Expression = new StringBuilder();
			}
			this.ProcessGroupInternal(rootGroup);
		}

		// Token: 0x0600F60E RID: 62990 RVA: 0x0037D464 File Offset: 0x0037B664
		protected override string PrepareQuery(RadFilterNonGroupExpression expression)
		{
			RadFilterSqlExpressionEvaluator evaluator = RadFilterSqlExpressionEvaluator.GetEvaluator(expression.FilterFunction);
			evaluator.OnExpressionEvaluated = base.OnExpressionEvaluated;
			return evaluator.Evaluate(expression);
		}

		// Token: 0x0400467A RID: 18042
		private IList<RadFilterFunction> _supportedFilterFunctions;

		// Token: 0x0400467B RID: 18043
		private IList<RadFilterGroupOperation> _supportedGroupOperations;
	}
}
