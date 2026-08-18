using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000072 RID: 114
	public class ConditionalCompilationIf : ConditionalCompilationStatement
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0002268C File Offset: 0x0002088C
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x000226D3 File Offset: 0x000208D3
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

		// Token: 0x0600072D RID: 1837 RVA: 0x0002270C File Offset: 0x0002090C
		public ConditionalCompilationIf(Context context) : base(context)
		{
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00022715 File Offset: 0x00020915
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Condition, null, null, null);
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00022725 File Offset: 0x00020925
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00022731 File Offset: 0x00020931
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x0400027B RID: 635
		private AstNode m_condition;
	}
}
