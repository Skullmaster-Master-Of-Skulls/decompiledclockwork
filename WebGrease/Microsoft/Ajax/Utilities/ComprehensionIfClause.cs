using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000010 RID: 16
	public class ComprehensionIfClause : ComprehensionClause
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003B21 File Offset: 0x00001D21
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00003B6B File Offset: 0x00001D6B
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

		// Token: 0x06000138 RID: 312 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public ComprehensionIfClause(Context context) : base(context)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003BAD File Offset: 0x00001DAD
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003BB9 File Offset: 0x00001DB9
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Condition, null, null, null);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00003BC9 File Offset: 0x00001DC9
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x0400002F RID: 47
		private AstNode m_condition;
	}
}
