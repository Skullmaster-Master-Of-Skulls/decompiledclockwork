using System;
using System.Linq.Expressions;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Filtering;

namespace Telerik.Web.UI.PivotGrid.Queryable.Filtering
{
	// Token: 0x02000734 RID: 1844
	public sealed class QueryableItemsFilterCondition : QueryableCondition, IItemsFilterCondition
	{
		// Token: 0x0600419E RID: 16798 RVA: 0x000CE15E File Offset: 0x000CC35E
		public QueryableItemsFilterCondition()
		{
			this.distinctCondition = new QueryableSetCondition();
			this.distinctCondition.Comparison = SetComparison.DoesNotInclude;
			this.condition = new QueryableComparisonCondition();
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000CE188 File Offset: 0x000CC388
		// (set) Token: 0x060041A0 RID: 16800 RVA: 0x000CE190 File Offset: 0x000CC390
		ISetCondition IItemsFilterCondition.DistinctCondition
		{
			get
			{
				return this.DistinctCondition;
			}
			set
			{
				this.DistinctCondition = (value as QueryableSetCondition);
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x000CE19E File Offset: 0x000CC39E
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x000CE1A6 File Offset: 0x000CC3A6
		Condition IItemsFilterCondition.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.condition = (value as QueryableCondition);
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000CE1B4 File Offset: 0x000CC3B4
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x000CE1BC File Offset: 0x000CC3BC
		public QueryableSetCondition DistinctCondition
		{
			get
			{
				return this.distinctCondition;
			}
			set
			{
				if (this.distinctCondition != value)
				{
					base.ChangeSettingsProperty<QueryableSetCondition>(ref this.distinctCondition, value);
					base.OnPropertyChanged("DistinctCondition");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000CE1EA File Offset: 0x000CC3EA
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x000CE1F2 File Offset: 0x000CC3F2
		public QueryableCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000CE1FC File Offset: 0x000CC3FC
		protected internal override Expression GetExpression(Expression valueExpression)
		{
			Expression expression = this.DistinctCondition.GetExpression(valueExpression);
			Expression expression2 = this.Condition.GetExpression(valueExpression);
			if (expression != null && expression2 != null)
			{
				return Expression.And(expression, expression2);
			}
			return expression ?? expression2;
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000CE237 File Offset: 0x000CC437
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableItemsFilterCondition();
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000CE240 File Offset: 0x000CC440
		protected sealed override void CloneCore(Cloneable source)
		{
			QueryableItemsFilterCondition queryableItemsFilterCondition = source as QueryableItemsFilterCondition;
			if (queryableItemsFilterCondition != null)
			{
				this.DistinctCondition = Cloneable.CloneOrDefault<QueryableSetCondition>(queryableItemsFilterCondition.DistinctCondition);
				this.Condition = Cloneable.CloneOrDefault<QueryableCondition>(queryableItemsFilterCondition.Condition);
			}
		}

		// Token: 0x04001158 RID: 4440
		private QueryableSetCondition distinctCondition;

		// Token: 0x04001159 RID: 4441
		private QueryableCondition condition;
	}
}
