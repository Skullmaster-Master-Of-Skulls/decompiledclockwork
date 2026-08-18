using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C55 RID: 3157
	[DataContract]
	public sealed class StdDevPAggregateFunction : StatisticalFormatAggregateFunction
	{
		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x06007743 RID: 30531 RVA: 0x001BAA38 File Offset: 0x001B8C38
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.StdDevP;
			}
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x001BAA3F File Offset: 0x001B8C3F
		protected internal override AggregateValue CreateAggregate(Type dataType)
		{
			return new StdDevPAggregate();
		}

		// Token: 0x06007745 RID: 30533 RVA: 0x001BAA46 File Offset: 0x001B8C46
		public override int GetHashCode()
		{
			return 7;
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x001BAA49 File Offset: 0x001B8C49
		public override bool Equals(object obj)
		{
			return obj is StdDevPAggregateFunction;
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x001BAA54 File Offset: 0x001B8C54
		public override string ToString()
		{
			return "StdDevP";
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x001BAA5B File Offset: 0x001B8C5B
		protected override Cloneable CreateInstanceCore()
		{
			return new StdDevPAggregateFunction();
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x001BAA62 File Offset: 0x001B8C62
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
