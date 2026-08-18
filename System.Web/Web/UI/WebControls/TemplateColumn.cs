using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000661 RID: 1633
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TemplateColumn : DataGridColumn
	{
		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x00140542 File Offset: 0x0013F542
		// (set) Token: 0x06004FCC RID: 20428 RVA: 0x0014054A File Offset: 0x0013F54A
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		[DefaultValue(null)]
		[WebSysDescription("TemplateColumn_EditItemTemplate")]
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

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x00140559 File Offset: 0x0013F559
		// (set) Token: 0x06004FCE RID: 20430 RVA: 0x00140561 File Offset: 0x0013F561
		[TemplateContainer(typeof(DataGridItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("TemplateColumn_FooterTemplate")]
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

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x00140570 File Offset: 0x0013F570
		// (set) Token: 0x06004FD0 RID: 20432 RVA: 0x00140578 File Offset: 0x0013F578
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		[Browsable(false)]
		[WebSysDescription("TemplateColumn_HeaderTemplate")]
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

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x00140587 File Offset: 0x0013F587
		// (set) Token: 0x06004FD2 RID: 20434 RVA: 0x0014058F File Offset: 0x0013F58F
		[DefaultValue(null)]
		[WebSysDescription("TemplateColumn_ItemTemplate")]
		[TemplateContainer(typeof(DataGridItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x06004FD3 RID: 20435 RVA: 0x001405A0 File Offset: 0x0013F5A0
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			ITemplate template = null;
			switch (itemType)
			{
			case ListItemType.Header:
				template = this.headerTemplate;
				goto IL_57;
			case ListItemType.Footer:
				template = this.footerTemplate;
				goto IL_57;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
				break;
			case ListItemType.EditItem:
				if (this.editItemTemplate != null)
				{
					template = this.editItemTemplate;
					goto IL_57;
				}
				break;
			default:
				goto IL_57;
			}
			template = this.itemTemplate;
			IL_57:
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

		// Token: 0x04002CFA RID: 11514
		private ITemplate headerTemplate;

		// Token: 0x04002CFB RID: 11515
		private ITemplate footerTemplate;

		// Token: 0x04002CFC RID: 11516
		private ITemplate itemTemplate;

		// Token: 0x04002CFD RID: 11517
		private ITemplate editItemTemplate;
	}
}
