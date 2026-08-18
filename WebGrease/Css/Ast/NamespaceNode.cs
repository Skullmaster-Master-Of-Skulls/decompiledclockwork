using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000121 RID: 289
	public sealed class NamespaceNode : AstNode
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x0004C67C File Offset: 0x0004A87C
		public NamespaceNode(string prefix, string value)
		{
			this.Prefix = prefix;
			this.Value = value;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0004C692 File Offset: 0x0004A892
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x0004C69A File Offset: 0x0004A89A
		public string Prefix { get; private set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0004C6A3 File Offset: 0x0004A8A3
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x0004C6AB File Offset: 0x0004A8AB
		public string Value { get; private set; }

		// Token: 0x06001187 RID: 4487 RVA: 0x0004C6B4 File Offset: 0x0004A8B4
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitNamespaceNode(this);
		}
	}
}
