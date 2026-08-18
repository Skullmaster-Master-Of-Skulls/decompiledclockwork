using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x020001A3 RID: 419
	public sealed class SelectorValidationOptimizationVisitor : NodeVisitor
	{
		// Token: 0x060015A8 RID: 5544 RVA: 0x0007DD8F File Offset: 0x0007BF8F
		public SelectorValidationOptimizationVisitor(HashSet<string> selectorsToValidateOrRemove, bool shouldMatchExactly, bool validate)
		{
			this.validate = validate;
			this.shouldMatchExactly = shouldMatchExactly;
			this.selectorsToValidateOrRemove = (selectorsToValidateOrRemove ?? new HashSet<string>());
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0007DDF0 File Offset: 0x0007BFF0
		public override AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			if (styleSheet == null)
			{
				throw new ArgumentNullException("styleSheet");
			}
			List<StyleSheetRuleNode> updatedStyleSheetRules = new List<StyleSheetRuleNode>();
			styleSheet.StyleSheetRules.ForEach(delegate(StyleSheetRuleNode ruleSetMediaPageNode)
			{
				StyleSheetRuleNode styleSheetRuleNode = (StyleSheetRuleNode)ruleSetMediaPageNode.Accept(this);
				if (styleSheetRuleNode != null)
				{
					updatedStyleSheetRules.Add(styleSheetRuleNode);
				}
			});
			return new StyleSheetNode(styleSheet.CharSetString, styleSheet.Imports, styleSheet.Namespaces, updatedStyleSheetRules.AsReadOnly());
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0007DEAC File Offset: 0x0007C0AC
		public override AstNode VisitRulesetNode(RulesetNode rulesetNode)
		{
			string text = rulesetNode.PrintSelector();
			string text2 = string.Empty;
			bool flag = false;
			if (this.shouldMatchExactly)
			{
				flag = this.selectorsToValidateOrRemove.Contains(text);
				text2 = text;
			}
			else
			{
				foreach (string text3 in this.selectorsToValidateOrRemove)
				{
					if (text.Contains(text3))
					{
						flag = true;
						text2 = text3;
						break;
					}
				}
			}
			if (!flag)
			{
				return rulesetNode;
			}
			if (this.validate)
			{
				throw new BuildWorkflowException(string.Format(CultureInfo.CurrentUICulture, CssStrings.CssSelectorHackError, new object[]
				{
					text2
				}));
			}
			if (rulesetNode.SelectorsGroupNode.SelectorNodes.Count > 1)
			{
				List<SelectorNode> list = (from sn in rulesetNode.SelectorsGroupNode.SelectorNodes
				where !this.selectorsToValidateOrRemove.Any((string sr) => sn.MinifyPrint().Contains(sr))
				select sn).ToList<SelectorNode>();
				if (list.Any<SelectorNode>())
				{
					return new RulesetNode(new SelectorsGroupNode(new ReadOnlyCollection<SelectorNode>(list)), rulesetNode.Declarations, rulesetNode.ImportantComments);
				}
			}
			return null;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0007E038 File Offset: 0x0007C238
		public override AstNode VisitMediaNode(MediaNode mediaNode)
		{
			if (mediaNode == null)
			{
				throw new ArgumentNullException("mediaNode");
			}
			List<RulesetNode> updatedRulesetNodes = new List<RulesetNode>();
			List<PageNode> updatePageNodes = new List<PageNode>();
			mediaNode.Rulesets.ForEach(delegate(RulesetNode rulesetNode)
			{
				RulesetNode rulesetNode2 = (RulesetNode)rulesetNode.Accept(this);
				if (rulesetNode2 != null)
				{
					updatedRulesetNodes.Add(rulesetNode2);
				}
			});
			mediaNode.PageNodes.ForEach(delegate(PageNode page)
			{
				PageNode pageNode = (PageNode)page.Accept(this);
				if (pageNode != null)
				{
					updatePageNodes.Add(pageNode);
				}
			});
			return new MediaNode(mediaNode.MediaQueries, updatedRulesetNodes.AsReadOnly(), updatePageNodes.AsReadOnly());
		}

		// Token: 0x04000B7D RID: 2941
		private readonly HashSet<string> selectorsToValidateOrRemove;

		// Token: 0x04000B7E RID: 2942
		private readonly bool shouldMatchExactly;

		// Token: 0x04000B7F RID: 2943
		private readonly bool validate;
	}
}
