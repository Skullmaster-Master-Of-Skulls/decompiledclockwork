using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020008D8 RID: 2264
	public class SpreadsheetToolbarAdapter : SpreadsheetAdapterBase, ISpreadsheetAdapter
	{
		// Token: 0x06005532 RID: 21810 RVA: 0x00105515 File Offset: 0x00103715
		public SpreadsheetToolbarAdapter(ISpreadsheet owner) : base(owner)
		{
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00105520 File Offset: 0x00103720
		public WebControl CreateToolbar(SpreadsheetToolbar toolbar)
		{
			if (toolbar.Tabs.Count == 0)
			{
				toolbar = base.GetDefaultToolbar();
			}
			Panel panel = new Panel
			{
				ID = "SpreadsheetToolbarWrapper",
				CssClass = "rssToolbarWrapper"
			};
			RadTabStrip child = this.CreateTabStrip(toolbar);
			RadMultiPage child2 = this.CreateMultiPage(toolbar);
			panel.Controls.Add(child);
			panel.Controls.Add(child2);
			return panel;
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x0010558C File Offset: 0x0010378C
		public RadTabStrip CreateTabStrip(SpreadsheetToolbar toolbar)
		{
			RadTabStrip radTabStrip = new RadTabStrip();
			radTabStrip.EnableViewState = false;
			radTabStrip.RenderMode = RenderMode.Lightweight;
			radTabStrip.CssClass = "rssTabstrip";
			radTabStrip.MultiPageID = "SpreadsheetMultiPage";
			radTabStrip.ID = "SpreadsheetTabStrip";
			radTabStrip.Skin = base.Owner.ResolvedSkin;
			radTabStrip.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			foreach (SpreadsheetToolbarTab spreadsheetToolbarTab in toolbar.Tabs)
			{
				radTabStrip.Tabs.Add(new RadTab
				{
					Text = spreadsheetToolbarTab.Text
				});
			}
			if (radTabStrip.Tabs.Count > 0)
			{
				radTabStrip.Tabs[0].Selected = true;
			}
			return radTabStrip;
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00105668 File Offset: 0x00103868
		public RadMultiPage CreateMultiPage(SpreadsheetToolbar toolbar)
		{
			RadMultiPage radMultiPage = new RadMultiPage();
			radMultiPage.EnableViewState = false;
			radMultiPage.RenderMode = RenderMode.Lightweight;
			radMultiPage.ID = "SpreadsheetMultiPage";
			radMultiPage.Skin = base.Owner.ResolvedSkin;
			radMultiPage.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			for (int i = 0; i < toolbar.Tabs.Count; i++)
			{
				SpreadsheetToolbarTab tab = toolbar.Tabs[i];
				RadPageView radPageView = new RadPageView();
				string id = "SpreadsheetToolbar" + i.ToString();
				WebControl child = this.CreateToolbarControl(tab, id);
				radPageView.Controls.Add(child);
				if (i == 0)
				{
					radPageView.Selected = true;
				}
				radMultiPage.PageViews.Add(radPageView);
			}
			return radMultiPage;
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00105724 File Offset: 0x00103924
		public WebControl CreateToolbarControl(SpreadsheetToolbarTab tab, string id)
		{
			RadToolBar radToolBar = new RadToolBar();
			radToolBar.EnableViewState = false;
			radToolBar.EnableImageSprites = true;
			radToolBar.RenderMode = RenderMode.Lightweight;
			radToolBar.CssClass = "rssToolbar";
			radToolBar.ID = id;
			radToolBar.Skin = base.Owner.ResolvedSkin;
			radToolBar.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			radToolBar.EnableShadows = true;
			radToolBar.EnableRoundedCorners = true;
			this.PopulateToolbar(radToolBar, tab);
			return radToolBar;
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x00105798 File Offset: 0x00103998
		private void PopulateToolbar(RadToolBar toolbar, SpreadsheetToolbarTab tab)
		{
			for (int i = 0; i < tab.Groups.Count; i++)
			{
				SpreadsheetToolbarGroup spreadsheetToolbarGroup = tab.Groups[i];
				for (int j = 0; j < spreadsheetToolbarGroup.Tools.Count; j++)
				{
					SpreadsheetToolBase spreadsheetToolBase = spreadsheetToolbarGroup.Tools[j];
					if (spreadsheetToolBase.Visible)
					{
						RadToolBarItem item = this.CreateToolbarItem(spreadsheetToolBase);
						toolbar.Items.Add(item);
					}
				}
				if (i < tab.Groups.Count - 1)
				{
					RadToolBarItem item2 = this.CreateSeparatorItem();
					toolbar.Items.Add(item2);
				}
			}
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00105834 File Offset: 0x00103A34
		private RadToolBarItem CreateToolbarItem(SpreadsheetToolBase tool)
		{
			RadToolBarItem radToolBarItem;
			switch (tool.Name)
			{
			case SpreadsheetToolName.Undo:
			case SpreadsheetToolName.Redo:
			case SpreadsheetToolName.Save:
			case SpreadsheetToolName.Cut:
			case SpreadsheetToolName.Copy:
			case SpreadsheetToolName.Paste:
			case SpreadsheetToolName.FormatIncreaseDecimal:
			case SpreadsheetToolName.FormatDecreaseDecimal:
			case SpreadsheetToolName.Validation:
			case SpreadsheetToolName.Hyperlink:
			case SpreadsheetToolName.InsertImage:
			case SpreadsheetToolName.ExportAs:
				radToolBarItem = this.CreateImageButton(tool);
				break;
			case SpreadsheetToolName.Bold:
			case SpreadsheetToolName.Italic:
			case SpreadsheetToolName.Underline:
			case SpreadsheetToolName.TextWrap:
			case SpreadsheetToolName.Filter:
			case SpreadsheetToolName.GridLines:
			case SpreadsheetToolName.InsertComment:
				radToolBarItem = this.CreateToggleButton(tool);
				break;
			case SpreadsheetToolName.BorderType:
			case SpreadsheetToolName.MergeCells:
			case SpreadsheetToolName.Freeze:
			case SpreadsheetToolName.InsertCells:
			case SpreadsheetToolName.Sort:
				radToolBarItem = this.CreateSplitButton(tool);
				break;
			case SpreadsheetToolName.BorderColor:
			case SpreadsheetToolName.BackgroundColor:
			case SpreadsheetToolName.TextColor:
				radToolBarItem = this.CreateColorPickerButton(tool);
				break;
			case SpreadsheetToolName.HorizontalAlignment:
			case SpreadsheetToolName.VerticalAlignment:
				radToolBarItem = this.CreateToggleDropDownButton(tool);
				break;
			case SpreadsheetToolName.FontSize:
				radToolBarItem = this.CreateFontSizeComboBox(tool);
				break;
			case SpreadsheetToolName.FontFamily:
				radToolBarItem = this.CreateFontFamilyDropDownList(tool);
				break;
			case SpreadsheetToolName.Format:
			case SpreadsheetToolName.DeleteCells:
				radToolBarItem = this.CreateDropDownButton(tool);
				break;
			case SpreadsheetToolName.Open:
				radToolBarItem = this.CreateOpenImageButton(tool);
				break;
			default:
				radToolBarItem = new RadToolBarButton();
				break;
			}
			SpreadsheetToolName name = tool.Name;
			switch (name)
			{
			case SpreadsheetToolName.BorderType:
			case SpreadsheetToolName.HorizontalAlignment:
			case SpreadsheetToolName.VerticalAlignment:
				break;
			case SpreadsheetToolName.BorderColor:
				return radToolBarItem;
			default:
				switch (name)
				{
				case SpreadsheetToolName.InsertCells:
				case SpreadsheetToolName.DeleteCells:
					break;
				default:
					return radToolBarItem;
				}
				break;
			}
			radToolBarItem.Attributes["gallery"] = "true";
			return radToolBarItem;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x00105984 File Offset: 0x00103B84
		private RadToolBarItem CreateOpenButton(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarButton radToolBarButton = new RadToolBarButton();
			radToolBarButton.CommandName = spreadsheetToolInfo.CommandName;
			radToolBarButton.CommandArgument = spreadsheetToolInfo.CommandArgument;
			radToolBarButton.Value = spreadsheetToolInfo.Value;
			radToolBarButton.ToolTip = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			radToolBarButton.SpriteCssClass = this.GetIconCssClass(spreadsheetToolInfo.IconClass);
			SpreadsheetToolbarAdapter.UploadFileToolBarItemTemplate uploadFileToolBarItemTemplate = new SpreadsheetToolbarAdapter.UploadFileToolBarItemTemplate();
			uploadFileToolBarItemTemplate.InstantiateIn(radToolBarButton);
			return radToolBarButton;
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00105A00 File Offset: 0x00103C00
		private RadToolBarItem CreateSeparatorItem()
		{
			return new RadToolBarButton
			{
				IsSeparator = true
			};
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x00105A20 File Offset: 0x00103C20
		private RadToolBarItem CreateToggleButton(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarButton radToolBarButton = (RadToolBarButton)this.CreateButton(spreadsheetToolInfo);
			radToolBarButton.Group = spreadsheetToolInfo.Group;
			radToolBarButton.CheckOnClick = true;
			radToolBarButton.AllowSelfUnCheck = true;
			radToolBarButton.Text = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			radToolBarButton.ShowText = (tool.ShowLabel ? ToolBarShowPosition.Both : ToolBarShowPosition.OverFlow);
			return radToolBarButton;
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00105A8C File Offset: 0x00103C8C
		private RadToolBarItem CreateImageButton(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarItem radToolBarItem = this.CreateButton(spreadsheetToolInfo);
			radToolBarItem.Text = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			radToolBarItem.ShowText = (tool.ShowLabel ? ToolBarShowPosition.Both : ToolBarShowPosition.OverFlow);
			return radToolBarItem;
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00105AD8 File Offset: 0x00103CD8
		private RadToolBarItem CreateOpenImageButton(SpreadsheetToolBase tool)
		{
			RadToolBarItem radToolBarItem = this.CreateImageButton(tool);
			radToolBarItem.Value = "open";
			RadToolBarItem radToolBarItem2 = radToolBarItem;
			radToolBarItem2.CssClass += " k-upload-button";
			return radToolBarItem;
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x00105B10 File Offset: 0x00103D10
		private RadToolBarItem CreateButton(SpreadsheetToolInfo toolInfo)
		{
			return new RadToolBarButton
			{
				CommandName = toolInfo.CommandName,
				CommandArgument = toolInfo.CommandArgument,
				Value = toolInfo.Value,
				ToolTip = base.GetLocalizedString(toolInfo.LocalizationTextKey),
				SpriteCssClass = this.GetIconCssClass(toolInfo.IconClass)
			};
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x00105B6C File Offset: 0x00103D6C
		private RadToolBarItem CreateFontSizeComboBox(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarButton radToolBarButton = (RadToolBarButton)this.CreateButton(spreadsheetToolInfo);
			RadComboBox radComboBox = this.CreateComboBox();
			radToolBarButton.OverFlow = ToolBarOverflow.Never;
			radComboBox.ID = spreadsheetToolInfo.CommandArgument;
			radComboBox.DataSource = SpreadsheetAdapterBase.DefaultFontSizes;
			radComboBox.SelectedValue = "12";
			radComboBox.DataBind();
			radComboBox.ToolTip = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			if (tool.ShowLabel)
			{
				radToolBarButton.Controls.Add(this.CreateLabel(base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey)));
			}
			radToolBarButton.Controls.Add(radComboBox);
			return radToolBarButton;
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x00105C14 File Offset: 0x00103E14
		private RadComboBox CreateComboBox()
		{
			return new RadComboBox
			{
				RenderMode = RenderMode.Lightweight,
				Skin = base.Owner.ResolvedSkin,
				EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins,
				Width = new Unit(4.25, UnitType.Em),
				CssClass = "rssFontSizePicker",
				AllowCustomText = true,
				ExpandAnimation = 
				{
					Type = AnimationType.OutExpo,
					Duration = 250
				},
				CollapseAnimation = 
				{
					Type = AnimationType.OutExpo,
					Duration = 150
				}
			};
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x00105CB4 File Offset: 0x00103EB4
		private RadToolBarItem CreateFontFamilyDropDownList(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarButton radToolBarButton = (RadToolBarButton)this.CreateButton(spreadsheetToolInfo);
			RadDropDownList radDropDownList = this.CreateDropDownList();
			radToolBarButton.OverFlow = ToolBarOverflow.Never;
			radDropDownList.ID = spreadsheetToolInfo.CommandArgument;
			radDropDownList.DataSource = SpreadsheetAdapterBase.DefaultFontFamilies;
			radDropDownList.SelectedText = "Arial";
			radDropDownList.DataBind();
			radDropDownList.ToolTip = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			if (tool.ShowLabel)
			{
				radToolBarButton.Controls.Add(this.CreateLabel(base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey)));
			}
			radToolBarButton.Controls.Add(radDropDownList);
			return radToolBarButton;
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00105D5C File Offset: 0x00103F5C
		private RadDropDownList CreateDropDownList()
		{
			return new RadDropDownList
			{
				RenderMode = RenderMode.Lightweight,
				Skin = base.Owner.ResolvedSkin,
				EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins,
				Width = Unit.Pixel(100),
				ExpandAnimation = 
				{
					Type = AnimationType.OutExpo,
					Duration = 250
				},
				CollapseAnimation = 
				{
					Type = AnimationType.OutExpo,
					Duration = 150
				}
			};
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x00105DE0 File Offset: 0x00103FE0
		private RadToolBarItem CreateColorPickerButton(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarButton radToolBarButton = (RadToolBarButton)this.CreateButton(spreadsheetToolInfo);
			RadColorPicker radColorPicker = this.CreateColorPicker();
			radToolBarButton.OverFlow = ToolBarOverflow.Never;
			radToolBarButton.CssClass = spreadsheetToolInfo.IconClass;
			radColorPicker.ID = spreadsheetToolInfo.CommandArgument;
			radColorPicker.ToolTip = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			if (tool.ShowLabel)
			{
				radToolBarButton.Controls.Add(this.CreateLabel(base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey)));
			}
			radToolBarButton.Controls.Add(radColorPicker);
			return radToolBarButton;
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x00105E78 File Offset: 0x00104078
		private RadColorPicker CreateColorPicker()
		{
			return new RadColorPicker
			{
				RenderMode = RenderMode.Lightweight,
				Skin = base.Owner.ResolvedSkin,
				EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins,
				ShowIcon = true
			};
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x00105EBC File Offset: 0x001040BC
		private RadToolBarItem CreateSplitButton(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarSplitButton radToolBarSplitButton = new RadToolBarSplitButton();
			radToolBarSplitButton.CommandName = spreadsheetToolInfo.CommandName;
			radToolBarSplitButton.Value = spreadsheetToolInfo.Value;
			radToolBarSplitButton.ToolTip = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			radToolBarSplitButton.SpriteCssClass = this.GetIconCssClass(spreadsheetToolInfo.IconClass);
			radToolBarSplitButton.Text = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			radToolBarSplitButton.ShowText = (tool.ShowLabel ? ToolBarShowPosition.Both : ToolBarShowPosition.OverFlow);
			this.PopulateChildButtons(radToolBarSplitButton, spreadsheetToolInfo);
			radToolBarSplitButton.EnableDefaultButton = false;
			return radToolBarSplitButton;
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00105F50 File Offset: 0x00104150
		private RadToolBarItem CreateDropDownButton(SpreadsheetToolBase tool)
		{
			SpreadsheetToolInfo spreadsheetToolInfo = SpreadsheetAdapterBase.DefaultTools[tool.Name];
			RadToolBarDropDown radToolBarDropDown = new RadToolBarDropDown();
			radToolBarDropDown.ToolTip = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			radToolBarDropDown.SpriteCssClass = this.GetIconCssClass(spreadsheetToolInfo.IconClass);
			radToolBarDropDown.Text = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
			radToolBarDropDown.ShowText = (tool.ShowLabel ? ToolBarShowPosition.Both : ToolBarShowPosition.OverFlow);
			this.PopulateChildButtons(radToolBarDropDown, spreadsheetToolInfo);
			return radToolBarDropDown;
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x00105FC8 File Offset: 0x001041C8
		private RadToolBarItem CreateToggleDropDownButton(SpreadsheetToolBase tool)
		{
			RadToolBarItem radToolBarItem = this.CreateDropDownButton(tool);
			radToolBarItem.Attributes["toggle"] = "true";
			return radToolBarItem;
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x00105FF4 File Offset: 0x001041F4
		private void PopulateChildButtons(IRadToolBarButtonContainer container, SpreadsheetToolInfo toolInfo)
		{
			foreach (SpreadsheetToolInfo spreadsheetToolInfo in toolInfo.ChildTools)
			{
				RadToolBarItem radToolBarItem = this.CreateButton(spreadsheetToolInfo);
				radToolBarItem.Text = base.GetLocalizedString(spreadsheetToolInfo.LocalizationTextKey);
				container.Buttons.Add(radToolBarItem);
			}
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x00106068 File Offset: 0x00104268
		private Label CreateLabel(string text)
		{
			return new Label
			{
				Text = text
			};
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00106083 File Offset: 0x00104283
		private string GetIconCssClass(string iconName)
		{
			if (string.IsNullOrEmpty(iconName))
			{
				return string.Empty;
			}
			return string.Format("{0} {1}{2}", "t-efi", "t-efi-", iconName);
		}

		// Token: 0x020008D9 RID: 2265
		public class UploadFileToolBarItemTemplate : ITemplate
		{
			// Token: 0x0600554B RID: 21835 RVA: 0x001060A8 File Offset: 0x001042A8
			public void InstantiateIn(Control container)
			{
				LiteralControl child = new LiteralControl("<div class='k-button k-upload-button k-button-icon'><span class='k-icon k-i-folder-open' /></div>");
				container.Controls.Add(child);
			}
		}
	}
}
