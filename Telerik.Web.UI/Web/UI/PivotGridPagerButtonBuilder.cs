using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DFC RID: 3580
	internal class PivotGridPagerButtonBuilder
	{
		// Token: 0x17002A03 RID: 10755
		// (get) Token: 0x060084D6 RID: 34006 RVA: 0x001E4DE6 File Offset: 0x001E2FE6
		// (set) Token: 0x060084D5 RID: 34005 RVA: 0x001E4DDD File Offset: 0x001E2FDD
		public PivotGridPagerItem PagerItem { get; internal set; }

		// Token: 0x060084D7 RID: 34007 RVA: 0x001E4DEE File Offset: 0x001E2FEE
		public PivotGridPagerButtonBuilder(PivotGridPagerItem pagerItem)
		{
			this.PagerItem = pagerItem;
		}

		// Token: 0x060084D8 RID: 34008 RVA: 0x001E4E00 File Offset: 0x001E3000
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public Control BuildContainer(string className)
		{
			return new Panel
			{
				CssClass = string.Format("{0} {1}", "rpgWrap", className)
			};
		}

		// Token: 0x060084D9 RID: 34009 RVA: 0x001E4E54 File Offset: 0x001E3054
		protected virtual void PrepareSkinnableControlProperties(ISkinnableControl control)
		{
			(control as Control).PreRender += delegate(object sender, EventArgs args)
			{
				control.Skin = this.PagerItem.OwnerPivotGrid.RuntimeSkin;
			};
			control.EnableEmbeddedSkins = this.PagerItem.OwnerPivotGrid.EnableEmbeddedSkins;
			control.EnableEmbeddedScripts = this.PagerItem.OwnerPivotGrid.EnableEmbeddedScripts;
			control.EnableEmbeddedBaseStylesheet = this.PagerItem.OwnerPivotGrid.EnableEmbeddedBaseStylesheet;
			control.RegisterWithScriptManager = this.PagerItem.OwnerPivotGrid.RegisterWithScriptManager;
		}

		// Token: 0x060084DA RID: 34010 RVA: 0x001E4F00 File Offset: 0x001E3100
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected string PrepareTextFormat(PivotGridPagerButtonType type, string text)
		{
			string text2 = string.IsNullOrEmpty(text) ? " " : text;
			if (type == PivotGridPagerButtonType.LinkButton && this.PagerItem.OwnerPivotGrid.ResolvedRenderMode != RenderMode.Lightweight)
			{
				text2 = string.Format("<span>{0}</span>", text2);
			}
			return text2;
		}

		// Token: 0x060084DB RID: 34011 RVA: 0x001E4F42 File Offset: 0x001E3142
		protected WebControl EnsureEnableState(WebControl button, string commandArgument)
		{
			if (this.IsCurrentPage(commandArgument))
			{
				button.Attributes.Add("onclick", "return false;");
			}
			return button;
		}

		// Token: 0x060084DC RID: 34012 RVA: 0x001E4F64 File Offset: 0x001E3164
		public bool IsCurrentPage(string commandArgument)
		{
			bool result = false;
			PivotGridPagingManager paging = this.PagerItem.Paging;
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

		// Token: 0x060084DD RID: 34013 RVA: 0x001E4FF0 File Offset: 0x001E31F0
		public WebControl CreateButtonField(PivotGridPagerButtonType type, string text, string toolTip, string commandName, string commandArgument, string className)
		{
			WebControl webControl = this.CreateButtonFieldForCommand(type, text, toolTip, commandName, commandArgument);
			if (this.PagerItem.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight && type == PivotGridPagerButtonType.PushButton)
			{
				webControl.CssClass = "rpgActionButton " + className;
			}
			else
			{
				webControl.CssClass = className;
			}
			webControl.Attributes.Add("aria-label", webControl.ToolTip);
			return this.EnsureEnableState(webControl, commandArgument);
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x001E5060 File Offset: 0x001E3260
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private WebControl CreateButtonFieldForCommand(PivotGridPagerButtonType type, string text, string toolTip, string commandName, string commandArgument)
		{
			IButtonControl buttonControl = null;
			switch (type)
			{
			case PivotGridPagerButtonType.PushButton:
				if (this.PagerItem.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					buttonControl = new ElasticButton("rpgIcon");
					((ElasticButton)buttonControl).ToolTip = toolTip;
				}
				else
				{
					buttonControl = new Button();
					((Button)buttonControl).ToolTip = toolTip;
				}
				break;
			case PivotGridPagerButtonType.LinkButton:
				buttonControl = new LinkButton();
				((LinkButton)buttonControl).ToolTip = toolTip;
				break;
			case PivotGridPagerButtonType.ImageButton:
				buttonControl = new ImageButton();
				((ImageButton)buttonControl).ToolTip = toolTip;
				((ImageButton)buttonControl).AlternateText = toolTip;
				break;
			}
			buttonControl.Text = this.PrepareTextFormat(type, text);
			buttonControl.CommandName = commandName;
			buttonControl.CommandArgument = commandArgument;
			buttonControl.CausesValidation = false;
			return buttonControl as WebControl;
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x001E5124 File Offset: 0x001E3324
		public Control CreatePrevButton()
		{
			WebControl webControl = this.CreateButtonField(PivotGridPagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerPivotGrid.PagerStyle.PrevPageToolTip, "Page", "Prev", "rpgPagePrev");
			webControl.ID = "PrevButton";
			return webControl;
		}

		// Token: 0x060084E0 RID: 34016 RVA: 0x001E5170 File Offset: 0x001E3370
		public Control CreateNextButton()
		{
			WebControl webControl = this.CreateButtonField(PivotGridPagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerPivotGrid.PagerStyle.NextPageToolTip, "Page", "Next", "rpgPageNext");
			webControl.ID = "NextButton";
			return webControl;
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x001E51BC File Offset: 0x001E33BC
		public Control CreateFirstButton()
		{
			WebControl webControl = this.CreateButtonField(PivotGridPagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerPivotGrid.PagerStyle.FirstPageToolTip, "Page", "First", "rpgPageFirst");
			webControl.ID = "FirstButton";
			return webControl;
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x001E5208 File Offset: 0x001E3408
		public Control CreateLastButton()
		{
			WebControl webControl = this.CreateButtonField(PivotGridPagerButtonType.PushButton, string.Empty, this.PagerItem.OwnerPivotGrid.PagerStyle.LastPageToolTip, "Page", "Last", "rpgPageLast");
			webControl.ID = "LastButton";
			return webControl;
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x001E5254 File Offset: 0x001E3454
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected Label CreateLabel(string id)
		{
			return new Label
			{
				ID = id,
				CssClass = "rpgPagerLabel"
			};
		}

		// Token: 0x060084E4 RID: 34020 RVA: 0x001E527C File Offset: 0x001E347C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		protected virtual Control CreateNumericButton(string text, int commandArgument)
		{
			string className = (this.PagerItem.OwnerPivotGrid.CurrentPageIndex == commandArgument) ? "rpgCurrentPage" : "";
			return this.CreateButtonField(PivotGridPagerButtonType.LinkButton, text, "", "Page", commandArgument.ToString(), className);
		}

		// Token: 0x060084E5 RID: 34021 RVA: 0x001E52C4 File Offset: 0x001E34C4
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		public Control CreateNumericPager()
		{
			RadPivotGrid ownerPivotGrid = this.PagerItem.OwnerPivotGrid;
			Panel panel = new Panel();
			panel.CssClass = string.Format("{0} {1}", "rpgWrap", "rpgNumPart");
			int pageButtonCount = ownerPivotGrid.PagerStyle.PageButtonCount;
			int num = (ownerPivotGrid.CurrentPageIndex + 1) / pageButtonCount + (((ownerPivotGrid.CurrentPageIndex + 1) % pageButtonCount == 0) ? 0 : 1) - 1;
			num = Math.Max(num, 0) * pageButtonCount;
			if (ownerPivotGrid.CurrentPageIndex + 1 > pageButtonCount)
			{
				panel.Controls.Add(this.CreateNumericButton("...", num - 1));
			}
			int num2 = Math.Min(num + pageButtonCount, ownerPivotGrid.PageCount);
			if (ownerPivotGrid.CurrentPageIndex + 1 > num2)
			{
				ownerPivotGrid.CurrentPageIndex = num2 - 1;
			}
			for (int i = num; i < num2; i++)
			{
				panel.Controls.Add(this.CreateNumericButton((i + 1).ToString(), i));
			}
			if (num2 < ownerPivotGrid.PageCount)
			{
				panel.Controls.Add(this.CreateNumericButton("...", num2));
			}
			if (ownerPivotGrid.PageCount == 0)
			{
				panel.Controls.Add(this.CreateNumericButton("1", 0));
			}
			return panel;
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x001E53F0 File Offset: 0x001E35F0
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		public Control CreatePageSize()
		{
			Control control = this.BuildContainer("rpgAdvPart");
			Label label = this.CreateLabel("ChangePageSizeLabel");
			label.Text = this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeLabelText;
			control.Controls.Add(label);
			ControlItemContainer controlItemContainer;
			if (this.PagerItem.OwnerPivotGrid.PagerStyle.PageSizeControlType == PagerDropDownControlType.RadComboBox)
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
			controlItemContainer.RenderMode = this.PagerItem.OwnerPivotGrid.RenderMode;
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

		// Token: 0x060084E7 RID: 34023 RVA: 0x001E54D8 File Offset: 0x001E36D8
		private bool InitalizePageSizeCombo(RadComboBox pageSizeCombo, IList<int> defaultPageSizes)
		{
			bool result = false;
			if (pageSizeCombo != null)
			{
				pageSizeCombo.EnableAriaSupport = this.PagerItem.OwnerPivotGrid.EnableAriaSupport;
				pageSizeCombo.TableSummary = this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeComboBoxTableSummary;
				pageSizeCombo.TableCaption = "PageSizeComboBox";
				pageSizeCombo.InputTitle = this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeComboBoxToolTip;
				if (!string.IsNullOrEmpty(this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeComboBoxTableSummary))
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
				RadComboBoxItem radComboBoxItem = pageSizeCombo.Items.FindItemByValue(this.PagerItem.OwnerPivotGrid.PageSize.ToString());
				if (radComboBoxItem != null)
				{
					radComboBoxItem.Selected = true;
				}
				pageSizeCombo.AutoPostBack = true;
				pageSizeCombo.SelectedIndexChanged += this.PageSizeComboSelectedIndexChanged;
				result = true;
			}
			return result;
		}

		// Token: 0x060084E8 RID: 34024 RVA: 0x001E5624 File Offset: 0x001E3824
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
				DropDownListItem dropDownListItem = ddl.FindChildByValue<DropDownListItem>(this.PagerItem.OwnerPivotGrid.PageSize.ToString());
				if (dropDownListItem != null)
				{
					dropDownListItem.Selected = true;
				}
				ddl.AutoPostBack = true;
				ddl.SelectedIndexChanged += this.PageSizeDropDownListSelectedIndexChanged;
			}
		}

		// Token: 0x060084E9 RID: 34025 RVA: 0x001E56E8 File Offset: 0x001E38E8
		private IList<int> PrepateDefaultPageSizes()
		{
			IList<int> list = new List<int>();
			list.Add(10);
			list.Add(20);
			list.Add(50);
			if (!list.Contains(this.PagerItem.OwnerPivotGrid.PageSize) || this.PagerItem.OwnerPivotGrid.CustomPageSize != null)
			{
				if (!list.Contains(this.PagerItem.OwnerPivotGrid.PageSize))
				{
					this.PagerItem.OwnerPivotGrid.CustomPageSize = new int?(this.PagerItem.OwnerPivotGrid.PageSize);
				}
				list.Add(this.PagerItem.OwnerPivotGrid.CustomPageSize.Value);
			}
			return list;
		}

		// Token: 0x060084EA RID: 34026 RVA: 0x001E57A4 File Offset: 0x001E39A4
		private void AdjustDropDownControlWidth(ControlItemContainer ddlControl)
		{
			int length = this.PagerItem.Paging.PageSize.ToString().Length;
			int num = 34;
			if (this.PagerItem.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				if (this.PagerItem.OwnerPivotGrid.RuntimeSkin == "MetroTouch" || this.PagerItem.OwnerPivotGrid.RuntimeSkin == "Glow" || this.PagerItem.OwnerPivotGrid.RuntimeSkin == "Silk" || this.PagerItem.OwnerPivotGrid.RuntimeSkin == "BlackMetroTouch")
				{
					num = 50;
				}
				if (this.PagerItem.OwnerPivotGrid.RuntimeSkin == "Bootstrap")
				{
					num = 55;
				}
				ddlControl.Width = Unit.Pixel(length * 6 + num);
				return;
			}
			ddlControl.Width = Unit.Parse((double)length * 2.1 + "em");
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x001E58B4 File Offset: 0x001E3AB4
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		public Control CreateAdvancedPager()
		{
			Control control = this.BuildContainer("rpgAdvPart");
			Label label = this.CreateLabel("GoToPageLabel");
			label.Text = this.PagerItem.OwnerPivotGrid.PagerStyle.GoToPageLabelText;
			control.Controls.Add(label);
			RadNumericTextBox radNumericTextBox = new RadNumericTextBox();
			radNumericTextBox.RenderMode = this.PagerItem.OwnerPivotGrid.RenderMode;
			radNumericTextBox.ID = "GoToPageTextBox";
			radNumericTextBox.ToolTip = this.PagerItem.OwnerPivotGrid.PagerStyle.GoToPageTextBoxToolTip;
			int num = 0;
			if (this.PagerItem.OwnerPivotGrid.RuntimeSkin == "MetroTouch" || this.PagerItem.OwnerPivotGrid.RuntimeSkin == "Glow" || this.PagerItem.OwnerPivotGrid.RuntimeSkin == "Silk" || this.PagerItem.OwnerPivotGrid.RuntimeSkin == "BlackMetroTouch")
			{
				num = 20;
			}
			if (radNumericTextBox.RenderMode == RenderMode.Classic)
			{
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
			int num2 = this.PagerItem.Paging.CurrentPageIndex + 1;
			if (num2 > this.PagerItem.Paging.PageCount)
			{
				num2 = this.PagerItem.Paging.PageCount - 1;
				this.PagerItem.OwnerPivotGrid.CurrentPageIndex = num2;
			}
			radNumericTextBox.Value = new double?((double)num2);
			this.PrepareSkinnableControlProperties(radNumericTextBox);
			control.Controls.Add(radNumericTextBox);
			Label label2 = this.CreateLabel("PageOfLabel");
			label2.Text = string.Format(this.PagerItem.OwnerPivotGrid.PagerStyle.PageOfLabelText, this.PagerItem.Paging.PageCount);
			control.Controls.Add(label2);
			Button button;
			if (this.PagerItem.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				button = new Button();
				button.CssClass = string.Empty;
			}
			else
			{
				button = new ElasticButton(string.Empty, "t-text rpgButtonText");
				button.CssClass = "t-button ";
			}
			button.ID = "GoToPageLinkButton";
			button.ToolTip = this.PagerItem.OwnerPivotGrid.PagerStyle.GoToPageButtonToolTip;
			Button button2 = button;
			button2.CssClass += "rpgPagerButton";
			button.Text = this.PagerItem.OwnerPivotGrid.PagerStyle.GoToPageLinkButtonText;
			button.Click += this.GoToPageLinkButtonClick;
			control.Controls.Add(button);
			Label label3 = this.CreateLabel("ChangePageSizeLabel");
			label3.Text = this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeLabelText;
			control.Controls.Add(label3);
			RadNumericTextBox radNumericTextBox2 = new RadNumericTextBox();
			radNumericTextBox2.RenderMode = this.PagerItem.OwnerPivotGrid.RenderMode;
			radNumericTextBox2.ID = "ChangePageSizeTextBox";
			if (radNumericTextBox.RenderMode == RenderMode.Classic)
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
			}
			radNumericTextBox2.ToolTip = this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeTextBoxToolTip;
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
			if (this.PagerItem.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				button3 = new Button();
				button3.CssClass = string.Empty;
			}
			else
			{
				button3 = new ElasticButton(string.Empty, "t-text rpgButtonText");
				button3.CssClass = "t-button ";
			}
			button3.ID = "ChangePageSizeLinkButton";
			Button button4 = button3;
			button4.CssClass += "rpgPagerButton";
			button3.ToolTip = this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeButtonToolTip;
			button3.Text = this.PagerItem.OwnerPivotGrid.PagerStyle.ChangePageSizeLinkButtonText;
			button3.Click += this.ChangePageSizeButtonClick;
			control.Controls.Add(button3);
			return control;
		}

		// Token: 0x060084EC RID: 34028 RVA: 0x001E5EC4 File Offset: 0x001E40C4
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public Control CreateSliderPager()
		{
			Control control = this.BuildContainer("");
			RadSlider radSlider = new RadSlider();
			radSlider.RenderMode = this.PagerItem.OwnerPivotGrid.RenderMode;
			radSlider.ID = "PivotGridSliderPager";
			radSlider.EnableServerSideRendering = true;
			radSlider.IncreaseText = this.PagerItem.OwnerPivotGrid.PagerStyle.PageSliderIncreaseToolTip;
			radSlider.DecreaseText = this.PagerItem.OwnerPivotGrid.PagerStyle.PageSliderDecreaseToolTip;
			radSlider.DragText = this.PagerItem.OwnerPivotGrid.PagerStyle.PageSliderDragToolTip;
			radSlider.Width = Unit.Pixel(200);
			this.PrepareSkinnableControlProperties(radSlider);
			radSlider.AutoPostBack = true;
			radSlider.MinimumValue = 1m;
			radSlider.MaximumValue = Math.Max(this.PagerItem.Paging.PageCount, 1);
			radSlider.Value = Math.Min(this.PagerItem.Paging.CurrentPageIndex + 1, radSlider.MaximumValue);
			radSlider.ValueChanged += this.SliderValueChanged;
			control.Controls.Add(radSlider);
			Label label = this.CreateLabel("SliderPagerLabel");
			label.Text = string.Format(this.PagerItem.OwnerPivotGrid.PagerStyle.PageSliderPagerLabel, this.PagerItem.Paging.CurrentPageIndex + 1, this.PagerItem.Paging.PageCount);
			control.Controls.Add(label);
			return control;
		}

		// Token: 0x060084ED RID: 34029 RVA: 0x001E6052 File Offset: 0x001E4252
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected virtual void PageSizeComboSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.PagerItem.OwnerPivotGrid.PageSize = int.Parse(e.Value);
		}

		// Token: 0x060084EE RID: 34030 RVA: 0x001E606F File Offset: 0x001E426F
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected virtual void PageSizeDropDownListSelectedIndexChanged(object sender, DropDownListEventArgs e)
		{
			this.PagerItem.OwnerPivotGrid.PageSize = int.Parse(e.Value);
		}

		// Token: 0x060084EF RID: 34031 RVA: 0x001E608C File Offset: 0x001E428C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Decimal.ToString")]
		protected void SliderValueChanged(object sender, EventArgs e)
		{
			string commandArgument = (--((RadSlider)sender).Value).ToString();
			this.PagerItem.FireCommandEvent("Page", commandArgument);
		}

		// Token: 0x060084F0 RID: 34032 RVA: 0x001E60C4 File Offset: 0x001E42C4
		protected void GoToPageLinkButtonClick(object sender, EventArgs e)
		{
			RadNumericTextBox radNumericTextBox = this.PagerItem.FindControl("GoToPageTextBox") as RadNumericTextBox;
			PivotGridItem pagerItem = this.PagerItem;
			string commandName = "Page";
			double? value = radNumericTextBox.Value;
			pagerItem.FireCommandEvent(commandName, ((value != null) ? new double?(value.GetValueOrDefault() - 1.0) : null).ToString());
		}

		// Token: 0x060084F1 RID: 34033 RVA: 0x001E6138 File Offset: 0x001E4338
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected void ChangePageSizeButtonClick(object sender, EventArgs e)
		{
			RadNumericTextBox radNumericTextBox = this.PagerItem.FindControl("ChangePageSizeTextBox") as RadNumericTextBox;
			this.PagerItem.OwnerPivotGrid.PageSize = int.Parse(radNumericTextBox.Text);
		}

		// Token: 0x0400250A RID: 9482
		public const string NextButtonClassName = "rpgPageNext";

		// Token: 0x0400250B RID: 9483
		public const string FirstButtonClassName = "rpgPageFirst";

		// Token: 0x0400250C RID: 9484
		public const string LastButtonClassName = "rpgPageLast";

		// Token: 0x0400250D RID: 9485
		public const string PrevButtonClassName = "rpgPagePrev";

		// Token: 0x0400250E RID: 9486
		public const string CurrentPagaButtonClassName = "rpgCurrentPage";

		// Token: 0x0400250F RID: 9487
		public const string ContainerClassName = "rpgWrap";

		// Token: 0x04002510 RID: 9488
		public const string NumPartContainerClassName = "rpgNumPart";

		// Token: 0x04002511 RID: 9489
		public const string LabelClassName = "rpgPagerLabel";
	}
}
