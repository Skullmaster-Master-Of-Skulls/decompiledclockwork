using System;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Resources;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000D4 RID: 212
	public class RangeExpression : ParameterDataSourceExpression
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000269A9 File Offset: 0x00024BA9
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x000269C9 File Offset: 0x00024BC9
		public string DataField
		{
			get
			{
				return ((string)base.ViewState["DataField"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00026E14 File Offset: 0x00025014
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00026E3D File Offset: 0x0002503D
		public RangeType MinType
		{
			get
			{
				object obj = base.ViewState["MinType"];
				if (obj == null)
				{
					return RangeType.None;
				}
				return (RangeType)obj;
			}
			set
			{
				base.ViewState["MinType"] = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00026E58 File Offset: 0x00025058
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00026E81 File Offset: 0x00025081
		public RangeType MaxType
		{
			get
			{
				object obj = base.ViewState["MaxType"];
				if (obj == null)
				{
					return RangeType.None;
				}
				return (RangeType)obj;
			}
			set
			{
				base.ViewState["MaxType"] = value;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00026E99 File Offset: 0x00025099
		internal new virtual IOrderedDictionary GetValues()
		{
			return base.Parameters.GetValues(base.Context, base.Owner);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00026EB4 File Offset: 0x000250B4
		public override IQueryable GetQueryable(IQueryable source)
		{
			if (source == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(this.DataField))
			{
				throw new InvalidOperationException(AtlasWeb.Expressions_DataFieldRequired);
			}
			IOrderedDictionary values = this.GetValues();
			ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
			Expression value = ExpressionHelper.GetValue(ExpressionHelper.CreatePropertyExpression(parameterExpression, this.DataField));
			if (this.MinType == RangeType.None && this.MaxType == RangeType.None)
			{
				throw new InvalidOperationException(AtlasWeb.RangeExpression_RangeTypeMustBeSpecified);
			}
			Expression expression = null;
			Expression expression2 = null;
			if (this.MinType != RangeType.None)
			{
				if (values.Count == 0)
				{
					throw new InvalidOperationException(AtlasWeb.RangeExpression_MinimumValueRequired);
				}
				if (values[0] != null)
				{
					expression = RangeExpression.GetMinRangeExpression(value, values[0], this.MinType);
				}
			}
			if (this.MaxType != RangeType.None)
			{
				if (values.Count == 0 || (expression != null && values.Count == 1))
				{
					throw new InvalidOperationException(AtlasWeb.RangeExpression_MaximumValueRequired);
				}
				object obj = (expression == null) ? values[0] : values[1];
				if (obj != null)
				{
					expression2 = RangeExpression.GetMaxRangeExpression(value, obj, this.MaxType);
				}
			}
			if (expression2 == null && expression == null)
			{
				return null;
			}
			Expression body = RangeExpression.CreateRangeExpressionBody(expression, expression2);
			return source.Where(Expression.Lambda(body, new ParameterExpression[]
			{
				parameterExpression
			}));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00026FDC File Offset: 0x000251DC
		private static Expression GetMinRangeExpression(Expression propertyExpression, object value, RangeType rangeType)
		{
			ConstantExpression right = Expression.Constant(ExpressionHelper.BuildObjectValue(value, propertyExpression.Type));
			switch (rangeType)
			{
			case RangeType.None:
				return null;
			case RangeType.Exclusive:
				return Expression.GreaterThan(propertyExpression, right);
			case RangeType.Inclusive:
				return Expression.GreaterThanOrEqual(propertyExpression, right);
			default:
				return null;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00027024 File Offset: 0x00025224
		private static Expression GetMaxRangeExpression(Expression propertyExpression, object value, RangeType rangeType)
		{
			ConstantExpression right = Expression.Constant(ExpressionHelper.BuildObjectValue(value, propertyExpression.Type));
			switch (rangeType)
			{
			case RangeType.None:
				return null;
			case RangeType.Exclusive:
				return Expression.LessThan(propertyExpression, right);
			case RangeType.Inclusive:
				return Expression.LessThanOrEqual(propertyExpression, right);
			default:
				return null;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002706A File Offset: 0x0002526A
		private static Expression CreateRangeExpressionBody(Expression minExpression, Expression maxExpression)
		{
			if (minExpression == null && maxExpression == null)
			{
				return null;
			}
			if (minExpression == null)
			{
				return maxExpression;
			}
			if (maxExpression == null)
			{
				return minExpression;
			}
			return Expression.AndAlso(minExpression, maxExpression);
		}
	}
}
