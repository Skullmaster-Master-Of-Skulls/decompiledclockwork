using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006CF RID: 1743
	[DataContract]
	public abstract class CalculatedItem
	{
		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06003E9C RID: 16028 RVA: 0x000C7E1B File Offset: 0x000C601B
		// (set) Token: 0x06003E9D RID: 16029 RVA: 0x000C7E23 File Offset: 0x000C6023
		[DataMember]
		public int SolveOrder { get; set; }

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x000C7E2C File Offset: 0x000C602C
		// (set) Token: 0x06003E9F RID: 16031 RVA: 0x000C7E34 File Offset: 0x000C6034
		[DataMember]
		public object GroupName { get; set; }

		// Token: 0x06003EA0 RID: 16032 RVA: 0x000C7E3D File Offset: 0x000C603D
		public override string ToString()
		{
			return Convert.ToString(this.GroupName, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003EA1 RID: 16033
		protected internal abstract AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues);
	}
}
