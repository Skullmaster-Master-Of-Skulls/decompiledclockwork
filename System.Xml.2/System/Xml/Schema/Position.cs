using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EC RID: 492
	internal struct Position
	{
		// Token: 0x06002093 RID: 8339 RVA: 0x000B2AA4 File Offset: 0x000B0CA4
		public Position(int symbol, object particle)
		{
			this.symbol = symbol;
			this.particle = particle;
		}

		// Token: 0x04000DB7 RID: 3511
		public int symbol;

		// Token: 0x04000DB8 RID: 3512
		public object particle;
	}
}
