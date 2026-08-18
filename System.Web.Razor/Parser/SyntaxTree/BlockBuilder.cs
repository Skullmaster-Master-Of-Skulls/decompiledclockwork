using System;
using System.Collections.Generic;
using System.Web.Razor.Generator;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x02000049 RID: 73
	public class BlockBuilder
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000DDBD File Offset: 0x0000BFBD
		public BlockBuilder()
		{
			this.Reset();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public BlockBuilder(Block original)
		{
			this.Type = new BlockType?(original.Type);
			this.Children = new List<SyntaxTreeNode>(original.Children);
			this.Name = original.Name;
			this.CodeGenerator = original.CodeGenerator;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000DE19 File Offset: 0x0000C019
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000DE21 File Offset: 0x0000C021
		public BlockType? Type { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000DE2A File Offset: 0x0000C02A
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000DE32 File Offset: 0x0000C032
		public IList<SyntaxTreeNode> Children { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000DE3B File Offset: 0x0000C03B
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000DE43 File Offset: 0x0000C043
		public string Name { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000DE4C File Offset: 0x0000C04C
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000DE54 File Offset: 0x0000C054
		public IBlockCodeGenerator CodeGenerator { get; set; }

		// Token: 0x06000354 RID: 852 RVA: 0x0000DE5D File Offset: 0x0000C05D
		public Block Build()
		{
			return new Block(this);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000DE68 File Offset: 0x0000C068
		public void Reset()
		{
			this.Type = null;
			this.Name = null;
			this.Children = new List<SyntaxTreeNode>();
			this.CodeGenerator = BlockCodeGenerator.Null;
		}
	}
}
