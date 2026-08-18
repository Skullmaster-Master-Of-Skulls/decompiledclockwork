using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001070 RID: 4208
	public sealed class EditorContextMenuCollection : StronglyTypedStateManagedCollection<EditorContextMenu>
	{
		// Token: 0x0600A9B2 RID: 43442 RVA: 0x0024D9EC File Offset: 0x0024BBEC
		internal EditorContextMenuCollection()
		{
		}

		// Token: 0x0600A9B3 RID: 43443 RVA: 0x0024D9F4 File Offset: 0x0024BBF4
		public override void Add(EditorContextMenu item)
		{
			EditorContextMenu editorContextMenu = this.FindByTagName(item.TagName);
			if (editorContextMenu != null)
			{
				this.Remove(editorContextMenu);
			}
			base.Add(item);
		}

		// Token: 0x0600A9B4 RID: 43444 RVA: 0x0024DA20 File Offset: 0x0024BC20
		public EditorContextMenu FindByTagName(string tagName)
		{
			foreach (object obj in this)
			{
				EditorContextMenu editorContextMenu = (EditorContextMenu)obj;
				if (editorContextMenu.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase))
				{
					return editorContextMenu;
				}
			}
			return null;
		}

		// Token: 0x17003680 RID: 13952
		// (get) Token: 0x0600A9B5 RID: 43445 RVA: 0x0024DA84 File Offset: 0x0024BC84
		public EditorContextMenuCollection EnabledContextMenus
		{
			get
			{
				EditorContextMenuCollection editorContextMenuCollection = new EditorContextMenuCollection();
				foreach (object obj in this)
				{
					EditorContextMenu editorContextMenu = (EditorContextMenu)obj;
					if (editorContextMenu.Enabled)
					{
						editorContextMenuCollection.Add(editorContextMenu);
					}
				}
				return editorContextMenuCollection;
			}
		}

		// Token: 0x0600A9B6 RID: 43446 RVA: 0x0024DAE8 File Offset: 0x0024BCE8
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}
	}
}
