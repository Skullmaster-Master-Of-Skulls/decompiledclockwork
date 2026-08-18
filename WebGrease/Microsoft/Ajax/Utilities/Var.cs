using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000CD RID: 205
	public sealed class Var : Declaration
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x00041AC7 File Offset: 0x0003FCC7
		public Var(Context context) : base(context)
		{
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00041AD0 File Offset: 0x0003FCD0
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
