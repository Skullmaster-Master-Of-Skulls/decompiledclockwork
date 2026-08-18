using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000796 RID: 1942
	internal class RibbonBarClassicRenderer : RibbonBarRenderBase
	{
		// Token: 0x06004420 RID: 17440 RVA: 0x000D5A04 File Offset: 0x000D3C04
		public RibbonBarClassicRenderer(RadRibbonBar ribbonBar) : base(ribbonBar)
		{
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x000D5A10 File Offset: 0x000D3C10
		public override string CssClassFormatString
		{
			get
			{
				List<string> list = new List<string>
				{
					"RadRibbonBar",
					"RadRibbonBar_{0}"
				};
				if (base.Owner.HasContextualTabs() || base.Owner.EnableQuickAccessToolbar)
				{
					list.Add("rrbExtendedChrome");
					if (base.Owner.Minimized && !base.Owner.shouldRenderMaximizedRibbon)
					{
						list.Add("rrbExtendedChromeMinimized");
					}
				}
				if (base.Owner.Minimized && !base.Owner.shouldRenderMaximizedRibbon)
				{
					list.Add("rrbMinimized");
				}
				return string.Join(" ", list.ToArray()).Trim();
			}
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x000D5AC0 File Offset: 0x000D3CC0
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbContentWrapOut");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbContentWrapMid");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbContentWrapIn");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.EnableQuickAccessToolbar)
			{
				this.RenderQuickAccessToolbar(writer);
			}
			if (base.Owner.Tabs.Count > 0 || base.Owner.HasContextualTabGroupsToRender() || base.Owner.ApplicationMenu != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTabs");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				this.RenderApplicationMenu(writer);
				this.RenderTabs(writer);
				if (base.Owner.RenderInactiveContextualTabGroups)
				{
					this.RenderAllContextualTabGroups(writer);
				}
				else
				{
					this.RenderActiveContextualTabGroups(writer);
				}
				writer.RenderEndTag();
				this.RenderTabsContent(writer);
			}
			if (base.Owner.EnableMinimizing)
			{
				this.RenderToggleHandle(writer);
			}
			if (!base.Owner.Width.IsEmpty && base.Owner.Width.Type != UnitType.Percentage)
			{
				this.RenderResizeHandle(writer);
			}
			this.RenderToolTip(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x000D5C00 File Offset: 0x000D3E00
		protected void RenderToolTip(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbToolTip");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.AddStyleAttribute(HtmlTextWriterStyle.MarginTop, "5px");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbToolTipTitle");
			writer.RenderBeginTag(HtmlTextWriterTag.Strong);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbToolTipDescription");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderBeginTag(HtmlTextWriterTag.P);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x000D5C86 File Offset: 0x000D3E86
		protected void RenderToggleHandle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbToggleHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x000D5CA4 File Offset: 0x000D3EA4
		protected void RenderApplicationMenu(HtmlTextWriter writer)
		{
			if (base.Owner.ApplicationMenu == null)
			{
				return;
			}
			base.Owner.ApplicationMenu.AllowRender = true;
			base.Owner.ApplicationMenu.SkinToRender = base.Owner.RuntimeSkin;
			base.Owner.ApplicationMenu.RenderControl(writer);
			base.Owner.ApplicationMenu.AllowRender = false;
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x000D5D0D File Offset: 0x000D3F0D
		protected void RenderResizeHandle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbResizeHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x000D5D2C File Offset: 0x000D3F2C
		protected void RenderTabs(HtmlTextWriter writer)
		{
			foreach (RibbonBarTab ribbonBarTab in base.Owner.GetVisibleTabs())
			{
				ribbonBarTab.RenderControl(writer);
			}
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x000D5D84 File Offset: 0x000D3F84
		protected void RenderTabsContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonArea");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			int selectedTabIndex = base.Owner.SelectedTabIndex;
			List<RibbonBarTab> allTabs = base.Owner.GetAllTabs();
			List<RibbonBarTab> tabsToRender = base.Owner.GetTabsToRender();
			for (int i = 0; i < allTabs.Count; i++)
			{
				RibbonBarTab ribbonBarTab = allTabs[i];
				if (tabsToRender.Contains(ribbonBarTab))
				{
					string text = "rrbButtonAreaIn";
					if (i != selectedTabIndex || !ribbonBarTab.Enabled)
					{
						text += " rrbHiddenButtonAreaIn";
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					foreach (RibbonBarGroup ribbonBarGroup in ribbonBarTab.Groups)
					{
						ribbonBarGroup.RenderControl(writer);
					}
					writer.RenderEndTag();
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x000D5E80 File Offset: 0x000D4080
		protected void RenderContextualTabGroups(Predicate<RibbonBarContextualTabGroup> filter, HtmlTextWriter writer)
		{
			foreach (RibbonBarContextualTabGroup ribbonBarContextualTabGroup in base.Owner.ContextualTabGroups)
			{
				if (ribbonBarContextualTabGroup.BackColor == Color.Empty)
				{
					base.Owner.SetContextualTabDefaultColor(ribbonBarContextualTabGroup);
				}
				if (filter(ribbonBarContextualTabGroup) && ribbonBarContextualTabGroup.GetVisibleTabs().Count > 0)
				{
					this.RenderTabGroup(writer, ribbonBarContextualTabGroup);
				}
			}
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x000D5F10 File Offset: 0x000D4110
		private string TabGroupCssClassToRender(RibbonBarContextualTabGroup contextualTabGroup)
		{
			List<string> list = new List<string>
			{
				"rrbContextualTabLabel"
			};
			if (!string.IsNullOrEmpty(contextualTabGroup.CssClass))
			{
				list.Add(contextualTabGroup.CssClass);
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x000D5F5C File Offset: 0x000D415C
		public void RenderTabGroup(HtmlTextWriter writer, RibbonBarContextualTabGroup contextualTabGroup)
		{
			List<string> list = new List<string>
			{
				"rrbContextualTab"
			};
			if (contextualTabGroup.Active)
			{
				list.Add("rrbContextualTabActive");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Join(" ", list.ToArray()));
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			if (contextualTabGroup.ForeColor != Color.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, this.GetHexFromNamedColor(contextualTabGroup.ForeColor));
			}
			if (contextualTabGroup.BackColor != Color.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, this.GetHexFromNamedColor(contextualTabGroup.BackColor));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.TabGroupCssClassToRender(contextualTabGroup));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(contextualTabGroup.Text);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTabs");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (RibbonBarTab ribbonBarTab in contextualTabGroup.Tabs)
			{
				ribbonBarTab.RenderControl(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x000D608C File Offset: 0x000D428C
		private string GetHexFromNamedColor(Color color)
		{
			return string.Format("#{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B);
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x000D60C4 File Offset: 0x000D42C4
		protected void RenderActiveContextualTabGroups(HtmlTextWriter writer)
		{
			this.RenderContextualTabGroups((RibbonBarContextualTabGroup group) => group.Active, writer);
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x000D60ED File Offset: 0x000D42ED
		protected void RenderAllContextualTabGroups(HtmlTextWriter writer)
		{
			this.RenderContextualTabGroups((RibbonBarContextualTabGroup group) => true, writer);
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x000D6114 File Offset: 0x000D4314
		private string QuickAccessToolbarDropDownCssClass
		{
			get
			{
				return string.Format("{0} {1}", "rrbQatItem", "rrbQatDropDown");
			}
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x000D6137 File Offset: 0x000D4337
		internal void RenderQuickAccessToolbar(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbQuickAccessToolbar");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			this.RenderQuickAccessToolbarActiveItems(writer);
			this.RenderQuickAccessToolbarDropDown(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x000D6164 File Offset: 0x000D4364
		private void RenderQuickAccessToolbarActiveItems(HtmlTextWriter writer)
		{
			foreach (RibbonBarClickableItem ribbonBarClickableItem in base.Owner.GetQuickAccessEnabledItems())
			{
				if (ribbonBarClickableItem.QuickAccess == RibbonBarItemQuickAccess.Inactive)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbQatItem");
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				bool shouldRenderButtonStripClasses = false;
				RibbonBarButton ribbonBarButton = ribbonBarClickableItem as RibbonBarButton;
				if (ribbonBarButton != null)
				{
					shouldRenderButtonStripClasses = ribbonBarButton.ShouldRenderButtonStripClasses;
					ribbonBarButton.ShouldRenderButtonStripClasses = false;
				}
				Unit width = ribbonBarClickableItem.Width;
				ribbonBarClickableItem.Width = Unit.Empty;
				IRibbonBarSizableItem ribbonBarSizableItem = ribbonBarClickableItem;
				if (ribbonBarSizableItem != null)
				{
					RibbonBarItemSize size = ribbonBarSizableItem.Size;
					ribbonBarSizableItem.Size = RibbonBarItemSize.Small;
					ribbonBarClickableItem.RenderControl(writer);
					ribbonBarSizableItem.Size = size;
				}
				ribbonBarClickableItem.Width = width;
				if (ribbonBarButton != null)
				{
					ribbonBarButton.ShouldRenderButtonStripClasses = shouldRenderButtonStripClasses;
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x000D6254 File Offset: 0x000D4454
		private void RenderQuickAccessToolbarDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.QuickAccessToolbarDropDownCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.RenderQuickAccessToolbarDropDownArrow(writer);
			this.RenderQuickAccessToolbarDropDownSlide(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x000D6280 File Offset: 0x000D4480
		private void RenderQuickAccessToolbarDropDownArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbQatButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, "Customize Quick Access Toolbar");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.Write("Customize Quick Access Toolbar");
			writer.RenderEndTag();
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x000D62CC File Offset: 0x000D44CC
		private void RenderQuickAccessToolbarDropDownSlide(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbSlide");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenu");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenuLabel");
			writer.RenderBeginTag(HtmlTextWriterTag.Strong);
			writer.Write("Customize Quick Access Toolbar");
			writer.RenderEndTag();
			if (base.Owner.HasQuickAccessEnabledItems())
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenuGroup");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				foreach (RibbonBarClickableItem ribbonBarClickableItem in base.Owner.GetQuickAccessEnabledItems())
				{
					string value = string.Format("{0} {1} {2}", "rrbMenuItem", "rrbMenuItemCheckbox", (ribbonBarClickableItem.QuickAccess == RibbonBarItemQuickAccess.Active) ? "rrbMenuItemCheckboxChecked" : string.Empty);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					writer.RenderBeginTag(HtmlTextWriterTag.Label);
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "radPreventDecorate");
					if (ribbonBarClickableItem.QuickAccess == RibbonBarItemQuickAccess.Active)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Input);
					writer.RenderEndTag();
					writer.Write(ribbonBarClickableItem.Text);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
