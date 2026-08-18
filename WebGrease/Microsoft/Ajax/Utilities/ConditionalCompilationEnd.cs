using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000071 RID: 113
	public class ConditionalCompilationEnd : ConditionalCompilationStatement
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x00022677 File Offset: 0x00020877
		public ConditionalCompilationEnd(Context context) : base(context)
		{
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00022680 File Offset: 0x00020880
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
