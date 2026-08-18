using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x020006FF RID: 1791
	internal class OlapExpressionStringBuilder : OlapExpressionVisitor
	{
		// Token: 0x06003FA1 RID: 16289 RVA: 0x000C96E7 File Offset: 0x000C78E7
		internal OlapExpressionStringBuilder()
		{
			this.output = new StringBuilder();
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000C96FC File Offset: 0x000C78FC
		public static string ExpressionNodeToString(OlapExpression node)
		{
			OlapExpressionStringBuilder olapExpressionStringBuilder = new OlapExpressionStringBuilder();
			olapExpressionStringBuilder.Visit(node);
			return olapExpressionStringBuilder.ToString();
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000C971D File Offset: 0x000C791D
		public override string ToString()
		{
			return this.output.ToString();
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000C972C File Offset: 0x000C792C
		protected internal override OlapExpression VisitConstant(OlapConstantExpression node)
		{
			if (node.Value != null)
			{
				string s = node.Value.ToString();
				this.Append(s);
			}
			else
			{
				this.Append("null");
			}
			return node;
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000C9764 File Offset: 0x000C7964
		protected internal override OlapExpression VisitIdentifier(OlapIdentifierExpression node)
		{
			bool flag = (node.Name.StartsWith("[", StringComparison.OrdinalIgnoreCase) && node.Name.EndsWith("]", StringComparison.OrdinalIgnoreCase)) || !node.DelmitIdentifier;
			if (flag)
			{
				this.Append(node.Name);
			}
			else
			{
				string s = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
				{
					node.Name
				});
				this.Append(s);
			}
			return node;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000C97E0 File Offset: 0x000C79E0
		protected internal override OlapExpression VisitWrapper(OlapWrapperExpression node)
		{
			this.Append(OlapExpressionStringBuilder.GetOpenTokenForWrapperType(node.WrapperType));
			foreach (OlapExpression olapExpression in node.Expressions)
			{
				this.Visit(olapExpression);
				if (olapExpression != node.Expressions.Last<OlapExpression>())
				{
					this.AppendComma();
				}
			}
			this.Append(OlapExpressionStringBuilder.GetCloseTokenForWrapperType(node.WrapperType));
			return node;
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000C9868 File Offset: 0x000C7A68
		protected internal override OlapExpression VisitSelectQueryAxisClause(OlapSelectQueryAxisClauseExpression node)
		{
			if (node.NonEmpty)
			{
				this.Append("NON EMPTY ");
			}
			this.Visit(node.SetExpression);
			if (node.DimensionProperties.Count<string>() > 0)
			{
				this.Append(" DIMENSION PROPERTIES ");
				foreach (string text in node.DimensionProperties)
				{
					this.Append(text);
					if (text != node.DimensionProperties.Last<string>())
					{
						this.AppendComma();
					}
				}
			}
			return node;
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000C9908 File Offset: 0x000C7B08
		protected internal override OlapExpression VisitSelectClause(OlapSelectClauseExpression node)
		{
			this.Append("SELECT ");
			int num = 0;
			foreach (OlapSelectQueryAxisClauseExpression olapSelectQueryAxisClauseExpression in node.QueryAxisClauses)
			{
				this.Visit(olapSelectQueryAxisClauseExpression);
				this.Append(string.Format(CultureInfo.InvariantCulture, " ON {0} ", new object[]
				{
					num
				}));
				if (olapSelectQueryAxisClauseExpression != node.QueryAxisClauses.Last<OlapSelectQueryAxisClauseExpression>())
				{
					this.AppendComma();
				}
				num++;
			}
			this.Append("FROM ");
			this.Visit(node.From);
			return node;
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000C99BC File Offset: 0x000C7BBC
		protected internal override OlapExpression VisitFunction(OlapFunctionExpression node)
		{
			this.Append(node.Name);
			this.AppendOpenParentheses();
			foreach (OlapExpression olapExpression in node.Arguments)
			{
				this.Visit(olapExpression);
				if (olapExpression != node.Arguments.Last<OlapExpression>())
				{
					this.AppendComma();
				}
			}
			this.AppendCloseParentheses();
			return node;
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000C9A38 File Offset: 0x000C7C38
		private static string GetTokenForOperator(OlapExpressionOperator expressionOperator)
		{
			switch (expressionOperator)
			{
			case OlapExpressionOperator.CrossJoin:
				return "*";
			case OlapExpressionOperator.Except:
				return "-";
			case OlapExpressionOperator.And:
				return "AND";
			case OlapExpressionOperator.Or:
				return "OR";
			case OlapExpressionOperator.Range:
				return ":";
			case OlapExpressionOperator.Equals:
				return "=";
			case OlapExpressionOperator.DoesNotEqual:
				return "<>";
			case OlapExpressionOperator.IsGreaterThan:
				return ">";
			case OlapExpressionOperator.IsGreaterThanOrEqualTo:
				return ">=";
			case OlapExpressionOperator.IsLessThan:
				return "<";
			case OlapExpressionOperator.IsLessThanOrEqualTo:
				return "<=";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000C9AC4 File Offset: 0x000C7CC4
		private static string GetOpenTokenForWrapperType(OlapWrapperExpressionType wrapperType)
		{
			switch (wrapperType)
			{
			case OlapWrapperExpressionType.Set:
				return "{";
			case OlapWrapperExpressionType.Tuple:
				return "(";
			case OlapWrapperExpressionType.Parenthesis:
				return "(";
			case OlapWrapperExpressionType.Quotes:
				return "\"";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000C9B08 File Offset: 0x000C7D08
		private static string GetCloseTokenForWrapperType(OlapWrapperExpressionType wrapperType)
		{
			switch (wrapperType)
			{
			case OlapWrapperExpressionType.Set:
				return "}";
			case OlapWrapperExpressionType.Tuple:
				return ")";
			case OlapWrapperExpressionType.Parenthesis:
				return ")";
			case OlapWrapperExpressionType.Quotes:
				return "\"";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000C9B4C File Offset: 0x000C7D4C
		protected internal override OlapExpression VisitBinary(OlapBinaryExpression node)
		{
			this.AppendOpenParentheses();
			this.Visit(node.Left);
			this.AppendFormat(" {0} ", new object[]
			{
				OlapExpressionStringBuilder.GetTokenForOperator(node.Operator)
			});
			this.Visit(node.Right);
			this.AppendCloseParentheses();
			return node;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000C9BA1 File Offset: 0x000C7DA1
		protected internal override OlapExpression VisitMemberFunction(OlapMemberFuntionExpression node)
		{
			this.Visit(node.Member);
			this.AppendDot();
			this.Append(node.Name);
			return node;
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x000C9BC3 File Offset: 0x000C7DC3
		private void AppendDot()
		{
			this.Append(".");
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x000C9BD0 File Offset: 0x000C7DD0
		private void AppendOpenParentheses()
		{
			this.Append("(");
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x000C9BDD File Offset: 0x000C7DDD
		private void AppendCloseParentheses()
		{
			this.Append(")");
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x000C9BEA File Offset: 0x000C7DEA
		private void AppendComma()
		{
			this.Append(",");
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x000C9BF7 File Offset: 0x000C7DF7
		private void Append(string s)
		{
			this.output.Append(s);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x000C9C06 File Offset: 0x000C7E06
		private void AppendFormat(string format, params object[] args)
		{
			this.output.AppendFormat(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x040010DE RID: 4318
		private readonly StringBuilder output;
	}
}
