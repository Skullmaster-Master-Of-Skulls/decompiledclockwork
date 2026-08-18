using System;

namespace Org.BouncyCastle.Utilities.Date
{
	// Token: 0x020002E5 RID: 741
	public sealed class DateTimeObject
	{
		// Token: 0x06001B7C RID: 7036 RVA: 0x000A5453 File Offset: 0x000A4453
		public DateTimeObject(DateTime dt)
		{
			this.dt = dt;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000A5462 File Offset: 0x000A4462
		public DateTime Value
		{
			get
			{
				return this.dt;
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000A546C File Offset: 0x000A446C
		public override string ToString()
		{
			return this.dt.ToString();
		}

		// Token: 0x040012F7 RID: 4855
		private readonly DateTime dt;
	}
}
