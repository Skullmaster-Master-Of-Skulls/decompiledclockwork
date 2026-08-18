using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011F5 RID: 4597
	public class TreeListTemplateColumnEditor : TreeListColumnEditor
	{
		// Token: 0x0600BDB3 RID: 48563 RVA: 0x002A06C9 File Offset: 0x0029E8C9
		public TreeListTemplateColumnEditor(TreeListEditableColumn column) : base(column)
		{
		}

		// Token: 0x17003D30 RID: 15664
		// (get) Token: 0x0600BDB4 RID: 48564 RVA: 0x002A06D2 File Offset: 0x0029E8D2
		// (set) Token: 0x0600BDB5 RID: 48565 RVA: 0x002A06DA File Offset: 0x0029E8DA
		public ITemplate Template { get; private set; }

		// Token: 0x17003D31 RID: 15665
		// (get) Token: 0x0600BDB6 RID: 48566 RVA: 0x002A06E3 File Offset: 0x0029E8E3
		// (set) Token: 0x0600BDB7 RID: 48567 RVA: 0x002A06EB File Offset: 0x0029E8EB
		public Control ContainerControl { get; private set; }

		// Token: 0x0600BDB8 RID: 48568 RVA: 0x002A06F4 File Offset: 0x0029E8F4
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			this.ContainerControl = container;
			TreeListTemplateColumn treeListTemplateColumn = base.Column as TreeListTemplateColumn;
			if (treeListTemplateColumn != null)
			{
				if (treeListTemplateColumn.ReadOnly)
				{
					this.Template = treeListTemplateColumn.ItemTemplate;
				}
				else if (editItem is ITreeListInsertItem)
				{
					this.Template = (treeListTemplateColumn.InsertItemTemplate ?? treeListTemplateColumn.EditItemTemplate);
				}
				else
				{
					this.Template = treeListTemplateColumn.EditItemTemplate;
				}
			}
			if (this.Template != null)
			{
				this.Template.InstantiateIn(this.ContainerControl);
			}
		}

		// Token: 0x0600BDB9 RID: 48569 RVA: 0x002A0772 File Offset: 0x0029E972
		public override void SetValues(IEnumerable values)
		{
		}

		// Token: 0x0600BDBA RID: 48570 RVA: 0x002A0774 File Offset: 0x0029E974
		public override IEnumerable GetValues()
		{
			IBindableTemplate bindableTemplate = this.Template as IBindableTemplate;
			if (bindableTemplate != null)
			{
				return bindableTemplate.ExtractValues(this.ContainerControl.BindingContainer);
			}
			return null;
		}
	}
}
