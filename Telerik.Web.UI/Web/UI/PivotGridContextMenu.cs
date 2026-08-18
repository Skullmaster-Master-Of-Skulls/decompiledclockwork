using System;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x02000DF1 RID: 3569
	public class PivotGridContextMenu : RadContextMenu
	{
		// Token: 0x0600849C RID: 33948 RVA: 0x001E3FC4 File Offset: 0x001E21C4
		public PivotGridContextMenu(RadPivotGrid ownerPivotGrid)
		{
			this.ownerPivotGrid = ownerPivotGrid;
			this.EnableEmbeddedScripts = ownerPivotGrid.EnableEmbeddedScripts;
			this.EnableEmbeddedSkins = ownerPivotGrid.EnableEmbeddedSkins;
			this.EnableEmbeddedBaseStylesheet = ownerPivotGrid.EnableEmbeddedBaseStylesheet;
			this.RenderMode = ownerPivotGrid.ResolvedRenderMode;
			base.PreRender += this.PivotGridContextMenu_PreRender;
			if (!base.DesignMode)
			{
				this.EnableTheming = ownerPivotGrid.EnableTheming;
			}
			else
			{
				ownerPivotGrid.Controls.Add(this);
			}
			this.Visible = !base.DesignMode;
		}

		// Token: 0x170029F2 RID: 10738
		// (get) Token: 0x0600849D RID: 33949 RVA: 0x001E4051 File Offset: 0x001E2251
		public RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.ownerPivotGrid;
			}
		}

		// Token: 0x170029F3 RID: 10739
		// (get) Token: 0x0600849E RID: 33950 RVA: 0x001E4059 File Offset: 0x001E2259
		private PivotGridStrings Localization
		{
			get
			{
				return this.ownerPivotGrid.Localization;
			}
		}

		// Token: 0x0600849F RID: 33951 RVA: 0x001E4066 File Offset: 0x001E2266
		internal void Initialize()
		{
			base.Items.Clear();
			this.GenerateMenuItems(base.Items);
		}

		// Token: 0x060084A0 RID: 33952 RVA: 0x001E4080 File Offset: 0x001E2280
		private void PivotGridContextMenu_PreRender(object sender, EventArgs e)
		{
			this.Skin = this.ownerPivotGrid.RuntimeSkin;
			this.CssClass = string.Format(CultureInfo.InvariantCulture, "PivotGridContextMenu PivotGridContextMenu_{0} {1}", new object[]
			{
				this.ownerPivotGrid.RuntimeSkin,
				"rpgContextMenu" + this.OwnerPivotGrid.ConfigurationPanelSettings.Position
			});
		}

		// Token: 0x060084A1 RID: 33953 RVA: 0x001E40EC File Offset: 0x001E22EC
		private void GenerateMenuItems(RadMenuItemCollection collection)
		{
			if (this.OwnerPivotGrid.EnableZoneContextMenu)
			{
				this.CreateZoneMenuItem(collection);
			}
			if (this.OwnerPivotGrid.EnableConfigurationPanel)
			{
				this.CreateConfigurationPanelMenuItems(collection);
				if (this.OwnerPivotGrid.ConfigurationPanelSettings.EnableFieldsContextMenu)
				{
					this.CreateConfigurationPanelFieldsMenuItems(collection);
				}
			}
		}

		// Token: 0x060084A2 RID: 33954 RVA: 0x001E413C File Offset: 0x001E233C
		private void CreateZoneMenuItem(RadMenuItemCollection collection)
		{
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ZoneContextMenuRefresh,
				Value = "Refresh"
			});
			collection.Add(new RadMenuItem
			{
				Value = "ZoneSeparator",
				IsSeparator = true
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ZoneContextMenuHide,
				Value = "Hide"
			});
			if (!this.ownerPivotGrid.IsBoundToOlap)
			{
				collection.Add(new RadMenuItem
				{
					Text = this.Localization.ZoneContextMenuSummarizeBySettings,
					Value = "ZoneSummarizeBySettings"
				});
			}
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ZoneContextMenuShowFieldsWindow,
				Value = "ShowHideFieldsWindow"
			});
		}

		// Token: 0x060084A3 RID: 33955 RVA: 0x001E421C File Offset: 0x001E241C
		private void CreateConfigurationPanelMenuItems(RadMenuItemCollection collection)
		{
			RadMenuItem item = this.CreateConfigurationPanelMenuItem(this.Localization.ConfigurationPanelContextMenuStacked, "Stacked");
			collection.Add(item);
			item = this.CreateConfigurationPanelMenuItem(this.Localization.ConfigurationPanelContextMenuSideBySide, "SideBySide");
			collection.Add(item);
			item = this.CreateConfigurationPanelMenuItem(this.Localization.ConfigurationPanelContextMenuTwoByTwo, "TwoByTwo");
			collection.Add(item);
			item = this.CreateConfigurationPanelMenuItem(this.Localization.ConfigurationPanelContextMenuOneByFour, "OneByFour");
			collection.Add(item);
		}

		// Token: 0x060084A4 RID: 33956 RVA: 0x001E42A4 File Offset: 0x001E24A4
		private RadMenuItem CreateConfigurationPanelMenuItem(string text, string value)
		{
			RadMenuItem radMenuItem = new RadMenuItem();
			radMenuItem.EnableImageSprite = (this.ownerPivotGrid.ResolvedRenderMode != RenderMode.Lightweight);
			radMenuItem.Text = text;
			radMenuItem.Value = value;
			radMenuItem.PreRender += this.menuItem_PreRender;
			radMenuItem.CssClass = "rpg" + radMenuItem.Value;
			return radMenuItem;
		}

		// Token: 0x060084A5 RID: 33957 RVA: 0x001E4305 File Offset: 0x001E2505
		private void menuItem_PreRender(object sender, EventArgs e)
		{
		}

		// Token: 0x060084A6 RID: 33958 RVA: 0x001E4308 File Offset: 0x001E2508
		private void CreateConfigurationPanelFieldsMenuItems(RadMenuItemCollection collection)
		{
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveUp,
				Value = "MoveUp"
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveDown,
				Value = "MoveDown"
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveToBeginning,
				Value = "MoveToBeginning"
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveToEnd,
				Value = "MoveToEnd"
			});
			collection.Add(new RadMenuItem
			{
				Value = "ConfigurationPanelSeparator1",
				IsSeparator = true
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveToFilterFields,
				Value = "MoveToFilterFields",
				Enabled = false
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveToRowFields,
				Value = "MoveToRowFields"
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveToColumnFields,
				Value = "MoveToColumnFields"
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuMoveToAggregateFields,
				Value = "MoveToAggregateFields"
			});
			collection.Add(new RadMenuItem
			{
				Value = "ConfigurationPanelSeparator2",
				IsSeparator = true
			});
			collection.Add(new RadMenuItem
			{
				Text = this.Localization.ConfigurationPanelContextMenuHideField,
				Value = "HideField"
			});
			if (!this.OwnerPivotGrid.IsBoundToOlap)
			{
				collection.Add(new RadMenuItem
				{
					Value = "ConfigurationPanelSeparator3",
					IsSeparator = true
				});
				collection.Add(new RadMenuItem
				{
					Text = this.Localization.ConfigurationPanelContextMenuSummarizeBySettings,
					Value = "SummarizeBySettings"
				});
			}
		}

		// Token: 0x040024F3 RID: 9459
		private const string ClassName = "rpgContextMenu";

		// Token: 0x040024F4 RID: 9460
		private readonly RadPivotGrid ownerPivotGrid;
	}
}
