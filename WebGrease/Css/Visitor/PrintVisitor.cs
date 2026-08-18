using System;
using System.Globalization;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.Animation;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x020001A2 RID: 418
	public class PrintVisitor : NodeVisitor
	{
		// Token: 0x06001571 RID: 5489 RVA: 0x0007CA67 File Offset: 0x0007AC67
		private PrintVisitor()
		{
			PrintVisitor.IndentSize = 2;
			PrintVisitor.IndentCharacter = ' ';
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0007CA87 File Offset: 0x0007AC87
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x0007CA8E File Offset: 0x0007AC8E
		public static char IndentCharacter { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0007CA96 File Offset: 0x0007AC96
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x0007CA9D File Offset: 0x0007AC9D
		public static int IndentSize { get; set; }

		// Token: 0x06001576 RID: 5494 RVA: 0x0007CAA5 File Offset: 0x0007ACA5
		public static string Print(AstNode node, bool prettyPrint)
		{
			return new PrintVisitor().Print(prettyPrint, node);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0007CAD4 File Offset: 0x0007ACD4
		public override AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			if (styleSheet == null)
			{
				return null;
			}
			if (!string.IsNullOrWhiteSpace(styleSheet.CharSetString))
			{
				this._printerFormatter.Append("@charset ");
				this._printerFormatter.Append(styleSheet.CharSetString);
				this._printerFormatter.AppendLine(';');
			}
			styleSheet.Imports.ForEach(delegate(ImportNode importNode)
			{
				importNode.Accept(this);
			});
			styleSheet.Namespaces.ForEach(delegate(NamespaceNode namespaceNode)
			{
				namespaceNode.Accept(this);
			});
			styleSheet.StyleSheetRules.ForEach(delegate(StyleSheetRuleNode styleSheetRuleNode)
			{
				styleSheetRuleNode.Accept(this);
			});
			return styleSheet;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0007CB84 File Offset: 0x0007AD84
		public override AstNode VisitImportNode(ImportNode importNode)
		{
			this._printerFormatter.Append("@import ");
			switch (importNode.AllowedImportDataType)
			{
			case AllowedImportData.String:
			case AllowedImportData.Uri:
				this._printerFormatter.Append(importNode.ImportDataValue);
				break;
			}
			if (importNode.MediaQueries.Count > 0)
			{
				this._printerFormatter.Append(' ');
				importNode.MediaQueries.ForEach(delegate(MediaQueryNode mediaQuery, bool last)
				{
					mediaQuery.Accept(this);
					if (!last)
					{
						this._printerFormatter.Append(',');
					}
				});
			}
			this._printerFormatter.AppendLine(';');
			return importNode;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0007CC10 File Offset: 0x0007AE10
		public override AstNode VisitNamespaceNode(NamespaceNode namespaceNode)
		{
			this._printerFormatter.Append("@namespace");
			if (!string.IsNullOrWhiteSpace(namespaceNode.Prefix))
			{
				this._printerFormatter.Append(' ');
				this._printerFormatter.Append(namespaceNode.Prefix);
			}
			this._printerFormatter.Append(' ');
			this._printerFormatter.Append(namespaceNode.Value);
			this._printerFormatter.AppendLine(';');
			return namespaceNode;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0007CCB8 File Offset: 0x0007AEB8
		public override AstNode VisitRulesetNode(RulesetNode rulesetNode)
		{
			rulesetNode.SelectorsGroupNode.Accept(this);
			this._printerFormatter.WriteIndent();
			this._printerFormatter.AppendLine('{');
			this._printerFormatter.IncrementIndentLevel();
			rulesetNode.Declarations.ForEach(delegate(DeclarationNode declaration, bool last)
			{
				AstNode astNode = declaration.Accept(this);
				if (!last && astNode != null)
				{
					this._printerFormatter.AppendLine(';');
				}
			});
			rulesetNode.ImportantComments.ForEach(delegate(ImportantCommentNode comment)
			{
				comment.Accept(this);
			});
			this._printerFormatter.DecrementIndentLevel();
			this._printerFormatter.AppendLine();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.AppendLine('}');
			return rulesetNode;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0007CD6C File Offset: 0x0007AF6C
		public override AstNode VisitSelectorsGroupNode(SelectorsGroupNode selectorsGroupNode)
		{
			selectorsGroupNode.SelectorNodes.ForEach(delegate(SelectorNode selector, bool last)
			{
				selector.Accept(this);
				if (!last)
				{
					this._printerFormatter.AppendLine(',');
				}
			});
			this._printerFormatter.AppendLine();
			return selectorsGroupNode;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0007CDED File Offset: 0x0007AFED
		public override AstNode VisitSelectorNode(SelectorNode selectorNode)
		{
			this._printerFormatter.WriteIndent();
			selectorNode.SimpleSelectorSequenceNode.Accept(this);
			selectorNode.CombinatorSimpleSelectorSequenceNodes.ForEach(delegate(CombinatorSimpleSelectorSequenceNode combinatorSimpleSelectorSequenceNode, bool selectorIndex)
			{
				if (combinatorSimpleSelectorSequenceNode.Combinator == Combinator.SingleSpace && this._printerFormatter.ToString().EndsWith(' '.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))
				{
					this._printerFormatter.Remove(this._printerFormatter.Length() - 1, 1);
				}
				combinatorSimpleSelectorSequenceNode.Accept(this);
			});
			return selectorNode;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0007CE2C File Offset: 0x0007B02C
		public override AstNode VisitSimpleSelectorSequenceNode(SimpleSelectorSequenceNode simpleSelectorSequenceNode)
		{
			if (simpleSelectorSequenceNode.TypeSelectorNode != null)
			{
				simpleSelectorSequenceNode.TypeSelectorNode.Accept(this);
			}
			if (simpleSelectorSequenceNode.UniversalSelectorNode != null)
			{
				simpleSelectorSequenceNode.UniversalSelectorNode.Accept(this);
			}
			if (simpleSelectorSequenceNode.HashClassAttribPseudoNegationNodes.Count > 0)
			{
				this._printerFormatter.Append(simpleSelectorSequenceNode.Separator);
			}
			simpleSelectorSequenceNode.HashClassAttribPseudoNegationNodes.ForEach(delegate(HashClassAtNameAttribPseudoNegationNode hashClassAttribPseudoNegationNode)
			{
				hashClassAttribPseudoNegationNode.Accept(this);
			});
			return simpleSelectorSequenceNode;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0007CE9A File Offset: 0x0007B09A
		public override AstNode VisitUniversalSelectorNode(UniversalSelectorNode universalSelectorNode)
		{
			if (universalSelectorNode.SelectorNamespacePrefixNode != null)
			{
				universalSelectorNode.SelectorNamespacePrefixNode.Accept(this);
			}
			this._printerFormatter.Append("*");
			return universalSelectorNode;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0007CEC2 File Offset: 0x0007B0C2
		public override AstNode VisitTypeSelectorNode(TypeSelectorNode typeSelectorNode)
		{
			if (typeSelectorNode.SelectorNamespacePrefixNode != null)
			{
				typeSelectorNode.SelectorNamespacePrefixNode.Accept(this);
			}
			this._printerFormatter.Append(typeSelectorNode.ElementName);
			return typeSelectorNode;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0007CEEB File Offset: 0x0007B0EB
		public override AstNode VisitSelectorNamespacePrefixNode(SelectorNamespacePrefixNode selectorNamespacePrefixNode)
		{
			this._printerFormatter.Append(selectorNamespacePrefixNode.Prefix);
			this._printerFormatter.Append("|");
			return selectorNamespacePrefixNode;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0007CF10 File Offset: 0x0007B110
		public override AstNode VisitHashClassAtNameAttribPseudoNegationNode(HashClassAtNameAttribPseudoNegationNode hashClassAtNameAttribPseudoNegationNode)
		{
			if (!string.IsNullOrWhiteSpace(hashClassAtNameAttribPseudoNegationNode.Hash))
			{
				this._printerFormatter.Append(hashClassAtNameAttribPseudoNegationNode.Hash);
			}
			else if (!string.IsNullOrWhiteSpace(hashClassAtNameAttribPseudoNegationNode.CssClass))
			{
				this._printerFormatter.Append(hashClassAtNameAttribPseudoNegationNode.CssClass);
			}
			else if (!string.IsNullOrWhiteSpace(hashClassAtNameAttribPseudoNegationNode.AtName))
			{
				this._printerFormatter.Append(hashClassAtNameAttribPseudoNegationNode.AtName);
			}
			else if (!string.IsNullOrWhiteSpace(hashClassAtNameAttribPseudoNegationNode.ReplacementToken))
			{
				this._printerFormatter.Append(hashClassAtNameAttribPseudoNegationNode.ReplacementToken);
			}
			else if (hashClassAtNameAttribPseudoNegationNode.AttribNode != null)
			{
				hashClassAtNameAttribPseudoNegationNode.AttribNode.Accept(this);
			}
			else if (hashClassAtNameAttribPseudoNegationNode.PseudoNode != null)
			{
				hashClassAtNameAttribPseudoNegationNode.PseudoNode.Accept(this);
			}
			else if (hashClassAtNameAttribPseudoNegationNode.NegationNode != null)
			{
				hashClassAtNameAttribPseudoNegationNode.NegationNode.Accept(this);
			}
			return hashClassAtNameAttribPseudoNegationNode;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0007CFE8 File Offset: 0x0007B1E8
		public override AstNode VisitAttribNode(AttribNode attrib)
		{
			this._printerFormatter.Append('[');
			if (attrib.SelectorNamespacePrefixNode != null)
			{
				attrib.SelectorNamespacePrefixNode.Accept(this);
			}
			this._printerFormatter.Append(attrib.Ident);
			if (attrib.OperatorAndValueNode != null)
			{
				attrib.OperatorAndValueNode.Accept(this);
			}
			this._printerFormatter.Append(']');
			return attrib;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0007D04C File Offset: 0x0007B24C
		public override AstNode VisitAttribOperatorAndValueNode(AttribOperatorAndValueNode attribOperatorAndValueNode)
		{
			if (string.IsNullOrWhiteSpace(attribOperatorAndValueNode.IdentOrString))
			{
				return attribOperatorAndValueNode;
			}
			switch (attribOperatorAndValueNode.AttribOperatorKind)
			{
			case AttribOperatorKind.Prefix:
				this._printerFormatter.Append("^=");
				break;
			case AttribOperatorKind.Suffix:
				this._printerFormatter.Append("$=");
				break;
			case AttribOperatorKind.Substring:
				this._printerFormatter.Append("*=");
				break;
			case AttribOperatorKind.Equal:
				this._printerFormatter.Append("=");
				break;
			case AttribOperatorKind.Includes:
				this._printerFormatter.Append("~=");
				break;
			case AttribOperatorKind.DashMatch:
				this._printerFormatter.Append("|=");
				break;
			}
			this._printerFormatter.Append(attribOperatorAndValueNode.IdentOrString);
			return attribOperatorAndValueNode;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0007D10C File Offset: 0x0007B30C
		public override AstNode VisitPseudoNode(PseudoNode pseudoNode)
		{
			for (int i = 0; i < pseudoNode.NumberOfColons; i++)
			{
				this._printerFormatter.Append(':');
			}
			if (pseudoNode.FunctionalPseudoNode != null)
			{
				pseudoNode.FunctionalPseudoNode.Accept(this);
			}
			else if (!string.IsNullOrWhiteSpace(pseudoNode.Ident))
			{
				this._printerFormatter.Append(pseudoNode.Ident);
				if (pseudoNode.Ident == "first-letter" || pseudoNode.Ident == "first-line")
				{
					this._printerFormatter.Append(' ');
				}
			}
			return pseudoNode;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0007D1A0 File Offset: 0x0007B3A0
		public override AstNode VisitNegationNode(NegationNode negationNode)
		{
			this._printerFormatter.Append(':');
			this._printerFormatter.Append("not");
			this._printerFormatter.Append('(');
			negationNode.NegationArgNode.Accept(this);
			this._printerFormatter.Append(')');
			return negationNode;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0007D1F4 File Offset: 0x0007B3F4
		public override AstNode VisitNegationArgNode(NegationArgNode negationArgNode)
		{
			if (negationArgNode.TypeSelectorNode != null)
			{
				negationArgNode.TypeSelectorNode.Accept(this);
			}
			else if (negationArgNode.UniversalSelectorNode != null)
			{
				negationArgNode.UniversalSelectorNode.Accept(this);
			}
			else if (!string.IsNullOrWhiteSpace(negationArgNode.Hash))
			{
				this._printerFormatter.Append(negationArgNode.Hash);
			}
			else if (!string.IsNullOrWhiteSpace(negationArgNode.CssClass))
			{
				this._printerFormatter.Append(negationArgNode.CssClass);
			}
			else if (negationArgNode.AttribNode != null)
			{
				negationArgNode.AttribNode.Accept(this);
			}
			else if (negationArgNode.PseudoNode != null)
			{
				negationArgNode.PseudoNode.Accept(this);
			}
			return negationArgNode;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0007D2A0 File Offset: 0x0007B4A0
		public override AstNode VisitDeclarationNode(DeclarationNode declarationNode)
		{
			bool flag = declarationNode.Property.StartsWith("/", StringComparison.OrdinalIgnoreCase);
			bool flag2 = declarationNode.Property.StartsWith("-wg-", StringComparison.OrdinalIgnoreCase);
			if (!this._printerFormatter.PrettyPrint && (flag || flag2))
			{
				return null;
			}
			foreach (ImportantCommentNode importantCommentNode in declarationNode.ImportantComments)
			{
				importantCommentNode.Accept(this);
			}
			this._printerFormatter.WriteIndent();
			this._printerFormatter.Append(declarationNode.Property);
			this._printerFormatter.Append(':');
			declarationNode.ExprNode.Accept(this);
			if (flag)
			{
				this._printerFormatter.AppendLine();
				return null;
			}
			this._printerFormatter.Append(declarationNode.Prio);
			return declarationNode;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0007D38C File Offset: 0x0007B58C
		public override AstNode VisitExprNode(ExprNode exprNode)
		{
			foreach (ImportantCommentNode importantCommentNode in exprNode.ImportantComments)
			{
				importantCommentNode.Accept(this);
			}
			exprNode.TermNode.Accept(this);
			exprNode.TermsWithOperators.ForEach(delegate(TermWithOperatorNode termWithOperator)
			{
				termWithOperator.Accept(this);
			});
			return exprNode;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0007D400 File Offset: 0x0007B600
		public override AstNode VisitTermNode(TermNode termNode)
		{
			this._printerFormatter.Append(termNode.UnaryOperator);
			if (termNode.IsBinary && FunctionNode.IsBinaryOperator(termNode.UnaryOperator))
			{
				this._printerFormatter.Append(" ");
			}
			if (!string.IsNullOrWhiteSpace(termNode.NumberBasedValue))
			{
				this._printerFormatter.Append(termNode.NumberBasedValue);
			}
			else if (!string.IsNullOrWhiteSpace(termNode.StringBasedValue))
			{
				this._printerFormatter.Append(termNode.StringBasedValue);
			}
			else if (!string.IsNullOrWhiteSpace(termNode.ReplacementTokenBasedValue))
			{
				this._printerFormatter.Append(termNode.ReplacementTokenBasedValue);
			}
			else if (!string.IsNullOrWhiteSpace(termNode.Hexcolor))
			{
				this._printerFormatter.Append(termNode.Hexcolor);
			}
			else if (termNode.FunctionNode != null)
			{
				termNode.FunctionNode.Accept(this);
			}
			foreach (ImportantCommentNode importantCommentNode in termNode.ImportantComments)
			{
				importantCommentNode.Accept(this);
			}
			return termNode;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0007D51C File Offset: 0x0007B71C
		public override AstNode VisitImportantCommentNode(ImportantCommentNode commentNode)
		{
			this._printerFormatter.Append(commentNode.Text);
			return base.VisitImportantCommentNode(commentNode);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0007D536 File Offset: 0x0007B736
		public override AstNode VisitTermWithOperatorNode(TermWithOperatorNode termWithOperatorNode)
		{
			this._printerFormatter.Append(termWithOperatorNode.Operator);
			termWithOperatorNode.TermNode.Accept(this);
			return termWithOperatorNode;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0007D574 File Offset: 0x0007B774
		public override AstNode VisitMediaNode(MediaNode mediaNode)
		{
			this._printerFormatter.Append("@media ");
			mediaNode.MediaQueries.ForEach(delegate(MediaQueryNode mediaQuery, bool last)
			{
				mediaQuery.Accept(this);
				if (!last)
				{
					this._printerFormatter.Append(',');
				}
			});
			this._printerFormatter.AppendLine();
			this._printerFormatter.AppendLine('{');
			this._printerFormatter.IncrementIndentLevel();
			foreach (RulesetNode rulesetNode in mediaNode.Rulesets)
			{
				rulesetNode.Accept(this);
			}
			foreach (PageNode pageNode in mediaNode.PageNodes)
			{
				this._printerFormatter.WriteIndent();
				pageNode.Accept(this);
			}
			this._printerFormatter.DecrementIndentLevel();
			this._printerFormatter.AppendLine('}');
			return mediaNode;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0007D698 File Offset: 0x0007B898
		public override AstNode VisitPageNode(PageNode pageNode)
		{
			this._printerFormatter.Append("@page");
			if (!string.IsNullOrWhiteSpace(pageNode.PseudoPage))
			{
				if (!pageNode.PseudoPage.StartsWith(':'.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))
				{
					this._printerFormatter.Append(' ');
				}
				this._printerFormatter.Append(pageNode.PseudoPage);
			}
			this._printerFormatter.AppendLine();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.AppendLine('{');
			this._printerFormatter.IncrementIndentLevel();
			pageNode.Declarations.ForEach(delegate(DeclarationNode declaration, bool last)
			{
				AstNode astNode = declaration.Accept(this);
				if (!last && astNode != null)
				{
					this._printerFormatter.AppendLine(';');
				}
			});
			this._printerFormatter.AppendLine();
			this._printerFormatter.DecrementIndentLevel();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.AppendLine('}');
			return pageNode;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0007D774 File Offset: 0x0007B974
		public override AstNode VisitDocumentQueryNode(DocumentQueryNode documentQueryNode)
		{
			this._printerFormatter.Append(documentQueryNode.DocumentSymbol);
			this._printerFormatter.Append(' ');
			this._printerFormatter.Append(documentQueryNode.MatchFunctionName);
			this._printerFormatter.AppendLine();
			this._printerFormatter.AppendLine('{');
			this._printerFormatter.IncrementIndentLevel();
			foreach (RulesetNode rulesetNode in documentQueryNode.Rulesets)
			{
				rulesetNode.Accept(this);
			}
			this._printerFormatter.DecrementIndentLevel();
			this._printerFormatter.AppendLine('}');
			return documentQueryNode;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0007D830 File Offset: 0x0007BA30
		public override AstNode VisitCombinatorSimpleSelectorSequenceNode(CombinatorSimpleSelectorSequenceNode combinatorSimpleSelectorSequenceNode)
		{
			switch (combinatorSimpleSelectorSequenceNode.Combinator)
			{
			case Combinator.PlusSign:
				this._printerFormatter.Append("+");
				break;
			case Combinator.GreaterThanSign:
				this._printerFormatter.Append(">");
				break;
			case Combinator.Tilde:
				this._printerFormatter.Append("~");
				break;
			case Combinator.SingleSpace:
				this._printerFormatter.Append(' ');
				break;
			}
			combinatorSimpleSelectorSequenceNode.SimpleSelectorSequenceNode.Accept(this);
			return combinatorSimpleSelectorSequenceNode;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0007D8B4 File Offset: 0x0007BAB4
		public override AstNode VisitFunctionNode(FunctionNode functionNode)
		{
			if (functionNode.FunctionName == "rgb")
			{
				string text = functionNode.ExprNode.MinifyPrint();
				if (text.StartsWith('#'.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))
				{
					this._printerFormatter.Append(text);
				}
			}
			this._printerFormatter.Append(functionNode.FunctionName);
			this._printerFormatter.Append('(');
			if (functionNode.ExprNode != null)
			{
				functionNode.ExprNode.Accept(this);
			}
			this._printerFormatter.Append(')');
			return functionNode;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0007D944 File Offset: 0x0007BB44
		public override AstNode VisitFunctionalPseudoNode(FunctionalPseudoNode functionalPseudoNode)
		{
			this._printerFormatter.Append(functionalPseudoNode.FunctionName);
			this._printerFormatter.Append('(');
			functionalPseudoNode.SelectorExpressionNode.Accept(this);
			this._printerFormatter.Append(')');
			return functionalPseudoNode;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0007D980 File Offset: 0x0007BB80
		public override AstNode VisitSelectorExpressionNode(SelectorExpressionNode selectorExpressionNode)
		{
			foreach (string content in selectorExpressionNode.SelectorExpressions)
			{
				this._printerFormatter.Append(content);
			}
			return selectorExpressionNode;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0007DA40 File Offset: 0x0007BC40
		public override AstNode VisitMediaQueryNode(MediaQueryNode mediaQueryNode)
		{
			if (!string.IsNullOrWhiteSpace(mediaQueryNode.OnlyText))
			{
				this._printerFormatter.Append(mediaQueryNode.OnlyText);
				this._printerFormatter.Append(' ');
			}
			else if (!string.IsNullOrWhiteSpace(mediaQueryNode.NotText))
			{
				this._printerFormatter.Append(mediaQueryNode.NotText);
				this._printerFormatter.Append(' ');
			}
			if (!string.IsNullOrWhiteSpace(mediaQueryNode.MediaType))
			{
				this._printerFormatter.Append(mediaQueryNode.MediaType);
				if (mediaQueryNode.MediaExpressions.Count > 0)
				{
					mediaQueryNode.MediaExpressions.ForEach(delegate(MediaExpressionNode mediaExpression)
					{
						this._printerFormatter.Append(' ');
						this._printerFormatter.Append("and");
						this._printerFormatter.Append(' ');
						mediaExpression.Accept(this);
					});
				}
			}
			else
			{
				mediaQueryNode.MediaExpressions.ForEach(delegate(MediaExpressionNode mediaExpression, bool last)
				{
					mediaExpression.Accept(this);
					if (!last)
					{
						this._printerFormatter.Append(' ');
						this._printerFormatter.Append("and");
						this._printerFormatter.Append(' ');
					}
				});
			}
			return mediaQueryNode;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0007DB10 File Offset: 0x0007BD10
		public override AstNode VisitMediaExpressionNode(MediaExpressionNode mediaExpressionNode)
		{
			this._printerFormatter.Append('(');
			this._printerFormatter.Append(mediaExpressionNode.MediaFeature);
			if (mediaExpressionNode.ExprNode != null)
			{
				this._printerFormatter.Append(':');
				mediaExpressionNode.ExprNode.Accept(this);
			}
			this._printerFormatter.Append(')');
			return mediaExpressionNode;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0007DB78 File Offset: 0x0007BD78
		public override AstNode VisitKeyFramesNode(KeyFramesNode keyFramesNode)
		{
			this._printerFormatter.Append(keyFramesNode.KeyFramesSymbol);
			this._printerFormatter.Append(' ');
			if (!string.IsNullOrWhiteSpace(keyFramesNode.IdentValue))
			{
				this._printerFormatter.Append(keyFramesNode.IdentValue);
			}
			else if (!string.IsNullOrWhiteSpace(keyFramesNode.StringValue))
			{
				this._printerFormatter.Append(keyFramesNode.StringValue);
			}
			this._printerFormatter.AppendLine();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.Append('{');
			keyFramesNode.KeyFramesBlockNodes.ForEach(delegate(KeyFramesBlockNode keyFramesBlockNode)
			{
				keyFramesBlockNode.Accept(this);
			});
			this._printerFormatter.AppendLine();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.AppendLine('}');
			return keyFramesNode;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0007DC68 File Offset: 0x0007BE68
		public override AstNode VisitKeyFramesBlockNode(KeyFramesBlockNode keyFramesBlockNode)
		{
			this._printerFormatter.AppendLine();
			this._printerFormatter.IncrementIndentLevel();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.Append(string.Join(','.ToString(), keyFramesBlockNode.KeyFramesSelectors));
			this._printerFormatter.AppendLine();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.Append('{');
			this._printerFormatter.AppendLine();
			this._printerFormatter.IncrementIndentLevel();
			keyFramesBlockNode.DeclarationNodes.ForEach(delegate(DeclarationNode declarationNode, bool last)
			{
				AstNode astNode = declarationNode.Accept(this);
				if (!last && astNode != null)
				{
					this._printerFormatter.AppendLine(';');
				}
			});
			this._printerFormatter.AppendLine();
			this._printerFormatter.DecrementIndentLevel();
			this._printerFormatter.WriteIndent();
			this._printerFormatter.Append('}');
			this._printerFormatter.DecrementIndentLevel();
			return keyFramesBlockNode;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0007DD40 File Offset: 0x0007BF40
		internal string Print(bool prettyPrint, AstNode node)
		{
			this._printerFormatter.PrettyPrint = prettyPrint;
			this._printerFormatter.IndentCharacter = PrintVisitor.IndentCharacter;
			this._printerFormatter.IndentSize = PrintVisitor.IndentSize;
			if (node != null)
			{
				node.Accept(this);
			}
			return this._printerFormatter.ToString();
		}

		// Token: 0x04000B7A RID: 2938
		private readonly PrinterFormatter _printerFormatter = new PrinterFormatter();
	}
}
