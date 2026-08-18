using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr.Runtime.Tree;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.Animation;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Extensions;

namespace WebGrease.Css
{
	// Token: 0x02000136 RID: 310
	internal static class CommonTreeTransformer
	{
		// Token: 0x06001223 RID: 4643 RVA: 0x0004D37C File Offset: 0x0004B57C
		internal static StyleSheetNode CreateStyleSheetNode(CommonTree commonTree)
		{
			CommonTree styleSheetTree = commonTree.Children(CommonTreeTransformer.T(180)).FirstOrDefault<CommonTree>();
			return new StyleSheetNode(CommonTreeTransformer.CreateCharsetNode(styleSheetTree), CommonTreeTransformer.CreateImportNodes(styleSheetTree), CommonTreeTransformer.CreateNamespaceNodes(styleSheetTree), CommonTreeTransformer.CreateStyleSheetRulesNodes(styleSheetTree));
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0004D3BC File Offset: 0x0004B5BC
		private static string CreateCharsetNode(CommonTree styleSheetTree)
		{
			if (styleSheetTree == null)
			{
				return null;
			}
			CommonTree commonTree = styleSheetTree.Children(CommonTreeTransformer.T(116)).FirstOrDefault<CommonTree>();
			if (commonTree == null)
			{
				return null;
			}
			return CommonTreeTransformer.StringOrUriBasedValue(commonTree.Children(CommonTreeTransformer.T(179)).FirstChildText());
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004D400 File Offset: 0x0004B600
		private static ReadOnlyCollection<StyleSheetRuleNode> CreateStyleSheetRulesNodes(CommonTree styleSheetTree)
		{
			if (styleSheetTree == null)
			{
				return Enumerable.Empty<StyleSheetRuleNode>().ToSafeReadOnlyCollection<StyleSheetRuleNode>();
			}
			List<StyleSheetRuleNode> list = new List<StyleSheetRuleNode>();
			foreach (CommonTree commonTree in styleSheetTree.Children(null))
			{
				if (commonTree.Text == CommonTreeTransformer.T(171))
				{
					list.Add(CommonTreeTransformer.CreateRulesetNode(commonTree));
				}
				else if (commonTree.Text == CommonTreeTransformer.T(147))
				{
					list.Add(CommonTreeTransformer.CreateMediaNode(commonTree));
				}
				else if (commonTree.Text == CommonTreeTransformer.T(163))
				{
					list.Add(CommonTreeTransformer.CreatePageNode(commonTree));
				}
				else if (commonTree.Text == CommonTreeTransformer.T(141))
				{
					list.Add(CommonTreeTransformer.CreateKeyFramesNode(commonTree));
				}
				else if (commonTree.Text == CommonTreeTransformer.T(124))
				{
					list.Add(CommonTreeTransformer.CreateDocumentQueryNode(commonTree));
				}
				else if (commonTree.Type.Equals(42))
				{
					list.Add(new StyleSheetRuleOrCommentNode(new ImportantCommentNode(commonTree.Text), true));
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0004D5E8 File Offset: 0x0004B7E8
		private static ReadOnlyCollection<ImportNode> CreateImportNodes(CommonTree styleSheetTree)
		{
			if (styleSheetTree == null)
			{
				return Enumerable.Empty<ImportNode>().ToSafeReadOnlyCollection<ImportNode>();
			}
			return styleSheetTree.GrandChildren(CommonTreeTransformer.T(140)).Select(delegate(CommonTree import)
			{
				CommonTree commonTree = import.Children(null).FirstOrDefault<CommonTree>();
				if (commonTree != null)
				{
					AllowedImportData allowedImportDataType = AllowedImportData.None;
					string importDataValue = null;
					if (commonTree.Text == CommonTreeTransformer.T(179))
					{
						allowedImportDataType = AllowedImportData.String;
						importDataValue = CommonTreeTransformer.StringOrUriBasedValue(commonTree.FirstChildText());
					}
					else if (commonTree.Text == CommonTreeTransformer.T(187))
					{
						allowedImportDataType = AllowedImportData.Uri;
						importDataValue = CommonTreeTransformer.StringOrUriBasedValue(commonTree.FirstChildText());
					}
					return new ImportNode(allowedImportDataType, importDataValue, import.GrandChildren(CommonTreeTransformer.T(152)).Select(new Func<CommonTree, MediaQueryNode>(CommonTreeTransformer.CreateMediaQueryNode)).ToSafeReadOnlyCollection<MediaQueryNode>());
				}
				return null;
			}).ToSafeReadOnlyCollection<ImportNode>();
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0004D63C File Offset: 0x0004B83C
		private static MediaQueryNode CreateMediaQueryNode(CommonTree mediaQueryTree)
		{
			return new MediaQueryNode(mediaQueryTree.Children(CommonTreeTransformer.T(161)).FirstChildText(), mediaQueryTree.Children(CommonTreeTransformer.T(159)).FirstChildText(), mediaQueryTree.Children(CommonTreeTransformer.T(153)).FirstChildText(), mediaQueryTree.GrandChildren(CommonTreeTransformer.T(149)).Select(new Func<CommonTree, MediaExpressionNode>(CommonTreeTransformer.CreateMediaExpressionNode)).ToSafeReadOnlyCollection<MediaExpressionNode>());
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0004D6B3 File Offset: 0x0004B8B3
		private static MediaExpressionNode CreateMediaExpressionNode(CommonTree mediaExpressionTree)
		{
			return new MediaExpressionNode(mediaExpressionTree.Children(CommonTreeTransformer.T(150)).FirstChildText(), CommonTreeTransformer.CreateExpressionNode(mediaExpressionTree.Children(CommonTreeTransformer.T(128)).FirstOrDefault<CommonTree>()));
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0004D754 File Offset: 0x0004B954
		private static ReadOnlyCollection<NamespaceNode> CreateNamespaceNodes(CommonTree styleSheetTree)
		{
			if (styleSheetTree == null)
			{
				return Enumerable.Empty<NamespaceNode>().ToSafeReadOnlyCollection<NamespaceNode>();
			}
			return styleSheetTree.GrandChildren(CommonTreeTransformer.T(155)).Select(delegate(CommonTree ns)
			{
				string value = CommonTreeTransformer.StringOrUriBasedValue(ns.Children(CommonTreeTransformer.T(179)).FirstChildText());
				if (string.IsNullOrWhiteSpace(value))
				{
					value = CommonTreeTransformer.StringOrUriBasedValue(ns.Children(CommonTreeTransformer.T(187)).FirstChildText());
				}
				return new NamespaceNode(ns.Children(CommonTreeTransformer.T(156)).FirstChildText(), value);
			}).ToSafeReadOnlyCollection<NamespaceNode>();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004D7A6 File Offset: 0x0004B9A6
		private static RulesetNode CreateRulesetNode(CommonTree rulesetTree)
		{
			if (rulesetTree == null)
			{
				return null;
			}
			return new RulesetNode(CommonTreeTransformer.CreateSelectorsGroupNode(rulesetTree.GrandChildren(CommonTreeTransformer.T(174))), CommonTreeTransformer.CreateDeclarationNodes(rulesetTree.GrandChildren(CommonTreeTransformer.T(123))).ToSafeReadOnlyCollection<DeclarationNode>(), CommonTreeTransformer.CreateImportantCommentNodes(rulesetTree));
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0004D7E4 File Offset: 0x0004B9E4
		private static ReadOnlyCollection<ImportantCommentNode> CreateImportantCommentNodes(CommonTree commonTree)
		{
			List<ImportantCommentNode> list = new List<ImportantCommentNode>();
			foreach (ITree tree in commonTree.Children)
			{
				if (tree.Type.Equals(42))
				{
					list.Add(new ImportantCommentNode(tree.Text));
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0004D85C File Offset: 0x0004BA5C
		private static MediaNode CreateMediaNode(CommonTree mediaTree)
		{
			if (mediaTree == null)
			{
				return null;
			}
			return new MediaNode(mediaTree.GrandChildren(CommonTreeTransformer.T(152)).Select(new Func<CommonTree, MediaQueryNode>(CommonTreeTransformer.CreateMediaQueryNode)).ToSafeReadOnlyCollection<MediaQueryNode>(), mediaTree.GrandChildren(CommonTreeTransformer.T(172)).Select(new Func<CommonTree, RulesetNode>(CommonTreeTransformer.CreateRulesetNode)).ToSafeReadOnlyCollection<RulesetNode>(), mediaTree.GrandChildren(CommonTreeTransformer.T(163)).Select(new Func<CommonTree, PageNode>(CommonTreeTransformer.CreatePageNode)).ToSafeReadOnlyCollection<PageNode>());
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0004D8F0 File Offset: 0x0004BAF0
		private static PageNode CreatePageNode(CommonTree pageTree)
		{
			if (pageTree == null)
			{
				return null;
			}
			return new PageNode(string.Join(string.Empty, from pseudo in pageTree.GrandChildren(CommonTreeTransformer.T(167))
			select pseudo.Text), CommonTreeTransformer.CreateDeclarationNodes(pageTree.GrandChildren(CommonTreeTransformer.T(123))).ToSafeReadOnlyCollection<DeclarationNode>());
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0004D964 File Offset: 0x0004BB64
		private static DocumentQueryNode CreateDocumentQueryNode(CommonTree documentTree)
		{
			return new DocumentQueryNode(string.Join(string.Empty, from _ in documentTree.GrandChildren(CommonTreeTransformer.T(125))
			select _.Text), documentTree.Children(CommonTreeTransformer.T(126)).FirstChildText(), documentTree.GrandChildren(CommonTreeTransformer.T(172)).Select(new Func<CommonTree, RulesetNode>(CommonTreeTransformer.CreateRulesetNode)).ToSafeReadOnlyCollection<RulesetNode>());
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004D9E8 File Offset: 0x0004BBE8
		private static KeyFramesNode CreateKeyFramesNode(CommonTree styleSheetChild)
		{
			return new KeyFramesNode(styleSheetChild.Children(CommonTreeTransformer.T(146)).FirstChildText(), styleSheetChild.Children(CommonTreeTransformer.T(137)).FirstChildText(), CommonTreeTransformer.StringOrUriBasedValue(styleSheetChild.Children(CommonTreeTransformer.T(179)).FirstChildText()), styleSheetChild.GrandChildren(CommonTreeTransformer.T(143)).Select(new Func<CommonTree, KeyFramesBlockNode>(CommonTreeTransformer.CreateKeyFramesBlockNode)).ToSafeReadOnlyCollection<KeyFramesBlockNode>());
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0004DA6C File Offset: 0x0004BC6C
		private static KeyFramesBlockNode CreateKeyFramesBlockNode(CommonTree keyFramesBlockTree)
		{
			return new KeyFramesBlockNode((from keyFramesSelector in keyFramesBlockTree.GrandChildren(CommonTreeTransformer.T(145))
			select keyFramesSelector.FirstChildText()).ToSafeReadOnlyCollection<string>(), CommonTreeTransformer.CreateDeclarationNodes(keyFramesBlockTree.GrandChildren(CommonTreeTransformer.T(123))).ToSafeReadOnlyCollection<DeclarationNode>());
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004DB57 File Offset: 0x0004BD57
		private static IEnumerable<DeclarationNode> CreateDeclarationNodes(IEnumerable<CommonTree> declarationTreeNodes)
		{
			return from declaration in declarationTreeNodes
			select new DeclarationNode(string.Join(string.Empty, from _ in declaration.GrandChildren(CommonTreeTransformer.T(164))
			select _.Text), CommonTreeTransformer.CreateExpressionNode(declaration.Children(CommonTreeTransformer.T(128)).FirstOrDefault<CommonTree>()), declaration.Children(CommonTreeTransformer.T(139)).FirstChildText(), CommonTreeTransformer.CreateImportantCommentNodes(declaration));
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0004DB7C File Offset: 0x0004BD7C
		private static ExprNode CreateExpressionNode(CommonTree exprTree)
		{
			if (exprTree == null)
			{
				return null;
			}
			return new ExprNode(CommonTreeTransformer.CreateTermNode(exprTree.Children(CommonTreeTransformer.T(181)).FirstOrDefault<CommonTree>()), CommonTreeTransformer.CreateTermWithOperatorsNode(exprTree.GrandChildren(CommonTreeTransformer.T(183))).ToSafeReadOnlyCollection<TermWithOperatorNode>(), CommonTreeTransformer.CreateImportantCommentNodes(exprTree));
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004DC13 File Offset: 0x0004BE13
		private static IEnumerable<TermWithOperatorNode> CreateTermWithOperatorsNode(IEnumerable<CommonTree> termWithOperatorTreeNodes)
		{
			return termWithOperatorTreeNodes.Select(delegate(CommonTree termWithOperatorNode)
			{
				string op = termWithOperatorNode.Children(CommonTreeTransformer.T(162)).FirstChildText();
				return new TermWithOperatorNode(op, CommonTreeTransformer.CreateTermNode(termWithOperatorNode.Children(CommonTreeTransformer.T(181)).FirstOrDefault<CommonTree>()));
			});
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004DC38 File Offset: 0x0004BE38
		private static TermNode CreateTermNode(CommonTree termTree)
		{
			if (termTree == null)
			{
				return null;
			}
			string unaryOperator = termTree.Children(CommonTreeTransformer.T(185)).FirstChildText();
			string numberBasedValue = termTree.Children(CommonTreeTransformer.T(160)).FirstChildText();
			string replacementTokenBasedValue = termTree.Children(CommonTreeTransformer.T(169)).FirstChildText();
			string text = CommonTreeTransformer.StringOrUriBasedValue(termTree.Children(CommonTreeTransformer.T(187)).FirstChildText());
			if (string.IsNullOrWhiteSpace(text))
			{
				text = CommonTreeTransformer.StringOrUriBasedValue(termTree.Children(CommonTreeTransformer.T(179)).FirstChildText());
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				text = CommonTreeTransformer.StringOrUriBasedValue(termTree.Children(CommonTreeTransformer.T(137)).FirstChildText());
			}
			CommonTree commonTree = termTree.Children(CommonTreeTransformer.T(136)).FirstOrDefault<CommonTree>();
			string hexColor = (commonTree != null) ? commonTree.Children(CommonTreeTransformer.T(135)).FirstChildText() : null;
			ReadOnlyCollection<ImportantCommentNode> importantComments = CommonTreeTransformer.CreateImportantCommentNodes(termTree);
			return new TermNode(unaryOperator, numberBasedValue, text, hexColor, CommonTreeTransformer.CreateFunctionNode(termTree.Children(CommonTreeTransformer.T(130)).FirstOrDefault<CommonTree>()), importantComments, replacementTokenBasedValue);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004DD52 File Offset: 0x0004BF52
		private static FunctionNode CreateFunctionNode(CommonTree functionTree)
		{
			if (functionTree == null)
			{
				return null;
			}
			return new FunctionNode(functionTree.Children(CommonTreeTransformer.T(131)).FirstChildText(), CommonTreeTransformer.CreateExpressionNode(functionTree.Children(CommonTreeTransformer.T(128)).FirstOrDefault<CommonTree>()));
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0004DDC5 File Offset: 0x0004BFC5
		private static SelectorsGroupNode CreateSelectorsGroupNode(IEnumerable<CommonTree> selectorTreeNodes)
		{
			return new SelectorsGroupNode((from selector in selectorTreeNodes
			select new SelectorNode(CommonTreeTransformer.CreateSimpleSelectorSequenceNode(selector.Children(CommonTreeTransformer.T(177)).FirstOrDefault<CommonTree>()), CommonTreeTransformer.CreateCombinatorSimpleSelectorSequenceNode(selector.GrandChildren(CommonTreeTransformer.T(121))).ToSafeReadOnlyCollection<CombinatorSimpleSelectorSequenceNode>())).ToSafeReadOnlyCollection<SelectorNode>());
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004DE2C File Offset: 0x0004C02C
		private static IEnumerable<CombinatorSimpleSelectorSequenceNode> CreateCombinatorSimpleSelectorSequenceNode(IEnumerable<CommonTree> combinatorSimpleSelectorSequenceTreeNodes)
		{
			return from combinatorSimpleSelectorSequenceNode in combinatorSimpleSelectorSequenceTreeNodes
			select new CombinatorSimpleSelectorSequenceNode(CommonTreeTransformer.CreateCombinatorNode(combinatorSimpleSelectorSequenceNode.Children(CommonTreeTransformer.T(119)).FirstOrDefault<CommonTree>()), CommonTreeTransformer.CreateSimpleSelectorSequenceNode(combinatorSimpleSelectorSequenceNode.Children(CommonTreeTransformer.T(177)).FirstOrDefault<CommonTree>()));
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0004DE54 File Offset: 0x0004C054
		private static Combinator CreateCombinatorNode(CommonTree combinatorTree)
		{
			Combinator result = Combinator.None;
			if (combinatorTree == null)
			{
				return result;
			}
			string text = combinatorTree.FirstChildText();
			string a;
			if ((a = text) != null)
			{
				if (!(a == "+"))
				{
					if (!(a == ">"))
					{
						if (!(a == "~"))
						{
							if (!(a == "WHITESPACE"))
							{
								goto IL_65;
							}
							result = ((CommonTreeTransformer.GetWhitespaceCount(combinatorTree) > 0) ? Combinator.SingleSpace : Combinator.ZeroSpace);
						}
						else
						{
							result = Combinator.Tilde;
						}
					}
					else
					{
						result = Combinator.GreaterThanSign;
					}
				}
				else
				{
					result = Combinator.PlusSign;
				}
				return result;
			}
			IL_65:
			throw new AstException("Encountered an invalid combinator.");
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004DED4 File Offset: 0x0004C0D4
		private static int GetWhitespaceCount(CommonTree commonTree)
		{
			string s = commonTree.Children(CommonTreeTransformer.T(190)).FirstChildText();
			int result;
			if (!int.TryParse(s, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004DF04 File Offset: 0x0004C104
		private static SimpleSelectorSequenceNode CreateSimpleSelectorSequenceNode(CommonTree simpleSelectorSequenceTree)
		{
			if (simpleSelectorSequenceTree != null)
			{
				return new SimpleSelectorSequenceNode(CommonTreeTransformer.CreateTypeSelectorNode(simpleSelectorSequenceTree.Children(CommonTreeTransformer.T(184)).FirstOrDefault<CommonTree>()), CommonTreeTransformer.CreateUniversalSelectorNode(simpleSelectorSequenceTree.Children(CommonTreeTransformer.T(186)).FirstOrDefault<CommonTree>()), (CommonTreeTransformer.GetWhitespaceCount(simpleSelectorSequenceTree) > 0) ? ' '.ToString() : null, CommonTreeTransformer.CreateHashClassAttribPseudoNegationNodes(simpleSelectorSequenceTree.GrandChildren(CommonTreeTransformer.T(134))).ToSafeReadOnlyCollection<HashClassAtNameAttribPseudoNegationNode>());
			}
			return null;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0004DF7F File Offset: 0x0004C17F
		private static UniversalSelectorNode CreateUniversalSelectorNode(CommonTree universalSelectorTree)
		{
			if (universalSelectorTree != null)
			{
				return new UniversalSelectorNode(CommonTreeTransformer.CreateNamespacePrefixNode(universalSelectorTree.Children(CommonTreeTransformer.T(176)).FirstOrDefault<CommonTree>()));
			}
			return null;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0004DFA5 File Offset: 0x0004C1A5
		private static TypeSelectorNode CreateTypeSelectorNode(CommonTree typeSelectorTree)
		{
			if (typeSelectorTree != null)
			{
				return new TypeSelectorNode(CommonTreeTransformer.CreateNamespacePrefixNode(typeSelectorTree.Children(CommonTreeTransformer.T(176)).FirstOrDefault<CommonTree>()), typeSelectorTree.Children(CommonTreeTransformer.T(127)).FirstChildText());
			}
			return null;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0004DFDD File Offset: 0x0004C1DD
		private static SelectorNamespacePrefixNode CreateNamespacePrefixNode(CommonTree namespacePrefixTree)
		{
			if (namespacePrefixTree != null)
			{
				return new SelectorNamespacePrefixNode(namespacePrefixTree.Children(CommonTreeTransformer.T(127)).FirstChildTextOrDefault(string.Empty));
			}
			return null;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004E10D File Offset: 0x0004C30D
		private static IEnumerable<HashClassAtNameAttribPseudoNegationNode> CreateHashClassAttribPseudoNegationNodes(IEnumerable<CommonTree> hashClassAttribPseudoNegationTreeNodes)
		{
			return hashClassAttribPseudoNegationTreeNodes.Select(delegate(CommonTree hashClassAttribPseudoNegationNode)
			{
				CommonTree commonTree = hashClassAttribPseudoNegationNode.Children(null).FirstOrDefault<CommonTree>();
				string hash = null;
				string replacementToken = null;
				string cssClass = null;
				string atName = null;
				AttribNode attribNode = null;
				PseudoNode pseudoNode = null;
				NegationNode negationNode = null;
				if (commonTree != null)
				{
					string text = commonTree.Text;
					if (text == CommonTreeTransformer.T(135))
					{
						hash = commonTree.FirstChildText();
					}
					else if (text == CommonTreeTransformer.T(117))
					{
						cssClass = commonTree.FirstChildText();
					}
					else if (text == CommonTreeTransformer.T(110))
					{
						atName = commonTree.FirstChildText();
					}
					else if (text == CommonTreeTransformer.T(111))
					{
						attribNode = CommonTreeTransformer.CreateAttribNode(commonTree);
					}
					else if (text == CommonTreeTransformer.T(165))
					{
						pseudoNode = CommonTreeTransformer.CreatePseudoNode(commonTree);
					}
					else if (text == CommonTreeTransformer.T(157))
					{
						negationNode = CommonTreeTransformer.CreateNegationNode(commonTree);
					}
					else if (text == CommonTreeTransformer.T(170))
					{
						replacementToken = commonTree.FirstChildText();
					}
				}
				return new HashClassAtNameAttribPseudoNegationNode(hash, cssClass, replacementToken, atName, attribNode, pseudoNode, negationNode);
			});
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004E132 File Offset: 0x0004C332
		private static NegationNode CreateNegationNode(CommonTree negationTree)
		{
			if (negationTree != null)
			{
				return new NegationNode(CommonTreeTransformer.CreateNegationArgNode(negationTree.Children(CommonTreeTransformer.T(158)).FirstOrDefault<CommonTree>()));
			}
			return null;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004E158 File Offset: 0x0004C358
		private static NegationArgNode CreateNegationArgNode(CommonTree negationArgTree)
		{
			if (negationArgTree != null)
			{
				return new NegationArgNode(CommonTreeTransformer.CreateTypeSelectorNode(negationArgTree.Children(CommonTreeTransformer.T(184)).FirstOrDefault<CommonTree>()), CommonTreeTransformer.CreateUniversalSelectorNode(negationArgTree.Children(CommonTreeTransformer.T(186)).FirstOrDefault<CommonTree>()), negationArgTree.Children(CommonTreeTransformer.T(135)).FirstChildText(), negationArgTree.Children(CommonTreeTransformer.T(117)).FirstChildText(), CommonTreeTransformer.CreateAttribNode(negationArgTree.Children(CommonTreeTransformer.T(111)).FirstOrDefault<CommonTree>()), CommonTreeTransformer.CreatePseudoNode(negationArgTree.Children(CommonTreeTransformer.T(165)).FirstOrDefault<CommonTree>()));
			}
			return null;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004E200 File Offset: 0x0004C400
		private static PseudoNode CreatePseudoNode(CommonTree pseudoTree)
		{
			if (pseudoTree != null)
			{
				return new PseudoNode(pseudoTree.GrandChildren(CommonTreeTransformer.T(118)).Count<CommonTree>(), pseudoTree.Children(CommonTreeTransformer.T(166)).FirstChildText(), CommonTreeTransformer.CreateFunctionalPseudoNode(pseudoTree.Children(CommonTreeTransformer.T(129)).FirstOrDefault<CommonTree>()));
			}
			return null;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004E258 File Offset: 0x0004C458
		private static FunctionalPseudoNode CreateFunctionalPseudoNode(CommonTree functionalPseudoTree)
		{
			if (functionalPseudoTree != null)
			{
				return new FunctionalPseudoNode(functionalPseudoTree.Children(CommonTreeTransformer.T(131)).FirstChildText(), CommonTreeTransformer.CreateSelectorExpressionNode(functionalPseudoTree.Children(CommonTreeTransformer.T(175)).FirstOrDefault<CommonTree>()));
			}
			return null;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004E29C File Offset: 0x0004C49C
		private static SelectorExpressionNode CreateSelectorExpressionNode(CommonTree selectorExpressionTree)
		{
			if (selectorExpressionTree != null)
			{
				return new SelectorExpressionNode((from _ in selectorExpressionTree.Children(null)
				select _.TextOrDefault(null)).ToSafeReadOnlyCollection<string>());
			}
			return null;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004E2D8 File Offset: 0x0004C4D8
		private static AttribNode CreateAttribNode(CommonTree attribTree)
		{
			if (attribTree != null)
			{
				return new AttribNode(CommonTreeTransformer.CreateNamespacePrefixNode(attribTree.Children(CommonTreeTransformer.T(176)).FirstOrDefault<CommonTree>()), attribTree.Children(CommonTreeTransformer.T(112)).FirstChildText(), CommonTreeTransformer.CreateAttribOperatorValueNode(attribTree.Children(CommonTreeTransformer.T(114)).FirstOrDefault<CommonTree>()));
			}
			return null;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004E334 File Offset: 0x0004C534
		private static AttribOperatorAndValueNode CreateAttribOperatorValueNode(CommonTree attribOperatorAndValueTree)
		{
			if (attribOperatorAndValueTree == null)
			{
				return null;
			}
			AttribOperatorKind operatorKind = AttribOperatorKind.None;
			string a;
			if ((a = attribOperatorAndValueTree.Children(CommonTreeTransformer.T(113)).FirstChildText()) != null)
			{
				if (!(a == "^="))
				{
					if (!(a == "$="))
					{
						if (!(a == "*="))
						{
							if (!(a == "="))
							{
								if (!(a == "~="))
								{
									if (a == "|=")
									{
										operatorKind = AttribOperatorKind.DashMatch;
									}
								}
								else
								{
									operatorKind = AttribOperatorKind.Includes;
								}
							}
							else
							{
								operatorKind = AttribOperatorKind.Equal;
							}
						}
						else
						{
							operatorKind = AttribOperatorKind.Substring;
						}
					}
					else
					{
						operatorKind = AttribOperatorKind.Suffix;
					}
				}
				else
				{
					operatorKind = AttribOperatorKind.Prefix;
				}
			}
			string identityOrString = null;
			CommonTree commonTree = attribOperatorAndValueTree.Children(CommonTreeTransformer.T(115)).FirstOrDefault<CommonTree>();
			if (commonTree != null)
			{
				identityOrString = ((commonTree.FirstChildText() == CommonTreeTransformer.T(179)) ? CommonTreeTransformer.StringOrUriBasedValue(commonTree.Children(CommonTreeTransformer.T(179)).FirstChildText()) : commonTree.FirstChildText());
			}
			return new AttribOperatorAndValueNode(operatorKind, identityOrString);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004E41D File Offset: 0x0004C61D
		private static string StringOrUriBasedValue(string text)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				text = text.Replace("\\\n", string.Empty).Replace("\\\r\n", string.Empty).Replace("\\\f", string.Empty);
			}
			return text;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004E458 File Offset: 0x0004C658
		private static string T(int tokenIndex)
		{
			return CssParser.tokenNames[tokenIndex];
		}
	}
}
