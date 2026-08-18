using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000620 RID: 1568
	public class Crc24
	{
		// Token: 0x06003554 RID: 13652 RVA: 0x0014B220 File Offset: 0x0014A220
		public void Update(int b)
		{
			this.crc ^= b << 16;
			for (int i = 0; i < 8; i++)
			{
				this.crc <<= 1;
				if ((this.crc & 16777216) != 0)
				{
					this.crc ^= 25578747;
				}
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x0014B278 File Offset: 0x0014A278
		[Obsolete("Use 'Value' property instead")]
		public int GetValue()
		{
			return this.crc;
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x0014B280 File Offset: 0x0014A280
		public int Value
		{
			get
			{
				return this.crc;
			}
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x0014B288 File Offset: 0x0014A288
		public void Reset()
		{
			this.crc = 11994318;
		}

		// Token: 0x040023A3 RID: 9123
		private const int Crc24Init = 11994318;

		// Token: 0x040023A4 RID: 9124
		private const int Crc24Poly = 25578747;

		// Token: 0x040023A5 RID: 9125
		private int crc = 11994318;
	}
}
