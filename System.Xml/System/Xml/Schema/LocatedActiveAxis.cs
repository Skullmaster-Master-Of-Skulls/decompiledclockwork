using System;

namespace System.Xml.Schema
{
	// Token: 0x0200018C RID: 396
	internal class LocatedActiveAxis : ActiveAxis
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0005E0CE File Offset: 0x0005D0CE
		internal int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0005E0D6 File Offset: 0x0005D0D6
		internal LocatedActiveAxis(Asttree astfield, KeySequence ks, int column) : base(astfield)
		{
			this.Ks = ks;
			this.column = column;
			this.isMatched = false;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0005E0F4 File Offset: 0x0005D0F4
		internal void Reactivate(KeySequence ks)
		{
			base.Reactivate();
			this.Ks = ks;
		}

		// Token: 0x04000CA4 RID: 3236
		private int column;

		// Token: 0x04000CA5 RID: 3237
		internal bool isMatched;

		// Token: 0x04000CA6 RID: 3238
		internal KeySequence Ks;
	}
}
