using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000546 RID: 1350
	internal class RadControlPagerButtonBuilder
	{
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0009BFC2 File Offset: 0x0009A1C2
		private string RuntimeSkin
		{
			get
			{
				return SkinRegistrar.GetRuntimeSkin(this.Control);
			}
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x0009BFD0 File Offset: 0x0009A1D0
		public RadControlPagerButtonBuilder(Control container, RadControlPagerItemProperties properties, Action<string, int> change)
		{
			this.Container = container;
			this.Control = properties.Control;
			this.PagerStyle = properties.PagerStyle;
			this.Paging = properties.PagingSettings;
			this.EnableAriaSupport = properties.EnableAriaSupport;
			this.Change = change;
			RadCompositeDataBoundControl radCompositeDataBoundControl = properties.Control as RadCompositeDataBoundControl;
			this.IsLightweightRendering = (radCompositeDataBoundControl != null && radCompositeDataBoundControl.ResolvedRenderMode == RenderMode.Lightweight);
			this.NextButtonClassName = string.Format(this.NextButtonClassName, this.PagerStyle.Prefix);
			this.FirstButtonClassName = string.Format(this.FirstButtonClassName, this.PagerStyle.Prefix);
			this.LastButtonClassName = string.Format(this.LastButtonClassName, this.PagerStyle.Prefix);
			this.PrevButtonClassName = string.Format(this.PrevButtonClassName, this.PagerStyle.Prefix);
			this.CurrentPageButtonClassName = string.Format(this.CurrentPageButtonClassName, this.PagerStyle.Prefix);
			this.InfoPartClassName = string.Format(this.InfoPartClassName, this.PagerStyle.Prefix);
			this.ContainerClassName = string.Format(this.ContainerClassName, this.PagerStyle.Prefix);
			this.NumPartContainerClassName = string.Format(this.NumPartContainerClassName, this.PagerStyle.Prefix);
			this.LabelClassName = string.Format(this.LabelClassName, this.PagerStyle.Prefix);
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x0009C1A4 File Offset: 0x0009A3A4
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public Control BuildContainer(string className)
		{
			return new Panel
			{
				CssClass = string.Format("{0} {1}", this.ContainerClassName, className)
			};
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x0009C1F0 File Offset: 0x0009A3F0
		protected virtual void PrepareSkinnableControlProperties(ISkinnableControl control)
		{
			(control as Control).PreRender += delegate(object sender, EventArgs args)
			{
				control.Skin = this.RuntimeSkin;
			};
			control.EnableEmbeddedSkins = this.Control.EnableEmbeddedSkins;
			control.EnableEmbeddedScripts = this.Control.EnableEmbeddedScripts;
			control.EnableEmbeddedBaseStylesheet = this.Control.EnableEmbeddedBaseStylesheet;
			control.RegisterWithScriptManager = this.Control.RegisterWithScriptManager;
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x0009C288 File Offset: 0x0009A488
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		protected string PrepareTextFormat(PagerButtonType type, string text)
		{
			string text2 = string.IsNullOrEmpty(text) ? " " : text;
			if (type == PagerButtonType.LinkButton && !this.IsLightweightRendering)
			{
				text2 = string.Format("<span>{0}</span>", text2);
			}
			return text2;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x0009C2BF File Offset: 0x0009A4BF
		protected WebControl EnsureEnableState(WebControl button, string commandArgument)
		{
			if (this.IsCurrentPage(commandArgument))
			{
				button.Attributes.Add("onclick", "return false;");
			}
			return button;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x0009C2E0 File Offset: 0x0009A4E0
		public bool IsCurrentPage(string commandArgument)
		{
			bool result = false;
			RadControlPagingSettings paging = this.Paging;
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

		// Token: 0x06002FB9 RID: 12217 RVA: 0x0009C368 File Offset: 0x0009A568
		public WebControl CreateButtonField(PagerButtonType type, string text, string toolTip, string commandName, string commandArgument, string className)
		{
			WebControl webControl = this.CreateButtonFieldForCommand(type, text, toolTip, commandName, commandArgument);
			webControl.CssClass = className;
			return this.EnsureEnableState(webControl, commandArgument);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x0009C398 File Offset: 0x0009A598
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private WebControl CreateButtonFieldForCommand(PagerButtonType type, string text, string toolTip, string commandName, string commandArgument)
		{
			IButtonControl buttonControl = null;
			switch (type)
			{
			case PagerButtonType.PushButton:
				buttonControl = new Button();
				((Button)buttonControl).ToolTip = toolTip;
				break;
			case PagerButtonType.LinkButton:
				buttonControl = new LinkButton();
				((LinkButton)buttonControl).ToolTip = toolTip;
				break;
			case PagerButtonType.ImageButton:
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

		// Token: 0x06002FBB RID: 12219 RVA: 0x0009C430 File Offset: 0x0009A630
		public Control CreatePrevButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerStyle.PrevPageToolTip, "Page", "Prev", this.PrevButtonClassName);
			webControl.ID = "PrevButton";
			return webControl;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x0009C474 File Offset: 0x0009A674
		public Control CreateNextButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerStyle.NextPageToolTip, "Page", "Next", this.NextButtonClassName);
			webControl.ID = "NextButton";
			return webControl;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x0009C4B8 File Offset: 0x0009A6B8
		public Control CreateFirstButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerStyle.FirstPageToolTip, "Page", "First", this.FirstButtonClassName);
			webControl.ID = "FirstButton";
			return webControl;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x0009C4FC File Offset: 0x0009A6FC
		public Control CreateLastButton()
		{
			WebControl webControl = this.CreateButtonField(PagerButtonType.PushButton, string.Empty, this.PagerStyle.LastPageToolTip, "Page", "Last", this.LastButtonClassName);
			webControl.ID = "LastButton";
			return webControl;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x0009C540 File Offset: 0x0009A740
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected Label CreateLabel(string id)
		{
			return new Label
			{
				ID = id,
				CssClass = this.LabelClassName
			};
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x0009C568 File Offset: 0x0009A768
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		protected virtual Control CreateNumericButton(string text, int commandArgument)
		{
			string className = (this.Paging.CurrentPageIndex == commandArgument) ? this.CurrentPageButtonClassName : "";
			return this.CreateButtonField(PagerButtonType.LinkButton, text, "", "Page", commandArgument.ToString(), className);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x0009C5AC File Offset: 0x0009A7AC
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		public Control CreateNumericPager()
		{
			PlaceHolder placeHolder = new PlaceHolder();
			Panel panel = new Panel();
			placeHolder.Controls.Add(panel);
			panel.CssClass = string.Format("{0} {1}", this.ContainerClassName, this.NumPartContainerClassName);
			int pageButtonCount = this.PagerStyle.PageButtonCount;
			int num = (this.Paging.CurrentPageIndex + 1) / pageButtonCount + (((this.Paging.CurrentPageIndex + 1) % pageButtonCount == 0) ? 0 : 1) - 1;
			num = Math.Max(num, 0) * pageButtonCount;
			if (this.Paging.CurrentPageIndex + 1 > pageButtonCount)
			{
				panel.Controls.Add(this.CreateNumericButton("...", num - 1));
			}
			int num2 = Math.Min(num + pageButtonCount, this.Paging.PageCount);
			for (int i = num; i < num2; i++)
			{
				panel.Controls.Add(this.CreateNumericButton((i + 1).ToString(), i));
			}
			if (num2 < this.Paging.PageCount)
			{
				panel.Controls.Add(this.CreateNumericButton("...", num2));
			}
			if (this.Paging.PageCount == 0)
			{
				panel.Controls.Add(this.CreateNumericButton("1", 0));
			}
			if (!string.IsNullOrEmpty(this.PagerStyle.PagerTextFormat))
			{
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div")
				{
					InnerHtml = string.Format(this.PagerStyle.PagerTextFormat, new object[]
					{
						this.Paging.CurrentPageIndex + 1,
						this.Paging.PageCount,
						this.Paging.FirstIndexInPage,
						Math.Min(this.Paging.FirstIndexInPage + this.Paging.PageSize, this.Paging.DataSourceCount),
						string.Empty,
						this.Paging.DataSourceCount
					})
				};
				htmlGenericControl.Attributes.Add("class", this.InfoPartClassName);
				panel.Controls.Add(htmlGenericControl);
				placeHolder.Controls.Add(htmlGenericControl);
			}
			return placeHolder;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x0009C7E8 File Offset: 0x0009A9E8
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		public Control CreatePageSize()
		{
			Control control = this.BuildContainer("rtlAdvPart");
			Label label = this.CreateLabel("ChangePageSizeLabel");
			label.Text = this.PagerStyle.ChangePageSizeLabelText;
			control.Controls.Add(label);
			ControlItemContainer controlItemContainer;
			if (this.PagerStyle.PageSizeControlType == PagerDropDownControlType.RadComboBox)
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

		// Token: 0x06002FC3 RID: 12227 RVA: 0x0009C8A4 File Offset: 0x0009AAA4
		private bool InitalizePageSizeCombo(RadComboBox pageSizeCombo, IList<int> defaultPageSizes)
		{
			bool result = false;
			if (pageSizeCombo != null)
			{
				pageSizeCombo.EnableAriaSupport = this.EnableAriaSupport;
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
				RadComboBoxItem radComboBoxItem = pageSizeCombo.Items.FindItemByValue(this.Paging.PageSize.ToString());
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

		// Token: 0x06002FC4 RID: 12228 RVA: 0x0009C9DC File Offset: 0x0009ABDC
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
				DropDownListItem dropDownListItem = ddl.FindChildByValue<DropDownListItem>(this.Paging.PageSize.ToString());
				if (dropDownListItem != null)
				{
					dropDownListItem.Selected = true;
				}
				ddl.AutoPostBack = true;
				ddl.CausesValidation = false;
				ddl.SelectedIndexChanged += this.PageSizeDropDownListSelectedIndexChanged;
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0009CAA0 File Offset: 0x0009ACA0
		private IList<int> PrepateDefaultPageSizes()
		{
			IList<int> list = new List<int>();
			list.Add(10);
			list.Add(20);
			list.Add(50);
			if (!list.Contains(this.Paging.PageSize) || this.Paging.CustomPageSize != null)
			{
				if (!list.Contains(this.Paging.PageSize))
				{
					this.Paging.CustomPageSize = new int?(this.Paging.PageSize);
				}
				list.Add(this.Paging.CustomPageSize.Value);
			}
			return list;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x0009CB3C File Offset: 0x0009AD3C
		private void AdjustDropDownControlWidth(ControlItemContainer ddlControl)
		{
			int length = this.Paging.PageSize.ToString().Length;
			int num = 34;
			if (this.RuntimeSkin == "MetroTouch" || this.RuntimeSkin == "Glow" || this.RuntimeSkin == "Silk" || this.RuntimeSkin == "BlackMetroTouch")
			{
				num = 50;
			}
			ddlControl.Width = Unit.Pixel(length * 6 + num);
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x0009CBC0 File Offset: 0x0009ADC0
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
		public Control CreateAdvancedPager()
		{
			Control control = this.BuildContainer("rtlAdvPart");
			Label label = this.CreateLabel("GoToPageLabel");
			label.Text = this.PagerStyle.GoToPageLabelText;
			control.Controls.Add(label);
			RadNumericTextBox radNumericTextBox = new RadNumericTextBox();
			radNumericTextBox.ID = "GoToPageTextBox";
			radNumericTextBox.EnableAriaSupport = this.EnableAriaSupport;
			AccessibilityHelper.AddToolTip(radNumericTextBox, this.PagerStyle.GoToPageTextBoxToolTip);
			int num = 0;
			if (this.RuntimeSkin == "MetroTouch" || this.RuntimeSkin == "Glow" || this.RuntimeSkin == "Silk" || this.RuntimeSkin == "BlackMetroTouch")
			{
				num = 20;
			}
			if (radNumericTextBox.EnableSingleInputRendering)
			{
				radNumericTextBox.Width = Unit.Pixel(num + 10 + this.Paging.PageCount.ToString().Length * 10);
			}
			else
			{
				radNumericTextBox.Width = Unit.Pixel(num + this.Paging.PageCount.ToString().Length * 10);
			}
			radNumericTextBox.NumberFormat.DecimalDigits = 0;
			radNumericTextBox.MinValue = 1.0;
			radNumericTextBox.MaxValue = (double)this.Paging.PageCount;
			radNumericTextBox.Value = new double?((double)(this.Paging.CurrentPageIndex + 1));
			this.PrepareSkinnableControlProperties(radNumericTextBox);
			control.Controls.Add(radNumericTextBox);
			Label label2 = this.CreateLabel("PageOfLabel");
			label2.Text = string.Format(this.PagerStyle.PageOfLabelText, this.Paging.PageCount);
			control.Controls.Add(label2);
			Button button = new Button();
			button.ID = "GoToPageLinkButton";
			AccessibilityHelper.AddToolTip(button, this.PagerStyle.GoToPageButtonToolTip);
			button.CssClass = "rtlPagerButton";
			button.Text = this.PagerStyle.GoToPageLinkButtonText;
			button.Click += this.GoToPageLinkButtonClick;
			control.Controls.Add(button);
			Label label3 = this.CreateLabel("ChangePageSizeLabel");
			label3.Text = this.PagerStyle.ChangePageSizeLabelText;
			control.Controls.Add(label3);
			RadNumericTextBox radNumericTextBox2 = new RadNumericTextBox();
			radNumericTextBox2.ID = "ChangePageSizeTextBox";
			radNumericTextBox2.EnableAriaSupport = this.EnableAriaSupport;
			AccessibilityHelper.AddToolTip(radNumericTextBox2, this.PagerStyle.ChangePageSizeTextBoxToolTip);
			if (radNumericTextBox2.EnableSingleInputRendering)
			{
				radNumericTextBox2.Width = Unit.Pixel(num + 10 + this.Paging.DataSourceCount.ToString().Length * 10);
			}
			else
			{
				radNumericTextBox2.Width = Unit.Pixel(num + this.Paging.DataSourceCount.ToString().Length * 10);
			}
			radNumericTextBox2.NumberFormat.DecimalDigits = 0;
			if (this.Paging.DataSourceCount > 0)
			{
				radNumericTextBox2.MinValue = 1.0;
				radNumericTextBox2.MaxValue = (double)this.Paging.DataSourceCount;
			}
			radNumericTextBox2.Value = new double?((double)Math.Min(this.Paging.PageSize, this.Paging.DataSourceCount));
			this.PrepareSkinnableControlProperties(radNumericTextBox2);
			control.Controls.Add(radNumericTextBox2);
			Button button2 = new Button();
			button2.ID = "ChangePageSizeLinkButton";
			AccessibilityHelper.AddToolTip(button2, this.PagerStyle.ChangePageSizeButtonToolTip);
			button2.CssClass = "rtlPagerButton";
			button2.Text = this.PagerStyle.ChangePageSizeLinkButtonText;
			button2.Click += this.ChangePageSizeButtonClick;
			control.Controls.Add(button2);
			return control;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x0009CF78 File Offset: 0x0009B178
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public Control CreateSliderPager()
		{
			Control control = this.BuildContainer("");
			RadSlider radSlider = new RadSlider();
			radSlider.ID = "SliderPager";
			radSlider.EnableServerSideRendering = true;
			radSlider.IncreaseText = this.PagerStyle.PageSliderIncreaseToolTip;
			radSlider.DecreaseText = this.PagerStyle.PageSliderDecreaseToolTip;
			radSlider.DragText = this.PagerStyle.PageSliderDragToolTip;
			radSlider.Width = Unit.Pixel(200);
			this.PrepareSkinnableControlProperties(radSlider);
			radSlider.AutoPostBack = true;
			radSlider.MinimumValue = 1m;
			radSlider.MaximumValue = Math.Max(this.Paging.PageCount, 1);
			radSlider.Value = Math.Min(this.Paging.CurrentPageIndex + 1, radSlider.MaximumValue);
			radSlider.ValueChanged += this.SliderValueChanged;
			control.Controls.Add(radSlider);
			Label label = this.CreateLabel("SliderPagerLabel");
			label.Text = string.Format(this.PagerStyle.PageSliderPagerLabel, this.Paging.CurrentPageIndex + 1, this.Paging.PageCount);
			control.Controls.Add(label);
			return control;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x0009D0B4 File Offset: 0x0009B2B4
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected virtual void PageSizeComboSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.Change("PageSize", int.Parse(e.Value));
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x0009D0D1 File Offset: 0x0009B2D1
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected virtual void PageSizeDropDownListSelectedIndexChanged(object sender, DropDownListEventArgs e)
		{
			this.Change("PageSisze", int.Parse(e.Value));
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x0009D0EE File Offset: 0x0009B2EE
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Decimal.ToString")]
		protected void SliderValueChanged(object sender, EventArgs e)
		{
			this.Change("CurrentPageIndex", (int)(--((RadSlider)sender).Value));
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x0009D118 File Offset: 0x0009B318
		protected void GoToPageLinkButtonClick(object sender, EventArgs e)
		{
			RadNumericTextBox radNumericTextBox = this.Container.FindControl("GoToPageTextBox") as RadNumericTextBox;
			Action<string, int> change = this.Change;
			string arg = "CurrentPageIndex";
			double? value = radNumericTextBox.Value;
			change(arg, (int)((value != null) ? new double?(value.GetValueOrDefault() - 1.0) : null).Value);
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x0009D188 File Offset: 0x0009B388
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
		protected void ChangePageSizeButtonClick(object sender, EventArgs e)
		{
			RadNumericTextBox radNumericTextBox = this.Container.FindControl("ChangePageSizeTextBox") as RadNumericTextBox;
			Action<string, int> change = this.Change;
			string arg = "PageSize";
			double? value = radNumericTextBox.Value;
			change(arg, (int)((value != null) ? new double?(value.GetValueOrDefault() - 1.0) : null).Value);
		}

		// Token: 0x04000CCC RID: 3276
		private readonly string NextButtonClassName = "{0}PageNext";

		// Token: 0x04000CCD RID: 3277
		private readonly string FirstButtonClassName = "{0}PageFirst";

		// Token: 0x04000CCE RID: 3278
		private readonly string LastButtonClassName = "{0}PageLast";

		// Token: 0x04000CCF RID: 3279
		private readonly string PrevButtonClassName = "{0}PagePrev";

		// Token: 0x04000CD0 RID: 3280
		private readonly string CurrentPageButtonClassName = "{0}CurrentPage";

		// Token: 0x04000CD1 RID: 3281
		private readonly string InfoPartClassName = "{0}InfoPart";

		// Token: 0x04000CD2 RID: 3282
		private readonly string ContainerClassName = "{0}Wrap";

		// Token: 0x04000CD3 RID: 3283
		private readonly string NumPartContainerClassName = "{0}NumPart";

		// Token: 0x04000CD4 RID: 3284
		private readonly string LabelClassName = "{0}PagerLabel";

		// Token: 0x04000CD5 RID: 3285
		private readonly Control Container;

		// Token: 0x04000CD6 RID: 3286
		private readonly ISkinnableControl Control;

		// Token: 0x04000CD7 RID: 3287
		private readonly RadControlPagerStyle PagerStyle;

		// Token: 0x04000CD8 RID: 3288
		private readonly RadControlPagingSettings Paging;

		// Token: 0x04000CD9 RID: 3289
		private readonly bool EnableAriaSupport;

		// Token: 0x04000CDA RID: 3290
		private readonly bool IsLightweightRendering;

		// Token: 0x04000CDB RID: 3291
		private readonly Action<string, int> Change;
	}
}
