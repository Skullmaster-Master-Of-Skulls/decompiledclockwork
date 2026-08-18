using System;

namespace System.Xml.Schema
{
	// Token: 0x02000196 RID: 406
	internal abstract class SyntaxTreeNode
	{
		// Token: 0x0600154D RID: 5453
		public abstract void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions);

		// Token: 0x0600154E RID: 5454
		public abstract SyntaxTreeNode Clone(Positions positions);

		// Token: 0x0600154F RID: 5455
		public abstract void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos);

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001550 RID: 5456
		public abstract bool IsNullable { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0005EE78 File Offset: 0x0005DE78
		public virtual bool IsRangeNode
		{
			get
			{
				return false;
			}
		}
	}
}
