using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C5B RID: 3163
	[DataContract]
	public abstract class SiblingTotalsFormat : TotalFormat, IDescriptionsReferencing
	{
		// Token: 0x06007768 RID: 30568 RVA: 0x001BAB72 File Offset: 0x001B8D72
		internal SiblingTotalsFormat()
		{
		}

		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x06007769 RID: 30569 RVA: 0x001BAB7A File Offset: 0x001B8D7A
		// (set) Token: 0x0600776A RID: 30570 RVA: 0x001BAB82 File Offset: 0x001B8D82
		[DataMember]
		public PivotAxis Axis { get; set; }

		// Token: 0x170026C8 RID: 9928
		// (get) Token: 0x0600776B RID: 30571 RVA: 0x001BAB8B File Offset: 0x001B8D8B
		// (set) Token: 0x0600776C RID: 30572 RVA: 0x001BAB93 File Offset: 0x001B8D93
		[DataMember]
		public int Level { get; set; }

		// Token: 0x0600776D RID: 30573
		internal abstract void FormatTotals(IReadOnlyList<TotalValue> valueFormatters, IAggregateResultProvider results);

		// Token: 0x0600776E RID: 30574 RVA: 0x001BAB9C File Offset: 0x001B8D9C
		protected override void CloneCore(Cloneable source)
		{
			SiblingTotalsFormat siblingTotalsFormat = source as SiblingTotalsFormat;
			if (siblingTotalsFormat != null)
			{
				this.Axis = siblingTotalsFormat.Axis;
				this.Level = siblingTotalsFormat.Level;
			}
		}

		// Token: 0x0600776F RID: 30575 RVA: 0x001BABCB File Offset: 0x001B8DCB
		internal virtual RunningTotalSubGroupVariation SubVariation()
		{
			return RunningTotalSubGroupVariation.ParentAndSelfNames;
		}

		// Token: 0x06007770 RID: 30576 RVA: 0x001BABD0 File Offset: 0x001B8DD0
		bool IDescriptionsReferencing.TrackDescriptions(IDescriptionIndexMap map)
		{
			GroupMapResult groupMapResult = DescriptionIndexMapExtensions.MapGroup(map, this.Axis, this.Level);
			this.Axis = groupMapResult.Axis;
			this.Level = groupMapResult.Index;
			return groupMapResult.Success;
		}
	}
}
