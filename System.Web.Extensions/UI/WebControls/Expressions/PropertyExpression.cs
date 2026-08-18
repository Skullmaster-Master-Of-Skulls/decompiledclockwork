using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000D2 RID: 210
	public class PropertyExpression : ParameterDataSourceExpression
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x00026C84 File Offset: 0x00024E84
		public override IQueryable GetQueryable(IQueryable source)
		{
			if (source == null)
			{
				return null;
			}
			IDictionary<string, object> values = this.GetValues();
			List<Expression> list = new List<Expression>();
			ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
			foreach (KeyValuePair<string, object> keyValuePair in values)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key))
				{
					Expression expression = ExpressionHelper.CreatePropertyExpression(parameterExpression, keyValuePair.Key);
					object obj = ExpressionHelper.BuildObjectValue(keyValuePair.Value, expression.Type);
					if (obj != null)
					{
						Expression right = Expression.Constant(obj, expression.Type);
						Expression item = Expression.Equal(expression, right);
						list.Add(item);
					}
				}
			}
			if (list.Any<Expression>())
			{
				Expression body = ExpressionHelper.And(list);
				return source.Where(Expression.Lambda(body, new ParameterExpression[]
				{
					parameterExpression
				}));
			}
			return source;
		}
	}
}
