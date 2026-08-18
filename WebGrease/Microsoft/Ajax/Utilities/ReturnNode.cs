using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C1 RID: 193
	public sealed class ReturnNode : AstNode
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x000400B8 File Offset: 0x0003E2B8
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x000400FF File Offset: 0x0003E2FF
		public AstNode Operand
		{
			get
			{
				return this.m_operand;
			}
			set
			{
				this.m_operand.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_operand = value;
				this.m_operand.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00040138 File Offset: 0x0003E338
		public ReturnNode(Context context) : base(context)
		{
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00040141 File Offset: 0x0003E341
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0004014D File Offset: 0x0003E34D
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Operand, null, null, null);
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0004015D File Offset: 0x0003E35D
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Operand == oldNode)
			{
				this.Operand = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x0400052A RID: 1322
		private AstNode m_operand;
	}
}
