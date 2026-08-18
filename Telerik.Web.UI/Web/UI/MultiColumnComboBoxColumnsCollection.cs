using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005EF RID: 1519
	[ParseChildren(typeof(MultiColumnComboBoxColumn))]
	public class MultiColumnComboBoxColumnsCollection : StronglyTypedStateManagedCollection<MultiColumnComboBoxColumn>
	{
		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000B632E File Offset: 0x000B452E
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}
