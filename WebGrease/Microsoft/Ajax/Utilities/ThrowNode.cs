using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C8 RID: 200
	public sealed class ThrowNode : AstNode
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00040C87 File Offset: 0x0003EE87
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x00040CCF File Offset: 0x0003EECF
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

		// Token: 0x06000D8C RID: 3468 RVA: 0x00040D08 File Offset: 0x0003EF08
		public ThrowNode(Context context) : base(context)
		{
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00040D11 File Offset: 0x0003EF11
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00040D1D File Offset: 0x0003EF1D
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Operand, null, null, null);
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00040D2D File Offset: 0x0003EF2D
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Operand == oldNode)
			{
				this.Operand = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x04000545 RID: 1349
		private AstNode m_operand;
	}
}
