using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200008D RID: 141
	public class EmptyStatement : AstNode
	{
		// Token: 0x0600086C RID: 2156 RVA: 0x00025901 File Offset: 0x00023B01
		public EmptyStatement(Context context) : base(context)
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002590A File Offset: 0x00023B0A
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
