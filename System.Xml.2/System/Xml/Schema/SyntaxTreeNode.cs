using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EE RID: 494
	internal abstract class SyntaxTreeNode
	{
		// Token: 0x06002098 RID: 8344
		public abstract void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions);

		// Token: 0x06002099 RID: 8345
		public abstract SyntaxTreeNode Clone(Positions positions);

		// Token: 0x0600209A RID: 8346
		public abstract void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos);

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600209B RID: 8347
		public abstract bool IsNullable { get; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x000B2B00 File Offset: 0x000B0D00
		public virtual bool IsRangeNode
		{
			get
			{
				return false;
			}
		}
	}
}
