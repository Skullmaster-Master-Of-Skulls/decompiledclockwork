using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000897 RID: 2199
	public class SpreadsheetContextMenus
	{
		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x000FF012 File Offset: 0x000FD212
		// (set) Token: 0x060051CC RID: 20940 RVA: 0x000FF01A File Offset: 0x000FD21A
		public ISpreadsheet Owner { get; set; }

		// Token: 0x060051CD RID: 20941 RVA: 0x000FF023 File Offset: 0x000FD223
		internal SpreadsheetContextMenus(ISpreadsheet owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x060051CE RID: 20942 RVA: 0x000FF032 File Offset: 0x000FD232
		[Description("Gets the cell context menu.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SpreadsheetContextMenu CellContextMenu
		{
			get
			{
				if (this._cellContextMenu == null)
				{
					this._cellContextMenu = this.CreateContextMenu("SpreadsheetCellContextMenu");
				}
				return this._cellContextMenu;
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x000FF053 File Offset: 0x000FD253
		[Description("Gets the row header context menu.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SpreadsheetContextMenu RowHeaderContextMenu
		{
			get
			{
				if (this._rowHeaderContextMenu == null)
				{
					this._rowHeaderContextMenu = this.CreateContextMenu("SpreadsheetRowHeaderContextMenu");
				}
				return this._rowHeaderContextMenu;
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x060051D0 RID: 20944 RVA: 0x000FF074 File Offset: 0x000FD274
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the col header context menu.")]
		public SpreadsheetContextMenu ColumnHeaderContextMenu
		{
			get
			{
				if (this._columnHeaderContextMenu == null)
				{
					this._columnHeaderContextMenu = this.CreateContextMenu("SpreadsheetColumnHeaderContextMenu");
				}
				return this._columnHeaderContextMenu;
			}
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x000FF098 File Offset: 0x000FD298
		private SpreadsheetContextMenu CreateContextMenu(string id)
		{
			return new SpreadsheetContextMenu
			{
				EnableViewState = false,
				RenderMode = RenderMode.Lightweight,
				EnableImageSprites = true,
				ID = id,
				EnableRoundedCorners = true,
				EnableShadows = true
			};
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x000FF0D8 File Offset: 0x000FD2D8
		internal void PopulateContextMenu(SpreadsheetContextMenu contextMenu, SpreadsheetToolInfo[] defaultCommands)
		{
			contextMenu.Skin = this.Owner.ResolvedSkin;
			contextMenu.EnableEmbeddedSkins = this.Owner.EnableEmbeddedSkins;
			if (contextMenu.Items.Count == 0 || contextMenu.IsDefault)
			{
				contextMenu.Items.Clear();
				foreach (SpreadsheetToolInfo spreadsheetToolInfo in defaultCommands)
				{
					contextMenu.Items.Add(new RadMenuItem
					{
						Text = this.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey),
						Value = spreadsheetToolInfo.CommandName,
						SpriteCssClass = spreadsheetToolInfo.IconClass
					});
				}
				contextMenu.IsDefault = true;
			}
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x000FF17E File Offset: 0x000FD37E
		protected string GetLocalizedString(string key)
		{
			return this.Owner.Localization.GetString(key);
		}

		// Token: 0x04001400 RID: 5120
		private SpreadsheetContextMenu _cellContextMenu;

		// Token: 0x04001401 RID: 5121
		private SpreadsheetContextMenu _rowHeaderContextMenu;

		// Token: 0x04001402 RID: 5122
		private SpreadsheetContextMenu _columnHeaderContextMenu;

		// Token: 0x04001403 RID: 5123
		internal static readonly SpreadsheetToolInfo[] DefaultCellContextMenuItems = new SpreadsheetToolInfo[]
		{
			new SpreadsheetToolInfo("CommandCut", string.Empty, string.Empty, string.Empty, "t-efi t-efi-cut", "ContextMenuCut"),
			new SpreadsheetToolInfo("CommandCopy", string.Empty, string.Empty, string.Empty, "t-efi t-efi-copy", "ContextMenuCopy"),
			new SpreadsheetToolInfo("CommandPaste", string.Empty, string.Empty, string.Empty, "t-efi t-efi-paste", "ContextMenuPaste")
		};

		// Token: 0x04001404 RID: 5124
		internal static readonly SpreadsheetToolInfo[] DefaultRowHeaderContextMenuItems = new SpreadsheetToolInfo[]
		{
			new SpreadsheetToolInfo("CommandHideRow", string.Empty, string.Empty, string.Empty, string.Empty, "ContextMenuHideRow"),
			new SpreadsheetToolInfo("CommandUnhideRow", string.Empty, string.Empty, string.Empty, string.Empty, "ContextMenuUnhideRow"),
			new SpreadsheetToolInfo("CommandDeleteRow", string.Empty, string.Empty, string.Empty, string.Empty, "ContextMenuDeleteRow")
		};

		// Token: 0x04001405 RID: 5125
		internal static readonly SpreadsheetToolInfo[] DefaultColumnHeaderContextMenuItems = new SpreadsheetToolInfo[]
		{
			new SpreadsheetToolInfo("CommandHideColumn", string.Empty, string.Empty, string.Empty, string.Empty, "ContextMenuHideColumn"),
			new SpreadsheetToolInfo("CommandUnhideColumn", string.Empty, string.Empty, string.Empty, string.Empty, "ContextMenuUnhideColumn"),
			new SpreadsheetToolInfo("CommandDeleteColumn", string.Empty, string.Empty, string.Empty, string.Empty, "ContextMenuDeleteColumn")
		};
	}
}
