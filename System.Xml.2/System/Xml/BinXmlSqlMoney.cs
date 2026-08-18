using System;
using System.Globalization;

namespace System.Xml
{
	// Token: 0x02000125 RID: 293
	internal struct BinXmlSqlMoney
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x00054BDE File Offset: 0x00052DDE
		public BinXmlSqlMoney(int v)
		{
			this.data = (long)v;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00054BE8 File Offset: 0x00052DE8
		public BinXmlSqlMoney(long v)
		{
			this.data = v;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00054BF4 File Offset: 0x00052DF4
		public decimal ToDecimal()
		{
			bool isNegative;
			ulong num;
			if (this.data < 0L)
			{
				isNegative = true;
				num = (ulong)(-(ulong)this.data);
			}
			else
			{
				isNegative = false;
				num = (ulong)this.data;
			}
			return new decimal((int)num, (int)(num >> 32), 0, isNegative, 4);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00054C30 File Offset: 0x00052E30
		public override string ToString()
		{
			return this.ToDecimal().ToString("#0.00##", CultureInfo.InvariantCulture);
		}

		// Token: 0x040005E8 RID: 1512
		private long data;
	}
}
