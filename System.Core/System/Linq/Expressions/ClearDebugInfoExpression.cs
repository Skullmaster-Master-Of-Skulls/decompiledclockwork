using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200022A RID: 554
	internal sealed class ClearDebugInfoExpression : DebugInfoExpression
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x000440AF File Offset: 0x000422AF
		internal ClearDebugInfoExpression(SymbolDocumentInfo document) : base(document)
		{
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x000440B8 File Offset: 0x000422B8
		public override bool IsClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000440BB File Offset: 0x000422BB
		public override int StartLine
		{
			get
			{
				return 16707566;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000440C2 File Offset: 0x000422C2
		public override int StartColumn
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x000440C5 File Offset: 0x000422C5
		public override int EndLine
		{
			get
			{
				return 16707566;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x000440CC File Offset: 0x000422CC
		public override int EndColumn
		{
			get
			{
				return 0;
			}
		}
	}
}
