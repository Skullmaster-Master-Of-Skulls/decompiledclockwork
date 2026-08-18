using System;

namespace System.Web.UI
{
	// Token: 0x02000272 RID: 626
	internal class DataBoundLiteralControlBuilder : ControlBuilder
	{
		// Token: 0x06001DC9 RID: 7625 RVA: 0x00057398 File Offset: 0x00055598
		internal DataBoundLiteralControlBuilder()
		{
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00060A04 File Offset: 0x0005EC04
		internal void AddLiteralString(string s)
		{
			object lastBuilder = base.GetLastBuilder();
			if (lastBuilder != null && lastBuilder is string)
			{
				base.AddSubBuilder(null);
			}
			base.AddSubBuilder(s);
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00060A34 File Offset: 0x0005EC34
		internal void AddDataBindingExpression(CodeBlockBuilder codeBlockBuilder)
		{
			object lastBuilder = base.GetLastBuilder();
			if (lastBuilder == null || lastBuilder is CodeBlockBuilder)
			{
				base.AddSubBuilder(null);
			}
			base.AddSubBuilder(codeBlockBuilder);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00060A61 File Offset: 0x0005EC61
		internal int GetStaticLiteralsCount()
		{
			return (base.SubBuilders.Count + 1) / 2;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00060A72 File Offset: 0x0005EC72
		internal int GetDataBoundLiteralCount()
		{
			return base.SubBuilders.Count / 2;
		}
	}
}
