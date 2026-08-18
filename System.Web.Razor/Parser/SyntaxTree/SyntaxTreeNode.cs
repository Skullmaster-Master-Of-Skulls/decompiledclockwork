using System;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x02000089 RID: 137
	public abstract class SyntaxTreeNode
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001649B File Offset: 0x0001469B
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000164A3 File Offset: 0x000146A3
		public Block Parent { get; internal set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005B3 RID: 1459
		public abstract bool IsBlock { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005B4 RID: 1460
		public abstract int Length { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005B5 RID: 1461
		public abstract SourceLocation Start { get; }

		// Token: 0x060005B6 RID: 1462
		public abstract void Accept(ParserVisitor visitor);

		// Token: 0x060005B7 RID: 1463
		public abstract bool EquivalentTo(SyntaxTreeNode node);
	}
}
