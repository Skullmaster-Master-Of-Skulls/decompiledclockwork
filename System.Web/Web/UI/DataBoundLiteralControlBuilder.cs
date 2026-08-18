using System;

namespace System.Web.UI
{
	// Token: 0x020003D3 RID: 979
	internal class DataBoundLiteralControlBuilder : ControlBuilder
	{
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000D4565 File Offset: 0x000D3565
		internal DataBoundLiteralControlBuilder()
		{
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000D4570 File Offset: 0x000D3570
		internal void AddLiteralString(string s)
		{
			object lastBuilder = base.GetLastBuilder();
			if (lastBuilder != null && lastBuilder is string)
			{
				base.AddSubBuilder(null);
			}
			base.AddSubBuilder(s);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000D45A0 File Offset: 0x000D35A0
		internal void AddDataBindingExpression(CodeBlockBuilder codeBlockBuilder)
		{
			object lastBuilder = base.GetLastBuilder();
			if (lastBuilder == null || lastBuilder is CodeBlockBuilder)
			{
				base.AddSubBuilder(null);
			}
			base.AddSubBuilder(codeBlockBuilder);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000D45CD File Offset: 0x000D35CD
		internal int GetStaticLiteralsCount()
		{
			return (base.SubBuilders.Count + 1) / 2;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000D45DE File Offset: 0x000D35DE
		internal int GetDataBoundLiteralCount()
		{
			return base.SubBuilders.Count / 2;
		}
	}
}
