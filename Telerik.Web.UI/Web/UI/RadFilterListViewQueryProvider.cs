using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020018DF RID: 6367
	public class RadFilterListViewQueryProvider : RadFilterQueryProvider
	{
		// Token: 0x0600F5C7 RID: 62919 RVA: 0x0037C78C File Offset: 0x0037A98C
		public RadFilterListViewQueryProvider(IList<RadFilterGroupOperation> supportedGroups) : this(new RadListViewFilterExpressionCollection(), supportedGroups)
		{
		}

		// Token: 0x0600F5C8 RID: 62920 RVA: 0x0037C79A File Offset: 0x0037A99A
		public RadFilterListViewQueryProvider(RadListViewFilterExpressionCollection listViewExpressions, IList<RadFilterGroupOperation> supportedGroups)
		{
			this._listViewExpressions = listViewExpressions;
			this._supportedGroups = supportedGroups;
			this._supportedTypes = new List<RadFilterFunction>();
		}

		// Token: 0x17004A05 RID: 18949
		// (get) Token: 0x0600F5C9 RID: 62921 RVA: 0x0037C7BB File Offset: 0x0037A9BB
		public override IList<RadFilterFunction> SupportedFilterFunctions
		{
			get
			{
				return this._supportedTypes;
			}
		}

		// Token: 0x17004A06 RID: 18950
		// (get) Token: 0x0600F5CA RID: 62922 RVA: 0x0037C7C3 File Offset: 0x0037A9C3
		public override IList<RadFilterGroupOperation> SupportedGroupOperations
		{
			get
			{
				return this._supportedGroups;
			}
		}

		// Token: 0x17004A07 RID: 18951
		// (get) Token: 0x0600F5CB RID: 62923 RVA: 0x0037C7CB File Offset: 0x0037A9CB
		public RadListViewFilterExpressionCollection ListViewExpressions
		{
			get
			{
				return this._listViewExpressions;
			}
		}

		// Token: 0x0600F5CC RID: 62924 RVA: 0x0037C7D3 File Offset: 0x0037A9D3
		public override void ProcessGroup(RadFilterGroupExpression rootGroup)
		{
			this.ProcessGroupInternal(rootGroup);
		}

		// Token: 0x0600F5CD RID: 62925 RVA: 0x0037C7DC File Offset: 0x0037A9DC
		protected override void ProcessGroupInternal(RadFilterGroupExpression group)
		{
			if (group.IsEmpty)
			{
				return;
			}
			if (!this.IsValidGroupOperation(group.GroupOperation))
			{
				return;
			}
			RadListViewGroupFilterExpression radListViewGroupFilterExpression = new RadListViewGroupFilterExpression(this.ExtractListViewGroupOperator(group.GroupOperation));
			this._listViewExpressions.Add(radListViewGroupFilterExpression);
			this.ProcessGroupCollection(radListViewGroupFilterExpression, group.Expressions, group.GroupOperation);
		}

		// Token: 0x0600F5CE RID: 62926 RVA: 0x0037C834 File Offset: 0x0037AA34
		protected virtual void ProcessGroupInternal(RadFilterGroupExpression group, RadListViewGroupFilterExpression container)
		{
			if (group.IsEmpty)
			{
				return;
			}
			if (!this.IsValidGroupOperation(group.GroupOperation))
			{
				return;
			}
			RadListViewGroupFilterExpression radListViewGroupFilterExpression = new RadListViewGroupFilterExpression(this.ExtractListViewGroupOperator(group.GroupOperation));
			container.Add(radListViewGroupFilterExpression);
			this.ProcessGroupCollection(radListViewGroupFilterExpression, group.Expressions, group.GroupOperation);
		}

		// Token: 0x0600F5CF RID: 62927 RVA: 0x0037C885 File Offset: 0x0037AA85
		private RadListViewGroupFilterOperator ExtractListViewGroupOperator(RadFilterGroupOperation groupOperation)
		{
			if (groupOperation == RadFilterGroupOperation.And)
			{
				return RadListViewGroupFilterOperator.And;
			}
			return RadListViewGroupFilterOperator.Or;
		}

		// Token: 0x0600F5D0 RID: 62928 RVA: 0x0037C890 File Offset: 0x0037AA90
		protected virtual void ProcessGroupCollection(RadListViewGroupFilterExpression container, RadFilterExpressionsCollection expressions, RadFilterGroupOperation groupOperation)
		{
			foreach (RadFilterExpression radFilterExpression in expressions)
			{
				if (this.IsValidFilterFunction(radFilterExpression.FilterFunction))
				{
					if (radFilterExpression.FilterFunction == RadFilterFunction.Group)
					{
						this.ProcessGroupInternal((RadFilterGroupExpression)radFilterExpression, container);
					}
					else
					{
						container.Add(this.RetrieveListViewExpression((RadFilterNonGroupExpression)radFilterExpression));
					}
				}
			}
		}

		// Token: 0x0600F5D1 RID: 62929 RVA: 0x0037C90C File Offset: 0x0037AB0C
		protected override void ProcessGroupCollection(RadFilterExpressionsCollection expressions, RadFilterGroupOperation groupOperation)
		{
			throw new InvalidOperationException("Not applicable for the current provider!");
		}

		// Token: 0x0600F5D2 RID: 62930 RVA: 0x0037C918 File Offset: 0x0037AB18
		private RadListViewFilterExpression RetrieveListViewExpression(RadFilterNonGroupExpression expression)
		{
			return RadFilterListViewExpressionEvaluator.GetEvaluator(expression.FilterFunction).Evaluate(expression);
		}

		// Token: 0x0600F5D3 RID: 62931 RVA: 0x0037C92B File Offset: 0x0037AB2B
		protected override string PrepareQuery(RadFilterNonGroupExpression expression)
		{
			throw new InvalidOperationException("Not applicable for the current provider!");
		}

		// Token: 0x04004673 RID: 18035
		private RadListViewFilterExpressionCollection _listViewExpressions;

		// Token: 0x04004674 RID: 18036
		private IList<RadFilterFunction> _supportedTypes;

		// Token: 0x04004675 RID: 18037
		private IList<RadFilterGroupOperation> _supportedGroups;
	}
}
