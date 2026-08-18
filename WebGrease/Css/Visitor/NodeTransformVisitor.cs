using System;
using System.Linq;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.Animation;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x0200002E RID: 46
	public class NodeTransformVisitor : NodeVisitor
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00007810 File Offset: 0x00005A10
		public override AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			if (styleSheet == null)
			{
				throw new ArgumentNullException("styleSheet");
			}
			return new StyleSheetNode(styleSheet.CharSetString, styleSheet.Imports, styleSheet.Namespaces, (from styleSheetRule in styleSheet.StyleSheetRules
			select (StyleSheetRuleNode)styleSheetRule.Accept(this)).ToSafeReadOnlyCollection<StyleSheetRuleNode>());
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000786C File Offset: 0x00005A6C
		public override AstNode VisitImportNode(ImportNode importNode)
		{
			return new ImportNode(importNode.AllowedImportDataType, importNode.ImportDataValue, (from mediaQueryNode in importNode.MediaQueries
			select (MediaQueryNode)mediaQueryNode.Accept(this)).ToSafeReadOnlyCollection<MediaQueryNode>());
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000078A9 File Offset: 0x00005AA9
		public override AstNode VisitRulesetNode(RulesetNode rulesetNode)
		{
			return new RulesetNode(rulesetNode.SelectorsGroupNode.Accept(this) as SelectorsGroupNode, (from declarationNode in rulesetNode.Declarations
			select (DeclarationNode)declarationNode.Accept(this)).ToSafeReadOnlyCollection<DeclarationNode>(), rulesetNode.ImportantComments);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00007910 File Offset: 0x00005B10
		public override AstNode VisitMediaNode(MediaNode mediaNode)
		{
			return new MediaNode((from ruleset in mediaNode.MediaQueries
			select (MediaQueryNode)ruleset.Accept(this)).ToSafeReadOnlyCollection<MediaQueryNode>(), (from ruleset in mediaNode.Rulesets
			select (RulesetNode)ruleset.Accept(this)).ToSafeReadOnlyCollection<RulesetNode>(), (from pages in mediaNode.PageNodes
			select (PageNode)pages.Accept(this)).ToSafeReadOnlyCollection<PageNode>());
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00007984 File Offset: 0x00005B84
		public override AstNode VisitPageNode(PageNode pageNode)
		{
			return new PageNode(pageNode.PseudoPage, (from declaration in pageNode.Declarations
			select (DeclarationNode)declaration.Accept(this)).ToSafeReadOnlyCollection<DeclarationNode>());
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000079BB File Offset: 0x00005BBB
		public override AstNode VisitDocumentQueryNode(DocumentQueryNode documentQueryNode)
		{
			return new DocumentQueryNode(documentQueryNode.MatchFunctionName, documentQueryNode.DocumentSymbol, (from ruleset in documentQueryNode.Rulesets
			select (RulesetNode)ruleset.Accept(this)).ToSafeReadOnlyCollection<RulesetNode>());
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000079EA File Offset: 0x00005BEA
		public override AstNode VisitAttribNode(AttribNode attrib)
		{
			return new AttribNode((attrib.SelectorNamespacePrefixNode != null) ? ((SelectorNamespacePrefixNode)attrib.SelectorNamespacePrefixNode.Accept(this)) : null, attrib.Ident, (AttribOperatorAndValueNode)attrib.OperatorAndValueNode.Accept(this));
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00007A24 File Offset: 0x00005C24
		public override AstNode VisitAttribOperatorAndValueNode(AttribOperatorAndValueNode attribOperatorAndValueNode)
		{
			return new AttribOperatorAndValueNode(attribOperatorAndValueNode.AttribOperatorKind, attribOperatorAndValueNode.IdentOrString);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00007A37 File Offset: 0x00005C37
		public override AstNode VisitDeclarationNode(DeclarationNode declarationNode)
		{
			return new DeclarationNode(declarationNode.Property, (ExprNode)declarationNode.ExprNode.Accept(this), declarationNode.Prio, declarationNode.ImportantComments);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00007A6F File Offset: 0x00005C6F
		public override AstNode VisitExprNode(ExprNode exprNode)
		{
			return new ExprNode((TermNode)exprNode.TermNode.Accept(this), (from termWithOperatorNode in exprNode.TermsWithOperators
			select (TermWithOperatorNode)termWithOperatorNode.Accept(this)).ToSafeReadOnlyCollection<TermWithOperatorNode>(), exprNode.ImportantComments);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00007AAC File Offset: 0x00005CAC
		public override AstNode VisitFunctionNode(FunctionNode functionNode)
		{
			AstNode astNode = (functionNode.ExprNode != null) ? functionNode.ExprNode.Accept(this) : null;
			return new FunctionNode(functionNode.FunctionName, (ExprNode)astNode);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00007AE2 File Offset: 0x00005CE2
		public override AstNode VisitPseudoNode(PseudoNode pseudoNode)
		{
			return new PseudoNode(pseudoNode.NumberOfColons, pseudoNode.Ident, (pseudoNode.FunctionalPseudoNode != null) ? ((FunctionalPseudoNode)pseudoNode.FunctionalPseudoNode.Accept(this)) : null);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00007B1F File Offset: 0x00005D1F
		public override AstNode VisitSelectorNode(SelectorNode selectorNode)
		{
			return new SelectorNode((SimpleSelectorSequenceNode)selectorNode.SimpleSelectorSequenceNode.Accept(this), (from combinatorSimpleSelectorSequenceNode in selectorNode.CombinatorSimpleSelectorSequenceNodes
			select (CombinatorSimpleSelectorSequenceNode)combinatorSimpleSelectorSequenceNode.Accept(this)).ToSafeReadOnlyCollection<CombinatorSimpleSelectorSequenceNode>());
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00007B5C File Offset: 0x00005D5C
		public override AstNode VisitTermNode(TermNode termNode)
		{
			return new TermNode(termNode.UnaryOperator, termNode.NumberBasedValue, termNode.StringBasedValue, termNode.Hexcolor, (FunctionNode)termNode.FunctionNode.NullSafeAction((FunctionNode nsa) => nsa.Accept(this)), termNode.ImportantComments, termNode.ReplacementTokenBasedValue);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00007BAE File Offset: 0x00005DAE
		public override AstNode VisitTermWithOperatorNode(TermWithOperatorNode termWithOperatorNode)
		{
			return new TermWithOperatorNode(termWithOperatorNode.Operator, (TermNode)termWithOperatorNode.TermNode.Accept(this));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00007BCC File Offset: 0x00005DCC
		public override AstNode VisitFunctionalPseudoNode(FunctionalPseudoNode functionalPseudoNode)
		{
			return new FunctionalPseudoNode(functionalPseudoNode.FunctionName, (SelectorExpressionNode)functionalPseudoNode.SelectorExpressionNode.Accept(this));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00007BEC File Offset: 0x00005DEC
		public override AstNode VisitHashClassAtNameAttribPseudoNegationNode(HashClassAtNameAttribPseudoNegationNode hashClassAtNameAttribPseudoNegationNode)
		{
			return new HashClassAtNameAttribPseudoNegationNode(hashClassAtNameAttribPseudoNegationNode.Hash, hashClassAtNameAttribPseudoNegationNode.CssClass, hashClassAtNameAttribPseudoNegationNode.ReplacementToken, hashClassAtNameAttribPseudoNegationNode.AtName, (hashClassAtNameAttribPseudoNegationNode.AttribNode != null) ? ((AttribNode)hashClassAtNameAttribPseudoNegationNode.AttribNode.Accept(this)) : null, (hashClassAtNameAttribPseudoNegationNode.PseudoNode != null) ? ((PseudoNode)hashClassAtNameAttribPseudoNegationNode.PseudoNode.Accept(this)) : null, (hashClassAtNameAttribPseudoNegationNode.NegationNode != null) ? ((NegationNode)hashClassAtNameAttribPseudoNegationNode.NegationNode.Accept(this)) : null);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00007C6A File Offset: 0x00005E6A
		public override AstNode VisitSelectorNamespacePrefixNode(SelectorNamespacePrefixNode selectorNamespacePrefixNode)
		{
			return new SelectorNamespacePrefixNode(selectorNamespacePrefixNode.Prefix);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00007C78 File Offset: 0x00005E78
		public override AstNode VisitNegationArgNode(NegationArgNode negationArgNode)
		{
			return new NegationArgNode((negationArgNode.TypeSelectorNode != null) ? ((TypeSelectorNode)negationArgNode.TypeSelectorNode.Accept(this)) : null, (negationArgNode.UniversalSelectorNode != null) ? ((UniversalSelectorNode)negationArgNode.UniversalSelectorNode.Accept(this)) : null, negationArgNode.Hash, negationArgNode.CssClass, (negationArgNode.AttribNode != null) ? ((AttribNode)negationArgNode.AttribNode.Accept(this)) : null, (negationArgNode.PseudoNode != null) ? ((PseudoNode)negationArgNode.PseudoNode.Accept(this)) : null);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00007D06 File Offset: 0x00005F06
		public override AstNode VisitNegationNode(NegationNode negationNode)
		{
			return new NegationNode((NegationArgNode)negationNode.NegationArgNode.Accept(this));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00007D1E File Offset: 0x00005F1E
		public override AstNode VisitSelectorExpressionNode(SelectorExpressionNode selectorExpressionNode)
		{
			return new SelectorExpressionNode(selectorExpressionNode.SelectorExpressions);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00007D39 File Offset: 0x00005F39
		public override AstNode VisitSelectorsGroupNode(SelectorsGroupNode selectorsGroupNode)
		{
			return new SelectorsGroupNode((from selectorNode in selectorsGroupNode.SelectorNodes
			select (SelectorNode)selectorNode.Accept(this)).ToSafeReadOnlyCollection<SelectorNode>());
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00007D6C File Offset: 0x00005F6C
		public override AstNode VisitSimpleSelectorSequenceNode(SimpleSelectorSequenceNode simpleSelectorSequenceNode)
		{
			return new SimpleSelectorSequenceNode((simpleSelectorSequenceNode.TypeSelectorNode != null) ? ((TypeSelectorNode)simpleSelectorSequenceNode.TypeSelectorNode.Accept(this)) : null, (simpleSelectorSequenceNode.UniversalSelectorNode != null) ? ((UniversalSelectorNode)simpleSelectorSequenceNode.UniversalSelectorNode.Accept(this)) : null, simpleSelectorSequenceNode.Separator, (from hashClassAtNameAttribPseudoNegationNode in simpleSelectorSequenceNode.HashClassAttribPseudoNegationNodes
			select (HashClassAtNameAttribPseudoNegationNode)hashClassAtNameAttribPseudoNegationNode.Accept(this)).ToSafeReadOnlyCollection<HashClassAtNameAttribPseudoNegationNode>());
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public override AstNode VisitTypeSelectorNode(TypeSelectorNode typeSelectorNode)
		{
			return new TypeSelectorNode((typeSelectorNode.SelectorNamespacePrefixNode != null) ? ((SelectorNamespacePrefixNode)typeSelectorNode.SelectorNamespacePrefixNode.Accept(this)) : null, typeSelectorNode.ElementName);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00007E01 File Offset: 0x00006001
		public override AstNode VisitUniversalSelectorNode(UniversalSelectorNode universalSelectorNode)
		{
			return new UniversalSelectorNode((universalSelectorNode.SelectorNamespacePrefixNode != null) ? ((SelectorNamespacePrefixNode)universalSelectorNode.SelectorNamespacePrefixNode.Accept(this)) : null);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00007E24 File Offset: 0x00006024
		public override AstNode VisitCombinatorSimpleSelectorSequenceNode(CombinatorSimpleSelectorSequenceNode combinatorSimpleSelectorSequenceNode)
		{
			return new CombinatorSimpleSelectorSequenceNode(combinatorSimpleSelectorSequenceNode.Combinator, (SimpleSelectorSequenceNode)combinatorSimpleSelectorSequenceNode.SimpleSelectorSequenceNode.Accept(this));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00007E42 File Offset: 0x00006042
		public override AstNode VisitNamespaceNode(NamespaceNode namespaceNode)
		{
			return new NamespaceNode(namespaceNode.Prefix, namespaceNode.Value);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00007E63 File Offset: 0x00006063
		public override AstNode VisitMediaQueryNode(MediaQueryNode mediaQueryNode)
		{
			return new MediaQueryNode(mediaQueryNode.OnlyText, mediaQueryNode.NotText, mediaQueryNode.MediaType, (from mediaExpressionNode in mediaQueryNode.MediaExpressions
			select (MediaExpressionNode)mediaExpressionNode.Accept(this)).ToSafeReadOnlyCollection<MediaExpressionNode>());
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00007E98 File Offset: 0x00006098
		public override AstNode VisitMediaExpressionNode(MediaExpressionNode mediaExpressionNode)
		{
			return new MediaExpressionNode(mediaExpressionNode.MediaFeature, (mediaExpressionNode.ExprNode != null) ? ((ExprNode)mediaExpressionNode.ExprNode.Accept(this)) : null);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00007ECF File Offset: 0x000060CF
		public override AstNode VisitKeyFramesNode(KeyFramesNode keyFramesNode)
		{
			return new KeyFramesNode(keyFramesNode.KeyFramesSymbol, keyFramesNode.IdentValue, keyFramesNode.StringValue, (from keyFramesBlockNode in keyFramesNode.KeyFramesBlockNodes
			select (KeyFramesBlockNode)keyFramesBlockNode.Accept(this)).ToSafeReadOnlyCollection<KeyFramesBlockNode>());
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00007F12 File Offset: 0x00006112
		public override AstNode VisitKeyFramesBlockNode(KeyFramesBlockNode keyFramesBlockNode)
		{
			return new KeyFramesBlockNode(keyFramesBlockNode.KeyFramesSelectors, (from declarationNode in keyFramesBlockNode.DeclarationNodes
			select (DeclarationNode)declarationNode.Accept(this)).ToSafeReadOnlyCollection<DeclarationNode>());
		}
	}
}
