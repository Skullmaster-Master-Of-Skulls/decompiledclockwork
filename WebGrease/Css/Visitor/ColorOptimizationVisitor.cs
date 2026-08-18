using System;
using System.Globalization;
using System.Text.RegularExpressions;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x0200019C RID: 412
	public sealed class ColorOptimizationVisitor : NodeTransformVisitor
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x0007AAB4 File Offset: 0x00078CB4
		public override AstNode VisitTermNode(TermNode termNode)
		{
			if (termNode == null)
			{
				throw new ArgumentNullException("termNode");
			}
			string text = termNode.Hexcolor;
			FunctionNode functionNode = termNode.FunctionNode;
			int num;
			int num2;
			int num3;
			if (functionNode != null && string.Compare(functionNode.FunctionName, "rgb", StringComparison.OrdinalIgnoreCase) == 0 && ColorOptimizationVisitor.TryGetRgb(functionNode.ExprNode, out num, out num2, out num3))
			{
				functionNode = null;
				text = string.Format(CultureInfo.InvariantCulture, "#{0:x2}{1:x2}{2:x2}", new object[]
				{
					num,
					num2,
					num3
				});
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				Match match = ColorOptimizationVisitor.ColorGroupCapture.Match(text);
				if (match.Success)
				{
					text = string.Format(CultureInfo.InvariantCulture, "#{0}{1}{2}", new object[]
					{
						match.Result("${r}"),
						match.Result("${g}"),
						match.Result("${b}")
					});
				}
				text = text.ToLowerInvariant();
			}
			return new TermNode(termNode.UnaryOperator, termNode.NumberBasedValue, termNode.StringBasedValue, text, functionNode, termNode.ImportantComments, null);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0007ABD0 File Offset: 0x00078DD0
		private static bool TryGetRgb(ExprNode exprNode, out int red, out int green, out int blue)
		{
			red = (green = (blue = 0));
			return ColorOptimizationVisitor.IsThreeNumberArguments(exprNode) && ColorOptimizationVisitor.TryGetColorFragment(exprNode.TermNode, out red) && ColorOptimizationVisitor.TryGetColorFragment(exprNode.TermsWithOperators[0].TermNode, out green) && ColorOptimizationVisitor.TryGetColorFragment(exprNode.TermsWithOperators[1].TermNode, out blue);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0007AC34 File Offset: 0x00078E34
		private static bool IsThreeNumberArguments(ExprNode exprNode)
		{
			return exprNode != null && ColorOptimizationVisitor.IsNumberTerm(exprNode.TermNode) && exprNode.TermsWithOperators != null && exprNode.TermsWithOperators.Count == 2 && ColorOptimizationVisitor.IsCommaNumber(exprNode.TermsWithOperators[0]) && ColorOptimizationVisitor.IsCommaNumber(exprNode.TermsWithOperators[1]);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0007AC8D File Offset: 0x00078E8D
		private static bool IsNumberTerm(TermNode termNode)
		{
			return termNode != null && !string.IsNullOrWhiteSpace(termNode.NumberBasedValue);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0007ACA2 File Offset: 0x00078EA2
		private static bool IsCommaNumber(TermWithOperatorNode termWithOperatorNode)
		{
			return termWithOperatorNode != null && termWithOperatorNode.Operator == "," && ColorOptimizationVisitor.IsNumberTerm(termWithOperatorNode.TermNode);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0007ACC8 File Offset: 0x00078EC8
		private static bool TryGetColorFragment(TermNode termNode, out int fragment)
		{
			bool result = false;
			fragment = 0;
			Match match = ColorOptimizationVisitor.NumberBasedValue.Match(termNode.NumberBasedValue);
			if (match != null)
			{
				string text = match.Result("$4");
				if (string.IsNullOrWhiteSpace(text))
				{
					if (string.IsNullOrWhiteSpace(match.Result("$3")))
					{
						result = (int.TryParse(match.Result("$2"), out fragment) && 0 <= fragment && fragment <= 255);
					}
				}
				else if (string.CompareOrdinal(text, "%") == 0)
				{
					fragment = (int)Math.Round((double)match.Result("$1").ParseFloat() / 100.0 * 255.0, 0);
					result = (0 <= fragment && fragment <= 255);
				}
			}
			return result;
		}

		// Token: 0x04000B5A RID: 2906
		private static readonly Regex NumberBasedValue = new Regex("^(([0-9]*)(\\.[0-9]+)?)([a-z%]*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000B5B RID: 2907
		private static readonly Regex ColorGroupCapture = new Regex("^\\#(?<r>[0-9a-f])\\k<r>(?<g>[0-9a-f])\\k<g>(?<b>[0-9a-f])\\k<b>$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
