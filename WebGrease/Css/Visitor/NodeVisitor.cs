using System;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.Animation;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;

namespace WebGrease.Css.Visitor
{
	// Token: 0x0200002D RID: 45
	public abstract class NodeVisitor
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00007799 File Offset: 0x00005999
		public virtual AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			return styleSheet;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000779C File Offset: 0x0000599C
		public virtual AstNode VisitImportNode(ImportNode importNode)
		{
			return importNode;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000779F File Offset: 0x0000599F
		public virtual AstNode VisitRulesetNode(RulesetNode rulesetNode)
		{
			return rulesetNode;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000077A2 File Offset: 0x000059A2
		public virtual AstNode VisitMediaNode(MediaNode mediaNode)
		{
			return mediaNode;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000077A5 File Offset: 0x000059A5
		public virtual AstNode VisitPageNode(PageNode pageNode)
		{
			return pageNode;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000077A8 File Offset: 0x000059A8
		public virtual AstNode VisitAttribNode(AttribNode attrib)
		{
			return attrib;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000077AB File Offset: 0x000059AB
		public virtual AstNode VisitAttribOperatorAndValueNode(AttribOperatorAndValueNode attribOperatorAndValueNode)
		{
			return attribOperatorAndValueNode;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000077AE File Offset: 0x000059AE
		public virtual AstNode VisitDeclarationNode(DeclarationNode declarationNode)
		{
			return declarationNode;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000077B1 File Offset: 0x000059B1
		public virtual AstNode VisitExprNode(ExprNode exprNode)
		{
			return exprNode;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000077B4 File Offset: 0x000059B4
		public virtual AstNode VisitFunctionNode(FunctionNode functionNode)
		{
			return functionNode;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000077B7 File Offset: 0x000059B7
		public virtual AstNode VisitPseudoNode(PseudoNode pseudoNode)
		{
			return pseudoNode;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000077BA File Offset: 0x000059BA
		public virtual AstNode VisitSelectorNode(SelectorNode selectorNode)
		{
			return selectorNode;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000077BD File Offset: 0x000059BD
		public virtual AstNode VisitTermNode(TermNode termNode)
		{
			return termNode;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000077C0 File Offset: 0x000059C0
		public virtual AstNode VisitTermWithOperatorNode(TermWithOperatorNode termWithOperatorNode)
		{
			return termWithOperatorNode;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000077C3 File Offset: 0x000059C3
		public virtual AstNode VisitFunctionalPseudoNode(FunctionalPseudoNode functionalPseudoNode)
		{
			return functionalPseudoNode;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000077C6 File Offset: 0x000059C6
		public virtual AstNode VisitHashClassAtNameAttribPseudoNegationNode(HashClassAtNameAttribPseudoNegationNode hashClassAtNameAttribPseudoNegationNode)
		{
			return hashClassAtNameAttribPseudoNegationNode;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000077C9 File Offset: 0x000059C9
		public virtual AstNode VisitSelectorNamespacePrefixNode(SelectorNamespacePrefixNode selectorNamespacePrefixNode)
		{
			return selectorNamespacePrefixNode;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000077CC File Offset: 0x000059CC
		public virtual AstNode VisitNegationArgNode(NegationArgNode negationArgNode)
		{
			return negationArgNode;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000077CF File Offset: 0x000059CF
		public virtual AstNode VisitNegationNode(NegationNode negationNode)
		{
			return negationNode;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000077D2 File Offset: 0x000059D2
		public virtual AstNode VisitSelectorExpressionNode(SelectorExpressionNode selectorExpressionNode)
		{
			return selectorExpressionNode;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000077D5 File Offset: 0x000059D5
		public virtual AstNode VisitSelectorsGroupNode(SelectorsGroupNode selectorsGroupNode)
		{
			return selectorsGroupNode;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000077D8 File Offset: 0x000059D8
		public virtual AstNode VisitSimpleSelectorSequenceNode(SimpleSelectorSequenceNode simpleSelectorSequenceNode)
		{
			return simpleSelectorSequenceNode;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000077DB File Offset: 0x000059DB
		public virtual AstNode VisitTypeSelectorNode(TypeSelectorNode typeSelectorNode)
		{
			return typeSelectorNode;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000077DE File Offset: 0x000059DE
		public virtual AstNode VisitUniversalSelectorNode(UniversalSelectorNode universalSelectorNode)
		{
			return universalSelectorNode;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000077E1 File Offset: 0x000059E1
		public virtual AstNode VisitCombinatorSimpleSelectorSequenceNode(CombinatorSimpleSelectorSequenceNode combinatorSimpleSelectorSequenceNode)
		{
			return combinatorSimpleSelectorSequenceNode;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000077E4 File Offset: 0x000059E4
		public virtual AstNode VisitNamespaceNode(NamespaceNode namespaceNode)
		{
			return namespaceNode;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000077E7 File Offset: 0x000059E7
		public virtual AstNode VisitMediaQueryNode(MediaQueryNode mediaQueryNode)
		{
			return mediaQueryNode;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000077EA File Offset: 0x000059EA
		public virtual AstNode VisitMediaExpressionNode(MediaExpressionNode mediaExpressionNode)
		{
			return mediaExpressionNode;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000077ED File Offset: 0x000059ED
		public virtual AstNode VisitKeyFramesNode(KeyFramesNode keyFramesNode)
		{
			return keyFramesNode;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000077F0 File Offset: 0x000059F0
		public virtual AstNode VisitKeyFramesBlockNode(KeyFramesBlockNode keyFramesBlockNode)
		{
			return keyFramesBlockNode;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000077F3 File Offset: 0x000059F3
		public virtual AstNode VisitDocumentQueryNode(DocumentQueryNode documentQueryNode)
		{
			return documentQueryNode;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000077F6 File Offset: 0x000059F6
		public virtual AstNode VisitImportantCommentNode(ImportantCommentNode commentNode)
		{
			return commentNode;
		}
	}
}
