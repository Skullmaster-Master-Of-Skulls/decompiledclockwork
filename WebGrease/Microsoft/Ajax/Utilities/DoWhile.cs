using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200008C RID: 140
	public sealed class DoWhile : IterationStatement
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00025811 File Offset: 0x00023A11
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0002585B File Offset: 0x00023A5B
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

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00025894 File Offset: 0x00023A94
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0002589C File Offset: 0x00023A9C
		public Context WhileContext { get; set; }

		// Token: 0x06000865 RID: 2149 RVA: 0x000258A5 File Offset: 0x00023AA5
		public DoWhile(Context context) : base(context)
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000258AE File Offset: 0x00023AAE
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000258BA File Offset: 0x00023ABA
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(base.Body, this.Condition, null, null);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000258CF File Offset: 0x00023ACF
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (base.Body == oldNode)
			{
				base.Body = AstNode.ForceToBlock(newNode);
				return true;
			}
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000258FB File Offset: 0x00023AFB
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			return type == EncloseBlockType.SingleDoWhile;
		}

		// Token: 0x0400031A RID: 794
		private AstNode m_condition;
	}
}
