using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Money;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000307 RID: 775
	public class TaxAmount
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x0001CBA2 File Offset: 0x0001ADA2
		public TaxAmount()
		{
			this.TaxPercentages = new List<decimal>();
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		public static TaxAmount GetTaxAmountsOnlyForUseTaxItems(TaxAmount TaxAmount, IList<bool> useTaxes)
		{
			bool flag = TaxAmount == null || useTaxes == null || useTaxes.Count < 1;
			TaxAmount result;
			if (flag)
			{
				result = new TaxAmount();
			}
			else
			{
				TaxAmount taxAmount = new TaxAmount();
				for (int i = 0; i < TaxAmount.Count; i++)
				{
					bool flag2 = i < useTaxes.Count && i < TaxAmount.Count;
					if (!flag2)
					{
						break;
					}
					taxAmount[i] = (useTaxes[i] ? TaxAmount[i] : 0m);
				}
				result = taxAmount;
			}
			return result;
		}

		// Token: 0x170009D9 RID: 2521
		public decimal this[int index]
		{
			get
			{
				return TaxAmount.GetTaxPercentage(this.TaxPercentages, index);
			}
			set
			{
				this.TaxPercentages = TaxAmount.SetTaxPercentage(this.TaxPercentages, index, value);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		public int Count
		{
			get
			{
				return (this.TaxPercentages == null) ? 0 : this.TaxPercentages.Count;
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		public static decimal GetTaxPercentage(IList<decimal> TaxPercentages, int index)
		{
			bool flag = TaxPercentages == null || index >= TaxPercentages.Count;
			decimal result;
			if (flag)
			{
				result = 0m;
			}
			else
			{
				result = TaxPercentages[index];
			}
			return result;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		public static IList<decimal> SetTaxPercentage(IList<decimal> TaxPercentages, int index, decimal value)
		{
			bool flag = TaxPercentages == null;
			if (flag)
			{
				TaxPercentages = new List<decimal>();
				for (int i = 0; i <= index; i++)
				{
					TaxPercentages.Add((i == index) ? value : 0m);
				}
			}
			else
			{
				bool flag2 = index >= TaxPercentages.Count;
				if (flag2)
				{
					for (int j = TaxPercentages.Count; j <= index; j++)
					{
						TaxPercentages.Add((j == index) ? value : 0m);
					}
				}
				else
				{
					TaxPercentages[index] = value;
				}
			}
			return TaxPercentages;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		public Money Calculate(Money Amount)
		{
			return TaxAmount.Calculate(Amount, this.TaxPercentages);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0001CD9C File Offset: 0x0001AF9C
		public static Money Calculate(Money Amount, IList<decimal> TaxPercentages)
		{
			bool flag = TaxPercentages == null || TaxPercentages.Count < 1;
			Money result;
			if (flag)
			{
				result = Amount;
			}
			else
			{
				Money money = Amount;
				for (int i = 0; i < TaxPercentages.Count; i++)
				{
					bool flag2 = TaxPercentages[i] >= 0m;
					if (flag2)
					{
						money += Amount * TaxPercentages[i];
					}
				}
				result = money;
			}
			return result;
		}

		// Token: 0x040013F6 RID: 5110
		private IList<decimal> TaxPercentages;
	}
}
