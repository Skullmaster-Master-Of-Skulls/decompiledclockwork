using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Queryable.Filtering
{
	// Token: 0x02000735 RID: 1845
	[DataContract]
	public sealed class QueryableSetCondition : QueryableCondition, ISetCondition
	{
		// Token: 0x060041AA RID: 16810 RVA: 0x000CE279 File Offset: 0x000CC479
		public QueryableSetCondition()
		{
			this.condition = SetComparison.Includes;
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x000CE288 File Offset: 0x000CC488
		public override bool IsActive
		{
			get
			{
				return this.Items.Count > 0 || this.Comparison == SetComparison.Includes;
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x000CE2A3 File Offset: 0x000CC4A3
		// (set) Token: 0x060041AD RID: 16813 RVA: 0x000CE2AB File Offset: 0x000CC4AB
		[DataMember]
		public SetComparison Comparison
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					this.condition = value;
					base.OnPropertyChanged("Comparison");
				}
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x060041AE RID: 16814 RVA: 0x000CE2C8 File Offset: 0x000CC4C8
		[DataMember]
		public SetConditionHashCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new SetConditionHashCollection();
				}
				return this.items;
			}
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000CE2E4 File Offset: 0x000CC4E4
		protected internal override Expression GetExpression(Expression valueExpression)
		{
			Expression expression = null;
			if (!base.IsValidExpression(valueExpression))
			{
				return null;
			}
			switch (this.Comparison)
			{
			case SetComparison.DoesNotInclude:
				foreach (object obj in this.Items)
				{
					if (expression == null)
					{
						expression = Expression.NotEqual(valueExpression, Expression.Constant(obj, obj.GetType()));
					}
					else
					{
						expression = Expression.And(expression, Expression.NotEqual(valueExpression, Expression.Constant(obj, obj.GetType())));
					}
				}
				if (expression == null)
				{
					return Expression.Constant(true);
				}
				return expression;
			}
			foreach (object obj2 in this.Items)
			{
				if (expression == null)
				{
					expression = Expression.Equal(valueExpression, Expression.Constant(obj2, obj2.GetType()));
				}
				else
				{
					expression = Expression.Or(expression, Expression.Equal(valueExpression, Expression.Constant(obj2, obj2.GetType())));
				}
			}
			if (expression == null)
			{
				expression = Expression.Constant(false);
			}
			return expression;
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000CE428 File Offset: 0x000CC628
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableSetCondition();
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x000CE430 File Offset: 0x000CC630
		protected override void CloneCore(Cloneable source)
		{
			QueryableSetCondition queryableSetCondition = source as QueryableSetCondition;
			if (queryableSetCondition != null)
			{
				this.Comparison = queryableSetCondition.Comparison;
				if (queryableSetCondition.items != null)
				{
					this.items = new SetConditionHashCollection(queryableSetCondition.items);
				}
			}
		}

		// Token: 0x0400115A RID: 4442
		private SetComparison condition;

		// Token: 0x0400115B RID: 4443
		private SetConditionHashCollection items;
	}
}
