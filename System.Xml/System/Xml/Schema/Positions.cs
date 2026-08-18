using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000195 RID: 405
	internal class Positions
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0005EE2C File Offset: 0x0005DE2C
		public int Add(int symbol, object particle)
		{
			return this.positions.Add(new Position(symbol, particle));
		}

		// Token: 0x1700051A RID: 1306
		public Position this[int pos]
		{
			get
			{
				return (Position)this.positions[pos];
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0005EE58 File Offset: 0x0005DE58
		public int Count
		{
			get
			{
				return this.positions.Count;
			}
		}

		// Token: 0x04000CC4 RID: 3268
		private ArrayList positions = new ArrayList();
	}
}
