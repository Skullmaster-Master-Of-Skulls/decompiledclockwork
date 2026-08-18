using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000768 RID: 1896
	[ParseChildren(false)]
	[PersistChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridCalculatedItemsCollection : StronglyTypedStateManagedCollection<PivotGridCalculatedItem>
	{
		// Token: 0x060042DD RID: 17117 RVA: 0x000D0A5C File Offset: 0x000CEC5C
		protected override void SetDirtyObject(object stateManagerObject)
		{
			StateManager stateManager = stateManagerObject as StateManager;
			if (stateManager != null)
			{
				stateManager.SetDirty();
			}
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x000D0A79 File Offset: 0x000CEC79
		public override void Add(PivotGridCalculatedItem item)
		{
			base.Add(item);
		}
	}
}
