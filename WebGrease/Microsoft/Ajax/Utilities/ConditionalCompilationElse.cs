using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000070 RID: 112
	public class ConditionalCompilationElse : ConditionalCompilationStatement
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x00022662 File Offset: 0x00020862
		public ConditionalCompilationElse(Context context) : base(context)
		{
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0002266B File Offset: 0x0002086B
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
