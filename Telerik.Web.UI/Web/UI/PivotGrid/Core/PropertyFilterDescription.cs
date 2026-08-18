using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D50 RID: 3408
	[DataContract]
	public sealed class PropertyFilterDescription : PropertyFilterDescriptionBase
	{
		// Token: 0x06007F1B RID: 32539 RVA: 0x001D11A4 File Offset: 0x001CF3A4
		protected override Cloneable CreateInstanceCore()
		{
			return new PropertyFilterDescription();
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x001D11AB File Offset: 0x001CF3AB
		protected override void CloneOverride(Cloneable source)
		{
		}
	}
}
