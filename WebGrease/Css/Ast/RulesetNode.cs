using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000123 RID: 291
	public sealed class RulesetNode : StyleSheetRuleNode
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x0004C6FE File Offset: 0x0004A8FE
		public RulesetNode(SelectorsGroupNode selectorsGroupNode, ReadOnlyCollection<DeclarationNode> declarations, ReadOnlyCollection<ImportantCommentNode> importantComments)
		{
			this.SelectorsGroupNode = selectorsGroupNode;
			this.Declarations = (declarations ?? new List<DeclarationNode>(0).AsReadOnly());
			this.ImportantComments = (importantComments ?? new List<ImportantCommentNode>(0).AsReadOnly());
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0004C739 File Offset: 0x0004A939
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x0004C741 File Offset: 0x0004A941
		public ReadOnlyCollection<ImportantCommentNode> ImportantComments { get; private set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0004C74A File Offset: 0x0004A94A
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x0004C752 File Offset: 0x0004A952
		public SelectorsGroupNode SelectorsGroupNode { get; private set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0004C75B File Offset: 0x0004A95B
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x0004C763 File Offset: 0x0004A963
		public ReadOnlyCollection<DeclarationNode> Declarations { get; private set; }

		// Token: 0x06001195 RID: 4501 RVA: 0x0004C76C File Offset: 0x0004A96C
		public bool HasConflictingDeclaration(OrderedDictionary declarationDictionary)
		{
			foreach (DeclarationNode declarationNode in this.Declarations)
			{
				if (declarationDictionary.Contains(declarationNode.Property))
				{
					return !((DeclarationNode)declarationDictionary[declarationNode.Property]).Equals(declarationNode);
				}
			}
			return false;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0004C7E0 File Offset: 0x0004A9E0
		public bool ShouldMergeWith(RulesetNode rulesetNode)
		{
			int num = 0;
			foreach (DeclarationNode declarationNode in this.Declarations)
			{
				foreach (DeclarationNode declarationNode2 in rulesetNode.Declarations)
				{
					if (declarationNode.Equals(declarationNode2))
					{
						num++;
						break;
					}
				}
				if (num > 1)
				{
					break;
				}
			}
			return num > 1 || (num == 1 && (this.Declarations.Count == 1 || rulesetNode.Declarations.Count == 1));
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0004C8A0 File Offset: 0x0004AAA0
		public RulesetNode GetMergedRulesetNode(RulesetNode otherRulesetNode)
		{
			List<SelectorNode> first = new List<SelectorNode>(this.SelectorsGroupNode.SelectorNodes);
			List<SelectorNode> second = new List<SelectorNode>(otherRulesetNode.SelectorsGroupNode.SelectorNodes);
			ReadOnlyCollection<SelectorNode> selectorNodes = first.Union(second).ToList<SelectorNode>().AsReadOnly();
			List<DeclarationNode> list = new List<DeclarationNode>(this.Declarations);
			List<DeclarationNode> list2 = new List<DeclarationNode>(otherRulesetNode.Declarations);
			List<DeclarationNode> list3 = new List<DeclarationNode>();
			foreach (DeclarationNode declarationNode in this.Declarations)
			{
				bool flag = true;
				foreach (DeclarationNode declarationNode2 in otherRulesetNode.Declarations)
				{
					if (declarationNode.Equals(declarationNode2))
					{
						flag = false;
						list2.Remove(declarationNode2);
						break;
					}
				}
				if (!flag)
				{
					list.Remove(declarationNode);
					list3.Add(declarationNode);
				}
			}
			this.Declarations = list.AsReadOnly();
			otherRulesetNode.Declarations = list2.AsReadOnly();
			return new RulesetNode(new SelectorsGroupNode(selectorNodes), list3.AsReadOnly(), this.ImportantComments);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0004C9E0 File Offset: 0x0004ABE0
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitRulesetNode(this);
		}
	}
}
