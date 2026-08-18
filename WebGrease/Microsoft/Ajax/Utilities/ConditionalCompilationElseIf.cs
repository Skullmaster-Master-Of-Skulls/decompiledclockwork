using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200006F RID: 111
	public class ConditionalCompilationElseIf : ConditionalCompilationStatement
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000225A8 File Offset: 0x000207A8
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x000225EF File Offset: 0x000207EF
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

		// Token: 0x06000721 RID: 1825 RVA: 0x00022628 File Offset: 0x00020828
		public ConditionalCompilationElseIf(Context context) : base(context)
		{
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00022631 File Offset: 0x00020831
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Condition, null, null, null);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00022641 File Offset: 0x00020841
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0002264D File Offset: 0x0002084D
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x0400027A RID: 634
		private AstNode m_condition;
	}
}
