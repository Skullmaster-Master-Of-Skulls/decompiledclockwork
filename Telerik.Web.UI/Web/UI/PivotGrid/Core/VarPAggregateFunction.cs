using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C58 RID: 3160
	[DataContract]
	public sealed class VarPAggregateFunction : StatisticalFormatAggregateFunction
	{
		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x0600775B RID: 30555 RVA: 0x001BAB24 File Offset: 0x001B8D24
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.VarP;
			}
		}

		// Token: 0x0600775C RID: 30556 RVA: 0x001BAB2B File Offset: 0x001B8D2B
		protected internal override AggregateValue CreateAggregate(Type dataType)
		{
			return new VarPAggregate();
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x001BAB32 File Offset: 0x001B8D32
		public override int GetHashCode()
		{
			return 9;
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x001BAB36 File Offset: 0x001B8D36
		public override bool Equals(object obj)
		{
			return obj is VarPAggregateFunction;
		}

		// Token: 0x0600775F RID: 30559 RVA: 0x001BAB41 File Offset: 0x001B8D41
		public override string ToString()
		{
			return "VarP";
		}

		// Token: 0x06007760 RID: 30560 RVA: 0x001BAB48 File Offset: 0x001B8D48
		protected override Cloneable CreateInstanceCore()
		{
			return new VarPAggregateFunction();
		}

		// Token: 0x06007761 RID: 30561 RVA: 0x001BAB4F File Offset: 0x001B8D4F
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
