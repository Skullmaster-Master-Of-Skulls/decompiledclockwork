using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200008B RID: 139
	public abstract class IterationStatement : AstNode
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00025787 File Offset: 0x00023987
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x000257CF File Offset: 0x000239CF
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

		// Token: 0x0600085E RID: 2142 RVA: 0x00025808 File Offset: 0x00023A08
		protected IterationStatement(Context context) : base(context)
		{
		}

		// Token: 0x04000319 RID: 793
		private Block m_body;
	}
}
