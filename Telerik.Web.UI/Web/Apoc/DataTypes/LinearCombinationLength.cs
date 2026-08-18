using System;
using System.Collections;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001387 RID: 4999
	internal class LinearCombinationLength : Length
	{
		// Token: 0x0600D077 RID: 53367 RVA: 0x002E31AB File Offset: 0x002E13AB
		public LinearCombinationLength()
		{
			this.factors = new ArrayList();
			this.lengths = new ArrayList();
		}

		// Token: 0x0600D078 RID: 53368 RVA: 0x002E31C9 File Offset: 0x002E13C9
		public void AddTerm(double factor, Length length)
		{
			this.factors.Add(factor);
			this.lengths.Add(length);
		}

		// Token: 0x0600D079 RID: 53369 RVA: 0x002E31EC File Offset: 0x002E13EC
		public override void ComputeValue()
		{
			int num = 0;
			int count = this.factors.Count;
			for (int i = 0; i < count; i++)
			{
				double num2 = (double)this.factors[i];
				Length length = (Length)this.lengths[i];
				num += (int)(num2 * (double)length.MValue());
			}
			base.SetComputedValue(num);
		}

		// Token: 0x040037F5 RID: 14325
		protected ArrayList factors;

		// Token: 0x040037F6 RID: 14326
		protected ArrayList lengths;
	}
}
