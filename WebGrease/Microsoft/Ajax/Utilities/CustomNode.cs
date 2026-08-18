using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000086 RID: 134
	public class CustomNode : AstNode
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00025573 File Offset: 0x00023773
		public CustomNode(Context context) : base(context)
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002557C File Offset: 0x0002377C
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00025588 File Offset: 0x00023788
		internal virtual bool RequiresSeparator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0002558B File Offset: 0x0002378B
		internal virtual bool IsDebuggerStatement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002558E File Offset: 0x0002378E
		public virtual string ToCode()
		{
			return string.Empty;
		}
	}
}
