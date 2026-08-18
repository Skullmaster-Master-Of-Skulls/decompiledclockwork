using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x0200186E RID: 6254
	public class RadFilterDynamicLinqQueryProvider : RadFilterQueryProvider
	{
		// Token: 0x0600F2BA RID: 62138 RVA: 0x003748CF File Offset: 0x00372ACF
		public RadFilterDynamicLinqQueryProvider() : this(new List<RadFilterFunction>(), new List<RadFilterGroupOperation>())
		{
		}

		// Token: 0x0600F2BB RID: 62139 RVA: 0x003748E1 File Offset: 0x00372AE1
		public RadFilterDynamicLinqQueryProvider(IList<RadFilterFunction> supportedFilterFunctions, IList<RadFilterGroupOperation> supportedGroupOperations)
		{
			this.Expression = new StringBuilder();
			this._supportedFilterFunctions = supportedFilterFunctions;
			this._supportedGroupOperations = supportedGroupOperations;
		}

		// Token: 0x17004938 RID: 18744
		// (get) Token: 0x0600F2BC RID: 62140 RVA: 0x00374902 File Offset: 0x00372B02
		public override IList<RadFilterFunction> SupportedFilterFunctions
		{
			get
			{
				return this._supportedFilterFunctions;
			}
		}

		// Token: 0x17004939 RID: 18745
		// (get) Token: 0x0600F2BD RID: 62141 RVA: 0x0037490A File Offset: 0x00372B0A
		public override IList<RadFilterGroupOperation> SupportedGroupOperations
		{
			get
			{
				return this._supportedGroupOperations;
			}
		}

		// Token: 0x0600F2BE RID: 62142 RVA: 0x00374912 File Offset: 0x00372B12
		public override void ProcessGroup(RadFilterGroupExpression rootGroup)
		{
			if (this.Expression != null && this.Expression.Length > 0)
			{
				this.Expression = new StringBuilder();
			}
			this.ProcessGroupInternal(rootGroup);
		}

		// Token: 0x0600F2BF RID: 62143 RVA: 0x0037493C File Offset: 0x00372B3C
		protected override string PrepareQuery(RadFilterNonGroupExpression expression)
		{
			RadFilterDynamicLinqExpressionEvaluator evaluator = RadFilterDynamicLinqExpressionEvaluator.GetEvaluator(expression.FilterFunction);
			evaluator.OnExpressionEvaluated = base.OnExpressionEvaluated;
			return evaluator.Evaluate(expression);
		}

		// Token: 0x040045C8 RID: 17864
		private IList<RadFilterFunction> _supportedFilterFunctions;

		// Token: 0x040045C9 RID: 17865
		private IList<RadFilterGroupOperation> _supportedGroupOperations;
	}
}
