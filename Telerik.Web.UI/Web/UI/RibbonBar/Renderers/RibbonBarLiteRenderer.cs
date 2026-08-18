using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007BB RID: 1979
	internal class RibbonBarLiteRenderer : RibbonBarRenderBase
	{
		// Token: 0x060044F7 RID: 17655 RVA: 0x000DA05B File Offset: 0x000D825B
		public RibbonBarLiteRenderer(RadRibbonBar ribbonBar) : base(ribbonBar)
		{
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x000DA064 File Offset: 0x000D8264
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

		// Token: 0x060044F9 RID: 17657 RVA: 0x000DA114 File Offset: 0x000D8314
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbWrap");
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
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x000DA220 File Offset: 0x000D8420
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
			writer.RenderBeginTag(HtmlTextWriterTag.P);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x000DA298 File Offset: 0x000D8498
		protected void RenderToggleHandle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbToggleHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x000DA2B8 File Offset: 0x000D84B8
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

		// Token: 0x060044FD RID: 17661 RVA: 0x000DA321 File Offset: 0x000D8521
		protected void RenderResizeHandle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbResizeHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000DA340 File Offset: 0x000D8540
		protected void RenderTabs(HtmlTextWriter writer)
		{
			foreach (RibbonBarTab ribbonBarTab in base.Owner.GetVisibleTabs())
			{
				ribbonBarTab.RenderControl(writer);
			}
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000DA398 File Offset: 0x000D8598
		protected void RenderTabsContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbCommandArea");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			int selectedTabIndex = base.Owner.SelectedTabIndex;
			List<RibbonBarTab> allTabs = base.Owner.GetAllTabs();
			List<RibbonBarTab> tabsToRender = base.Owner.GetTabsToRender();
			for (int i = 0; i < allTabs.Count; i++)
			{
				RibbonBarTab ribbonBarTab = allTabs[i];
				if (tabsToRender.Contains(ribbonBarTab))
				{
					string text = "rrbCommands";
					if (i != selectedTabIndex || !ribbonBarTab.Enabled)
					{
						text += " rrbHidden";
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

		// Token: 0x06004500 RID: 17664 RVA: 0x000DA494 File Offset: 0x000D8694
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

		// Token: 0x06004501 RID: 17665 RVA: 0x000DA524 File Offset: 0x000D8724
		private string TabGroupCssClassToRender(RibbonBarContextualTabGroup contextualTabGroup)
		{
			List<string> list = new List<string>
			{
				"rrbLabel"
			};
			if (!string.IsNullOrEmpty(contextualTabGroup.CssClass))
			{
				list.Add(contextualTabGroup.CssClass);
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000DA570 File Offset: 0x000D8770
		public void RenderTabGroup(HtmlTextWriter writer, RibbonBarContextualTabGroup contextualTabGroup)
		{
			List<string> list = new List<string>
			{
				"rrbContextualItem"
			};
			if (contextualTabGroup.Active)
			{
				list.Add("rrbActive");
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

		// Token: 0x06004503 RID: 17667 RVA: 0x000DA6A0 File Offset: 0x000D88A0
		private string GetHexFromNamedColor(Color color)
		{
			return string.Format("#{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000DA6D8 File Offset: 0x000D88D8
		protected void RenderActiveContextualTabGroups(HtmlTextWriter writer)
		{
			this.RenderContextualTabGroups((RibbonBarContextualTabGroup group) => group.Active, writer);
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x000DA701 File Offset: 0x000D8901
		protected void RenderAllContextualTabGroups(HtmlTextWriter writer)
		{
			this.RenderContextualTabGroups((RibbonBarContextualTabGroup group) => true, writer);
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x000DA728 File Offset: 0x000D8928
		private string QuickAccessToolbarDropDownCssClass
		{
			get
			{
				return string.Format("{0} {1}", "rrbItem", "rrbQatDropDown");
			}
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x000DA74B File Offset: 0x000D894B
		internal void RenderQuickAccessToolbar(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbQat");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			this.RenderQuickAccessToolbarActiveItems(writer);
			this.RenderQuickAccessToolbarDropDown(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x000DA778 File Offset: 0x000D8978
		private void RenderQuickAccessToolbarActiveItems(HtmlTextWriter writer)
		{
			foreach (RibbonBarClickableItem ribbonBarClickableItem in base.Owner.GetQuickAccessEnabledItems())
			{
				if (ribbonBarClickableItem.QuickAccess == RibbonBarItemQuickAccess.Inactive)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbItem");
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

		// Token: 0x06004509 RID: 17673 RVA: 0x000DA868 File Offset: 0x000D8A68
		private void RenderQuickAccessToolbarDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.QuickAccessToolbarDropDownCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.RenderQuickAccessToolbarDropDownArrow(writer);
			this.RenderQuickAccessToolbarDropDownSlide(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x000DA894 File Offset: 0x000D8A94
		private void RenderQuickAccessToolbarDropDownArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, "Customize Quick Access Toolbar");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"radIcon",
				"radIconExpand"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x000DA908 File Offset: 0x000D8B08
		private void RenderQuickAccessToolbarDropDownSlide(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbSlide");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenu");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbHeader");
			writer.RenderBeginTag(HtmlTextWriterTag.Strong);
			writer.Write("Customize Quick Access Toolbar");
			writer.RenderEndTag();
			if (base.Owner.HasQuickAccessEnabledItems())
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbUL");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				foreach (RibbonBarClickableItem ribbonBarClickableItem in base.Owner.GetQuickAccessEnabledItems())
				{
					string value = string.Format("{0} {1} {2}", "rrbItem", "rrbItemCheckbox", (ribbonBarClickableItem.QuickAccess == RibbonBarItemQuickAccess.Active) ? "rrbChecked" : string.Empty);
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
