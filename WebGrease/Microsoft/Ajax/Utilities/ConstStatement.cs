using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200007E RID: 126
	public class ConstStatement : Declaration
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x00023D30 File Offset: 0x00021F30
		public ConstStatement(Context context) : base(context)
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00023D39 File Offset: 0x00021F39
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
