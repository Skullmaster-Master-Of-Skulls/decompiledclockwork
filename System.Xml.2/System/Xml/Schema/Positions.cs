using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001ED RID: 493
	internal class Positions
	{
		// Token: 0x06002094 RID: 8340 RVA: 0x000B2AB4 File Offset: 0x000B0CB4
		public int Add(int symbol, object particle)
		{
			return this.positions.Add(new Position(symbol, particle));
		}

		// Token: 0x170006BC RID: 1724
		public Position this[int pos]
		{
			get
			{
				return (Position)this.positions[pos];
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x000B2AE0 File Offset: 0x000B0CE0
		public int Count
		{
			get
			{
				return this.positions.Count;
			}
		}

		// Token: 0x04000DB9 RID: 3513
		private ArrayList positions = new ArrayList();
	}
}
