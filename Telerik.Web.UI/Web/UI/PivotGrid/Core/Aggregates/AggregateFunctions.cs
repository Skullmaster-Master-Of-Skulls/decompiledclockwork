using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C48 RID: 3144
	public static class AggregateFunctions
	{
		// Token: 0x170026AB RID: 9899
		// (get) Token: 0x060076E5 RID: 30437 RVA: 0x001B9E34 File Offset: 0x001B8034
		public static AggregateFunction Sum
		{
			get
			{
				return new SumAggregateFunction();
			}
		}

		// Token: 0x170026AC RID: 9900
		// (get) Token: 0x060076E6 RID: 30438 RVA: 0x001B9E3B File Offset: 0x001B803B
		public static AggregateFunction Count
		{
			get
			{
				return new CountAggregateFunction();
			}
		}

		// Token: 0x170026AD RID: 9901
		// (get) Token: 0x060076E7 RID: 30439 RVA: 0x001B9E42 File Offset: 0x001B8042
		public static AggregateFunction Average
		{
			get
			{
				return new AverageAggregateFunction();
			}
		}

		// Token: 0x170026AE RID: 9902
		// (get) Token: 0x060076E8 RID: 30440 RVA: 0x001B9E49 File Offset: 0x001B8049
		public static AggregateFunction Max
		{
			get
			{
				return new MaxAggregateFunction();
			}
		}

		// Token: 0x170026AF RID: 9903
		// (get) Token: 0x060076E9 RID: 30441 RVA: 0x001B9E50 File Offset: 0x001B8050
		public static AggregateFunction Min
		{
			get
			{
				return new MinAggregateFunction();
			}
		}

		// Token: 0x170026B0 RID: 9904
		// (get) Token: 0x060076EA RID: 30442 RVA: 0x001B9E57 File Offset: 0x001B8057
		public static AggregateFunction Product
		{
			get
			{
				return new ProductAggregateFunction();
			}
		}

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x060076EB RID: 30443 RVA: 0x001B9E5E File Offset: 0x001B805E
		public static AggregateFunction StdDev
		{
			get
			{
				return new StdDevAggregateFunction();
			}
		}

		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x060076EC RID: 30444 RVA: 0x001B9E65 File Offset: 0x001B8065
		public static AggregateFunction StdDevP
		{
			get
			{
				return new StdDevPAggregateFunction();
			}
		}

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x060076ED RID: 30445 RVA: 0x001B9E6C File Offset: 0x001B806C
		public static AggregateFunction Var
		{
			get
			{
				return new VarAggregateFunction();
			}
		}

		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x060076EE RID: 30446 RVA: 0x001B9E73 File Offset: 0x001B8073
		public static AggregateFunction VarP
		{
			get
			{
				return new VarPAggregateFunction();
			}
		}
	}
}
