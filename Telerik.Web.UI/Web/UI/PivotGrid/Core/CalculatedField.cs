using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000677 RID: 1655
	[DataContract]
	public abstract class CalculatedField : INamed
	{
		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x000C3C85 File Offset: 0x000C1E85
		// (set) Token: 0x06003C6F RID: 15471 RVA: 0x000C3C8D File Offset: 0x000C1E8D
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000C3C96 File Offset: 0x000C1E96
		// (set) Token: 0x06003C71 RID: 15473 RVA: 0x000C3C9E File Offset: 0x000C1E9E
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x06003C72 RID: 15474
		protected internal abstract IEnumerable<RequiredField> RequiredFields();

		// Token: 0x06003C73 RID: 15475
		protected internal abstract AggregateValue CalculateValue(IAggregateValues aggregateValues);
	}
}
