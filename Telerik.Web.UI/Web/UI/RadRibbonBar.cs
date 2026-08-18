using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E39 RID: 3641
	[DefaultEvent("ButtonClick")]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadRibbonBar), "Telerik.Web.UI.RibbonBar.png")]
	[ClientScriptResource("Telerik.Web.UI.RadRibbonBar", "Telerik.Web.UI.RibbonBar.RadRibbonBarScripts.js")]
	[EmbeddedSkin("RibbonBar", typeof(RadRibbonBar))]
	[EmbeddedSkin("RibbonBar", "Default", typeof(RadRibbonBar))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadRibbonBar))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(jQueryPlugins), 0)]
	[RequiredScript(typeof(RibbonBarBaseScripts), 1)]
	[RequiredScript(typeof(MaterialRipple))]
	[Designer("Telerik.Web.Design.RadRibbonBarDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[XmlRoot("RibbonBar")]
	[ParseChildren(typeof(RibbonBarTab), ChildrenAsProperties = true, DefaultProperty = "Tabs")]
	[ToolboxData("<{0}:RadRibbonBar runat=\"server\"></{0}:RadRibbonBar>")]
	[LightweightRendering]
	[DefaultProperty("Tabs")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadRibbonBar : RadWebControl, IPostBackEventHandler, IPostBackDataHandler, IXmlSerializable
	{
		// Token: 0x17002B8F RID: 11151
		// (get) Token: 0x060089A4 RID: 35236 RVA: 0x001F64A5 File Offset: 0x001F46A5
		// (set) Token: 0x060089A5 RID: 35237 RVA: 0x001F64AD File Offset: 0x001F46AD
		public bool RenderInactiveContextualTabGroups { get; set; }

		// Token: 0x060089A6 RID: 35238 RVA: 0x001F64B8 File Offset: 0x001F46B8
		private List<RibbonBarTab> GetContextualTabs()
		{
			List<RibbonBarTab> list = new List<RibbonBarTab>();
			foreach (RibbonBarContextualTabGroup ribbonBarContextualTabGroup in this.ContextualTabGroups)
			{
				foreach (RibbonBarTab item in ribbonBarContextualTabGroup.Tabs)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060089A7 RID: 35239 RVA: 0x001F6550 File Offset: 0x001F4750
		private List<RibbonBarTab> GetContextualTabsToRender()
		{
			List<RibbonBarTab> list = new List<RibbonBarTab>();
			foreach (RibbonBarContextualTabGroup ribbonBarContextualTabGroup in this.ContextualTabGroups)
			{
				if (this.RenderInactiveContextualTabGroups || ribbonBarContextualTabGroup.Active)
				{
					foreach (RibbonBarTab ribbonBarTab in ribbonBarContextualTabGroup.Tabs)
					{
						if (ribbonBarTab.Visible)
						{
							list.Add(ribbonBarTab);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x001F6600 File Offset: 0x001F4800
		internal List<RibbonBarGroup> GetContextualTabGroupsToRender()
		{
			List<RibbonBarGroup> list = new List<RibbonBarGroup>();
			foreach (RibbonBarContextualTabGroup ribbonBarContextualTabGroup in this.ContextualTabGroups)
			{
				if (this.RenderInactiveContextualTabGroups || ribbonBarContextualTabGroup.Active)
				{
					foreach (RibbonBarTab ribbonBarTab in ribbonBarContextualTabGroup.GetVisibleTabs())
					{
						list.AddRange(ribbonBarTab.GetVisibleGroups());
					}
				}
			}
			return list;
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x001F66AC File Offset: 0x001F48AC
		internal bool HasContextualTabs()
		{
			return this.GetContextualTabs().Count > 0;
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x001F66C0 File Offset: 0x001F48C0
		internal bool HasContextualTabGroupsToRender()
		{
			foreach (RibbonBarContextualTabGroup ribbonBarContextualTabGroup in this.ContextualTabGroups)
			{
				if (this.RenderInactiveContextualTabGroups || ribbonBarContextualTabGroup.Active)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x001F6724 File Offset: 0x001F4924
		private string GetNextAvailableContextualTabDefaultColor()
		{
			if (this._currentContextualTabGroupColorIndex == this.ContextualTabGroupDefaultColors.Length)
			{
				this._currentContextualTabGroupColorIndex = 0;
			}
			return this.ContextualTabGroupDefaultColors[this._currentContextualTabGroupColorIndex++];
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x001F6760 File Offset: 0x001F4960
		internal void SetContextualTabDefaultColor(RibbonBarContextualTabGroup contextualTab)
		{
			string nextAvailableContextualTabDefaultColor = this.GetNextAvailableContextualTabDefaultColor();
			if (!string.IsNullOrEmpty(nextAvailableContextualTabDefaultColor))
			{
				contextualTab.BackColor = ColorTranslator.FromHtml(nextAvailableContextualTabDefaultColor);
			}
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x001F6788 File Offset: 0x001F4988
		internal bool HasQuickAccessEnabledItems()
		{
			foreach (RibbonBarGroup ribbonBarGroup in this.GetGroupsToRender())
			{
				foreach (RibbonBarItem ribbonBarItem in ribbonBarGroup.GetVisibleFunctionalItems())
				{
					RibbonBarClickableItem ribbonBarClickableItem = ribbonBarItem as RibbonBarClickableItem;
					if (ribbonBarClickableItem != null && ribbonBarClickableItem.QuickAccess != RibbonBarItemQuickAccess.Disabled)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060089AE RID: 35246 RVA: 0x001F682C File Offset: 0x001F4A2C
		internal List<RibbonBarClickableItem> GetQuickAccessEnabledItems()
		{
			List<RibbonBarClickableItem> list = new List<RibbonBarClickableItem>();
			foreach (RibbonBarGroup ribbonBarGroup in this.GetGroupsToRender())
			{
				foreach (RibbonBarItem ribbonBarItem in ribbonBarGroup.GetVisibleFunctionalItems())
				{
					RibbonBarClickableItem ribbonBarClickableItem = ribbonBarItem as RibbonBarClickableItem;
					if (ribbonBarClickableItem != null && ribbonBarClickableItem.Visible && ribbonBarClickableItem.QuickAccess != RibbonBarItemQuickAccess.Disabled)
					{
						list.Add(ribbonBarClickableItem);
					}
				}
			}
			return list;
		}

		// Token: 0x060089AF RID: 35247 RVA: 0x001F68E0 File Offset: 0x001F4AE0
		private string GetClickableItemHierarchicalIndex(RibbonBarClickableItem item)
		{
			RibbonBarGroup group = item.Group;
			if (group == null || group.Tab == null || group.Tab.RibbonBar == null)
			{
				return null;
			}
			RibbonBarTab tab = group.Tab;
			int num = group.GetVisibleFunctionalItems().IndexOf(item);
			int num2 = tab.GetVisibleGroups().IndexOf(group);
			int num3 = tab.RibbonBar.GetTabsToRender().IndexOf(tab);
			return string.Format("{0}:{1}:{2}", num3, num2, num);
		}

		// Token: 0x060089B0 RID: 35248 RVA: 0x001F6964 File Offset: 0x001F4B64
		private List<string> GetQuickAccessEnabledItemsHierarchicalIndices()
		{
			List<RibbonBarClickableItem> quickAccessEnabledItems = this.GetQuickAccessEnabledItems();
			List<string> list = new List<string>();
			foreach (RibbonBarClickableItem item in quickAccessEnabledItems)
			{
				list.Add(this.GetClickableItemHierarchicalIndex(item));
			}
			return list;
		}

		// Token: 0x1400014D RID: 333
		// (add) Token: 0x060089B1 RID: 35249 RVA: 0x001F69C8 File Offset: 0x001F4BC8
		// (remove) Token: 0x060089B2 RID: 35250 RVA: 0x001F69DB File Offset: 0x001F4BDB
		public event RibbonBarSelectedTabChangeEventHandler SelectedTabChange
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.SelectedTabChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.SelectedTabChangeEvent, value);
			}
		}

		// Token: 0x1400014E RID: 334
		// (add) Token: 0x060089B3 RID: 35251 RVA: 0x001F69EE File Offset: 0x001F4BEE
		// (remove) Token: 0x060089B4 RID: 35252 RVA: 0x001F6A01 File Offset: 0x001F4C01
		public event RibbonBarButtonClickEventHandler ButtonClick
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.ButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.ButtonClickEvent, value);
			}
		}

		// Token: 0x1400014F RID: 335
		// (add) Token: 0x060089B5 RID: 35253 RVA: 0x001F6A14 File Offset: 0x001F4C14
		// (remove) Token: 0x060089B6 RID: 35254 RVA: 0x001F6A27 File Offset: 0x001F4C27
		public event RibbonBarSplitButtonClickEventHandler SplitButtonClick
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.SplitButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.SplitButtonClickEvent, value);
			}
		}

		// Token: 0x14000150 RID: 336
		// (add) Token: 0x060089B7 RID: 35255 RVA: 0x001F6A3A File Offset: 0x001F4C3A
		// (remove) Token: 0x060089B8 RID: 35256 RVA: 0x001F6A4D File Offset: 0x001F4C4D
		public event RibbonBarMenuItemClickEventHandler MenuItemClick
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.MenuItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.MenuItemClickEvent, value);
			}
		}

		// Token: 0x14000151 RID: 337
		// (add) Token: 0x060089B9 RID: 35257 RVA: 0x001F6A60 File Offset: 0x001F4C60
		// (remove) Token: 0x060089BA RID: 35258 RVA: 0x001F6A73 File Offset: 0x001F4C73
		public event RibbonBarLauncherClickEventHandler LauncherClick
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.LauncherClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.LauncherClickEvent, value);
			}
		}

		// Token: 0x14000152 RID: 338
		// (add) Token: 0x060089BB RID: 35259 RVA: 0x001F6A86 File Offset: 0x001F4C86
		// (remove) Token: 0x060089BC RID: 35260 RVA: 0x001F6A99 File Offset: 0x001F4C99
		public event RibbonBarButtonToggleEventHandler ButtonToggle
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.ButtonToggleEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.ButtonToggleEvent, value);
			}
		}

		// Token: 0x14000153 RID: 339
		// (add) Token: 0x060089BD RID: 35261 RVA: 0x001F6AAC File Offset: 0x001F4CAC
		// (remove) Token: 0x060089BE RID: 35262 RVA: 0x001F6ABF File Offset: 0x001F4CBF
		public event RibbonBarToggleListToggleEventHandler ToggleListToggle
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.ToggleListToggleEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.ToggleListToggleEvent, value);
			}
		}

		// Token: 0x14000154 RID: 340
		// (add) Token: 0x060089BF RID: 35263 RVA: 0x001F6AD2 File Offset: 0x001F4CD2
		// (remove) Token: 0x060089C0 RID: 35264 RVA: 0x001F6AE5 File Offset: 0x001F4CE5
		public event RibbonBarApplicationMenuItemClickEventHandler ApplicationMenuItemClick
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.ApplicationMenuItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.ApplicationMenuItemClickEvent, value);
			}
		}

		// Token: 0x14000155 RID: 341
		// (add) Token: 0x060089C1 RID: 35265 RVA: 0x001F6AF8 File Offset: 0x001F4CF8
		// (remove) Token: 0x060089C2 RID: 35266 RVA: 0x001F6B0B File Offset: 0x001F4D0B
		public event RibbonBarComboBoxSelectedIndexChangedEventHandler ComboBoxSelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.ComboBoxSelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.ComboBoxSelectedIndexChangedEvent, value);
			}
		}

		// Token: 0x14000156 RID: 342
		// (add) Token: 0x060089C3 RID: 35267 RVA: 0x001F6B1E File Offset: 0x001F4D1E
		// (remove) Token: 0x060089C4 RID: 35268 RVA: 0x001F6B31 File Offset: 0x001F4D31
		public event RibbonBarComboBoxTextChangedEventHandler ComboBoxTextChanged
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.ComboBoxTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.ComboBoxTextChangedEvent, value);
			}
		}

		// Token: 0x14000157 RID: 343
		// (add) Token: 0x060089C5 RID: 35269 RVA: 0x001F6B44 File Offset: 0x001F4D44
		// (remove) Token: 0x060089C6 RID: 35270 RVA: 0x001F6B57 File Offset: 0x001F4D57
		public event RibbonBarDropDownSelectedIndexChangedEventHandler DropDownSelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.DropDownSelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.DropDownSelectedIndexChangedEvent, value);
			}
		}

		// Token: 0x14000158 RID: 344
		// (add) Token: 0x060089C7 RID: 35271 RVA: 0x001F6B6A File Offset: 0x001F4D6A
		// (remove) Token: 0x060089C8 RID: 35272 RVA: 0x001F6B7D File Offset: 0x001F4D7D
		public event RibbonBarNumericTextBoxValueChangedEventHandler NumericTextBoxValueChanged
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.NumericTextBoxValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.NumericTextBoxValueChangedEvent, value);
			}
		}

		// Token: 0x14000159 RID: 345
		// (add) Token: 0x060089C9 RID: 35273 RVA: 0x001F6B90 File Offset: 0x001F4D90
		// (remove) Token: 0x060089CA RID: 35274 RVA: 0x001F6BA3 File Offset: 0x001F4DA3
		public event RibbonBarColorPickerColorChangedEventHandler ColorPickerColorChanged
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.ColorPickerColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.ColorPickerColorChangedEvent, value);
			}
		}

		// Token: 0x1400015A RID: 346
		// (add) Token: 0x060089CB RID: 35275 RVA: 0x001F6BB6 File Offset: 0x001F4DB6
		// (remove) Token: 0x060089CC RID: 35276 RVA: 0x001F6BC9 File Offset: 0x001F4DC9
		public event RibbonBarGalleryCommandEventHandler GalleryCommand
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.GalleryCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.GalleryCommandEvent, value);
			}
		}

		// Token: 0x1400015B RID: 347
		// (add) Token: 0x060089CD RID: 35277 RVA: 0x001F6BDC File Offset: 0x001F4DDC
		// (remove) Token: 0x060089CE RID: 35278 RVA: 0x001F6BEF File Offset: 0x001F4DEF
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(RadRibbonBar.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadRibbonBar.CommandEvent, value);
			}
		}

		// Token: 0x060089CF RID: 35279 RVA: 0x001F6C04 File Offset: 0x001F4E04
		protected virtual void OnSelectedTabChange(RibbonBarSelectedTabChangeEventArgs e)
		{
			RibbonBarSelectedTabChangeEventHandler ribbonBarSelectedTabChangeEventHandler = (RibbonBarSelectedTabChangeEventHandler)base.Events[RadRibbonBar.SelectedTabChangeEvent];
			if (ribbonBarSelectedTabChangeEventHandler != null)
			{
				ribbonBarSelectedTabChangeEventHandler(this, e);
			}
		}

		// Token: 0x060089D0 RID: 35280 RVA: 0x001F6C34 File Offset: 0x001F4E34
		protected virtual void OnButtonClick(RibbonBarButtonClickEventArgs e)
		{
			RibbonBarButtonClickEventHandler ribbonBarButtonClickEventHandler = (RibbonBarButtonClickEventHandler)base.Events[RadRibbonBar.ButtonClickEvent];
			if (ribbonBarButtonClickEventHandler != null)
			{
				ribbonBarButtonClickEventHandler(this, e);
			}
		}

		// Token: 0x060089D1 RID: 35281 RVA: 0x001F6C64 File Offset: 0x001F4E64
		protected virtual void OnSplitButtonClick(RibbonBarSplitButtonClickEventArgs e)
		{
			RibbonBarSplitButtonClickEventHandler ribbonBarSplitButtonClickEventHandler = (RibbonBarSplitButtonClickEventHandler)base.Events[RadRibbonBar.SplitButtonClickEvent];
			if (ribbonBarSplitButtonClickEventHandler != null)
			{
				ribbonBarSplitButtonClickEventHandler(this, e);
			}
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x001F6C94 File Offset: 0x001F4E94
		protected virtual void OnMenuItemClick(RibbonBarMenuItemClickEventArgs e)
		{
			RibbonBarMenuItemClickEventHandler ribbonBarMenuItemClickEventHandler = (RibbonBarMenuItemClickEventHandler)base.Events[RadRibbonBar.MenuItemClickEvent];
			if (ribbonBarMenuItemClickEventHandler != null)
			{
				ribbonBarMenuItemClickEventHandler(this, e);
			}
		}

		// Token: 0x060089D3 RID: 35283 RVA: 0x001F6CC4 File Offset: 0x001F4EC4
		protected virtual void OnLauncherClick(RibbonBarLauncherClickEventArgs e)
		{
			RibbonBarLauncherClickEventHandler ribbonBarLauncherClickEventHandler = (RibbonBarLauncherClickEventHandler)base.Events[RadRibbonBar.LauncherClickEvent];
			if (ribbonBarLauncherClickEventHandler != null)
			{
				ribbonBarLauncherClickEventHandler(this, e);
			}
		}

		// Token: 0x060089D4 RID: 35284 RVA: 0x001F6CF4 File Offset: 0x001F4EF4
		protected virtual void OnButtonToggle(RibbonBarButtonToggleEventArgs e)
		{
			RibbonBarButtonToggleEventHandler ribbonBarButtonToggleEventHandler = (RibbonBarButtonToggleEventHandler)base.Events[RadRibbonBar.ButtonToggleEvent];
			if (ribbonBarButtonToggleEventHandler != null)
			{
				ribbonBarButtonToggleEventHandler(this, e);
			}
		}

		// Token: 0x060089D5 RID: 35285 RVA: 0x001F6D24 File Offset: 0x001F4F24
		protected virtual void OnToggleListToggle(RibbonBarToggleListToggleEventArgs e)
		{
			RibbonBarToggleListToggleEventHandler ribbonBarToggleListToggleEventHandler = (RibbonBarToggleListToggleEventHandler)base.Events[RadRibbonBar.ToggleListToggleEvent];
			if (ribbonBarToggleListToggleEventHandler != null)
			{
				ribbonBarToggleListToggleEventHandler(this, e);
			}
		}

		// Token: 0x060089D6 RID: 35286 RVA: 0x001F6D54 File Offset: 0x001F4F54
		protected virtual void OnApplicationMenuItemClick(RibbonBarApplicationMenuItemClickEventArgs e)
		{
			RibbonBarApplicationMenuItemClickEventHandler ribbonBarApplicationMenuItemClickEventHandler = (RibbonBarApplicationMenuItemClickEventHandler)base.Events[RadRibbonBar.ApplicationMenuItemClickEvent];
			if (ribbonBarApplicationMenuItemClickEventHandler != null)
			{
				ribbonBarApplicationMenuItemClickEventHandler(this, e);
			}
		}

		// Token: 0x060089D7 RID: 35287 RVA: 0x001F6D84 File Offset: 0x001F4F84
		protected virtual void OnComboBoxSelectedIndexChanged(RibbonBarComboBoxSelectedIndexChangedEventArgs e)
		{
			RibbonBarComboBoxSelectedIndexChangedEventHandler ribbonBarComboBoxSelectedIndexChangedEventHandler = (RibbonBarComboBoxSelectedIndexChangedEventHandler)base.Events[RadRibbonBar.ComboBoxSelectedIndexChangedEvent];
			if (ribbonBarComboBoxSelectedIndexChangedEventHandler != null)
			{
				ribbonBarComboBoxSelectedIndexChangedEventHandler(this, e);
			}
		}

		// Token: 0x060089D8 RID: 35288 RVA: 0x001F6DB4 File Offset: 0x001F4FB4
		protected virtual void OnComboBoxTextChanged(RibbonBarComboBoxTextChangedEventArgs e)
		{
			RibbonBarComboBoxTextChangedEventHandler ribbonBarComboBoxTextChangedEventHandler = (RibbonBarComboBoxTextChangedEventHandler)base.Events[RadRibbonBar.ComboBoxTextChangedEvent];
			if (ribbonBarComboBoxTextChangedEventHandler != null)
			{
				ribbonBarComboBoxTextChangedEventHandler(this, e);
			}
		}

		// Token: 0x060089D9 RID: 35289 RVA: 0x001F6DE4 File Offset: 0x001F4FE4
		protected virtual void OnDropDownSelectedIndexChanged(RibbonBarDropDownSelectedIndexChangedEventArgs e)
		{
			RibbonBarDropDownSelectedIndexChangedEventHandler ribbonBarDropDownSelectedIndexChangedEventHandler = (RibbonBarDropDownSelectedIndexChangedEventHandler)base.Events[RadRibbonBar.DropDownSelectedIndexChangedEvent];
			if (ribbonBarDropDownSelectedIndexChangedEventHandler != null)
			{
				ribbonBarDropDownSelectedIndexChangedEventHandler(this, e);
			}
		}

		// Token: 0x060089DA RID: 35290 RVA: 0x001F6E14 File Offset: 0x001F5014
		protected virtual void OnNumericTextBoxValueChanged(RibbonBarNumericTextBoxValueChangedEventArgs e)
		{
			RibbonBarNumericTextBoxValueChangedEventHandler ribbonBarNumericTextBoxValueChangedEventHandler = (RibbonBarNumericTextBoxValueChangedEventHandler)base.Events[RadRibbonBar.NumericTextBoxValueChangedEvent];
			if (ribbonBarNumericTextBoxValueChangedEventHandler != null)
			{
				ribbonBarNumericTextBoxValueChangedEventHandler(this, e);
			}
		}

		// Token: 0x060089DB RID: 35291 RVA: 0x001F6E44 File Offset: 0x001F5044
		protected virtual void OnColorPickerColorChanged(RibbonBarColorPickerColorChangedEventArgs e)
		{
			RibbonBarColorPickerColorChangedEventHandler ribbonBarColorPickerColorChangedEventHandler = (RibbonBarColorPickerColorChangedEventHandler)base.Events[RadRibbonBar.ColorPickerColorChangedEvent];
			if (ribbonBarColorPickerColorChangedEventHandler != null)
			{
				ribbonBarColorPickerColorChangedEventHandler(this, e);
			}
		}

		// Token: 0x060089DC RID: 35292 RVA: 0x001F6E74 File Offset: 0x001F5074
		protected virtual void OnGalleryCommand(RibbonBarGalleryCommandEventArgs e)
		{
			RibbonBarGalleryCommandEventHandler ribbonBarGalleryCommandEventHandler = (RibbonBarGalleryCommandEventHandler)base.Events[RadRibbonBar.GalleryCommandEvent];
			if (ribbonBarGalleryCommandEventHandler != null)
			{
				ribbonBarGalleryCommandEventHandler(this, e);
			}
		}

		// Token: 0x060089DD RID: 35293 RVA: 0x001F6EA4 File Offset: 0x001F50A4
		protected virtual void OnCommand(object sender, CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[RadRibbonBar.CommandEvent];
			if (commandEventHandler != null)
			{
				commandEventHandler(sender, e);
			}
		}

		// Token: 0x060089DE RID: 35294 RVA: 0x001F6ED2 File Offset: 0x001F50D2
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060089DF RID: 35295 RVA: 0x001F6EDC File Offset: 0x001F50DC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.StartsWith("-"))
			{
				this.RaiseApplicationMenuItemClickPostBackEvent(eventArgument);
				return;
			}
			string[] array = eventArgument.Split(new char[]
			{
				':'
			});
			RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
			object obj = null;
			if (array.Length > 1)
			{
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				if (array.Length == 2)
				{
					this.OnLauncherClick(new RibbonBarLauncherClickEventArgs(ribbonBarGroup));
					return;
				}
				RibbonBarItem ribbonBarItem = ribbonBarGroup.GetFunctionalItems(true)[int.Parse(array[2])];
				if (array.Length == 3)
				{
					RibbonBarToggleButton ribbonBarToggleButton = ribbonBarItem as RibbonBarToggleButton;
					if (ribbonBarToggleButton != null)
					{
						obj = ribbonBarToggleButton;
						RibbonBarToggleList ribbonBarToggleList = ribbonBarToggleButton.ParentWebControl as RibbonBarToggleList;
						if (ribbonBarToggleList != null)
						{
							RibbonBarToggleListToggleEventArgs e = new RibbonBarToggleListToggleEventArgs(ribbonBarToggleButton, ribbonBarToggleList, ribbonBarGroup);
							this.OnToggleListToggle(e);
						}
						else
						{
							RibbonBarButtonToggleEventArgs e2 = new RibbonBarButtonToggleEventArgs(ribbonBarToggleButton, ribbonBarGroup);
							this.OnButtonToggle(e2);
						}
					}
					else
					{
						RibbonBarButton ribbonBarButton = ribbonBarItem as RibbonBarButton;
						obj = ribbonBarButton;
						RibbonBarButtonClickEventArgs e3 = new RibbonBarButtonClickEventArgs(ribbonBarButton, ribbonBarGroup);
						this.OnButtonClick(e3);
					}
				}
				else
				{
					RibbonBarSplitButton ribbonBarSplitButton = ribbonBarItem as RibbonBarSplitButton;
					RibbonBarMenu ribbonBarMenu = ribbonBarItem as RibbonBarMenu;
					RibbonBarComboBox ribbonBarComboBox = ribbonBarItem as RibbonBarComboBox;
					RibbonBarNumericTextBox ribbonBarNumericTextBox = ribbonBarItem as RibbonBarNumericTextBox;
					RibbonBarColorPicker ribbonBarColorPicker = ribbonBarItem as RibbonBarColorPicker;
					RibbonBarGallery ribbonBarGallery = ribbonBarItem as RibbonBarGallery;
					if (ribbonBarSplitButton != null)
					{
						RibbonBarButton ribbonBarButton2 = ribbonBarSplitButton.GetVisibleButtons()[int.Parse(array[3])];
						obj = ribbonBarButton2;
						RibbonBarSplitButtonClickEventArgs e4 = new RibbonBarSplitButtonClickEventArgs(ribbonBarButton2, ribbonBarSplitButton, ribbonBarGroup);
						this.OnSplitButtonClick(e4);
					}
					else if (ribbonBarMenu != null)
					{
						RibbonBarMenuItem menuItem = this.GetMenuItem(ribbonBarMenu, array);
						obj = menuItem;
						RibbonBarMenuItemClickEventArgs e5 = new RibbonBarMenuItemClickEventArgs(menuItem, ribbonBarMenu, ribbonBarGroup);
						this.OnMenuItemClick(e5);
					}
					else if (ribbonBarNumericTextBox != null)
					{
						double value = double.Parse(array[3]);
						RibbonBarNumericTextBoxValueChangedEventArgs e6 = new RibbonBarNumericTextBoxValueChangedEventArgs(value, ribbonBarNumericTextBox, ribbonBarGroup);
						this.OnNumericTextBoxValueChanged(e6);
					}
					else if (ribbonBarComboBox != null)
					{
						if (array.Length == 4)
						{
							RibbonBarListItem item = ribbonBarComboBox.Items[int.Parse(array[3])];
							RibbonBarComboBoxSelectedIndexChangedEventArgs e7 = new RibbonBarComboBoxSelectedIndexChangedEventArgs(item, ribbonBarComboBox, ribbonBarGroup);
							this.OnComboBoxSelectedIndexChanged(e7);
						}
						else
						{
							RibbonBarComboBoxTextChangedEventArgs e8 = new RibbonBarComboBoxTextChangedEventArgs(array[4], ribbonBarComboBox, ribbonBarGroup);
							this.OnComboBoxTextChanged(e8);
						}
					}
					else if (ribbonBarColorPicker != null)
					{
						Color color = ColorTranslator.FromHtml(array[3]);
						RibbonBarColorPickerColorChangedEventArgs e9 = new RibbonBarColorPickerColorChangedEventArgs(color, ribbonBarColorPicker, ribbonBarGroup);
						this.OnColorPickerColorChanged(e9);
					}
					else if (ribbonBarGallery != null)
					{
						RibbonBarGalleryCategory ribbonBarGalleryCategory = ribbonBarGallery.Categories[int.Parse(array[3])];
						RibbonBarGalleryItem ribbonBarGalleryItem = ribbonBarGalleryCategory.Items[int.Parse(array[4])];
						RibbonBarGalleryCommandEventArgs e10 = new RibbonBarGalleryCommandEventArgs(ribbonBarGalleryItem, ribbonBarGalleryCategory, ribbonBarGallery, ribbonBarGroup);
						this.OnGalleryCommand(e10);
						this.OnCommand(ribbonBarGalleryItem, new CommandEventArgs(ribbonBarGallery.CommandName, ribbonBarGalleryItem.CommandArgument));
					}
					else
					{
						RibbonBarDropDown ribbonBarDropDown = ribbonBarItem as RibbonBarDropDown;
						RibbonBarListItem item2 = ribbonBarDropDown.Items[int.Parse(array[3])];
						RibbonBarDropDownSelectedIndexChangedEventArgs e11 = new RibbonBarDropDownSelectedIndexChangedEventArgs(item2, ribbonBarDropDown, ribbonBarGroup);
						this.OnDropDownSelectedIndexChanged(e11);
					}
				}
				if (obj != null)
				{
					this.OnCommand(obj, new CommandEventArgs((obj as IRibbonBarCommandItem).CommandName, (obj as IRibbonBarCommandItem).CommandArgument));
					return;
				}
			}
			else if (array.Length == 1)
			{
				this.OnSelectedTabChange(new RibbonBarSelectedTabChangeEventArgs(ribbonBarTab, this.GetAllTabs()[this.SelectedTabIndex]));
				this.shouldRenderMaximizedRibbon = true;
			}
		}

		// Token: 0x060089E0 RID: 35296 RVA: 0x001F720C File Offset: 0x001F540C
		private void RaiseApplicationMenuItemClickPostBackEvent(string eventArgument)
		{
			string[] array = eventArgument.TrimStart(new char[]
			{
				'-'
			}).Split(new char[]
			{
				':'
			});
			RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase = this.ApplicationMenu.Items[int.Parse(array[0])];
			if (array.Length == 2)
			{
				ribbonBarApplicationMenuItemBase = (ribbonBarApplicationMenuItemBase as RibbonBarApplicationSplitMenuItem).Items[int.Parse(array[1])];
			}
			IRibbonBarCommandItem ribbonBarCommandItem = ribbonBarApplicationMenuItemBase;
			this.OnApplicationMenuItemClick(new RibbonBarApplicationMenuItemClickEventArgs(ribbonBarApplicationMenuItemBase));
			this.OnCommand(ribbonBarApplicationMenuItemBase, new CommandEventArgs(ribbonBarCommandItem.CommandName, ribbonBarCommandItem.CommandArgument));
		}

		// Token: 0x060089E1 RID: 35297 RVA: 0x001F72A0 File Offset: 0x001F54A0
		private RibbonBarMenuItem GetMenuItem(RibbonBarMenu menu, string[] args)
		{
			IList<RibbonBarMenuItem> visibleItems = menu.GetVisibleItems();
			for (int i = 3; i < args.Length - 1; i++)
			{
				visibleItems = visibleItems[int.Parse(args[i])].GetVisibleItems();
			}
			return visibleItems[int.Parse(args[args.Length - 1])];
		}

		// Token: 0x060089E2 RID: 35298 RVA: 0x001F72EC File Offset: 0x001F54EC
		private PostBackOptions GetPostBackOptions(string argument)
		{
			return new PostBackOptions(this, argument)
			{
				ClientSubmit = true
			};
		}

		// Token: 0x060089E3 RID: 35299 RVA: 0x001F730C File Offset: 0x001F550C
		private string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions("arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x17002B90 RID: 11152
		// (get) Token: 0x060089E4 RID: 35300 RVA: 0x001F7348 File Offset: 0x001F5548
		private bool ShouldRenderPostBackReference
		{
			get
			{
				return base.Events[RadRibbonBar.SelectedTabChangeEvent] != null || base.Events[RadRibbonBar.ButtonClickEvent] != null || base.Events[RadRibbonBar.SplitButtonClickEvent] != null || base.Events[RadRibbonBar.MenuItemClickEvent] != null || base.Events[RadRibbonBar.LauncherClickEvent] != null || base.Events[RadRibbonBar.ButtonToggleEvent] != null || base.Events[RadRibbonBar.ToggleListToggleEvent] != null || base.Events[RadRibbonBar.ApplicationMenuItemClickEvent] != null || base.Events[RadRibbonBar.ComboBoxSelectedIndexChangedEvent] != null || base.Events[RadRibbonBar.ComboBoxTextChangedEvent] != null || base.Events[RadRibbonBar.DropDownSelectedIndexChangedEvent] != null || base.Events[RadRibbonBar.NumericTextBoxValueChangedEvent] != null || base.Events[RadRibbonBar.ColorPickerColorChangedEvent] != null || base.Events[RadRibbonBar.GalleryCommandEvent] != null || base.Events[RadRibbonBar.CommandEvent] != null;
			}
		}

		// Token: 0x060089E5 RID: 35301 RVA: 0x001F7484 File Offset: 0x001F5684
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadRibbonBarClientState radRibbonBarClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				radRibbonBarClientState = javaScriptSerializer.Deserialize<RadRibbonBarClientState>(text);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (radRibbonBarClientState == null)
			{
				return false;
			}
			this.LoadToggledIndices(radRibbonBarClientState.ToggledIndices);
			this.LoadSplitButtonSelection(radRibbonBarClientState.SplitButtonSelectedIndices);
			this.LoadComboBoxTexts();
			this.LoadComboBoxSelectedIndices(radRibbonBarClientState.ComboBoxSelectedIndices);
			this.LoadDropDownSelectedIndices(radRibbonBarClientState.DropDownSelectedIndices);
			this.LoadColorPickerColorIndices(radRibbonBarClientState.ColorPickerColorIndices);
			this.LoadGallerySelectedIndices(radRibbonBarClientState.GallerySelectedIndices);
			this.LoadNumericTextBoxValues();
			this.LoadTabsEnabledState(radRibbonBarClientState.ClientSideEnabledTabs, radRibbonBarClientState.ClientSideDisabledTabs);
			this.LoadGroupsEnabledState(radRibbonBarClientState.ClientSideEnabledGroups, radRibbonBarClientState.ClientSideDisabledGroups);
			this.LoadItemsEnabledState(radRibbonBarClientState.ClientSideEnabledItems, radRibbonBarClientState.ClientSideDisabledItems);
			if (this.EnableQuickAccessToolbar && radRibbonBarClientState.QatActiveItemIndices != null)
			{
				this.LoadQatActiveItems(radRibbonBarClientState.QatActiveItemIndices);
			}
			if (radRibbonBarClientState.Width != null)
			{
				this.Width = Unit.Pixel(radRibbonBarClientState.Width.Value);
			}
			this.Minimized = radRibbonBarClientState.Minimized;
			this.Enabled = radRibbonBarClientState.Enabled;
			this.SelectedTabIndex = radRibbonBarClientState.SelectedTabIndex;
			this.KeyboardNavigationSettings.Activated = radRibbonBarClientState.Activated;
			return false;
		}

		// Token: 0x060089E6 RID: 35302 RVA: 0x001F75E8 File Offset: 0x001F57E8
		private void LoadToggledIndices(string[] toggledIndices)
		{
			this.UntoggleAllToggleButtons();
			foreach (string text in toggledIndices)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				RibbonBarToggleButton ribbonBarToggleButton = ribbonBarGroup.GetVisibleFunctionalItems()[int.Parse(array[2])] as RibbonBarToggleButton;
				if (ribbonBarToggleButton != null)
				{
					ribbonBarToggleButton.Toggled = true;
				}
			}
		}

		// Token: 0x060089E7 RID: 35303 RVA: 0x001F767C File Offset: 0x001F587C
		private void LoadSplitButtonSelection(string[] selectedButtonIndices)
		{
			foreach (string text in selectedButtonIndices)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				RibbonBarSplitButton ribbonBarSplitButton = ribbonBarGroup.GetVisibleFunctionalItems()[int.Parse(array[2])] as RibbonBarSplitButton;
				int selectedButtonIndex = int.Parse(array[3]);
				ribbonBarSplitButton.SelectedButtonIndex = selectedButtonIndex;
			}
		}

		// Token: 0x060089E8 RID: 35304 RVA: 0x001F7714 File Offset: 0x001F5914
		private void LoadComboBoxTexts()
		{
			foreach (RibbonBarTab ribbonBarTab in this.GetVisibleTabs())
			{
				foreach (RibbonBarGroup ribbonBarGroup in ribbonBarTab.GetVisibleGroups())
				{
					foreach (RibbonBarItem ribbonBarItem in ribbonBarGroup.GetVisibleFunctionalItems())
					{
						RibbonBarComboBox ribbonBarComboBox = ribbonBarItem as RibbonBarComboBox;
						if (ribbonBarComboBox != null)
						{
							ribbonBarComboBox.Text = this.Page.Request[this.ClientID + ":" + this.GetItemHierarchicalIndex(ribbonBarComboBox)];
						}
					}
				}
			}
		}

		// Token: 0x060089E9 RID: 35305 RVA: 0x001F7818 File Offset: 0x001F5A18
		private void LoadNumericTextBoxValues()
		{
			foreach (RibbonBarTab ribbonBarTab in this.GetVisibleTabs())
			{
				foreach (RibbonBarGroup ribbonBarGroup in ribbonBarTab.GetVisibleGroups())
				{
					foreach (RibbonBarItem ribbonBarItem in ribbonBarGroup.GetVisibleFunctionalItems())
					{
						RibbonBarNumericTextBox ribbonBarNumericTextBox = ribbonBarItem as RibbonBarNumericTextBox;
						if (ribbonBarNumericTextBox != null)
						{
							string text = this.Page.Request[this.ClientID + ":" + this.GetItemHierarchicalIndex(ribbonBarNumericTextBox)];
							int length = ribbonBarNumericTextBox.Prefix.Length;
							int length2 = text.Length - length - ribbonBarNumericTextBox.Suffix.Length;
							string text2 = text.Substring(length, length2);
							ribbonBarNumericTextBox.Text = text2;
						}
					}
				}
			}
		}

		// Token: 0x060089EA RID: 35306 RVA: 0x001F7958 File Offset: 0x001F5B58
		private void LoadComboBoxSelectedIndices(string[] comboIndices)
		{
			foreach (string text in comboIndices)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				RibbonBarComboBox ribbonBarComboBox = ribbonBarGroup.GetVisibleFunctionalItems()[int.Parse(array[2])] as RibbonBarComboBox;
				int selectedIndex = int.Parse(array[3]);
				ribbonBarComboBox.SelectedIndex = selectedIndex;
			}
		}

		// Token: 0x060089EB RID: 35307 RVA: 0x001F79F0 File Offset: 0x001F5BF0
		private void LoadDropDownSelectedIndices(string[] dropDownIndices)
		{
			foreach (string text in dropDownIndices)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				RibbonBarDropDown ribbonBarDropDown = ribbonBarGroup.GetVisibleFunctionalItems()[int.Parse(array[2])] as RibbonBarDropDown;
				int selectedIndex = int.Parse(array[3]);
				ribbonBarDropDown.SelectedIndex = selectedIndex;
			}
		}

		// Token: 0x060089EC RID: 35308 RVA: 0x001F7A88 File Offset: 0x001F5C88
		private void LoadColorPickerColorIndices(string[] colorPickerIndices)
		{
			foreach (string text in colorPickerIndices)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				RibbonBarColorPicker ribbonBarColorPicker = ribbonBarGroup.GetVisibleFunctionalItems()[int.Parse(array[2])] as RibbonBarColorPicker;
				Color selectedColor = ColorTranslator.FromHtml(array[3]);
				ribbonBarColorPicker.SelectedColor = selectedColor;
			}
		}

		// Token: 0x060089ED RID: 35309 RVA: 0x001F7B20 File Offset: 0x001F5D20
		private void LoadGallerySelectedIndices(string[] galleryIndices)
		{
			foreach (string text in galleryIndices)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				RibbonBarGallery ribbonBarGallery = ribbonBarGroup.GetFunctionalItems()[int.Parse(array[2])] as RibbonBarGallery;
				RibbonBarGalleryCategory ribbonBarGalleryCategory = ribbonBarGallery.Categories[int.Parse(array[3])];
				ribbonBarGalleryCategory.Items[int.Parse(array[4])].Selected = true;
			}
		}

		// Token: 0x060089EE RID: 35310 RVA: 0x001F7BD8 File Offset: 0x001F5DD8
		private void LoadTabsEnabledState(string[] enabledTabsHierarchicalIndices, string[] disabledTabsHierarchicalIndices)
		{
			foreach (string s in enabledTabsHierarchicalIndices)
			{
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(s)];
				ribbonBarTab.Enabled = true;
			}
			foreach (string s2 in disabledTabsHierarchicalIndices)
			{
				RibbonBarTab ribbonBarTab2 = this.GetTabsToRender()[int.Parse(s2)];
				ribbonBarTab2.Enabled = false;
			}
		}

		// Token: 0x060089EF RID: 35311 RVA: 0x001F7C50 File Offset: 0x001F5E50
		private void LoadGroupsEnabledState(string[] enabledGroupsHierarchicalIndices, string[] disabledGroupsHierarchicalIndices)
		{
			foreach (string text in enabledGroupsHierarchicalIndices)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab = this.GetTabsToRender()[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])];
				ribbonBarGroup.Enabled = true;
			}
			foreach (string text2 in disabledGroupsHierarchicalIndices)
			{
				string[] array2 = text2.Split(new char[]
				{
					':'
				});
				RibbonBarTab ribbonBarTab2 = this.GetTabsToRender()[int.Parse(array2[0])];
				RibbonBarGroup ribbonBarGroup2 = ribbonBarTab2.GetVisibleGroups()[int.Parse(array2[1])];
				ribbonBarGroup2.Enabled = false;
			}
		}

		// Token: 0x060089F0 RID: 35312 RVA: 0x001F7D2C File Offset: 0x001F5F2C
		private void LoadItemsEnabledState(string[] enabledItemsHierarchicalIndices, string[] disabledItemsHierarchicalIndices)
		{
			foreach (string index in enabledItemsHierarchicalIndices)
			{
				WebControl itemByHierarchicalIndex = this.GetItemByHierarchicalIndex(index, true);
				itemByHierarchicalIndex.Enabled = true;
			}
			foreach (string index2 in disabledItemsHierarchicalIndices)
			{
				WebControl itemByHierarchicalIndex2 = this.GetItemByHierarchicalIndex(index2, true);
				itemByHierarchicalIndex2.Enabled = false;
			}
		}

		// Token: 0x060089F1 RID: 35313 RVA: 0x001F7D94 File Offset: 0x001F5F94
		private void LoadQatActiveItems(string[] qatActiveItemIndices)
		{
			this.DeactivateQatActiveItems();
			foreach (string index in qatActiveItemIndices)
			{
				RibbonBarClickableItem ribbonBarClickableItem = this.GetItemByHierarchicalIndex(index, true) as RibbonBarClickableItem;
				ribbonBarClickableItem.QuickAccess = RibbonBarItemQuickAccess.Active;
			}
		}

		// Token: 0x060089F2 RID: 35314 RVA: 0x001F7DD0 File Offset: 0x001F5FD0
		private void DeactivateQatActiveItems()
		{
			foreach (RibbonBarClickableItem ribbonBarClickableItem in this.GetQuickAccessEnabledItems())
			{
				if (ribbonBarClickableItem.QuickAccess == RibbonBarItemQuickAccess.Active)
				{
					ribbonBarClickableItem.QuickAccess = RibbonBarItemQuickAccess.Inactive;
				}
			}
		}

		// Token: 0x060089F3 RID: 35315 RVA: 0x001F7E2C File Offset: 0x001F602C
		private void UntoggleAllToggleButtons()
		{
			foreach (RibbonBarGroup ribbonBarGroup in this.GetVisibleGroups())
			{
				foreach (RibbonBarItem ribbonBarItem in ribbonBarGroup.GetFunctionalItems())
				{
					RibbonBarToggleButton ribbonBarToggleButton = ribbonBarItem as RibbonBarToggleButton;
					if (ribbonBarToggleButton != null)
					{
						ribbonBarToggleButton.Toggled = false;
					}
				}
			}
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x001F7EC8 File Offset: 0x001F60C8
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060089F5 RID: 35317 RVA: 0x001F7EE4 File Offset: 0x001F60E4
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "applicationMenuItemClicked", this.OnClientApplicationMenuItemClicked);
			RadWebControl.DescribeEvent(descriptor, "applicationMenuItemClicking", this.OnClientApplicationMenuItemClicking);
			RadWebControl.DescribeEvent(descriptor, "buttonClicked", this.OnClientButtonClicked);
			RadWebControl.DescribeEvent(descriptor, "buttonClicking", this.OnClientButtonClicking);
			RadWebControl.DescribeEvent(descriptor, "buttonToggled", this.OnClientButtonToggled);
			RadWebControl.DescribeEvent(descriptor, "buttonToggling", this.OnClientButtonToggling);
			RadWebControl.DescribeEvent(descriptor, "colorPickerColorChanged", this.OnClientColorPickerColorChanged);
			RadWebControl.DescribeEvent(descriptor, "colorPickerColorChanging", this.OnClientColorPickerColorChanging);
			RadWebControl.DescribeEvent(descriptor, "comboBoxSelectedIndexChanged", this.OnClientComboBoxSelectedIndexChanged);
			RadWebControl.DescribeEvent(descriptor, "comboBoxSelectedIndexChanging", this.OnClientComboBoxSelectedIndexChanging);
			RadWebControl.DescribeEvent(descriptor, "comboBoxTextChanged", this.OnClientComboBoxTextChanged);
			RadWebControl.DescribeEvent(descriptor, "dropDownSelectedIndexChanged", this.OnClientDropDownSelectedIndexChanged);
			RadWebControl.DescribeEvent(descriptor, "dropDownSelectedIndexChanging", this.OnClientDropDownSelectedIndexChanging);
			RadWebControl.DescribeEvent(descriptor, "galleryCommand", this.OnClientGalleryCommand);
			RadWebControl.DescribeEvent(descriptor, "galleryCommandPreview", this.OnClientGalleryCommandPreview);
			RadWebControl.DescribeEvent(descriptor, "galleryCommandPreviewEnd", this.OnClientGalleryCommandPreviewEnd);
			RadWebControl.DescribeEvent(descriptor, "launcherClicked", this.OnClientLauncherClicked);
			RadWebControl.DescribeEvent(descriptor, "launcherClicking", this.OnClientLauncherClicking);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "maximized", this.OnClientMaximized);
			RadWebControl.DescribeEvent(descriptor, "maximizing", this.OnClientMaximizing);
			RadWebControl.DescribeEvent(descriptor, "menuItemClicked", this.OnClientMenuItemClicked);
			RadWebControl.DescribeEvent(descriptor, "menuItemClicking", this.OnClientMenuItemClicking);
			RadWebControl.DescribeEvent(descriptor, "minimized", this.OnClientMinimized);
			RadWebControl.DescribeEvent(descriptor, "minimizing", this.OnClientMinimizing);
			RadWebControl.DescribeEvent(descriptor, "numericTextBoxValueChanged", this.OnClientNumericTextBoxValueChanged);
			RadWebControl.DescribeEvent(descriptor, "numericTextBoxValueChanging", this.OnClientNumericTextBoxValueChanging);
			RadWebControl.DescribeEvent(descriptor, "selectedTabChanged", this.OnClientSelectedTabChanged);
			RadWebControl.DescribeEvent(descriptor, "selectedTabChanging", this.OnClientSelectedTabChanging);
			RadWebControl.DescribeEvent(descriptor, "splitButtonClicked", this.OnClientSplitButtonClicked);
			RadWebControl.DescribeEvent(descriptor, "splitButtonClicking", this.OnClientSplitButtonClicking);
			RadWebControl.DescribeEvent(descriptor, "toggleListToggled", this.OnClientToggleListToggled);
			RadWebControl.DescribeEvent(descriptor, "toggleListToggling", this.OnClientToggleListToggling);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17002B91 RID: 11153
		// (get) Token: 0x060089F6 RID: 35318 RVA: 0x001F8129 File Offset: 0x001F6329
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarTabCollection Tabs
		{
			get
			{
				if (this._tabs == null)
				{
					this._tabs = new RibbonBarTabCollection();
					this._tabs.RibbonBar = this;
					this._tabs.ParentWebControl = this;
				}
				return this._tabs;
			}
		}

		// Token: 0x17002B92 RID: 11154
		// (get) Token: 0x060089F7 RID: 35319 RVA: 0x001F815C File Offset: 0x001F635C
		[ClientControlProperty]
		[ClientPropertyName("_uniqueId")]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x17002B93 RID: 11155
		// (get) Token: 0x060089F8 RID: 35320 RVA: 0x001F8164 File Offset: 0x001F6364
		// (set) Token: 0x060089F9 RID: 35321 RVA: 0x001F8185 File Offset: 0x001F6385
		[ClientPersistedProperty]
		[DefaultValue(RibbonBarImageRenderingMode.Auto)]
		[Category("Appearance")]
		[Description("The rendering mode for all RibbonBarClickableItems images.")]
		public RibbonBarImageRenderingMode ImageRenderingMode
		{
			get
			{
				return (RibbonBarImageRenderingMode)(this.ViewState["ImageRenderingMode"] ?? RibbonBarImageRenderingMode.Auto);
			}
			set
			{
				this.ViewState["ImageRenderingMode"] = value;
			}
		}

		// Token: 0x17002B94 RID: 11156
		// (get) Token: 0x060089FA RID: 35322 RVA: 0x001F819D File Offset: 0x001F639D
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Navigation settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RibbonBarKeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				if (this.keyboardSettings == null)
				{
					this.keyboardSettings = new RibbonBarKeyboardNavigationSettings(this.ViewState);
				}
				return this.keyboardSettings;
			}
		}

		// Token: 0x17002B95 RID: 11157
		// (get) Token: 0x060089FB RID: 35323 RVA: 0x001F81BE File Offset: 0x001F63BE
		// (set) Token: 0x060089FC RID: 35324 RVA: 0x001F81C6 File Offset: 0x001F63C6
		[SimplePersistenceSetting]
		[Description("The index of the selected RibbonBarTab.")]
		public int SelectedTabIndex
		{
			get
			{
				return this.GetResolvedSelectedTabIndex;
			}
			set
			{
				this.ViewState["SelectedTabIndex"] = value;
			}
		}

		// Token: 0x17002B96 RID: 11158
		// (get) Token: 0x060089FD RID: 35325 RVA: 0x001F81DE File Offset: 0x001F63DE
		// (set) Token: 0x060089FE RID: 35326 RVA: 0x001F81FF File Offset: 0x001F63FF
		[Category("Appearance")]
		[ClientPersistedProperty]
		[DefaultValue(false)]
		[Description("Gets or sets whether maximizing/minimizing should be enabled.")]
		public bool EnableMinimizing
		{
			get
			{
				return (bool)(this.ViewState["EnableMinimizing"] ?? false);
			}
			set
			{
				this.ViewState["EnableMinimizing"] = value;
			}
		}

		// Token: 0x17002B97 RID: 11159
		// (get) Token: 0x060089FF RID: 35327 RVA: 0x001F8217 File Offset: 0x001F6417
		// (set) Token: 0x06008A00 RID: 35328 RVA: 0x001F8242 File Offset: 0x001F6442
		[SimplePersistenceSetting]
		[Description("Gets or sets whether the RibbonBar should be minimized.")]
		[DefaultValue(false)]
		[Category("Appearance")]
		[ClientPersistedProperty]
		public bool Minimized
		{
			get
			{
				return this.EnableMinimizing && (bool)(this.ViewState["Minimized"] ?? false);
			}
			set
			{
				this.ViewState["Minimized"] = value;
			}
		}

		// Token: 0x17002B98 RID: 11160
		// (get) Token: 0x06008A01 RID: 35329 RVA: 0x001F825A File Offset: 0x001F645A
		// (set) Token: 0x06008A02 RID: 35330 RVA: 0x001F827B File Offset: 0x001F647B
		[ClientPersistedProperty]
		[DefaultValue(true)]
		[Category("Appearance")]
		[Description("Enables automatic arrange of items within a group.")]
		public bool EnableAutoArrange
		{
			get
			{
				return (bool)(this.ViewState["EnableAutoArrange"] ?? true);
			}
			set
			{
				this.ViewState["EnableAutoArrange"] = value;
			}
		}

		// Token: 0x17002B99 RID: 11161
		// (get) Token: 0x06008A03 RID: 35331 RVA: 0x001F8293 File Offset: 0x001F6493
		// (set) Token: 0x06008A04 RID: 35332 RVA: 0x001F82B3 File Offset: 0x001F64B3
		[Description("Gets or sets the ID of the RibbonBarApplicationMenu control that will be shown in the RadRibbonBar.")]
		[DefaultValue("")]
		public string ApplicationMenuID
		{
			get
			{
				return (string)(this.ViewState["ApplicationMenuID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ApplicationMenuID"] = value;
			}
		}

		// Token: 0x17002B9A RID: 11162
		// (get) Token: 0x06008A05 RID: 35333 RVA: 0x001F82C8 File Offset: 0x001F64C8
		// (set) Token: 0x06008A06 RID: 35334 RVA: 0x001F832C File Offset: 0x001F652C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarApplicationMenu ApplicationMenu
		{
			get
			{
				if (this._applicationMenu != null)
				{
					return this._applicationMenu;
				}
				if (string.IsNullOrEmpty(this.ApplicationMenuID))
				{
					return null;
				}
				RibbonBarApplicationMenu ribbonBarApplicationMenu = (RibbonBarApplicationMenu)this.NamingContainer.FindControl(this.ApplicationMenuID);
				if (ribbonBarApplicationMenu == null)
				{
					ribbonBarApplicationMenu = (RibbonBarApplicationMenu)this.Page.FindControl(this.ApplicationMenuID);
				}
				ribbonBarApplicationMenu.RibbonBar = this;
				return ribbonBarApplicationMenu;
			}
			set
			{
				this._applicationMenu = value;
				this._applicationMenu.RibbonBar = this;
				this.Controls.Add(this._applicationMenu);
			}
		}

		// Token: 0x06008A07 RID: 35335 RVA: 0x001F8354 File Offset: 0x001F6554
		public RibbonBarTab FindTabByValue(string value)
		{
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				if (ribbonBarTab.Value.Equals(value))
				{
					return ribbonBarTab;
				}
			}
			return null;
		}

		// Token: 0x06008A08 RID: 35336 RVA: 0x001F83B8 File Offset: 0x001F65B8
		public RibbonBarGroup FindGroupByValue(string value)
		{
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				RibbonBarGroup ribbonBarGroup = ribbonBarTab.FindGroupByValue(value);
				if (ribbonBarGroup != null)
				{
					return ribbonBarGroup;
				}
			}
			return null;
		}

		// Token: 0x06008A09 RID: 35337 RVA: 0x001F8418 File Offset: 0x001F6618
		public RibbonBarButton FindButtonByValue(string value)
		{
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				RibbonBarButton ribbonBarButton = ribbonBarTab.FindButtonByValue(value);
				if (ribbonBarButton != null)
				{
					return ribbonBarButton;
				}
			}
			return null;
		}

		// Token: 0x06008A0A RID: 35338 RVA: 0x001F8478 File Offset: 0x001F6678
		public RibbonBarToggleButton FindToggleButtonByValue(string value)
		{
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				RibbonBarToggleButton ribbonBarToggleButton = ribbonBarTab.FindToggleButtonByValue(value);
				if (ribbonBarToggleButton != null)
				{
					return ribbonBarToggleButton;
				}
			}
			return null;
		}

		// Token: 0x06008A0B RID: 35339 RVA: 0x001F84D8 File Offset: 0x001F66D8
		public RibbonBarMenuItem FindMenuItemByValue(string value)
		{
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				RibbonBarMenuItem ribbonBarMenuItem = ribbonBarTab.FindMenuItemByValue(value);
				if (ribbonBarMenuItem != null)
				{
					return ribbonBarMenuItem;
				}
			}
			return null;
		}

		// Token: 0x06008A0C RID: 35340 RVA: 0x001F8538 File Offset: 0x001F6738
		public void LoadContentFile(string xmlFileName)
		{
			string xml = File.ReadAllText(this.Context.Server.MapPath(xmlFileName));
			this.LoadXml(xml);
		}

		// Token: 0x06008A0D RID: 35341 RVA: 0x001F8564 File Offset: 0x001F6764
		public void LoadXml(string xml)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RadRibbonBar));
			RadRibbonBar deserialized = (RadRibbonBar)xmlSerializer.Deserialize(new StringReader(xml));
			this.LoadXml(deserialized);
		}

		// Token: 0x06008A0E RID: 35342 RVA: 0x001F859C File Offset: 0x001F679C
		protected void LoadXml(RadRibbonBar deserialized)
		{
			XmlPersister.MergeObjects(deserialized, this);
			foreach (object obj in deserialized.Attributes.Keys)
			{
				string key = (string)obj;
				base.Attributes[key] = deserialized.Attributes[key];
			}
			if (deserialized.ApplicationMenu != null)
			{
				this.ApplicationMenu = deserialized.ApplicationMenu;
			}
			this.Tabs.Clear();
			RibbonBarTab[] array = new RibbonBarTab[deserialized.Tabs.Count];
			deserialized.Tabs.CopyTo(array, 0);
			foreach (RibbonBarTab tab in array)
			{
				this.Tabs.Add(tab);
			}
		}

		// Token: 0x06008A0F RID: 35343 RVA: 0x001F867C File Offset: 0x001F687C
		public string GetXml()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(RadRibbonBar));
			StringWriter stringWriter = new StringWriter();
			this.GetXml(serializer, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06008A10 RID: 35344 RVA: 0x001F86AD File Offset: 0x001F68AD
		protected virtual void GetXml(XmlSerializer serializer, TextWriter output)
		{
			serializer.Serialize(output, this);
		}

		// Token: 0x17002B9B RID: 11163
		// (get) Token: 0x06008A11 RID: 35345 RVA: 0x001F86B7 File Offset: 0x001F68B7
		// (set) Token: 0x06008A12 RID: 35346 RVA: 0x001F86BF File Offset: 0x001F68BF
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		public string OnClientLoad
		{
			get
			{
				return this._onClientLoad;
			}
			set
			{
				this._onClientLoad = value;
			}
		}

		// Token: 0x17002B9C RID: 11164
		// (get) Token: 0x06008A13 RID: 35347 RVA: 0x001F86C8 File Offset: 0x001F68C8
		// (set) Token: 0x06008A14 RID: 35348 RVA: 0x001F86D0 File Offset: 0x001F68D0
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("selectedTabChanging")]
		public string OnClientSelectedTabChanging
		{
			get
			{
				return this._onClientSelectedTabChanging;
			}
			set
			{
				this._onClientSelectedTabChanging = value;
			}
		}

		// Token: 0x17002B9D RID: 11165
		// (get) Token: 0x06008A15 RID: 35349 RVA: 0x001F86D9 File Offset: 0x001F68D9
		// (set) Token: 0x06008A16 RID: 35350 RVA: 0x001F86E1 File Offset: 0x001F68E1
		[Category("Client-side events")]
		[ClientPropertyName("selectedTabChanged")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientSelectedTabChanged
		{
			get
			{
				return this._onClientSelectedTabChanged;
			}
			set
			{
				this._onClientSelectedTabChanged = value;
			}
		}

		// Token: 0x17002B9E RID: 11166
		// (get) Token: 0x06008A17 RID: 35351 RVA: 0x001F86EA File Offset: 0x001F68EA
		// (set) Token: 0x06008A18 RID: 35352 RVA: 0x001F86F2 File Offset: 0x001F68F2
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("buttonClicking")]
		public string OnClientButtonClicking
		{
			get
			{
				return this._onClientButtonClicking;
			}
			set
			{
				this._onClientButtonClicking = value;
			}
		}

		// Token: 0x17002B9F RID: 11167
		// (get) Token: 0x06008A19 RID: 35353 RVA: 0x001F86FB File Offset: 0x001F68FB
		// (set) Token: 0x06008A1A RID: 35354 RVA: 0x001F8703 File Offset: 0x001F6903
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("buttonClicked")]
		public string OnClientButtonClicked
		{
			get
			{
				return this._onClientButtonClicked;
			}
			set
			{
				this._onClientButtonClicked = value;
			}
		}

		// Token: 0x17002BA0 RID: 11168
		// (get) Token: 0x06008A1B RID: 35355 RVA: 0x001F870C File Offset: 0x001F690C
		// (set) Token: 0x06008A1C RID: 35356 RVA: 0x001F8714 File Offset: 0x001F6914
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("splitButtonClicking")]
		public string OnClientSplitButtonClicking
		{
			get
			{
				return this._onClientSplitButtonClicking;
			}
			set
			{
				this._onClientSplitButtonClicking = value;
			}
		}

		// Token: 0x17002BA1 RID: 11169
		// (get) Token: 0x06008A1D RID: 35357 RVA: 0x001F871D File Offset: 0x001F691D
		// (set) Token: 0x06008A1E RID: 35358 RVA: 0x001F8725 File Offset: 0x001F6925
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("splitButtonClicked")]
		public string OnClientSplitButtonClicked
		{
			get
			{
				return this._onClientSplitButtonClicked;
			}
			set
			{
				this._onClientSplitButtonClicked = value;
			}
		}

		// Token: 0x17002BA2 RID: 11170
		// (get) Token: 0x06008A1F RID: 35359 RVA: 0x001F872E File Offset: 0x001F692E
		// (set) Token: 0x06008A20 RID: 35360 RVA: 0x001F8736 File Offset: 0x001F6936
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("menuItemClicking")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientMenuItemClicking
		{
			get
			{
				return this._onClientMenuItemClicking;
			}
			set
			{
				this._onClientMenuItemClicking = value;
			}
		}

		// Token: 0x17002BA3 RID: 11171
		// (get) Token: 0x06008A21 RID: 35361 RVA: 0x001F873F File Offset: 0x001F693F
		// (set) Token: 0x06008A22 RID: 35362 RVA: 0x001F8747 File Offset: 0x001F6947
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("menuItemClicked")]
		public string OnClientMenuItemClicked
		{
			get
			{
				return this._onClientMenuItemClicked;
			}
			set
			{
				this._onClientMenuItemClicked = value;
			}
		}

		// Token: 0x17002BA4 RID: 11172
		// (get) Token: 0x06008A23 RID: 35363 RVA: 0x001F8750 File Offset: 0x001F6950
		// (set) Token: 0x06008A24 RID: 35364 RVA: 0x001F8758 File Offset: 0x001F6958
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("launcherClicking")]
		public string OnClientLauncherClicking
		{
			get
			{
				return this._onClientLauncherClicking;
			}
			set
			{
				this._onClientLauncherClicking = value;
			}
		}

		// Token: 0x17002BA5 RID: 11173
		// (get) Token: 0x06008A25 RID: 35365 RVA: 0x001F8761 File Offset: 0x001F6961
		// (set) Token: 0x06008A26 RID: 35366 RVA: 0x001F8769 File Offset: 0x001F6969
		[DefaultValue("")]
		[ClientPropertyName("launcherClicked")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientLauncherClicked
		{
			get
			{
				return this._onClientLauncherClicked;
			}
			set
			{
				this._onClientLauncherClicked = value;
			}
		}

		// Token: 0x17002BA6 RID: 11174
		// (get) Token: 0x06008A27 RID: 35367 RVA: 0x001F8772 File Offset: 0x001F6972
		// (set) Token: 0x06008A28 RID: 35368 RVA: 0x001F877A File Offset: 0x001F697A
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("buttonToggling")]
		public string OnClientButtonToggling
		{
			get
			{
				return this._onClientButtonToggling;
			}
			set
			{
				this._onClientButtonToggling = value;
			}
		}

		// Token: 0x17002BA7 RID: 11175
		// (get) Token: 0x06008A29 RID: 35369 RVA: 0x001F8783 File Offset: 0x001F6983
		// (set) Token: 0x06008A2A RID: 35370 RVA: 0x001F878B File Offset: 0x001F698B
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("buttonToggled")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientButtonToggled
		{
			get
			{
				return this._onClientButtonToggled;
			}
			set
			{
				this._onClientButtonToggled = value;
			}
		}

		// Token: 0x17002BA8 RID: 11176
		// (get) Token: 0x06008A2B RID: 35371 RVA: 0x001F8794 File Offset: 0x001F6994
		// (set) Token: 0x06008A2C RID: 35372 RVA: 0x001F879C File Offset: 0x001F699C
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("toggleListToggling")]
		public string OnClientToggleListToggling
		{
			get
			{
				return this._onClientToggleListToggling;
			}
			set
			{
				this._onClientToggleListToggling = value;
			}
		}

		// Token: 0x17002BA9 RID: 11177
		// (get) Token: 0x06008A2D RID: 35373 RVA: 0x001F87A5 File Offset: 0x001F69A5
		// (set) Token: 0x06008A2E RID: 35374 RVA: 0x001F87AD File Offset: 0x001F69AD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("toggleListToggled")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientToggleListToggled
		{
			get
			{
				return this._onClientToggleListToggled;
			}
			set
			{
				this._onClientToggleListToggled = value;
			}
		}

		// Token: 0x17002BAA RID: 11178
		// (get) Token: 0x06008A2F RID: 35375 RVA: 0x001F87B6 File Offset: 0x001F69B6
		// (set) Token: 0x06008A30 RID: 35376 RVA: 0x001F87BE File Offset: 0x001F69BE
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("applicationMenuItemClicking")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientApplicationMenuItemClicking
		{
			get
			{
				return this._onClientApplicationMenuItemClicking;
			}
			set
			{
				this._onClientApplicationMenuItemClicking = value;
			}
		}

		// Token: 0x17002BAB RID: 11179
		// (get) Token: 0x06008A31 RID: 35377 RVA: 0x001F87C7 File Offset: 0x001F69C7
		// (set) Token: 0x06008A32 RID: 35378 RVA: 0x001F87CF File Offset: 0x001F69CF
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("applicationMenuItemClicked")]
		[DefaultValue("")]
		public string OnClientApplicationMenuItemClicked
		{
			get
			{
				return this._onClientApplicationMenuItemClicked;
			}
			set
			{
				this._onClientApplicationMenuItemClicked = value;
			}
		}

		// Token: 0x17002BAC RID: 11180
		// (get) Token: 0x06008A33 RID: 35379 RVA: 0x001F87D8 File Offset: 0x001F69D8
		// (set) Token: 0x06008A34 RID: 35380 RVA: 0x001F87E0 File Offset: 0x001F69E0
		[ClientPropertyName("minimizing")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientMinimizing
		{
			get
			{
				return this._onClientMinimizing;
			}
			set
			{
				this._onClientMinimizing = value;
			}
		}

		// Token: 0x17002BAD RID: 11181
		// (get) Token: 0x06008A35 RID: 35381 RVA: 0x001F87E9 File Offset: 0x001F69E9
		// (set) Token: 0x06008A36 RID: 35382 RVA: 0x001F87F1 File Offset: 0x001F69F1
		[ClientControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("minimized")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientMinimized
		{
			get
			{
				return this._onClientMinimized;
			}
			set
			{
				this._onClientMinimized = value;
			}
		}

		// Token: 0x17002BAE RID: 11182
		// (get) Token: 0x06008A37 RID: 35383 RVA: 0x001F87FA File Offset: 0x001F69FA
		// (set) Token: 0x06008A38 RID: 35384 RVA: 0x001F8802 File Offset: 0x001F6A02
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("maximizing")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientMaximizing
		{
			get
			{
				return this._onClientMaximizing;
			}
			set
			{
				this._onClientMaximizing = value;
			}
		}

		// Token: 0x17002BAF RID: 11183
		// (get) Token: 0x06008A39 RID: 35385 RVA: 0x001F880B File Offset: 0x001F6A0B
		// (set) Token: 0x06008A3A RID: 35386 RVA: 0x001F8813 File Offset: 0x001F6A13
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("maximized")]
		public string OnClientMaximized
		{
			get
			{
				return this._onClientMaximized;
			}
			set
			{
				this._onClientMaximized = value;
			}
		}

		// Token: 0x17002BB0 RID: 11184
		// (get) Token: 0x06008A3B RID: 35387 RVA: 0x001F881C File Offset: 0x001F6A1C
		// (set) Token: 0x06008A3C RID: 35388 RVA: 0x001F8824 File Offset: 0x001F6A24
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("comboBoxSelectedIndexChanging")]
		public string OnClientComboBoxSelectedIndexChanging
		{
			get
			{
				return this._onClientComboBoxSelectedIndexChanging;
			}
			set
			{
				this._onClientComboBoxSelectedIndexChanging = value;
			}
		}

		// Token: 0x17002BB1 RID: 11185
		// (get) Token: 0x06008A3D RID: 35389 RVA: 0x001F882D File Offset: 0x001F6A2D
		// (set) Token: 0x06008A3E RID: 35390 RVA: 0x001F8835 File Offset: 0x001F6A35
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("comboBoxSelectedIndexChanged")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientComboBoxSelectedIndexChanged
		{
			get
			{
				return this._onClientComboBoxSelectedIndexChanged;
			}
			set
			{
				this._onClientComboBoxSelectedIndexChanged = value;
			}
		}

		// Token: 0x17002BB2 RID: 11186
		// (get) Token: 0x06008A3F RID: 35391 RVA: 0x001F883E File Offset: 0x001F6A3E
		// (set) Token: 0x06008A40 RID: 35392 RVA: 0x001F8846 File Offset: 0x001F6A46
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("comboBoxTextChanged")]
		public string OnClientComboBoxTextChanged
		{
			get
			{
				return this._onClientComboBoxTextChanged;
			}
			set
			{
				this._onClientComboBoxTextChanged = value;
			}
		}

		// Token: 0x17002BB3 RID: 11187
		// (get) Token: 0x06008A41 RID: 35393 RVA: 0x001F884F File Offset: 0x001F6A4F
		// (set) Token: 0x06008A42 RID: 35394 RVA: 0x001F8857 File Offset: 0x001F6A57
		[ClientPropertyName("dropDownSelectedIndexChanging")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientDropDownSelectedIndexChanging
		{
			get
			{
				return this._onClientDropDownSelectedIndexChanging;
			}
			set
			{
				this._onClientDropDownSelectedIndexChanging = value;
			}
		}

		// Token: 0x17002BB4 RID: 11188
		// (get) Token: 0x06008A43 RID: 35395 RVA: 0x001F8860 File Offset: 0x001F6A60
		// (set) Token: 0x06008A44 RID: 35396 RVA: 0x001F8868 File Offset: 0x001F6A68
		[ClientPropertyName("dropDownSelectedIndexChanged")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientDropDownSelectedIndexChanged
		{
			get
			{
				return this._onClientDropDownSelectedIndexChanged;
			}
			set
			{
				this._onClientDropDownSelectedIndexChanged = value;
			}
		}

		// Token: 0x17002BB5 RID: 11189
		// (get) Token: 0x06008A45 RID: 35397 RVA: 0x001F8871 File Offset: 0x001F6A71
		// (set) Token: 0x06008A46 RID: 35398 RVA: 0x001F8879 File Offset: 0x001F6A79
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("numericTextBoxValueChanging")]
		public string OnClientNumericTextBoxValueChanging
		{
			get
			{
				return this._onClientNumericTextBoxValueChanging;
			}
			set
			{
				this._onClientNumericTextBoxValueChanging = value;
			}
		}

		// Token: 0x17002BB6 RID: 11190
		// (get) Token: 0x06008A47 RID: 35399 RVA: 0x001F8882 File Offset: 0x001F6A82
		// (set) Token: 0x06008A48 RID: 35400 RVA: 0x001F888A File Offset: 0x001F6A8A
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("numericTextBoxValueChanged")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientNumericTextBoxValueChanged
		{
			get
			{
				return this._onClientNumericTextBoxValueChanged;
			}
			set
			{
				this._onClientNumericTextBoxValueChanged = value;
			}
		}

		// Token: 0x17002BB7 RID: 11191
		// (get) Token: 0x06008A49 RID: 35401 RVA: 0x001F8893 File Offset: 0x001F6A93
		// (set) Token: 0x06008A4A RID: 35402 RVA: 0x001F889B File Offset: 0x001F6A9B
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("colorPickerColorChanging")]
		[Category("Client-side events")]
		public string OnClientColorPickerColorChanging
		{
			get
			{
				return this._onClientColorPickerColorChanging;
			}
			set
			{
				this._onClientColorPickerColorChanging = value;
			}
		}

		// Token: 0x17002BB8 RID: 11192
		// (get) Token: 0x06008A4B RID: 35403 RVA: 0x001F88A4 File Offset: 0x001F6AA4
		// (set) Token: 0x06008A4C RID: 35404 RVA: 0x001F88AC File Offset: 0x001F6AAC
		[Category("Client-side events")]
		[ClientPropertyName("colorPickerColorChanged")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientColorPickerColorChanged
		{
			get
			{
				return this._onClientColorPickerColorChanged;
			}
			set
			{
				this._onClientColorPickerColorChanged = value;
			}
		}

		// Token: 0x17002BB9 RID: 11193
		// (get) Token: 0x06008A4D RID: 35405 RVA: 0x001F88B5 File Offset: 0x001F6AB5
		// (set) Token: 0x06008A4E RID: 35406 RVA: 0x001F88BD File Offset: 0x001F6ABD
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("galleryCommandPreview")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientGalleryCommandPreview
		{
			get
			{
				return this._onClientGalleryCommandPreview;
			}
			set
			{
				this._onClientGalleryCommandPreview = value;
			}
		}

		// Token: 0x17002BBA RID: 11194
		// (get) Token: 0x06008A4F RID: 35407 RVA: 0x001F88C6 File Offset: 0x001F6AC6
		// (set) Token: 0x06008A50 RID: 35408 RVA: 0x001F88CE File Offset: 0x001F6ACE
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("galleryCommandPreviewEnd")]
		[Category("Client-side events")]
		public string OnClientGalleryCommandPreviewEnd
		{
			get
			{
				return this._onClientGalleryCommandPreviewEnd;
			}
			set
			{
				this._onClientGalleryCommandPreviewEnd = value;
			}
		}

		// Token: 0x17002BBB RID: 11195
		// (get) Token: 0x06008A51 RID: 35409 RVA: 0x001F88D7 File Offset: 0x001F6AD7
		// (set) Token: 0x06008A52 RID: 35410 RVA: 0x001F88DF File Offset: 0x001F6ADF
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("galleryCommand")]
		public string OnClientGalleryCommand
		{
			get
			{
				return this._onClientGalleryCommand;
			}
			set
			{
				this._onClientGalleryCommand = value;
			}
		}

		// Token: 0x17002BBC RID: 11196
		// (get) Token: 0x06008A53 RID: 35411 RVA: 0x001F88E8 File Offset: 0x001F6AE8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarContextualTabGroupCollection ContextualTabGroups
		{
			get
			{
				if (this._contextualTabGroups == null)
				{
					this._contextualTabGroups = new RibbonBarContextualTabGroupCollection();
					this._contextualTabGroups.ParentWebControl = this;
					this._contextualTabGroups.RibbonBar = this;
				}
				return this._contextualTabGroups;
			}
		}

		// Token: 0x17002BBD RID: 11197
		// (get) Token: 0x06008A54 RID: 35412 RVA: 0x001F891B File Offset: 0x001F6B1B
		// (set) Token: 0x06008A55 RID: 35413 RVA: 0x001F8923 File Offset: 0x001F6B23
		public bool EnableQuickAccessToolbar { get; set; }

		// Token: 0x06008A56 RID: 35414 RVA: 0x001F892C File Offset: 0x001F6B2C
		private List<RibbonBarGroup> GetAllGroups()
		{
			List<RibbonBarGroup> list = new List<RibbonBarGroup>();
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				list.AddRange(ribbonBarTab.Groups);
			}
			return list;
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x001F898C File Offset: 0x001F6B8C
		internal List<RibbonBarGroup> GetVisibleGroups()
		{
			List<RibbonBarGroup> list = new List<RibbonBarGroup>();
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				if (ribbonBarTab.Visible)
				{
					list.AddRange(ribbonBarTab.GetVisibleGroups());
				}
			}
			return list;
		}

		// Token: 0x17002BBE RID: 11198
		// (get) Token: 0x06008A58 RID: 35416 RVA: 0x001F89F4 File Offset: 0x001F6BF4
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002BBF RID: 11199
		// (get) Token: 0x06008A59 RID: 35417 RVA: 0x001F89F7 File Offset: 0x001F6BF7
		protected override IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateControlRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x001F8A13 File Offset: 0x001F6C13
		protected override IRenderer CreateControlRenderer()
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarLiteRenderer(this);
			}
			return new RibbonBarClassicRenderer(this);
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x001F8A2C File Offset: 0x001F6C2C
		protected override void OnPreRender(EventArgs e)
		{
			this.Tabs.ParentWebControl = this;
			int selectedTabIndex = this.SelectedTabIndex;
			if (selectedTabIndex > -1)
			{
				this.GetAllTabs()[selectedTabIndex].Selected = true;
			}
			base.OnPreRender(e);
			if (base.ScriptManager.LoadScriptsBeforeUI)
			{
				string text = string.Format("Telerik.Web.UI.RadRibbonBar._preInitialize(\"{0}\");", this.ClientID);
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadRibbonBar), this.ClientID + text, text, true);
			}
		}

		// Token: 0x17002BC0 RID: 11200
		// (get) Token: 0x06008A5C RID: 35420 RVA: 0x001F8AAA File Offset: 0x001F6CAA
		// (set) Token: 0x06008A5D RID: 35421 RVA: 0x001F8AB2 File Offset: 0x001F6CB2
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x001F8ABC File Offset: 0x001F6CBC
		internal List<RibbonBarTab> GetVisibleTabs()
		{
			List<RibbonBarTab> list = new List<RibbonBarTab>();
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				if (ribbonBarTab.Visible)
				{
					list.Add(ribbonBarTab);
				}
			}
			return list;
		}

		// Token: 0x06008A5F RID: 35423 RVA: 0x001F8B20 File Offset: 0x001F6D20
		internal List<RibbonBarTab> GetTabsToRender()
		{
			List<RibbonBarTab> list = new List<RibbonBarTab>();
			list.AddRange(this.GetVisibleTabs());
			list.AddRange(this.GetContextualTabsToRender());
			return list;
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x001F8B4C File Offset: 0x001F6D4C
		internal List<RibbonBarTab> GetAllTabs()
		{
			List<RibbonBarTab> list = new List<RibbonBarTab>();
			list.AddRange(this.Tabs);
			list.AddRange(this.GetContextualTabs());
			return list;
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x001F8B78 File Offset: 0x001F6D78
		private List<RibbonBarGroup> GetGroupsToRender()
		{
			List<RibbonBarGroup> list = new List<RibbonBarGroup>();
			list.AddRange(this.GetVisibleGroups());
			list.AddRange(this.GetContextualTabGroupsToRender());
			return list;
		}

		// Token: 0x17002BC1 RID: 11201
		// (get) Token: 0x06008A62 RID: 35426 RVA: 0x001F8BA4 File Offset: 0x001F6DA4
		private int GetResolvedSelectedTabIndex
		{
			get
			{
				int num = (int)(this.ViewState["SelectedTabIndex"] ?? -1);
				List<RibbonBarTab> tabsToRender = this.GetTabsToRender();
				if (num == -1)
				{
					if (tabsToRender.Count > 0)
					{
						num = 0;
					}
				}
				else if (num < -1 || num > tabsToRender.Count - 1)
				{
					num = ((tabsToRender.Count > 0) ? 0 : -1);
				}
				if (num > -1)
				{
					num = this.GetAllTabs().IndexOf(tabsToRender[num]);
				}
				return num;
			}
		}

		// Token: 0x17002BC2 RID: 11202
		// (get) Token: 0x06008A63 RID: 35427 RVA: 0x001F8C1E File Offset: 0x001F6E1E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002BC3 RID: 11203
		// (get) Token: 0x06008A64 RID: 35428 RVA: 0x001F8C22 File Offset: 0x001F6E22
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x06008A65 RID: 35429 RVA: 0x001F8C2F File Offset: 0x001F6E2F
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x001F8C40 File Offset: 0x001F6E40
		private void DescribePostBack(IScriptDescriptor descriptor)
		{
			if (base.Events[RadRibbonBar.SelectedTabChangeEvent] != null)
			{
				descriptor.AddProperty("_postBackOnSelectedTabChange", true);
			}
			if (base.Events[RadRibbonBar.ButtonClickEvent] != null)
			{
				descriptor.AddProperty("_postBackOnButtonClick", true);
			}
			if (base.Events[RadRibbonBar.SplitButtonClickEvent] != null)
			{
				descriptor.AddProperty("_postBackOnSplitButtonClick", true);
			}
			if (base.Events[RadRibbonBar.MenuItemClickEvent] != null)
			{
				descriptor.AddProperty("_postBackOnMenuItemClick", true);
			}
			if (base.Events[RadRibbonBar.LauncherClickEvent] != null)
			{
				descriptor.AddProperty("_postBackOnLauncherClick", true);
			}
			if (base.Events[RadRibbonBar.ButtonToggleEvent] != null)
			{
				descriptor.AddProperty("_postBackOnButtonToggle", true);
			}
			if (base.Events[RadRibbonBar.ToggleListToggleEvent] != null)
			{
				descriptor.AddProperty("_postBackOnToggleListToggle", true);
			}
			if (base.Events[RadRibbonBar.ApplicationMenuItemClickEvent] != null)
			{
				descriptor.AddProperty("_postBackOnApplicationMenuItemClick", true);
			}
			if (base.Events[RadRibbonBar.ComboBoxSelectedIndexChangedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnComboBoxSelectedIndexChanged", true);
			}
			if (base.Events[RadRibbonBar.ComboBoxTextChangedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnComboBoxTextChanged", true);
			}
			if (base.Events[RadRibbonBar.DropDownSelectedIndexChangedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnDropDownSelectedIndexChanged", true);
			}
			if (base.Events[RadRibbonBar.NumericTextBoxValueChangedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnNumericTextBoxValueChanged", true);
			}
			if (base.Events[RadRibbonBar.ColorPickerColorChangedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnColorPickerColorChanged", true);
			}
			if (base.Events[RadRibbonBar.GalleryCommandEvent] != null)
			{
				descriptor.AddProperty("_postBackOnGalleryCommand", true);
			}
			if (base.Events[RadRibbonBar.CommandEvent] != null)
			{
				descriptor.AddProperty("_postBackOnCommand", true);
			}
			if (this.ShouldRenderPostBackReference)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
		}

		// Token: 0x06008A67 RID: 35431 RVA: 0x001F8E74 File Offset: 0x001F7074
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.DescribePostBack(descriptor);
			base.DescribeRenderMode(descriptor);
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new RibbonBarTabConverter(),
				new RibbonBarGroupConverter(),
				new RibbonBarButtonConverter(),
				new RibbonBarSplitButtonConverter(),
				new RibbonBarMenuConverter(),
				new RibbonBarMenuItemConverter(),
				new RibbonBarToggleListConverter(),
				new RibbonBarTemplateItemConvertor(),
				new RibbonBarKeyboardNavigationConverter(),
				new RibbonBarComboBoxConverter(),
				new RibbonBarDropDownConverter(),
				new RibbonBarListItemConverter(),
				new RibbonBarNumericTextBoxConverter(),
				new RibbonBarColorPickerConverter(),
				new RibbonBarGalleryConverter(),
				new RibbonBarGalleryCategoryConverter(),
				new RibbonBarGalleryItemConverter(),
				new RibbonBarApplicationMenuItemConverter(),
				new RibbonBarApplicationSplitMenuItemConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			descriptor.AddScriptProperty("defaultImageUrl", "\"" + this.Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), "Telerik.Web.UI.Skins.Common.RibbonBar.NoImage.png") + "\"");
			descriptor.AddScriptProperty("defaultImageUrlLarge", "\"" + this.Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), "Telerik.Web.UI.Skins.Common.RibbonBar.NoImageLarge.png") + "\"");
			descriptor.AddScriptProperty("defaultDisabledImageUrl", "\"" + this.Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImage.png") + "\"");
			descriptor.AddScriptProperty("defaultDisabledImageUrlLarge", "\"" + this.Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImageLarge.png") + "\"");
			descriptor.AddScriptProperty("tabData", javaScriptSerializer.Serialize(this.GetTabsToRender()));
			descriptor.AddScriptProperty("groupData", javaScriptSerializer.Serialize(this.GetGroupsToRender()));
			descriptor.AddScriptProperty("_navigationSettings", javaScriptSerializer.Serialize(this.KeyboardNavigationSettings));
			descriptor.AddProperty("_enableMinimizing", this.EnableMinimizing);
			descriptor.AddProperty("_minimized", this.Minimized);
			descriptor.AddProperty("_enableAutoArrange", this.EnableAutoArrange);
			if (this.ApplicationMenu != null)
			{
				descriptor.AddScriptProperty("applicationMenuData", javaScriptSerializer.Serialize(this.ApplicationMenu.GetVisibleItems()));
			}
			if (this.EnableQuickAccessToolbar)
			{
				descriptor.AddScriptProperty("qatItemsHierarchicalIndices", javaScriptSerializer.Serialize(this.GetQuickAccessEnabledItemsHierarchicalIndices()));
			}
		}

		// Token: 0x06008A68 RID: 35432 RVA: 0x001F9110 File Offset: 0x001F7310
		internal WebControl GetItemByHierarchicalIndex(string index, bool visibleOnly)
		{
			string[] array = index.Split(new char[]
			{
				':'
			});
			WebControl result = null;
			if (array.Length > 2)
			{
				RibbonBarTab ribbonBarTab = visibleOnly ? this.GetTabsToRender()[int.Parse(array[0])] : this.Tabs[int.Parse(array[0])];
				RibbonBarGroup ribbonBarGroup = visibleOnly ? ribbonBarTab.GetVisibleGroups()[int.Parse(array[1])] : ribbonBarTab.Groups[int.Parse(array[1])];
				RibbonBarItem ribbonBarItem = ribbonBarGroup.GetFunctionalItems(visibleOnly)[int.Parse(array[2])];
				if (array.Length <= 3)
				{
					return ribbonBarItem;
				}
				if (ribbonBarItem.ItemType == RibbonBarItemType.Menu)
				{
					RibbonBarMenuItem ribbonBarMenuItem = (ribbonBarItem as RibbonBarMenu).Items[int.Parse(array[3])];
					for (int i = 4; i < array.Length; i++)
					{
						ribbonBarMenuItem = ribbonBarMenuItem.Items[int.Parse(array[i])];
					}
					result = ribbonBarMenuItem;
				}
				else if (ribbonBarItem.ItemType == RibbonBarItemType.SplitButton)
				{
					result = (ribbonBarItem as RibbonBarSplitButton).Buttons[int.Parse(array[3])];
				}
				else if (ribbonBarItem.ItemType == RibbonBarItemType.ToggleList)
				{
					result = (ribbonBarItem as RibbonBarToggleList).ToggleButtons[int.Parse(array[3])];
				}
			}
			return result;
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x001F9259 File Offset: 0x001F7459
		internal WebControl GetItemByHierarchicalIndex(string index)
		{
			return this.GetItemByHierarchicalIndex(index, false);
		}

		// Token: 0x06008A6A RID: 35434 RVA: 0x001F9264 File Offset: 0x001F7464
		internal string GetItemHierarchicalIndex(RibbonBarItem item)
		{
			RibbonBarGroup group = item.Group;
			if (group == null || group.Tab == null || group.Tab.RibbonBar == null || !item.Visible)
			{
				return null;
			}
			RibbonBarTab tab = group.Tab;
			int num = group.GetVisibleFunctionalItems().IndexOf(item);
			int num2 = tab.GetVisibleGroups().IndexOf(group);
			int num3 = tab.RibbonBar.GetVisibleTabs().IndexOf(tab);
			return string.Format("{0}:{1}:{2}", num3, num2, num);
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x001F92F0 File Offset: 0x001F74F0
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = ((IStateManager)this.ContextualTabGroups).SaveViewState();
			return new object[]
			{
				obj,
				obj2
			};
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x001F9320 File Offset: 0x001F7520
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					base.LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					((IStateManager)this.ContextualTabGroups).LoadViewState(array[1]);
				}
			}
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x001F9358 File Offset: 0x001F7558
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ContextualTabGroups).TrackViewState();
		}

		// Token: 0x06008A6E RID: 35438 RVA: 0x001F936B File Offset: 0x001F756B
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06008A6F RID: 35439 RVA: 0x001F9377 File Offset: 0x001F7577
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06008A70 RID: 35440 RVA: 0x001F9380 File Offset: 0x001F7580
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06008A71 RID: 35441 RVA: 0x001F9389 File Offset: 0x001F7589
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForContents(reader);
		}

		// Token: 0x06008A72 RID: 35442 RVA: 0x001F93A0 File Offset: 0x001F75A0
		protected virtual void ReadXmlForContents(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						string name;
						if ((name = reader.Name) != null)
						{
							if (!(name == "ApplicationMenu"))
							{
								if (name == "Tab")
								{
									XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarTab));
									RibbonBarTab tab = (RibbonBarTab)xmlSerializer.Deserialize(xmlReader);
									this.Tabs.Add(tab);
								}
							}
							else
							{
								XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarApplicationMenu));
								RibbonBarApplicationMenu applicationMenu = (RibbonBarApplicationMenu)xmlSerializer.Deserialize(xmlReader);
								this.ApplicationMenu = applicationMenu;
							}
						}
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06008A73 RID: 35443 RVA: 0x001F947C File Offset: 0x001F767C
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForContents(writer);
		}

		// Token: 0x06008A74 RID: 35444 RVA: 0x001F9498 File Offset: 0x001F7698
		protected virtual void WriteXmlForContents(XmlWriter writer)
		{
			if (this.ApplicationMenu != null)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarApplicationMenu));
				xmlSerializer.Serialize(writer, this.ApplicationMenu);
			}
			foreach (RibbonBarTab o in this.Tabs)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarTab));
				xmlSerializer.Serialize(writer, o);
			}
		}

		// Token: 0x06008A76 RID: 35446 RVA: 0x001F9524 File Offset: 0x001F7724
		// Note: this type is marked as 'beforefieldinit'.
		static RadRibbonBar()
		{
			RadRibbonBar.SelectedTabChangeEvent = new object();
			RadRibbonBar.ButtonClickEvent = new object();
			RadRibbonBar.SplitButtonClickEvent = new object();
			RadRibbonBar.MenuItemClickEvent = new object();
			RadRibbonBar.LauncherClickEvent = new object();
			RadRibbonBar.ButtonToggleEvent = new object();
			RadRibbonBar.ToggleListToggleEvent = new object();
			RadRibbonBar.ApplicationMenuItemClickEvent = new object();
			RadRibbonBar.ComboBoxSelectedIndexChangedEvent = new object();
			RadRibbonBar.DropDownSelectedIndexChangedEvent = new object();
			RadRibbonBar.ComboBoxTextChangedEvent = new object();
			RadRibbonBar.NumericTextBoxValueChangedEvent = new object();
			RadRibbonBar.ColorPickerColorChangedEvent = new object();
			RadRibbonBar.GalleryCommandEvent = new object();
			RadRibbonBar.CommandEvent = new object();
		}

		// Token: 0x0400267E RID: 9854
		private readonly string[] ContextualTabGroupDefaultColors = new string[]
		{
			"#007dc5",
			"#cc0000",
			"#008800",
			"#ffa500",
			"#d0c000",
			"#800080"
		};

		// Token: 0x0400267F RID: 9855
		private int _currentContextualTabGroupColorIndex;

		// Token: 0x0400268F RID: 9871
		private RibbonBarTabCollection _tabs;

		// Token: 0x04002690 RID: 9872
		private RibbonBarKeyboardNavigationSettings keyboardSettings;

		// Token: 0x04002691 RID: 9873
		private RibbonBarApplicationMenu _applicationMenu;

		// Token: 0x04002692 RID: 9874
		private string _onClientLoad = string.Empty;

		// Token: 0x04002693 RID: 9875
		private string _onClientSelectedTabChanging = string.Empty;

		// Token: 0x04002694 RID: 9876
		private string _onClientSelectedTabChanged = string.Empty;

		// Token: 0x04002695 RID: 9877
		private string _onClientButtonClicking = string.Empty;

		// Token: 0x04002696 RID: 9878
		private string _onClientButtonClicked = string.Empty;

		// Token: 0x04002697 RID: 9879
		private string _onClientSplitButtonClicking = string.Empty;

		// Token: 0x04002698 RID: 9880
		private string _onClientSplitButtonClicked = string.Empty;

		// Token: 0x04002699 RID: 9881
		private string _onClientMenuItemClicking = string.Empty;

		// Token: 0x0400269A RID: 9882
		private string _onClientMenuItemClicked = string.Empty;

		// Token: 0x0400269B RID: 9883
		private string _onClientLauncherClicking = string.Empty;

		// Token: 0x0400269C RID: 9884
		private string _onClientLauncherClicked = string.Empty;

		// Token: 0x0400269D RID: 9885
		private string _onClientButtonToggling = string.Empty;

		// Token: 0x0400269E RID: 9886
		private string _onClientButtonToggled = string.Empty;

		// Token: 0x0400269F RID: 9887
		private string _onClientToggleListToggling = string.Empty;

		// Token: 0x040026A0 RID: 9888
		private string _onClientToggleListToggled = string.Empty;

		// Token: 0x040026A1 RID: 9889
		private string _onClientApplicationMenuItemClicking = string.Empty;

		// Token: 0x040026A2 RID: 9890
		private string _onClientApplicationMenuItemClicked = string.Empty;

		// Token: 0x040026A3 RID: 9891
		private string _onClientMinimizing = string.Empty;

		// Token: 0x040026A4 RID: 9892
		private string _onClientMinimized = string.Empty;

		// Token: 0x040026A5 RID: 9893
		private string _onClientMaximizing = string.Empty;

		// Token: 0x040026A6 RID: 9894
		private string _onClientMaximized = string.Empty;

		// Token: 0x040026A7 RID: 9895
		private string _onClientComboBoxSelectedIndexChanging = string.Empty;

		// Token: 0x040026A8 RID: 9896
		private string _onClientComboBoxSelectedIndexChanged = string.Empty;

		// Token: 0x040026A9 RID: 9897
		private string _onClientComboBoxTextChanged = string.Empty;

		// Token: 0x040026AA RID: 9898
		private string _onClientDropDownSelectedIndexChanging = string.Empty;

		// Token: 0x040026AB RID: 9899
		private string _onClientDropDownSelectedIndexChanged = string.Empty;

		// Token: 0x040026AC RID: 9900
		private string _onClientNumericTextBoxValueChanging = string.Empty;

		// Token: 0x040026AD RID: 9901
		private string _onClientNumericTextBoxValueChanged = string.Empty;

		// Token: 0x040026AE RID: 9902
		private string _onClientColorPickerColorChanging = string.Empty;

		// Token: 0x040026AF RID: 9903
		private string _onClientColorPickerColorChanged = string.Empty;

		// Token: 0x040026B0 RID: 9904
		private string _onClientGalleryCommandPreview = string.Empty;

		// Token: 0x040026B1 RID: 9905
		private string _onClientGalleryCommandPreviewEnd = string.Empty;

		// Token: 0x040026B2 RID: 9906
		private string _onClientGalleryCommand = string.Empty;

		// Token: 0x040026B3 RID: 9907
		private RibbonBarContextualTabGroupCollection _contextualTabGroups;

		// Token: 0x040026B4 RID: 9908
		private IRenderer _renderer;

		// Token: 0x040026B5 RID: 9909
		internal bool shouldRenderMaximizedRibbon;
	}
}
