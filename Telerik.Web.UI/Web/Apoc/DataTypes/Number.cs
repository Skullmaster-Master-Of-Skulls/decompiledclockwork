using System;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001389 RID: 5001
	internal class Number
	{
		// Token: 0x0600D080 RID: 53376 RVA: 0x002E34B4 File Offset: 0x002E16B4
		public Number(int n)
		{
			this.value = n;
		}

		// Token: 0x0600D081 RID: 53377 RVA: 0x002E34C8 File Offset: 0x002E16C8
		public Number(decimal n)
		{
			this.value = n;
		}

		// Token: 0x0600D082 RID: 53378 RVA: 0x002E34D7 File Offset: 0x002E16D7
		public Number(double n)
		{
			this.value = (decimal)n;
		}

		// Token: 0x0600D083 RID: 53379 RVA: 0x002E34EC File Offset: 0x002E16EC
		public int IntValue()
		{
			return (int)this.value;
		}

		// Token: 0x0600D084 RID: 53380 RVA: 0x002E34F9 File Offset: 0x002E16F9
		public double DoubleValue()
		{
			return (double)this.value;
		}

		// Token: 0x0600D085 RID: 53381 RVA: 0x002E3507 File Offset: 0x002E1707
		public float FloatValue()
		{
			return (float)this.value;
		}

		// Token: 0x0600D086 RID: 53382 RVA: 0x002E3515 File Offset: 0x002E1715
		public decimal DecimalValue()
		{
			return this.value;
		}

		// Token: 0x040037F8 RID: 14328
		private decimal value;
	}
}
