using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Queryable.Filtering
{
	// Token: 0x02000732 RID: 1842
	[DataContract]
	public sealed class QueryableComparisonCondition : QueryableCondition, IComparisonCondition
	{
		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x000CDBD2 File Offset: 0x000CBDD2
		// (set) Token: 0x06004184 RID: 16772 RVA: 0x000CDBDA File Offset: 0x000CBDDA
		[DataMember]
		public object Than
		{
			get
			{
				return this.than;
			}
			set
			{
				if (this.than != value)
				{
					this.than = value;
					base.OnPropertyChanged("Than");
				}
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x000CDBF7 File Offset: 0x000CBDF7
		// (set) Token: 0x06004186 RID: 16774 RVA: 0x000CDBFF File Offset: 0x000CBDFF
		[DataMember]
		public Comparison Condition
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
					base.OnPropertyChanged("Condition");
				}
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x000CDC1C File Offset: 0x000CBE1C
		// (set) Token: 0x06004188 RID: 16776 RVA: 0x000CDC24 File Offset: 0x000CBE24
		[DataMember]
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
			set
			{
				if (this.ignoreCase != value)
				{
					this.ignoreCase = value;
					base.OnPropertyChanged("IgnoreCase");
				}
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x000CDC41 File Offset: 0x000CBE41
		public override bool IsActive
		{
			get
			{
				return this.Than != null;
			}
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000CDC4F File Offset: 0x000CBE4F
		protected internal override Expression GetExpression(Expression valueExpression)
		{
			if (!base.IsValidExpression(valueExpression))
			{
				return null;
			}
			if (valueExpression.Type == typeof(string))
			{
				return this.GetStringExpression(valueExpression);
			}
			return this.GetNonStringExpression(valueExpression);
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000CDC84 File Offset: 0x000CBE84
		private Expression GetNonStringExpression(Expression valueExpression)
		{
			Expression valueExpression2 = QueryableExpressionHelper.GetValueExpression(this.Than, valueExpression.Type);
			if (valueExpression2 == null)
			{
				return null;
			}
			switch (this.Condition)
			{
			case Comparison.DoesNotEqual:
				return Expression.NotEqual(valueExpression, valueExpression2);
			case Comparison.IsGreaterThan:
				return Expression.GreaterThan(valueExpression, valueExpression2);
			case Comparison.IsGreaterThanOrEqualTo:
				return Expression.GreaterThanOrEqual(valueExpression, valueExpression2);
			case Comparison.IsLessThan:
				return Expression.LessThan(valueExpression, valueExpression2);
			case Comparison.IsLessThanOrEqualTo:
				return Expression.LessThanOrEqual(valueExpression, valueExpression2);
			}
			return Expression.Equal(valueExpression, valueExpression2);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000CDD00 File Offset: 0x000CBF00
		private Expression GetStringExpression(Expression valueExpression)
		{
			ConstantExpression arg = Expression.Constant(this.Than, typeof(string));
			ConstantExpression arg2 = Expression.Constant(this.IgnoreCase, typeof(bool));
			ConstantExpression right = Expression.Constant(0, typeof(int));
			MethodCallExpression left = Expression.Call(typeof(string).GetMethod("Compare", new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(bool)
			}), valueExpression, arg, arg2);
			switch (this.Condition)
			{
			case Comparison.DoesNotEqual:
				return Expression.NotEqual(left, right);
			case Comparison.IsGreaterThan:
				return Expression.GreaterThan(left, right);
			case Comparison.IsGreaterThanOrEqualTo:
				return Expression.GreaterThanOrEqual(left, right);
			case Comparison.IsLessThan:
				return Expression.LessThan(left, right);
			case Comparison.IsLessThanOrEqualTo:
				return Expression.LessThanOrEqual(left, right);
			}
			return Expression.Equal(left, right);
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000CDE14 File Offset: 0x000CC014
		protected override void CloneCore(Cloneable source)
		{
			QueryableComparisonCondition queryableComparisonCondition = source as QueryableComparisonCondition;
			if (queryableComparisonCondition != null)
			{
				this.Than = queryableComparisonCondition.Than;
				this.Condition = queryableComparisonCondition.Condition;
				this.IgnoreCase = queryableComparisonCondition.IgnoreCase;
			}
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000CDE4F File Offset: 0x000CC04F
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableComparisonCondition();
		}

		// Token: 0x04001151 RID: 4433
		private object than;

		// Token: 0x04001152 RID: 4434
		private Comparison condition;

		// Token: 0x04001153 RID: 4435
		private bool ignoreCase;
	}
}
