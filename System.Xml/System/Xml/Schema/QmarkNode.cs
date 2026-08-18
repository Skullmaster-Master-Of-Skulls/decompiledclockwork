using System;

namespace System.Xml.Schema
{
	// Token: 0x0200019D RID: 413
	internal sealed class QmarkNode : InteriorNode
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x0005F25F File Offset: 0x0005E25F
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0005F26F File Offset: 0x0005E26F
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
