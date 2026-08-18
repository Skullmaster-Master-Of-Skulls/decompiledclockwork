using System;

namespace System.Xml.Schema
{
	// Token: 0x0200019F RID: 415
	internal sealed class LeafRangeNode : LeafNode
	{
		// Token: 0x06001576 RID: 5494 RVA: 0x0005F2C1 File Offset: 0x0005E2C1
		public LeafRangeNode(decimal min, decimal max) : this(-1, min, max)
		{
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005F2CC File Offset: 0x0005E2CC
		public LeafRangeNode(int pos, decimal min, decimal max) : base(pos)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0005F2E3 File Offset: 0x0005E2E3
		public decimal Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0005F2EB File Offset: 0x0005E2EB
		public decimal Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0005F2F3 File Offset: 0x0005E2F3
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x0005F2FB File Offset: 0x0005E2FB
		public BitSet NextIteration
		{
			get
			{
				return this.nextIteration;
			}
			set
			{
				this.nextIteration = value;
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0005F304 File Offset: 0x0005E304
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafRangeNode(base.Pos, this.min, this.max);
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0005F31D File Offset: 0x0005E31D
		public override bool IsRangeNode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000CCA RID: 3274
		private decimal min;

		// Token: 0x04000CCB RID: 3275
		private decimal max;

		// Token: 0x04000CCC RID: 3276
		private BitSet nextIteration;
	}
}
