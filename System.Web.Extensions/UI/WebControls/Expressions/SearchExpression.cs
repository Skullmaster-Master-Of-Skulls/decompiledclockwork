using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Resources;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000D6 RID: 214
	public class SearchExpression : ParameterDataSourceExpression
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00027085 File Offset: 0x00025285
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x000270A5 File Offset: 0x000252A5
		public string DataFields
		{
			get
			{
				return ((string)base.ViewState["DataFields"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataFields"] = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x000270B8 File Offset: 0x000252B8
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x000270E1 File Offset: 0x000252E1
		public SearchType SearchType
		{
			get
			{
				object obj = base.ViewState["SearchType"];
				if (obj == null)
				{
					return SearchType.StartsWith;
				}
				return (SearchType)obj;
			}
			set
			{
				base.ViewState["SearchType"] = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000270FC File Offset: 0x000252FC
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x00027125 File Offset: 0x00025325
		public StringComparison ComparisonType
		{
			get
			{
				object obj = base.ViewState["ComparisonType"];
				if (obj == null)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return (StringComparison)obj;
			}
			set
			{
				base.ViewState["ComparisonType"] = value;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00027140 File Offset: 0x00025340
		public override IQueryable GetQueryable(IQueryable source)
		{
			if (source == null)
			{
				return null;
			}
			if (this.DataFields == null || string.IsNullOrEmpty(this.DataFields.Trim()))
			{
				throw new InvalidOperationException(AtlasWeb.Expressions_DataFieldRequired);
			}
			IDictionary<string, object> values = this.GetValues();
			if (values.Count == 0)
			{
				throw new InvalidOperationException(AtlasWeb.SearchExpression_ParameterRequired);
			}
			string text = Convert.ToString(values.First<KeyValuePair<string, object>>().Value, CultureInfo.CurrentCulture);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string[] array = this.DataFields.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			List<Expression> list = new List<Expression>();
			ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
			foreach (string text2 in array)
			{
				Expression property = ExpressionHelper.CreatePropertyExpression(parameterExpression, text2.Trim());
				list.Add(this.CreateCallExpression(property, text));
			}
			return source.Where(Expression.Lambda(ExpressionHelper.Or(list), new ParameterExpression[]
			{
				parameterExpression
			}));
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00027240 File Offset: 0x00025440
		private Expression CreateCallExpression(Expression property, string query)
		{
			if (this.SearchType == SearchType.Contains || base.ViewState["ComparisonType"] == null)
			{
				return Expression.Call(property, this.SearchType.ToString(), Type.EmptyTypes, new Expression[]
				{
					Expression.Constant(query, property.Type)
				});
			}
			return Expression.Call(property, this.SearchType.ToString(), Type.EmptyTypes, new Expression[]
			{
				Expression.Constant(query, property.Type),
				Expression.Constant(this.ComparisonType)
			});
		}
	}
}
