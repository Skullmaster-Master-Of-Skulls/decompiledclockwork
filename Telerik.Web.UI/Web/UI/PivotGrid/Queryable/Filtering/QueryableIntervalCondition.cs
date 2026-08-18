using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Queryable.Filtering
{
	// Token: 0x02000733 RID: 1843
	[DataContract]
	public sealed class QueryableIntervalCondition : QueryableCondition, IIntervalCondition
	{
		// Token: 0x0600418F RID: 16783 RVA: 0x000CDE56 File Offset: 0x000CC056
		public QueryableIntervalCondition()
		{
			this.IgnoreCase = true;
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x000CDE65 File Offset: 0x000CC065
		public override bool IsActive
		{
			get
			{
				return this.From != null && this.To != null;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x000CDE7D File Offset: 0x000CC07D
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x000CDE85 File Offset: 0x000CC085
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

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x000CDEA2 File Offset: 0x000CC0A2
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x000CDEAA File Offset: 0x000CC0AA
		[DataMember]
		public object From
		{
			get
			{
				return this.from;
			}
			set
			{
				if (this.from != value)
				{
					this.from = value;
					base.OnPropertyChanged("From");
				}
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x000CDEC7 File Offset: 0x000CC0C7
		// (set) Token: 0x06004196 RID: 16790 RVA: 0x000CDECF File Offset: 0x000CC0CF
		[DataMember]
		public object To
		{
			get
			{
				return this.to;
			}
			set
			{
				if (this.to != value)
				{
					this.to = value;
					base.OnPropertyChanged("To");
				}
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x000CDEEC File Offset: 0x000CC0EC
		// (set) Token: 0x06004198 RID: 16792 RVA: 0x000CDEF4 File Offset: 0x000CC0F4
		[DataMember]
		public IntervalComparison Condition
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

		// Token: 0x06004199 RID: 16793 RVA: 0x000CDF11 File Offset: 0x000CC111
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

		// Token: 0x0600419A RID: 16794 RVA: 0x000CDF44 File Offset: 0x000CC144
		private Expression GetNonStringExpression(Expression valueExpression)
		{
			Expression valueExpression2 = QueryableExpressionHelper.GetValueExpression(this.From, valueExpression.Type);
			Expression valueExpression3 = QueryableExpressionHelper.GetValueExpression(this.To, valueExpression.Type);
			if (valueExpression2 == null || valueExpression3 == null)
			{
				return null;
			}
			switch (this.Condition)
			{
			case IntervalComparison.IsNotBetween:
				return Expression.Or(Expression.LessThan(valueExpression, valueExpression2), Expression.GreaterThan(valueExpression, valueExpression3));
			}
			return Expression.And(Expression.GreaterThanOrEqual(valueExpression, valueExpression2), Expression.LessThanOrEqual(valueExpression, valueExpression3));
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000CDFBC File Offset: 0x000CC1BC
		private Expression GetStringExpression(Expression valueExpression)
		{
			ConstantExpression arg = Expression.Constant(this.IgnoreCase, typeof(bool));
			ConstantExpression right = Expression.Constant(0, typeof(int));
			ConstantExpression arg2 = Expression.Constant(this.From, typeof(string));
			ConstantExpression arg3 = Expression.Constant(this.To, typeof(string));
			MethodCallExpression left = Expression.Call(typeof(string).GetMethod("Compare", new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(bool)
			}), arg2, valueExpression, arg);
			MethodCallExpression left2 = Expression.Call(typeof(string).GetMethod("Compare", new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(bool)
			}), valueExpression, arg3, arg);
			switch (this.Condition)
			{
			case IntervalComparison.IsNotBetween:
				return Expression.Or(Expression.GreaterThan(left, right), Expression.GreaterThan(left2, right));
			}
			return Expression.And(Expression.LessThanOrEqual(left, right), Expression.LessThanOrEqual(left2, right));
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000CE110 File Offset: 0x000CC310
		protected override void CloneCore(Cloneable source)
		{
			QueryableIntervalCondition queryableIntervalCondition = source as QueryableIntervalCondition;
			if (queryableIntervalCondition != null)
			{
				this.From = queryableIntervalCondition.From;
				this.To = queryableIntervalCondition.To;
				this.Condition = queryableIntervalCondition.Condition;
				this.IgnoreCase = queryableIntervalCondition.IgnoreCase;
			}
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000CE157 File Offset: 0x000CC357
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableIntervalCondition();
		}

		// Token: 0x04001154 RID: 4436
		private object from;

		// Token: 0x04001155 RID: 4437
		private object to;

		// Token: 0x04001156 RID: 4438
		private IntervalComparison condition;

		// Token: 0x04001157 RID: 4439
		private bool ignoreCase;
	}
}
