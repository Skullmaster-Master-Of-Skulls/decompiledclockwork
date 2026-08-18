using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Money;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000308 RID: 776
	public class TaxRule
	{
		// Token: 0x060017DE RID: 6110 RVA: 0x0001CE0C File Offset: 0x0001B00C
		public TaxRule()
		{
			this.UseTaxes = new List<bool>();
		}

		// Token: 0x170009DB RID: 2523
		public bool this[int index]
		{
			get
			{
				return TaxRule.GetUseTax(this.UseTaxes, index);
			}
			set
			{
				this.UseTaxes = TaxRule.SetUseTax(this.UseTaxes, index, value);
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0001CE58 File Offset: 0x0001B058
		public static bool GetUseTax(IList<bool> UseTaxes, int index)
		{
			bool flag = UseTaxes == null || index >= UseTaxes.Count;
			return !flag && UseTaxes[index];
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0001CE8C File Offset: 0x0001B08C
		public static IList<bool> SetUseTax(IList<bool> UseTaxes, int index, bool value)
		{
			bool flag = UseTaxes == null;
			if (flag)
			{
				UseTaxes = new List<bool>();
				for (int i = 0; i <= index; i++)
				{
					UseTaxes.Add(i == index && value);
				}
			}
			else
			{
				bool flag2 = index >= UseTaxes.Count;
				if (flag2)
				{
					for (int j = UseTaxes.Count; j <= index; j++)
					{
						UseTaxes.Add(j == index && value);
					}
				}
				else
				{
					UseTaxes[index] = value;
				}
			}
			return UseTaxes;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0001CF24 File Offset: 0x0001B124
		public Money Calculate(Money Cost, TaxAmount taxAmount)
		{
			return TaxRule.Calculate(Cost, taxAmount, this.UseTaxes);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0001CF44 File Offset: 0x0001B144
		public static Money Calculate(Money Cost, TaxAmount taxAmount, IList<bool> useTaxes)
		{
			bool flag = useTaxes == null || useTaxes.Count < 1;
			Money result;
			if (flag)
			{
				result = Cost;
			}
			else
			{
				TaxAmount taxAmountsOnlyForUseTaxItems = TaxAmount.GetTaxAmountsOnlyForUseTaxItems(taxAmount, useTaxes);
				result = taxAmountsOnlyForUseTaxItems.Calculate(Cost);
			}
			return result;
		}

		// Token: 0x040013F7 RID: 5111
		private IList<bool> UseTaxes;
	}
}
