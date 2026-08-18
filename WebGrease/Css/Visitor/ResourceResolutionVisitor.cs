using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebGrease.Activities;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;

namespace WebGrease.Css.Visitor
{
	// Token: 0x0200002F RID: 47
	public class ResourceResolutionVisitor : NodeTransformVisitor
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00007F43 File Offset: 0x00006143
		public ResourceResolutionVisitor(IEnumerable<IDictionary<string, string>> resources)
		{
			if (resources == null)
			{
				throw new ArgumentNullException("resources");
			}
			if (!resources.Any<IDictionary<string, string>>())
			{
				throw new ArgumentException("The resources should have at least 1 item.");
			}
			this.resources = resources;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00007F74 File Offset: 0x00006174
		public override AstNode VisitHashClassAtNameAttribPseudoNegationNode(HashClassAtNameAttribPseudoNegationNode hashClassAtNameAttribPseudoNegationNode)
		{
			if (string.IsNullOrWhiteSpace(hashClassAtNameAttribPseudoNegationNode.ReplacementToken))
			{
				return base.VisitHashClassAtNameAttribPseudoNegationNode(hashClassAtNameAttribPseudoNegationNode);
			}
			string text = ResourceResolutionVisitor.ReplaceTokens(hashClassAtNameAttribPseudoNegationNode.ReplacementToken, this.resources);
			if (text.StartsWith("#", StringComparison.OrdinalIgnoreCase))
			{
				return new HashClassAtNameAttribPseudoNegationNode(text, null, null, null, null, null, null);
			}
			if (text.StartsWith(".", StringComparison.OrdinalIgnoreCase))
			{
				return new HashClassAtNameAttribPseudoNegationNode(null, text, null, null, null, null, null);
			}
			if (text.StartsWith(".", StringComparison.OrdinalIgnoreCase))
			{
				return new HashClassAtNameAttribPseudoNegationNode(null, text, null, null, null, null, null);
			}
			return new HashClassAtNameAttribPseudoNegationNode(null, null, text, null, null, null, null);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00008008 File Offset: 0x00006208
		public override AstNode VisitTermNode(TermNode termNode)
		{
			if (!string.IsNullOrWhiteSpace(termNode.ReplacementTokenBasedValue))
			{
				string newValue = ResourceResolutionVisitor.ReplaceTokens(termNode.ReplacementTokenBasedValue, this.resources);
				return ResourceResolutionVisitor.CreateTermNode(termNode, newValue);
			}
			if (ResourceResolutionVisitor.HasTokens(termNode.StringBasedValue))
			{
				string newValue2 = ResourceResolutionVisitor.ReplaceTokens(termNode.StringBasedValue, this.resources);
				return ResourceResolutionVisitor.CreateTermNode(termNode, newValue2);
			}
			return base.VisitTermNode(termNode);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000806C File Offset: 0x0000626C
		private static AstNode CreateTermNode(TermNode termNode, string newValue)
		{
			newValue = newValue.Trim();
			if (ResourceResolutionVisitor.IsNumberBasedValue(newValue))
			{
				return new TermNode(termNode.UnaryOperator, newValue, null, null, null, null, null);
			}
			if (ResourceResolutionVisitor.IsHexColor(newValue))
			{
				return new TermNode(termNode.UnaryOperator, null, null, newValue, null, null, null);
			}
			return new TermNode(termNode.UnaryOperator, null, newValue, null, null, null, null);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000080C8 File Offset: 0x000062C8
		private static bool IsNumberBasedValue(string newValue)
		{
			newValue = newValue.TrimStart(new char[]
			{
				'-'
			});
			return newValue != null && newValue.Length > 0 && ResourceResolutionVisitor.IsNumber(newValue[0]);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00008104 File Offset: 0x00006304
		private static bool IsNumber(char c)
		{
			return ResourceResolutionVisitor.numberChars.Contains(c);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00008111 File Offset: 0x00006311
		private static bool IsHexColor(string newValue)
		{
			return newValue != null && newValue.Length > 3 && newValue[0] == '#' && ResourceResolutionVisitor.IsHexColorValue(newValue.Substring(1));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00008138 File Offset: 0x00006338
		private static bool IsHexColorValue(string value)
		{
			return value.All(new Func<char, bool>(ResourceResolutionVisitor.hexChars.Contains<char>));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00008150 File Offset: 0x00006350
		public override AstNode VisitDeclarationNode(DeclarationNode declarationNode)
		{
			if (ResourceResolutionVisitor.HasTokens(declarationNode.Property))
			{
				return new DeclarationNode(ResourceResolutionVisitor.ReplaceTokens(declarationNode.Property, this.resources), declarationNode.ExprNode.Accept(this) as ExprNode, declarationNode.Prio, declarationNode.ImportantComments);
			}
			return base.VisitDeclarationNode(declarationNode);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000081A5 File Offset: 0x000063A5
		public override AstNode VisitMediaExpressionNode(MediaExpressionNode mediaExpressionNode)
		{
			if (ResourceResolutionVisitor.HasTokens(mediaExpressionNode.MediaFeature))
			{
				return new MediaExpressionNode(ResourceResolutionVisitor.ReplaceTokens(mediaExpressionNode.MediaFeature, this.resources), mediaExpressionNode.ExprNode.Accept(this) as ExprNode);
			}
			return base.VisitMediaExpressionNode(mediaExpressionNode);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000081E3 File Offset: 0x000063E3
		private static bool HasTokens(string stringBasedValue)
		{
			return !string.IsNullOrWhiteSpace(stringBasedValue) && stringBasedValue.Contains("%");
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000828C File Offset: 0x0000648C
		private static string ReplaceTokens(string value, IEnumerable<IDictionary<string, string>> resources)
		{
			return ResourcesResolver.LocalizationResourceKeyRegex.Replace(value, delegate(Match match)
			{
				string key = match.Result("$1");
				foreach (IDictionary<string, string> dictionary in resources)
				{
					string text;
					if (dictionary.TryGetValue(key, out text))
					{
						if (text.Contains("%"))
						{
							text = ResourceResolutionVisitor.ReplaceTokens(text, resources);
						}
						return text;
					}
				}
				return match.Value;
			});
		}

		// Token: 0x0400009D RID: 157
		private readonly IEnumerable<IDictionary<string, string>> resources;

		// Token: 0x0400009E RID: 158
		private static char[] numberChars = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9'
		};

		// Token: 0x0400009F RID: 159
		private static char[] hexChars = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};
	}
}
