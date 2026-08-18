using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C79 RID: 3193
	internal class ConstantValueAggregate : AggregateValue
	{
		// Token: 0x060077FC RID: 30716 RVA: 0x001BBBDF File Offset: 0x001B9DDF
		public ConstantValueAggregate(object value)
		{
			if (value is AggregateError)
			{
				base.RaiseError();
				return;
			}
			this.value = value;
		}

		// Token: 0x060077FD RID: 30717 RVA: 0x001BBBFD File Offset: 0x001B9DFD
		protected override object GetValueOverride()
		{
			return this.value;
		}

		// Token: 0x060077FE RID: 30718 RVA: 0x001BBC05 File Offset: 0x001B9E05
		protected override void AccumulateOverride(object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060077FF RID: 30719 RVA: 0x001BBC0C File Offset: 0x001B9E0C
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040020D8 RID: 8408
		private object value;
	}
}
