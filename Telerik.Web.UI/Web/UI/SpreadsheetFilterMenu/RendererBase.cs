using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetFilterMenu
{
	// Token: 0x020008B1 RID: 2225
	internal abstract class RendererBase : IFilterMenuRenderer
	{
		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x0600528D RID: 21133 RVA: 0x001007EA File Offset: 0x000FE9EA
		// (set) Token: 0x0600528E RID: 21134 RVA: 0x001007F2 File Offset: 0x000FE9F2
		public IFilterMenuView View
		{
			get
			{
				return this._view;
			}
			protected set
			{
				this._view = value;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x0600528F RID: 21135 RVA: 0x001007FB File Offset: 0x000FE9FB
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.View.Localization;
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x06005290 RID: 21136 RVA: 0x00100808 File Offset: 0x000FEA08
		// (set) Token: 0x06005291 RID: 21137 RVA: 0x00100810 File Offset: 0x000FEA10
		public WebControl SortMenu { get; set; }

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x06005292 RID: 21138 RVA: 0x00100819 File Offset: 0x000FEA19
		// (set) Token: 0x06005293 RID: 21139 RVA: 0x00100821 File Offset: 0x000FEA21
		public Panel ButtonsPanel { get; set; }

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x0010082A File Offset: 0x000FEA2A
		// (set) Token: 0x06005295 RID: 21141 RVA: 0x00100832 File Offset: 0x000FEA32
		public WebControl FilterByConditionPanel { get; set; }

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x0010083B File Offset: 0x000FEA3B
		// (set) Token: 0x06005297 RID: 21143 RVA: 0x00100843 File Offset: 0x000FEA43
		public WebControl FilterByValuePanel { get; set; }

		// Token: 0x06005298 RID: 21144 RVA: 0x0010084C File Offset: 0x000FEA4C
		public RendererBase(IFilterMenuView view)
		{
			this.View = view;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x0010085C File Offset: 0x000FEA5C
		public void CreateLayout(Control container)
		{
			this.SortMenu = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rssMenu"
			};
			container.Controls.Add(this.SortMenu);
			WebControl child = new WebControl(HtmlTextWriterTag.Hr)
			{
				CssClass = "rssSeparator"
			};
			container.Controls.Add(child);
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rssCollapsibleList"
			};
			container.Controls.Add(webControl);
			this.CreatePanelBarLayout(webControl);
			WebControl child2 = new WebControl(HtmlTextWriterTag.Hr)
			{
				CssClass = "rssSeparator"
			};
			container.Controls.Add(child2);
			this.ButtonsPanel = new Panel
			{
				CssClass = "rssButtons"
			};
			container.Controls.Add(this.ButtonsPanel);
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x00100934 File Offset: 0x000FEB34
		private void CreatePanelBarLayout(Control container)
		{
			this.FilterByConditionPanel = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rssFormList"
			};
			WebControl webControl = this.CreatePanelBarListItem(this.Localization.FilterMenuFilterByCondition, this.FilterByConditionPanel);
			webControl.Attributes.Add("data-value", "customFilter");
			container.Controls.Add(webControl);
			this.FilterByValuePanel = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rssFormList"
			};
			WebControl webControl2 = this.CreatePanelBarListItem(this.Localization.FilterMenuFilterByValue, this.FilterByValuePanel);
			webControl2.Attributes.Add("data-value", "valueFilter");
			container.Controls.Add(webControl2);
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x001009E7 File Offset: 0x000FEBE7
		public virtual void CreateControls()
		{
			this.CreateSortButtons();
			this.CreatePanelBarControls();
			this.CreateCommandButtons();
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x001009FC File Offset: 0x000FEBFC
		private void CreateSortButtons()
		{
			WebControl webControl = this.CreateListItem();
			webControl.Controls.Add(this.View.SortAscButton);
			this.SortMenu.Controls.Add(webControl);
			WebControl webControl2 = this.CreateListItem();
			webControl2.Controls.Add(this.View.SortDescButton);
			this.SortMenu.Controls.Add(webControl2);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00100A65 File Offset: 0x000FEC65
		private void CreatePanelBarControls()
		{
			this.CreateFilterByConditionControls();
			this.CreateFilterByValueControls();
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00100A73 File Offset: 0x000FEC73
		private void CreateCommandButtons()
		{
			this.ButtonsPanel.Controls.Add(this.View.ApplyButton);
			this.ButtonsPanel.Controls.Add(this.View.ClearButton);
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00100AAC File Offset: 0x000FECAC
		private void CreateFilterByConditionControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			this.FilterByConditionPanel.Controls.Add(webControl);
			webControl.Controls.Add(this.View.ConditionDropDownList);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li)
			{
				CssClass = "rssFormListOption"
			};
			this.FilterByConditionPanel.Controls.Add(webControl2);
			webControl2.Controls.Add(this.View.ConditionTextBox);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li)
			{
				CssClass = "rssFormListOption"
			};
			this.FilterByConditionPanel.Controls.Add(webControl3);
			webControl3.Controls.Add(this.View.ConditionNumericTextBox);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Li)
			{
				CssClass = "rssFormListOption"
			};
			this.FilterByConditionPanel.Controls.Add(webControl4);
			webControl4.Controls.Add(this.View.ConditionDatePicker);
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00100BA8 File Offset: 0x000FEDA8
		private void CreateFilterByValueControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			this.FilterByValuePanel.Controls.Add(webControl);
			webControl.Controls.Add(this.View.ValueSearchBox);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			this.FilterByValuePanel.Controls.Add(webControl2);
			webControl2.Controls.Add(this.View.ValueListBox);
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x00100C14 File Offset: 0x000FEE14
		private WebControl CreateListItem()
		{
			return new WebControl(HtmlTextWriterTag.Li)
			{
				CssClass = "rssLI"
			};
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00100C38 File Offset: 0x000FEE38
		private WebControl CreatePanelBarListItem(string text, WebControl detailsPanel)
		{
			WebControl webControl = this.CreateListItem();
			WebControl child = this.CreatePanelBarToggle(text);
			webControl.Controls.Add(child);
			WebControl webControl2 = this.CreatePanelBarDetails();
			webControl.Controls.Add(webControl2);
			webControl2.Controls.Add(detailsPanel);
			return webControl;
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x00100C80 File Offset: 0x000FEE80
		private WebControl CreatePanelBarToggle(string text)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format("{0} {1}", "rssLink", "rssSummary")
			};
			WebControl child = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rssIcon"
			};
			webControl.Controls.Add(child);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rssText"
			};
			webControl.Controls.Add(webControl2);
			webControl2.Controls.Add(new LiteralControl(text));
			return webControl;
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00100D0C File Offset: 0x000FEF0C
		private WebControl CreatePanelBarDetails()
		{
			return new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rssDetails"
			};
		}

		// Token: 0x04001449 RID: 5193
		private IFilterMenuView _view;
	}
}
