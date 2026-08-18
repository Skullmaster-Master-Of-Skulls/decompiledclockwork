using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000087 RID: 135
	public sealed class DebuggerNode : AstNode
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x00025595 File Offset: 0x00023795
		public DebuggerNode(Context context) : base(context)
		{
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002559E File Offset: 0x0002379E
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
