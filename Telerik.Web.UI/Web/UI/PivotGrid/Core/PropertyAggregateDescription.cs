using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C52 RID: 3154
	[DataContract]
	public sealed class PropertyAggregateDescription : PropertyAggregateDescriptionBase
	{
		// Token: 0x06007737 RID: 30519 RVA: 0x001BA9B4 File Offset: 0x001B8BB4
		protected override Cloneable CreateInstanceCore()
		{
			return new PropertyAggregateDescription();
		}

		// Token: 0x06007738 RID: 30520 RVA: 0x001BA9BB File Offset: 0x001B8BBB
		protected override void CloneOverride(Cloneable source)
		{
		}
	}
}
