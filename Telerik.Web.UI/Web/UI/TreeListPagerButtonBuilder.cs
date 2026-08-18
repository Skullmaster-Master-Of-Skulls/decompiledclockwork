using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001259 RID: 4697
	internal class TreeListPagerButtonBuilder
	{
		// Token: 0x17003E61 RID: 15969
		// (get) Token: 0x0600C17A RID: 49530 RVA: 0x002B25F6 File Offset: 0x002B07F6
		// (set) Token: 0x0600C179 RID: 49529 RVA: 0x002B25ED File Offset: 0x002B07ED
		public TreeListPagerItem PagerItem { get; internal set; }

		// Token: 0x17003E62 RID: 15970
		// (get) Token: 0x0600C17B RID: 49531 RVA: 0x002B25FE File Offset: 0x002B07FE
		private TreeListPagerStyle PagerStyle
		{
			get
			{
				return this.PagerItem.OwnerTreeList.PagerStyle;
			}
		}

		// Token: 0x0600C17C RID: 49532 RVA: 0x002B2610 File Offset: 0x002B0810
		public TreeListPagerButtonBuilder(TreeListPagerItem pagerItem)
		{
			this.PagerItem = pagerItem;
		}

		// Token: 0x0600C17D RID: 49533 RVA: 0x002B2620 File Offset: 0x002B0820
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public Control BuildContainer(string className)
		{
			return new Panel
			{
				CssClass = string.Format("{0} {1}", "rtlWrap", className)
			};
		}

		// Token: 0x0600C17E RID: 49534 RVA: 0x002B2674 File Offset: 0x002B0874
		protected virtual void PrepareSkinnableControlProperties(ISkinnableControl control)
		{
			(control as Control).PreRender += delegate(object sender, EventArgs args)
			{
				control.Skin = this.PagerItem.OwnerTreeList.RuntimeSkin;
			};
			control.EnableEmbeddedSkins = this.PagerItem.OwnerTreeList.EnableEmbeddedSkins;
			control.EnableEmbeddedScripts = this.PagerItem.OwnerTreeList.EnableEmbeddedScripts;
			control.EnableEmbeddedBaseStylesheet = this.PagerItem.OwnerTreeList.EnableEmbeddedBaseStylesheet;
			control.RegisterWithScriptManager = this.PagerItem.OwnerTreeList.RegisterWithScriptManager;
		}

		// Token: 0x0600C17F RID: 49535 RVA: 0x002B2720 File Offset: 0x002B0920
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected string PrepareTextFormat(PagerButtonType type, string text)
		{
			string text2 = string.IsNullOrEmpty(text) ? " " : text;
			if (type == PagerButtonType.LinkButton)
			{
				text2 = string.Format("<span>{0}</span>", text2);
			}
			return text2;
		}

		// Token: 0x0600C180 RID: 49536 RVA: 0x002B274F File Offset: 0x002B094F
		protected WebControl EnsureEnableState(WebControl button, string commandArgument)
		{
			if (this.IsCurrentPage(commandArgument))
			{
				button.Attributes.Add("onclick", "return false;");
			}
			return button;
		}

		// Token: 0x0600C181 RID: 49537 RVA: 0x002B2770 File Offset: 0x002B0970
		public bool IsCurrentPage(string commandArgument)
		{
			bool result = false;
			TreeListPagingManager paging = this.PagerItem.Paging;
			if (commandArgument != null)
			{
				if (commandArgument == "Prev" || commandArgument == "First")
				{
					return paging.CurrentPageIndex == 0;
				}
				if (commandArgument == "Next" || commandArgument == "Last")
				{
					return paging.CurrentPageIndex == paging.PageCount - 1;
				}
			}
			int num;
			if (int.TryParse(commandArgument, out num))
			{
				result = (paging.CurrentPageIndex == num);
			}
			return result;
		}

		// Token: 0x0600C182 RID: 49538 RVA: 0x002B27FC File Offset: 0x002B09FC
		public WebControl CreateButtonField(PagerButtonType type, string text, string toolTip, string commandName, string commandArgument, string className, string hiddenSpanText)
		{
			int num = -1;
			bool flag = int.TryParse(commandArgument, out num);
			WebControl webControl = this.CreateButtonFieldForCommand(type, text, toolTip, commandName, commandArgument, hiddenSpanText);
			webControl.CssClass = className;
			if ((this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile) && !flag)
			{
				webControl.CssClass = "t-button rtlActionButton " + webControl.CssClass;
				((ElasticButton)webControl).UseSubmitBehavior = false;
				if (this.PagerItem.OwnerTreeList.EnableAriaSupport && !flag)
				{
					((ElasticButton)webControl).Attributes.Add("aria-label", string.IsNullOrEmpty(toolTip) ? hiddenSpanText : toolTip);
				}
			}
			return this.EnsureEnableState(webControl, commandArgument);
		}

		// Token: 0x0600C183 RID: 49539 RVA: 0x002B28BC File Offset: 0x002B0ABC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private WebControl CreateButtonFieldForCommand(PagerButtonType type, string text, string toolTip, string commandName, string commandArgument, string hiddenSpanText)
		{
			IButtonControl buttonControl = null;
			switch (type)
			{
			case PagerButtonType.PushButton:
				buttonControl = new Button();
				if (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile)
				{
					buttonControl = new ElasticButton
					{
						FirstSpanClass = "t-font-icon rtlIcon",
						Text = hiddenSpanText
					};
				}
				else
				{
					((Button)buttonControl).ToolTip = toolTip;
				}
				break;
			case PagerButtonType.LinkButton:
				buttonControl = new LinkButton();
				((LinkButton)buttonControl).ToolTip = toolTip;
				break;
			case PagerButtonType.ImageButton:
				if (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile)
				{
					ElasticButton elasticButton = new ElasticButton
					{
						FirstSpanClass = "t-font-icon rtlIcon"
					};
					buttonControl = elasticButton;
					elasticButton.ToolTip = toolTip;
				}
				else
				{
					buttonControl = new ImageButton();
					((ImageButton)buttonControl).ToolTip = toolTip;
					((ImageButton)buttonControl).AlternateText = toolTip;
				}
				break;
			}
			int num = -1;
			bool flag = int.TryParse(commandArgument, out num);
			buttonControl.Text = (((this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile) && flag) ? text : this.PrepareTextFormat(type, text));
			buttonControl.CommandName = commandName;
			buttonControl.CommandArgument = commandArgument;
			buttonControl.CausesValidation = false;
			return buttonControl as WebControl;
		}

		// Token: 0x0600C184 RID: 49540 RVA: 0x002B2A1C File Offset: 0x002B0C1C
		public Control CreatePrevButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerTreeList.PagerStyle.PrevPageToolTip, "Page", "Prev", "rtlPagePrev", "Previous Button");
			webControl.ID = "PrevButton";
			return webControl;
		}

		// Token: 0x0600C185 RID: 49541 RVA: 0x002B2A6C File Offset: 0x002B0C6C
		public Control CreateNextButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerTreeList.PagerStyle.NextPageToolTip, "Page", "Next", "rtlPageNext", "Next Button");
			webControl.ID = "NextButton";
			return webControl;
		}

		// Token: 0x0600C186 RID: 49542 RVA: 0x002B2ABC File Offset: 0x002B0CBC
		public Control CreateFirstButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerTreeList.PagerStyle.FirstPageToolTip, "Page", "First", "rtlPageFirst", "First Button");
			webControl.ID = "FirstButton";
			return webControl;
		}

		// Token: 0x0600C187 RID: 49543 RVA: 0x002B2B0C File Offset: 0x002B0D0C
		public Control CreateLastButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerTreeList.PagerStyle.LastPageToolTip, "Page", "Last", "rtlPageLast", "Last Button");
			webControl.ID = "LastButton";
			return webControl;
		}

		// Token: 0x0600C188 RID: 49544 RVA: 0x002B2B5C File Offset: 0x002B0D5C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected Label CreateLabel(string id)
		{
			return new Label
			{
				ID = id,
				CssClass = "rtlPagerLabel"
			};
		}

		// Token: 0x0600C189 RID: 49545 RVA: 0x002B2B84 File Offset: 0x002B0D84
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		protected virtual Control CreateNumericButton(string text, int commandArgument)
		{
			string className = (this.PagerItem.OwnerTreeList.CurrentPageIndex == commandArgument) ? "rtlCurrentPage" : "";
			return this.CreateButtonField(PagerButtonType.LinkButton, text, "", "Page", commandArgument.ToString(), className, commandArgument.ToString());
		}

		// Token: 0x0600C18A RID: 49546 RVA: 0x002B2BD4 File Offset: 0x002B0DD4
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		public Control CreateNumericPager()
		{
			RadTreeList ownerTreeList = this.PagerItem.OwnerTreeList;
			Panel panel = new Panel();
			panel.CssClass = string.Format("{0} {1}", "rtlWrap", "rtlNumPart");
			int pageButtonCount = ownerTreeList.PagerStyle.PageButtonCount;
			int num = (ownerTreeList.CurrentPageIndex + 1) / pageButtonCount + (((ownerTreeList.CurrentPageIndex + 1) % pageButtonCount == 0) ? 0 : 1) - 1;
			num = Math.Max(num, 0) * pageButtonCount;
			if (ownerTreeList.CurrentPageIndex + 1 > pageButtonCount)
			{
				panel.Controls.Add(this.CreateNumericButton("...", num - 1));
			}
			int num2 = Math.Min(num + pageButtonCount, ownerTreeList.PageCount);
			for (int i = num; i < num2; i++)
			{
				panel.Controls.Add(this.CreateNumericButton((i + 1).ToString(), i));
			}
			if (num2 < ownerTreeList.PageCount)
			{
				panel.Controls.Add(this.CreateNumericButton("...", num2));
			}
			if (ownerTreeList.PageCount == 0)
			{
				panel.Controls.Add(this.CreateNumericButton("1", 0));
			}
			return panel;
		}

		// Token: 0x0600C18B RID: 49547 RVA: 0x002B2CE8 File Offset: 0x002B0EE8
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		public Control CreatePageSize()
		{
			Control control = this.BuildContainer("rtlAdvPart");
			Label label = this.CreateLabel("ChangePageSizeLabel");
			label.Text = this.PagerItem.OwnerTreeList.PagerStyle.ChangePageSizeLabelText;
			control.Controls.Add(label);
			ControlItemContainer controlItemContainer;
			if (this.PagerItem.OwnerTreeList.PagerStyle.PageSizeControlType == PagerDropDownControlType.RadComboBox)
			{
				controlItemContainer = new RadComboBox
				{
					ID = "PageSizeComboBox"
				};
			}
			else
			{
				controlItemContainer = new RadDropDownList
				{
					ID = "PageSizeDropDownList"
				};
			}
			controlItemContainer.RenderMode = this.PagerItem.OwnerTreeList.ResolvedRenderMode;
			this.PrepareSkinnableControlProperties(controlItemContainer);
			IList<int> defaultPageSizes = this.PrepateDefaultPageSizes();
			this.AdjustDropDownControlWidth(controlItemContainer);
			if (!this.InitalizePageSizeCombo(controlItemContainer as RadComboBox, defaultPageSizes))
			{
				this.InitalizePageSizeDropDownList(controlItemContainer as RadDropDownList, defaultPageSizes);
			}
			control.Controls.Add(controlItemContainer);
			return control;
		}

		// Token: 0x0600C18C RID: 49548 RVA: 0x002B2DD0 File Offset: 0x002B0FD0
		private bool InitalizePageSizeCombo(RadComboBox pageSizeCombo, IList<int> defaultPageSizes)
		{
			bool result = false;
			if (pageSizeCombo != null)
			{
				pageSizeCombo.EnableAriaSupport = this.PagerItem.OwnerTreeList.EnableAriaSupport;
				pageSizeCombo.TableSummary = this.PagerStyle.ChangePageSizeComboBoxTableSummary;
				pageSizeCombo.InputTitle = this.PagerStyle.ChangePageSizeComboBoxToolTip;
				pageSizeCombo.TableCaption = "PageSizeComboBox";
				pageSizeCombo.CausesValidation = false;
				if (!string.IsNullOrEmpty(this.PagerStyle.ChangePageSizeComboBoxTableSummary) && !string.IsNullOrEmpty(this.PagerStyle.ChangePageSizeComboBoxToolTip))
				{
					pageSizeCombo.EnableTableHeaders = true;
				}
				pageSizeCombo.ClearSelection();
				foreach (int num in defaultPageSizes)
				{
					RadComboBoxItem item = new RadComboBoxItem(num.ToString(), num.ToString());
					pageSizeCombo.Items.Add(item);
				}
				pageSizeCombo.Items.Sort(new PageSizeItemsComparer());
				RadComboBoxItem radComboBoxItem = pageSizeCombo.Items.FindItemByValue(this.PagerItem.OwnerTreeList.PageSize.ToString());
				if (radComboBoxItem != null)
				{
					radComboBoxItem.Selected = true;
				}
				if (pageSizeCombo.EnableAriaSupport && (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile))
				{
					pageSizeCombo.InputTitle = (string.IsNullOrEmpty(this.PagerStyle.ChangePageSizeComboBoxToolTip) ? "Page size" : this.PagerStyle.ChangePageSizeComboBoxToolTip);
				}
				pageSizeCombo.AutoPostBack = true;
				pageSizeCombo.SelectedIndexChanged += this.PageSizeComboSelectedIndexChanged;
				result = true;
			}
			return result;
		}

		// Token: 0x0600C18D RID: 49549 RVA: 0x002B2F70 File Offset: 0x002B1170
		private void InitalizePageSizeDropDownList(RadDropDownList ddl, IList<int> defaultPageSizes)
		{
			if (ddl != null)
			{
				ddl.ClearSelection();
				foreach (int num in defaultPageSizes)
				{
					DropDownListItem item = new DropDownListItem(num.ToString(), num.ToString());
					ddl.Items.Add(item);
				}
				ddl.Items.Sort(new PageSizeItemsComparer());
				DropDownListItem dropDownListItem = ddl.FindChildByValue<DropDownListItem>(this.PagerItem.OwnerTreeList.PageSize.ToString());
				if (dropDownListItem != null)
				{
					dropDownListItem.Selected = true;
				}
				ddl.AutoPostBack = true;
				ddl.CausesValidation = false;
				ddl.SelectedIndexChanged += this.PageSizeDropDownListSelectedIndexChanged;
			}
		}

		// Token: 0x0600C18E RID: 49550 RVA: 0x002B3038 File Offset: 0x002B1238
		private IList<int> PrepateDefaultPageSizes()
		{
			IList<int> list = new List<int>();
			list.Add(10);
			list.Add(20);
			list.Add(50);
			if (!list.Contains(this.PagerItem.OwnerTreeList.PageSize) || this.PagerItem.OwnerTreeList.CustomPageSize != null)
			{
				if (!list.Contains(this.PagerItem.OwnerTreeList.PageSize))
				{
					this.PagerItem.OwnerTreeList.CustomPageSize = new int?(this.PagerItem.OwnerTreeList.PageSize);
				}
				list.Add(this.PagerItem.OwnerTreeList.CustomPageSize.Value);
			}
			return list;
		}

		// Token: 0x0600C18F RID: 49551 RVA: 0x002B30F4 File Offset: 0x002B12F4
		private void AdjustDropDownControlWidth(ControlItemContainer ddlControl)
		{
			int length = this.PagerItem.Paging.PageSize.ToString().Length;
			int num = 34;
			if (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Classic)
			{
				if (this.PagerItem.OwnerTreeList.RuntimeSkin == "MetroTouch" || this.PagerItem.OwnerTreeList.RuntimeSkin == "Glow" || this.PagerItem.OwnerTreeList.RuntimeSkin == "Silk" || this.PagerItem.OwnerTreeList.RuntimeSkin == "BlackMetroTouch")
				{
					num = 50;
				}
				if (this.PagerItem.OwnerTreeList.RuntimeSkin == "Bootstrap")
				{
					num = 55;
				}
				ddlControl.Width = Unit.Pixel(length * 6 + num);
				return;
			}
			ddlControl.Width = Unit.Parse((double)length * 2.1 + "em");
		}

		// Token: 0x0600C190 RID: 49552 RVA: 0x002B3204 File Offset: 0x002B1404
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		public Control CreateAdvancedPager()
		{
			Control control = this.BuildContainer("rtlAdvPart");
			Label label = this.CreateLabel("GoToPageLabel");
			label.Text = this.PagerItem.OwnerTreeList.PagerStyle.GoToPageLabelText;
			control.Controls.Add(label);
			RadNumericTextBox radNumericTextBox = new RadNumericTextBox();
			radNumericTextBox.RenderMode = this.PagerItem.OwnerTreeList.RenderMode;
			radNumericTextBox.ID = "GoToPageTextBox";
			radNumericTextBox.EnableAriaSupport = this.PagerItem.OwnerTreeList.EnableAriaSupport;
			AccessibilityHelper.AddToolTip(radNumericTextBox, this.PagerStyle.GoToPageTextBoxToolTip);
			int num = 0;
			if (this.PagerItem.OwnerTreeList.EnableAriaSupport && (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile))
			{
				radNumericTextBox.Attributes.Add("aria-label", string.IsNullOrEmpty(this.PagerStyle.GoToPageTextBoxToolTip) ? "Page" : this.PagerStyle.GoToPageTextBoxToolTip);
			}
			if (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Classic)
			{
				if (this.PagerItem.OwnerTreeList.RuntimeSkin == "MetroTouch" || this.PagerItem.OwnerTreeList.RuntimeSkin == "Glow" || this.PagerItem.OwnerTreeList.RuntimeSkin == "Silk" || this.PagerItem.OwnerTreeList.RuntimeSkin == "BlackMetroTouch" || this.PagerItem.OwnerTreeList.RuntimeSkin == "Bootstrap")
				{
					num = 20;
				}
				if (radNumericTextBox.EnableSingleInputRendering)
				{
					radNumericTextBox.Width = Unit.Pixel(num + 10 + this.PagerItem.Paging.PageCount.ToString().Length * 10);
				}
				else
				{
					radNumericTextBox.Width = Unit.Pixel(num + this.PagerItem.Paging.PageCount.ToString().Length * 10);
				}
			}
			else
			{
				radNumericTextBox.Width = Unit.Parse(2.2857000827789307 + (double)(this.PagerItem.Paging.PageCount.ToString().Length - 1) * 0.6 + "em");
			}
			radNumericTextBox.NumberFormat.DecimalDigits = 0;
			radNumericTextBox.MinValue = 1.0;
			radNumericTextBox.MaxValue = (double)this.PagerItem.Paging.PageCount;
			radNumericTextBox.Value = new double?((double)(this.PagerItem.Paging.CurrentPageIndex + 1));
			this.PrepareSkinnableControlProperties(radNumericTextBox);
			control.Controls.Add(radNumericTextBox);
			Label label2 = this.CreateLabel("PageOfLabel");
			label2.Text = string.Format(this.PagerItem.OwnerTreeList.PagerStyle.PageOfLabelText, this.PagerItem.Paging.PageCount);
			control.Controls.Add(label2);
			Button button;
			if (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button = new ElasticButton(string.Empty, "t-text rtlButtonText");
				button.CssClass = "t-button ";
			}
			else
			{
				button = new Button();
				button.CssClass = string.Empty;
			}
			button.ID = "GoToPageLinkButton";
			AccessibilityHelper.AddToolTip(button, this.PagerStyle.GoToPageButtonToolTip);
			Button button2 = button;
			button2.CssClass += "rtlPagerButton";
			button.Text = this.PagerItem.OwnerTreeList.PagerStyle.GoToPageLinkButtonText;
			button.Click += this.GoToPageLinkButtonClick;
			control.Controls.Add(button);
			Label label3 = this.CreateLabel("ChangePageSizeLabel");
			label3.Text = this.PagerItem.OwnerTreeList.PagerStyle.ChangePageSizeLabelText;
			control.Controls.Add(label3);
			RadNumericTextBox radNumericTextBox2 = new RadNumericTextBox();
			radNumericTextBox2.RenderMode = this.PagerItem.OwnerTreeList.RenderMode;
			radNumericTextBox2.ID = "ChangePageSizeTextBox";
			radNumericTextBox2.EnableAriaSupport = this.PagerItem.OwnerTreeList.EnableAriaSupport;
			AccessibilityHelper.AddToolTip(radNumericTextBox2, this.PagerStyle.ChangePageSizeTextBoxToolTip);
			if (this.PagerItem.OwnerTreeList.RenderMode == RenderMode.Classic)
			{
				if (radNumericTextBox2.EnableSingleInputRendering)
				{
					radNumericTextBox2.Width = Unit.Pixel(num + 10 + this.PagerItem.Paging.DataSourceCount.ToString().Length * 10);
				}
				else
				{
					radNumericTextBox2.Width = Unit.Pixel(num + this.PagerItem.Paging.DataSourceCount.ToString().Length * 10);
				}
			}
			else
			{
				radNumericTextBox2.Width = Unit.Parse(2.2857000827789307 + (double)(this.PagerItem.Paging.DataSourceCount.ToString().Length - 1) * 0.6 + "em");
				if (radNumericTextBox2.EnableAriaSupport)
				{
					radNumericTextBox2.Attributes.Add("aria-label", string.IsNullOrEmpty(this.PagerStyle.ChangePageSizeTextBoxToolTip) ? "Page size" : this.PagerStyle.ChangePageSizeTextBoxToolTip);
				}
			}
			radNumericTextBox2.NumberFormat.DecimalDigits = 0;
			if (this.PagerItem.Paging.DataSourceCount > 0)
			{
				radNumericTextBox2.MinValue = 1.0;
				radNumericTextBox2.MaxValue = (double)this.PagerItem.Paging.DataSourceCount;
			}
			radNumericTextBox2.Value = new double?((double)Math.Min(this.PagerItem.Paging.PageSize, this.PagerItem.Paging.DataSourceCount));
			this.PrepareSkinnableControlProperties(radNumericTextBox2);
			control.Controls.Add(radNumericTextBox2);
			Button button3;
			if (this.PagerItem.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button3 = new ElasticButton(string.Empty, "t-text rtlButtonText");
				button3.CssClass = "t-button ";
			}
			else
			{
				button3 = new Button();
				button3.CssClass = string.Empty;
			}
			button3.ID = "ChangePageSizeLinkButton";
			AccessibilityHelper.AddToolTip(button3, this.PagerStyle.ChangePageSizeButtonToolTip);
			Button button4 = button3;
			button4.CssClass += "rtlPagerButton";
			button3.Text = this.PagerItem.OwnerTreeList.PagerStyle.ChangePageSizeLinkButtonText;
			button3.Click += this.ChangePageSizeButtonClick;
			control.Controls.Add(button3);
			return control;
		}

		// Token: 0x0600C191 RID: 49553 RVA: 0x002B38B8 File Offset: 0x002B1AB8
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public Control CreateSliderPager()
		{
			Control control = this.BuildContainer("");
			RadSlider radSlider = new RadSlider();
			radSlider.ID = "TreeListSliderPager";
			radSlider.EnableServerSideRendering = true;
			radSlider.IncreaseText = this.PagerItem.OwnerTreeList.PagerStyle.PageSliderIncreaseToolTip;
			radSlider.DecreaseText = this.PagerItem.OwnerTreeList.PagerStyle.PageSliderDecreaseToolTip;
			radSlider.DragText = this.PagerItem.OwnerTreeList.PagerStyle.PageSliderDragToolTip;
			radSlider.Width = Unit.Pixel(200);
			this.PrepareSkinnableControlProperties(radSlider);
			radSlider.RenderMode = this.PagerItem.OwnerTreeList.ResolvedRenderMode;
			radSlider.AutoPostBack = true;
			radSlider.MinimumValue = 1m;
			radSlider.MaximumValue = Math.Max(this.PagerItem.Paging.PageCount, 1);
			radSlider.Value = Math.Min(this.PagerItem.Paging.CurrentPageIndex + 1, radSlider.MaximumValue);
			radSlider.ValueChanged += this.SliderValueChanged;
			control.Controls.Add(radSlider);
			Label label = this.CreateLabel("SliderPagerLabel");
			label.Text = string.Format(this.PagerItem.OwnerTreeList.PagerStyle.PageSliderPagerLabel, this.PagerItem.Paging.CurrentPageIndex + 1, this.PagerItem.Paging.PageCount);
			control.Controls.Add(label);
			return control;
		}

		// Token: 0x0600C192 RID: 49554 RVA: 0x002B3A46 File Offset: 0x002B1C46
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected virtual void PageSizeComboSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.PagerItem.OwnerTreeList.PageSize = int.Parse(e.Value);
		}

		// Token: 0x0600C193 RID: 49555 RVA: 0x002B3A63 File Offset: 0x002B1C63
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected virtual void PageSizeDropDownListSelectedIndexChanged(object sender, DropDownListEventArgs e)
		{
			this.PagerItem.OwnerTreeList.PageSize = int.Parse(e.Value);
		}

		// Token: 0x0600C194 RID: 49556 RVA: 0x002B3A80 File Offset: 0x002B1C80
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Decimal.ToString")]
		protected void SliderValueChanged(object sender, EventArgs e)
		{
			string commandArgument = (--((RadSlider)sender).Value).ToString();
			this.PagerItem.FireCommandEvent("Page", commandArgument);
		}

		// Token: 0x0600C195 RID: 49557 RVA: 0x002B3AB8 File Offset: 0x002B1CB8
		protected void GoToPageLinkButtonClick(object sender, EventArgs e)
		{
			RadNumericTextBox radNumericTextBox = this.PagerItem.FindControl("GoToPageTextBox") as RadNumericTextBox;
			TreeListItem pagerItem = this.PagerItem;
			string commandName = "Page";
			double? value = radNumericTextBox.Value;
			pagerItem.FireCommandEvent(commandName, ((value != null) ? new double?(value.GetValueOrDefault() - 1.0) : null).ToString());
		}

		// Token: 0x0600C196 RID: 49558 RVA: 0x002B3B2C File Offset: 0x002B1D2C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected void ChangePageSizeButtonClick(object sender, EventArgs e)
		{
			RadNumericTextBox radNumericTextBox = this.PagerItem.FindControl("ChangePageSizeTextBox") as RadNumericTextBox;
			this.PagerItem.OwnerTreeList.PageSize = int.Parse(radNumericTextBox.Text);
		}

		// Token: 0x040032F3 RID: 13043
		public const string NextButtonClassName = "rtlPageNext";

		// Token: 0x040032F4 RID: 13044
		public const string FirstButtonClassName = "rtlPageFirst";

		// Token: 0x040032F5 RID: 13045
		public const string LastButtonClassName = "rtlPageLast";

		// Token: 0x040032F6 RID: 13046
		public const string PrevButtonClassName = "rtlPagePrev";

		// Token: 0x040032F7 RID: 13047
		public const string CurrentPagaButtonClassName = "rtlCurrentPage";

		// Token: 0x040032F8 RID: 13048
		public const string ContainerClassName = "rtlWrap";

		// Token: 0x040032F9 RID: 13049
		public const string NumPartContainerClassName = "rtlNumPart";

		// Token: 0x040032FA RID: 13050
		public const string LabelClassName = "rtlPagerLabel";
	}
}
