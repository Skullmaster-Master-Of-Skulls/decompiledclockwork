using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CCA RID: 3274
	[DataContract]
	public sealed class PropertyGroupDescription : PropertyGroupDescriptionBase
	{
		// Token: 0x06007A81 RID: 31361 RVA: 0x001C1D3F File Offset: 0x001BFF3F
		protected override Cloneable CreateInstanceCore()
		{
			return new PropertyGroupDescription();
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x001C1D46 File Offset: 0x001BFF46
		protected override void CloneOverride(Cloneable source)
		{
		}
	}
}
