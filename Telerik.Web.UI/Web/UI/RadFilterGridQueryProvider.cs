using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x020018CB RID: 6347
	public class RadFilterGridQueryProvider : RadFilterDynamicLinqQueryProvider
	{
		// Token: 0x0600F597 RID: 62871 RVA: 0x0037C1FF File Offset: 0x0037A3FF
		public RadFilterGridQueryProvider()
		{
		}

		// Token: 0x0600F598 RID: 62872 RVA: 0x0037C207 File Offset: 0x0037A407
		internal RadFilterGridQueryProvider(RadFilterGridContext context)
		{
			this._context = context;
		}

		// Token: 0x0600F599 RID: 62873 RVA: 0x0037C216 File Offset: 0x0037A416
		public RadFilterGridQueryProvider(IList<RadFilterFunction> supportedFilterFunctions, IList<RadFilterGroupOperation> supportedGroupOperations) : base(supportedFilterFunctions, supportedGroupOperations)
		{
		}

		// Token: 0x17004A03 RID: 18947
		// (get) Token: 0x0600F59A RID: 62874 RVA: 0x0037C220 File Offset: 0x0037A420
		// (set) Token: 0x0600F59B RID: 62875 RVA: 0x0037C228 File Offset: 0x0037A428
		public bool IsCaseSensitive { get; set; }

		// Token: 0x0600F59C RID: 62876 RVA: 0x0037C234 File Offset: 0x0037A434
		protected override string PrepareQuery(RadFilterNonGroupExpression expression)
		{
			RadFilterExpressionEvaluatorBase evaluator = this.GetEvaluator(expression.FilterFunction);
			evaluator.IsCaseSensitive = this.IsCaseSensitive;
			evaluator.OnExpressionEvaluated = base.OnExpressionEvaluated;
			return evaluator.Evaluate(expression);
		}

		// Token: 0x0600F59D RID: 62877 RVA: 0x0037C270 File Offset: 0x0037A470
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Function")]
		protected virtual RadFilterExpressionEvaluatorBase GetEvaluator(RadFilterFunction function)
		{
			if (this._context.ExpressionType == GridFilterExpressionType.Sql)
			{
				return RadFilterSqlExpressionEvaluator.GetEvaluator(function);
			}
			if (this._context.ExpressionType == GridFilterExpressionType.BindableType)
			{
				return RadFilterGridBindableTypeExpressionEvaluator.GetEvaluator(function);
			}
			if (this._context.ExpressionType == GridFilterExpressionType.CalculatedColumns)
			{
				return RadFilterGridCalculatedColumnExpressionEvaluator.GetEvaluator(function);
			}
			if (this._context.ExpressionType == GridFilterExpressionType.RowLinq)
			{
				return RadFilterLinqRowExpressionEvaluator.GetEvaluator(function);
			}
			if (this._context.ExpressionType == GridFilterExpressionType.EntitySql)
			{
				return RadFilterEntitySqlExpressionEvaluator.GetEvaluator(function);
			}
			if (this._context.ExpressionType == GridFilterExpressionType.Oql)
			{
				return RadFilterOqlExpressionEvaluator.GetEvaluator(function);
			}
			return RadFilterDynamicLinqExpressionEvaluator.GetEvaluator(function);
		}

		// Token: 0x04004668 RID: 18024
		private RadFilterGridContext _context;
	}
}
