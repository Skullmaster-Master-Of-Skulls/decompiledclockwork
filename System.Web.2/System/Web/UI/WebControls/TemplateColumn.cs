using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F2 RID: 1266
	public class TemplateColumn : DataGridColumn
	{
		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003F0B RID: 16139 RVA: 0x000CAB2D File Offset: 0x000C8D2D
		// (set) Token: 0x06003F0C RID: 16140 RVA: 0x000CAB35 File Offset: 0x000C8D35
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateColumn_EditItemTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x000CAB44 File Offset: 0x000C8D44
		// (set) Token: 0x06003F0E RID: 16142 RVA: 0x000CAB4C File Offset: 0x000C8D4C
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateColumn_FooterTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000CAB5B File Offset: 0x000C8D5B
		// (set) Token: 0x06003F10 RID: 16144 RVA: 0x000CAB63 File Offset: 0x000C8D63
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateColumn_HeaderTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06003F11 RID: 16145 RVA: 0x000CAB72 File Offset: 0x000C8D72
		// (set) Token: 0x06003F12 RID: 16146 RVA: 0x000CAB7A File Offset: 0x000C8D7A
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateColumn_ItemTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000CAB8C File Offset: 0x000C8D8C
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			ITemplate template = null;
			switch (itemType)
			{
			case ListItemType.Header:
				template = this.headerTemplate;
				goto IL_55;
			case ListItemType.Footer:
				template = this.footerTemplate;
				goto IL_55;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
				break;
			case ListItemType.EditItem:
				if (this.editItemTemplate != null)
				{
					template = this.editItemTemplate;
					goto IL_55;
				}
				break;
			default:
				goto IL_55;
			}
			template = this.itemTemplate;
			IL_55:
			if (template != null)
			{
				cell.Text = string.Empty;
				template.InstantiateIn(cell);
				return;
			}
			if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem || itemType == ListItemType.SelectedItem || itemType == ListItemType.EditItem)
			{
				cell.Text = "&nbsp;";
			}
		}

		// Token: 0x04002429 RID: 9257
		private ITemplate headerTemplate;

		// Token: 0x0400242A RID: 9258
		private ITemplate footerTemplate;

		// Token: 0x0400242B RID: 9259
		private ITemplate itemTemplate;

		// Token: 0x0400242C RID: 9260
		private ITemplate editItemTemplate;
	}
}
