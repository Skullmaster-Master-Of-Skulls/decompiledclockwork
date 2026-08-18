using System;

namespace System.Xml.Schema
{
	// Token: 0x02000194 RID: 404
	internal struct Position
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0005EE1C File Offset: 0x0005DE1C
		public Position(int symbol, object particle)
		{
			this.symbol = symbol;
			this.particle = particle;
		}

		// Token: 0x04000CC2 RID: 3266
		public int symbol;

		// Token: 0x04000CC3 RID: 3267
		public object particle;
	}
}
