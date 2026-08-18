using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C82 RID: 3202
	internal abstract class VarianceAggregateBase : AggregateValue
	{
		// Token: 0x06007828 RID: 30760 RVA: 0x001BC12D File Offset: 0x001BA32D
		internal VarianceAggregateBase()
		{
			this.values = new List<double>();
		}

		// Token: 0x170026D5 RID: 9941
		// (get) Token: 0x06007829 RID: 30761 RVA: 0x001BC140 File Offset: 0x001BA340
		internal int Count
		{
			get
			{
				return this.values.Count;
			}
		}

		// Token: 0x0600782A RID: 30762 RVA: 0x001BC150 File Offset: 0x001BA350
		internal double GetSquaredDifferencesSum()
		{
			double num = this.values.Sum() / (double)this.values.Count;
			double num2 = 0.0;
			foreach (double num3 in this.values)
			{
				double num4 = num3;
				num2 += Math.Pow(num4 - num, 2.0);
			}
			return num2;
		}

		// Token: 0x0600782B RID: 30763 RVA: 0x001BC1D8 File Offset: 0x001BA3D8
		protected override void AccumulateOverride(object value)
		{
			this.values.Add(Convert.ToDouble(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600782C RID: 30764 RVA: 0x001BC1F0 File Offset: 0x001BA3F0
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			VarianceAggregateBase varianceAggregateBase = childAggregate as VarianceAggregateBase;
			if (varianceAggregateBase != null)
			{
				using (List<double>.Enumerator enumerator = varianceAggregateBase.values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						double num = enumerator.Current;
						double item = num;
						this.values.Add(item);
					}
					return;
				}
			}
			base.RaiseError();
		}

		// Token: 0x040020E1 RID: 8417
		private List<double> values;
	}
}
