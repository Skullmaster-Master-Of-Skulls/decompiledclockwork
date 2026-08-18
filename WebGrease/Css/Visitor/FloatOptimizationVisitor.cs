using System;
using System.Text.RegularExpressions;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x0200019E RID: 414
	public sealed class FloatOptimizationVisitor : NodeTransformVisitor
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x0007ADEC File Offset: 0x00078FEC
		public override AstNode VisitTermNode(TermNode termNode)
		{
			if (termNode == null)
			{
				throw new ArgumentNullException("termNode");
			}
			FunctionNode functionNode = termNode.FunctionNode;
			string numberBasedValue = termNode.NumberBasedValue;
			if (!string.IsNullOrWhiteSpace(numberBasedValue))
			{
				Match match = FloatOptimizationVisitor.NumberBasedValue.Match(numberBasedValue);
				if (match.Success)
				{
					float num = match.Result("$1").ParseFloat();
					string text = match.Result("$4");
					if (num != 0f)
					{
						string str = match.Result("$2").TrimStart("0".ToCharArray());
						string text2 = match.Result("$3").TrimEnd("0".ToCharArray());
						if (text2 == '.'.ToString())
						{
							text2 = string.Empty;
						}
						return new TermNode(termNode.UnaryOperator, str + text2 + text, termNode.StringBasedValue, termNode.Hexcolor, termNode.FunctionNode, termNode.ImportantComments, null);
					}
					if (string.IsNullOrEmpty(text) || text == "%" || FloatOptimizationVisitor.LengthUnits.IsMatch(text))
					{
						return new TermNode(termNode.UnaryOperator, "0", termNode.StringBasedValue, termNode.Hexcolor, termNode.FunctionNode, termNode.ImportantComments, null);
					}
					return new TermNode(termNode.UnaryOperator, "0" + text, termNode.StringBasedValue, termNode.Hexcolor, termNode.FunctionNode, termNode.ImportantComments, null);
				}
			}
			else if (functionNode != null)
			{
				functionNode = (FunctionNode)functionNode.Accept(this);
			}
			return new TermNode(termNode.UnaryOperator, numberBasedValue, termNode.StringBasedValue, termNode.Hexcolor, functionNode, termNode.ImportantComments, null);
		}

		// Token: 0x04000B5D RID: 2909
		private static readonly Regex NumberBasedValue = new Regex("^(([0-9]+)([\\.]?[0-9]*))([a-z%]*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000B5E RID: 2910
		private static readonly Regex LengthUnits = new Regex("^(cm|mm|in|px|pt|pc|em|ex|ch|rem|vw|vh|vmin|vmax|fr|gr)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
