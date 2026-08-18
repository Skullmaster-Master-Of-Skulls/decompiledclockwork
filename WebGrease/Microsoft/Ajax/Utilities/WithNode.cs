using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000D0 RID: 208
	public sealed class WithNode : AstNode
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00041C42 File Offset: 0x0003FE42
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00041C8B File Offset: 0x0003FE8B
		public AstNode WithObject
		{
			get
			{
				return this.m_withObject;
			}
			set
			{
				this.m_withObject.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_withObject = value;
				this.m_withObject.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00041CC4 File Offset: 0x0003FEC4
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x00041D0B File Offset: 0x0003FF0B
		public Block Body
		{
			get
			{
				return this.m_body;
			}
			set
			{
				this.m_body.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_body = value;
				this.m_body.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00041D4C File Offset: 0x0003FF4C
		public override Context TerminatingContext
		{
			get
			{
				Context result;
				if ((result = base.TerminatingContext) == null)
				{
					result = this.Body.IfNotNull((Block b) => b.TerminatingContext);
				}
				return result;
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00041D80 File Offset: 0x0003FF80
		public WithNode(Context context) : base(context)
		{
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00041D89 File Offset: 0x0003FF89
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00041D95 File Offset: 0x0003FF95
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.WithObject, this.Body, null, null);
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00041DAA File Offset: 0x0003FFAA
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.WithObject == oldNode)
			{
				this.WithObject = newNode;
				return true;
			}
			if (this.Body == oldNode)
			{
				this.Body = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00041DD6 File Offset: 0x0003FFD6
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			return this.Body != null && this.Body.EncloseBlock(type);
		}

		// Token: 0x04000573 RID: 1395
		private AstNode m_withObject;

		// Token: 0x04000574 RID: 1396
		private Block m_body;
	}
}
