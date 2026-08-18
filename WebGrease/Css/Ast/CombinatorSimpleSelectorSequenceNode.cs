using System;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000118 RID: 280
	public sealed class CombinatorSimpleSelectorSequenceNode : AstNode
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x0004C124 File Offset: 0x0004A324
		public CombinatorSimpleSelectorSequenceNode(Combinator combinator, SimpleSelectorSequenceNode simpleSelectorSequenceNode)
		{
			this.Combinator = combinator;
			this.SimpleSelectorSequenceNode = simpleSelectorSequenceNode;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004C13A File Offset: 0x0004A33A
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x0004C142 File Offset: 0x0004A342
		public Combinator Combinator { get; private set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0004C14B File Offset: 0x0004A34B
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0004C153 File Offset: 0x0004A353
		public SimpleSelectorSequenceNode SimpleSelectorSequenceNode { get; private set; }

		// Token: 0x06001139 RID: 4409 RVA: 0x0004C15C File Offset: 0x0004A35C
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitCombinatorSimpleSelectorSequenceNode(this);
		}
	}
}
