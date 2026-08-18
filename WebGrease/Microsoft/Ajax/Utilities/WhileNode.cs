using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000CF RID: 207
	public sealed class WhileNode : IterationStatement
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00041B16 File Offset: 0x0003FD16
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00041B5F File Offset: 0x0003FD5F
		public AstNode Condition
		{
			get
			{
				return this.m_condition;
			}
			set
			{
				this.m_condition.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_condition = value;
				this.m_condition.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00041BA0 File Offset: 0x0003FDA0
		public override Context TerminatingContext
		{
			get
			{
				Context result;
				if ((result = base.TerminatingContext) == null)
				{
					result = base.Body.IfNotNull((Block b) => b.TerminatingContext);
				}
				return result;
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		public WhileNode(Context context) : base(context)
		{
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00041BDD File Offset: 0x0003FDDD
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00041BE9 File Offset: 0x0003FDE9
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Condition, base.Body, null, null);
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00041BFE File Offset: 0x0003FDFE
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			if (base.Body == oldNode)
			{
				base.Body = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00041C2A File Offset: 0x0003FE2A
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			return base.Body != null && base.Body.EncloseBlock(type);
		}

		// Token: 0x04000571 RID: 1393
		private AstNode m_condition;
	}
}
