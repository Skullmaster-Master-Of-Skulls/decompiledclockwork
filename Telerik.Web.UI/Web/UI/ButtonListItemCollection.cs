using System;

namespace Telerik.Web.UI
{
	// Token: 0x020000B6 RID: 182
	public class ButtonListItemCollection : StronglyTypedStateManagedCollection<ButtonListItem>
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x0001C399 File Offset: 0x0001A599
		public ButtonListItemCollection(RadButtonList parent)
		{
			this.parent = parent;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			ButtonListItem buttonListItem = value as ButtonListItem;
			buttonListItem.Parent = this.parent;
			if (this.parent != null)
			{
				this.parent.RaiseItemCreated(buttonListItem);
			}
		}

		// Token: 0x0400017C RID: 380
		protected RadButtonList parent;
	}
}
