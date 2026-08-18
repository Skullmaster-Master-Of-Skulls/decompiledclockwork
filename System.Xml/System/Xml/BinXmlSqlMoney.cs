using System;
using System.Globalization;

namespace System.Xml
{
	// Token: 0x020000F6 RID: 246
	internal struct BinXmlSqlMoney
	{
		// Token: 0x06000EEA RID: 3818 RVA: 0x0004187E File Offset: 0x0004087E
		public BinXmlSqlMoney(int v)
		{
			this.data = (long)v;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00041888 File Offset: 0x00040888
		public BinXmlSqlMoney(long v)
		{
			this.data = v;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00041894 File Offset: 0x00040894
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

		// Token: 0x06000EED RID: 3821 RVA: 0x000418D0 File Offset: 0x000408D0
		public override string ToString()
		{
			return this.ToDecimal().ToString("#0.00##", CultureInfo.InvariantCulture);
		}

		// Token: 0x04000A08 RID: 2568
		private long data;
	}
}
