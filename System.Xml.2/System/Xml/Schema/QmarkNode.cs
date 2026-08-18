using System;

namespace System.Xml.Schema
{
	// Token: 0x020001F5 RID: 501
	internal sealed class QmarkNode : InteriorNode
	{
		// Token: 0x060020BF RID: 8383 RVA: 0x000B309B File Offset: 0x000B129B
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x000B30AB File Offset: 0x000B12AB
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
