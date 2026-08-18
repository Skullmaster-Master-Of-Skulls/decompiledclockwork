using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Queryable.Filtering
{
	// Token: 0x02000736 RID: 1846
	[DataContract]
	public sealed class QueryableTextCondition : QueryableCondition, ITextCondition
	{
		// Token: 0x060041B2 RID: 16818 RVA: 0x000CE46C File Offset: 0x000CC66C
		public QueryableTextCondition()
		{
			this.ignoreCase = true;
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x000CE47B File Offset: 0x000CC67B
		public override bool IsActive
		{
			get
			{
				return !string.IsNullOrEmpty(this.Pattern);
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000CE48B File Offset: 0x000CC68B
		// (set) Token: 0x060041B5 RID: 16821 RVA: 0x000CE493 File Offset: 0x000CC693
		[DataMember]
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				if (this.pattern != value)
				{
					this.pattern = value;
					base.OnPropertyChanged("Pattern");
				}
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x000CE4B5 File Offset: 0x000CC6B5
		// (set) Token: 0x060041B7 RID: 16823 RVA: 0x000CE4BD File Offset: 0x000CC6BD
		[DataMember]
		public TextComparison Comparison
		{
			get
			{
				return this.comparison;
			}
			set
			{
				if (this.comparison != value)
				{
					this.comparison = value;
					base.OnPropertyChanged("Comparison");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x000CE4E5 File Offset: 0x000CC6E5
		// (set) Token: 0x060041B9 RID: 16825 RVA: 0x000CE4ED File Offset: 0x000CC6ED
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
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000CE518 File Offset: 0x000CC718
		protected internal override Expression GetExpression(Expression valueExpression)
		{
			if (!base.IsValidExpression(valueExpression))
			{
				return null;
			}
			Expression expression = valueExpression;
			if (valueExpression.Type != typeof(string))
			{
				expression = Expression.Call(valueExpression, valueExpression.Type.GetMethod("ToString", new Type[0]));
			}
			expression = (this.IgnoreCase ? Expression.Call(expression, typeof(string).GetMethod("ToUpper", new Type[0])) : expression);
			Expression expression2 = Expression.Constant(this.Pattern, typeof(string));
			expression2 = (this.IgnoreCase ? Expression.Call(expression2, typeof(string).GetMethod("ToUpper", new Type[0])) : expression2);
			Expression expression3;
			switch (this.Comparison)
			{
			case TextComparison.BeginsWith:
				return Expression.Call(expression, typeof(string).GetMethod("StartsWith", new Type[]
				{
					typeof(string)
				}), new Expression[]
				{
					expression2
				});
			case TextComparison.DoesNotBeginWith:
				expression3 = Expression.Call(expression, typeof(string).GetMethod("StartsWith", new Type[]
				{
					typeof(string)
				}), new Expression[]
				{
					expression2
				});
				return Expression.Not(expression3);
			case TextComparison.EndsWith:
				return Expression.Call(expression, typeof(string).GetMethod("EndsWith", new Type[]
				{
					typeof(string)
				}), new Expression[]
				{
					expression2
				});
			case TextComparison.DoesNotEndWith:
				expression3 = Expression.Call(expression, typeof(string).GetMethod("EndsWith", new Type[]
				{
					typeof(string)
				}), new Expression[]
				{
					expression2
				});
				return Expression.Not(expression3);
			case TextComparison.Contains:
				expression3 = Expression.Call(expression, typeof(string).GetMethod("IndexOf", new Type[]
				{
					typeof(string)
				}), new Expression[]
				{
					expression2
				});
				return Expression.GreaterThanOrEqual(expression3, Expression.Constant(0, typeof(int)));
			}
			expression3 = Expression.Call(expression, typeof(string).GetMethod("IndexOf", new Type[]
			{
				typeof(string)
			}), new Expression[]
			{
				expression2
			});
			return Expression.LessThan(expression3, Expression.Constant(0, typeof(int)));
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000CE7D8 File Offset: 0x000CC9D8
		protected override void CloneCore(Cloneable source)
		{
			QueryableTextCondition queryableTextCondition = source as QueryableTextCondition;
			if (queryableTextCondition != null)
			{
				this.Pattern = queryableTextCondition.Pattern;
				this.Comparison = queryableTextCondition.Comparison;
				this.IgnoreCase = queryableTextCondition.IgnoreCase;
			}
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000CE813 File Offset: 0x000CCA13
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableTextCondition();
		}

		// Token: 0x0400115C RID: 4444
		private string pattern;

		// Token: 0x0400115D RID: 4445
		private TextComparison comparison;

		// Token: 0x0400115E RID: 4446
		private bool ignoreCase;
	}
}
