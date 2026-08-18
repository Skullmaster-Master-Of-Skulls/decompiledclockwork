using System;
using System.Globalization;
using WebGrease.Activities;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x020001A4 RID: 420
	public sealed class ValidateLowercaseVisitor : NodeVisitor
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x0007E0DC File Offset: 0x0007C2DC
		public override AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			if (styleSheet == null)
			{
				throw new ArgumentNullException("styleSheet");
			}
			ValidateLowercaseVisitor.ValidateForLowerCase(styleSheet.CharSetString);
			styleSheet.Imports.ForEach(delegate(ImportNode importNode)
			{
				ValidateLowercaseVisitor.ValidateForLowerCase(importNode.MinifyPrint());
			});
			styleSheet.StyleSheetRules.ForEach(delegate(StyleSheetRuleNode styleSheetRule)
			{
				styleSheetRule.Accept(this);
			});
			return styleSheet;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0007E15C File Offset: 0x0007C35C
		public override AstNode VisitRulesetNode(RulesetNode rulesetNode)
		{
			if (rulesetNode == null)
			{
				throw new ArgumentNullException("rulesetNode");
			}
			try
			{
				rulesetNode.SelectorsGroupNode.SelectorNodes.ForEach(delegate(SelectorNode selectorNode)
				{
					ValidateLowercaseVisitor.ValidateForLowerCase(selectorNode.MinifyPrint());
				});
				rulesetNode.Declarations.ForEach(delegate(DeclarationNode declarationNode)
				{
					declarationNode.Accept(this);
				});
			}
			catch (BuildWorkflowException inner)
			{
				throw new WorkflowException(string.Format(CultureInfo.CurrentUICulture, CssStrings.CssLowercaseValidationParentNodeError, new object[]
				{
					rulesetNode.PrettyPrint()
				}), inner);
			}
			return rulesetNode;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0007E218 File Offset: 0x0007C418
		public override AstNode VisitMediaNode(MediaNode mediaNode)
		{
			if (mediaNode == null)
			{
				throw new ArgumentNullException("mediaNode");
			}
			try
			{
				mediaNode.MediaQueries.ForEach(delegate(MediaQueryNode mediaQuery)
				{
					ValidateLowercaseVisitor.ValidateForLowerCase(mediaQuery.MinifyPrint());
				});
				mediaNode.Rulesets.ForEach(delegate(RulesetNode rulesetNode)
				{
					rulesetNode.Accept(this);
				});
			}
			catch (BuildWorkflowException inner)
			{
				throw new WorkflowException(string.Format(CultureInfo.CurrentUICulture, CssStrings.CssLowercaseValidationParentNodeError, new object[]
				{
					mediaNode.PrettyPrint()
				}), inner);
			}
			return mediaNode;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0007E2C0 File Offset: 0x0007C4C0
		public override AstNode VisitPageNode(PageNode pageNode)
		{
			if (pageNode == null)
			{
				throw new ArgumentNullException("pageNode");
			}
			try
			{
				ValidateLowercaseVisitor.ValidateForLowerCase(pageNode.PseudoPage);
				pageNode.Declarations.ForEach(delegate(DeclarationNode declarationNode)
				{
					declarationNode.Accept(this);
				});
			}
			catch (BuildWorkflowException inner)
			{
				throw new WorkflowException(string.Format(CultureInfo.CurrentUICulture, CssStrings.CssLowercaseValidationParentNodeError, new object[]
				{
					pageNode.PrettyPrint()
				}), inner);
			}
			return pageNode;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0007E340 File Offset: 0x0007C540
		public override AstNode VisitDeclarationNode(DeclarationNode declarationNode)
		{
			if (declarationNode == null)
			{
				throw new ArgumentNullException("declarationNode");
			}
			ValidateLowercaseVisitor.ValidateForLowerCase(declarationNode.Property);
			return declarationNode;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0007E35C File Offset: 0x0007C55C
		private static void ValidateForLowerCase(string textToValidate)
		{
			textToValidate = ResourcesResolver.LocalizationResourceKeyRegex.Replace(textToValidate, string.Empty);
			if (string.IsNullOrWhiteSpace(textToValidate))
			{
				return;
			}
			if (string.CompareOrdinal(textToValidate, textToValidate.ToLower(CultureInfo.InvariantCulture)) != 0)
			{
				throw new BuildWorkflowException(string.Format(CultureInfo.InvariantCulture, CssStrings.CssLowercaseValidationError, new object[]
				{
					textToValidate
				}));
			}
		}
	}
}
