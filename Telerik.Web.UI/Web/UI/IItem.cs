using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200005E RID: 94
	internal interface IItem
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002C1 RID: 705
		// (set) Token: 0x060002C2 RID: 706
		object DataItem { get; set; }

		// Token: 0x060002C3 RID: 707
		void DataBind();

		// Token: 0x060002C4 RID: 708
		void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth);

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002C5 RID: 709
		IList Children { get; }
	}
}
