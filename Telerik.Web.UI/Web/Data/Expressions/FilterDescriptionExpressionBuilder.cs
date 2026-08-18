using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BAF RID: 7087
	internal class FilterDescriptionExpressionBuilder : FilterExpressionBuilder
	{
		// Token: 0x1700539A RID: 21402
		// (get) Token: 0x0601122E RID: 70190 RVA: 0x003C774B File Offset: 0x003C594B
		public FilterDescription FilterDescription
		{
			get
			{
				return this.filterDescription;
			}
		}

		// Token: 0x0601122F RID: 70191 RVA: 0x003C7753 File Offset: 0x003C5953
		public FilterDescriptionExpressionBuilder(ParameterExpression parameterExpression, FilterDescription filterDescription) : base(parameterExpression)
		{
			this.filterDescription = filterDescription;
		}

		// Token: 0x06011230 RID: 70192 RVA: 0x003C7763 File Offset: 0x003C5963
		public override Expression CreateBodyExpression()
		{
			if (this.filterDescription.IsActive)
			{
				return this.CreateActiveFilterExpression();
			}
			return ExpressionParser.TrueLiteral;
		}

		// Token: 0x06011231 RID: 70193 RVA: 0x003C777E File Offset: 0x003C597E
		protected virtual Expression CreateActiveFilterExpression()
		{
			return this.CreateSatisfiesFilterExpression();
		}

		// Token: 0x06011232 RID: 70194 RVA: 0x003C7788 File Offset: 0x003C5988
		private MethodCallExpression CreateSatisfiesFilterExpression()
		{
			Expression expression = base.ParameterExpression;
			if (expression.Type.IsValueType)
			{
				expression = Expression.Convert(expression, typeof(object));
			}
			return Expression.Call(this.FilterDescriptionExpression, this.SatisfiesFilterMethodInfo, new Expression[]
			{
				expression
			});
		}

		// Token: 0x1700539B RID: 21403
		// (get) Token: 0x06011233 RID: 70195 RVA: 0x003C77D7 File Offset: 0x003C59D7
		private Expression FilterDescriptionExpression
		{
			get
			{
				return Expression.Constant(this.filterDescription);
			}
		}

		// Token: 0x1700539C RID: 21404
		// (get) Token: 0x06011234 RID: 70196 RVA: 0x003C77E4 File Offset: 0x003C59E4
		private MethodInfo SatisfiesFilterMethodInfo
		{
			get
			{
				return this.filterDescription.GetType().GetMethod("SatisfiesFilter", new Type[]
				{
					typeof(object)
				});
			}
		}

		// Token: 0x04004CB7 RID: 19639
		private readonly FilterDescription filterDescription;
	}
}
