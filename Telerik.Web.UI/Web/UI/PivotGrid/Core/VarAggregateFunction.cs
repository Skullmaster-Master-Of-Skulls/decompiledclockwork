using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C57 RID: 3159
	[DataContract]
	public sealed class VarAggregateFunction : StatisticalFormatAggregateFunction
	{
		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x06007753 RID: 30547 RVA: 0x001BAAF0 File Offset: 0x001B8CF0
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.Var;
			}
		}

		// Token: 0x06007754 RID: 30548 RVA: 0x001BAAF7 File Offset: 0x001B8CF7
		protected internal override AggregateValue CreateAggregate(Type dataType)
		{
			return new VarAggregate();
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x001BAAFE File Offset: 0x001B8CFE
		public override int GetHashCode()
		{
			return 8;
		}

		// Token: 0x06007756 RID: 30550 RVA: 0x001BAB01 File Offset: 0x001B8D01
		public override bool Equals(object obj)
		{
			return obj is VarAggregateFunction;
		}

		// Token: 0x06007757 RID: 30551 RVA: 0x001BAB0C File Offset: 0x001B8D0C
		public override string ToString()
		{
			return "Var";
		}

		// Token: 0x06007758 RID: 30552 RVA: 0x001BAB13 File Offset: 0x001B8D13
		protected override Cloneable CreateInstanceCore()
		{
			return new VarAggregateFunction();
		}

		// Token: 0x06007759 RID: 30553 RVA: 0x001BAB1A File Offset: 0x001B8D1A
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
