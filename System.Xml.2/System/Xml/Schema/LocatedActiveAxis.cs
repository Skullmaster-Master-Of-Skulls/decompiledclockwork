using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E5 RID: 485
	internal class LocatedActiveAxis : ActiveAxis
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x000B1DEA File Offset: 0x000AFFEA
		internal int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000B1DF2 File Offset: 0x000AFFF2
		internal LocatedActiveAxis(Asttree astfield, KeySequence ks, int column) : base(astfield)
		{
			this.Ks = ks;
			this.column = column;
			this.isMatched = false;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000B1E10 File Offset: 0x000B0010
		internal void Reactivate(KeySequence ks)
		{
			base.Reactivate();
			this.Ks = ks;
		}

		// Token: 0x04000D9B RID: 3483
		private int column;

		// Token: 0x04000D9C RID: 3484
		internal bool isMatched;

		// Token: 0x04000D9D RID: 3485
		internal KeySequence Ks;
	}
}
