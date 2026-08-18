using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B4 RID: 1716
	public abstract class PropertyFieldInfo : PivotFieldInfo
	{
		// Token: 0x06003DD1 RID: 15825
		public abstract object GetValue(object item);

		// Token: 0x06003DD2 RID: 15826
		public abstract void SetValue(object item, object fieldValue);
	}
}
