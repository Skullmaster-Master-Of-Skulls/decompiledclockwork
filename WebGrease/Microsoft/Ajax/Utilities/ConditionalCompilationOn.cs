using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000073 RID: 115
	public class ConditionalCompilationOn : ConditionalCompilationStatement
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x00022746 File Offset: 0x00020946
		public ConditionalCompilationOn(Context context) : base(context)
		{
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0002274F File Offset: 0x0002094F
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
