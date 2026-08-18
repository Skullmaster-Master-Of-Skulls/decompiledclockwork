using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetValidation
{
	// Token: 0x020008DE RID: 2270
	internal abstract class RendererBase : IValidationRenderer
	{
		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06005569 RID: 21865 RVA: 0x00106199 File Offset: 0x00104399
		// (set) Token: 0x0600556A RID: 21866 RVA: 0x001061A1 File Offset: 0x001043A1
		public IValidationView View
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

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x0600556B RID: 21867 RVA: 0x001061AA File Offset: 0x001043AA
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.View.Localization;
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x0600556C RID: 21868 RVA: 0x001061B7 File Offset: 0x001043B7
		// (set) Token: 0x0600556D RID: 21869 RVA: 0x001061BF File Offset: 0x001043BF
		public WebControl CriteriaPanel { get; set; }

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x0600556E RID: 21870 RVA: 0x001061C8 File Offset: 0x001043C8
		// (set) Token: 0x0600556F RID: 21871 RVA: 0x001061D0 File Offset: 0x001043D0
		public WebControl NumberCriteriaPanel { get; set; }

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x001061D9 File Offset: 0x001043D9
		// (set) Token: 0x06005571 RID: 21873 RVA: 0x001061E1 File Offset: 0x001043E1
		public WebControl TextCriteriaPanel { get; set; }

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x06005572 RID: 21874 RVA: 0x001061EA File Offset: 0x001043EA
		// (set) Token: 0x06005573 RID: 21875 RVA: 0x001061F2 File Offset: 0x001043F2
		public WebControl DateCriteriaPanel { get; set; }

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x001061FB File Offset: 0x001043FB
		// (set) Token: 0x06005575 RID: 21877 RVA: 0x00106203 File Offset: 0x00104403
		public WebControl CustomCriteriaPanel { get; set; }

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x0010620C File Offset: 0x0010440C
		// (set) Token: 0x06005577 RID: 21879 RVA: 0x00106214 File Offset: 0x00104414
		public WebControl InvalidDataPanel { get; set; }

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x0010621D File Offset: 0x0010441D
		// (set) Token: 0x06005579 RID: 21881 RVA: 0x00106225 File Offset: 0x00104425
		public WebControl HintPanel { get; set; }

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x0600557A RID: 21882 RVA: 0x0010622E File Offset: 0x0010442E
		// (set) Token: 0x0600557B RID: 21883 RVA: 0x00106236 File Offset: 0x00104436
		public Panel ButtonsPanel { get; set; }

		// Token: 0x0600557C RID: 21884 RVA: 0x0010623F File Offset: 0x0010443F
		public RendererBase(IValidationView view)
		{
			this.View = view;
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x00106250 File Offset: 0x00104450
		public void CreateLayout(Control container)
		{
			this.CriteriaPanel = this.CreateFormList("");
			container.Controls.Add(this.CriteriaPanel);
			this.NumberCriteriaPanel = this.CreateFormList("rssFormListOption");
			container.Controls.Add(this.NumberCriteriaPanel);
			this.NumberCriteriaPanel.Attributes.Add("data-value", "number");
			this.TextCriteriaPanel = this.CreateFormList("rssFormListOption");
			container.Controls.Add(this.TextCriteriaPanel);
			this.TextCriteriaPanel.Attributes.Add("data-value", "text");
			this.DateCriteriaPanel = this.CreateFormList("rssFormListOption");
			container.Controls.Add(this.DateCriteriaPanel);
			this.DateCriteriaPanel.Attributes.Add("data-value", "date");
			this.CustomCriteriaPanel = this.CreateFormList("rssFormListOption");
			container.Controls.Add(this.CustomCriteriaPanel);
			this.CustomCriteriaPanel.Attributes.Add("data-value", "custom");
			container.Controls.Add(this.CreateSeparator());
			this.InvalidDataPanel = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = string.Format("{0} {1}", "rssFormList", "invalidData")
			};
			container.Controls.Add(this.InvalidDataPanel);
			container.Controls.Add(this.CreateSeparator());
			this.HintPanel = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rssFormList"
			};
			container.Controls.Add(this.HintPanel);
			this.ButtonsPanel = new Panel
			{
				CssClass = "rssButtons"
			};
			container.Controls.Add(this.ButtonsPanel);
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x0010641F File Offset: 0x0010461F
		public virtual void CreateControls()
		{
			this.CreateCriteriaControls();
			this.CreateNumberCriteriaControls();
			this.CreateTextCriteriaControls();
			this.CreateDateCriteriaControls();
			this.CreateCustomCriteriaControls();
			this.CreateInvalidDataControls();
			this.CreateHintControls();
			this.CreateCommandButtons();
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00106454 File Offset: 0x00104654
		private void CreateCriteriaControls()
		{
			WebControl webControl = this.CreateListItem();
			this.CriteriaPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(this.View.CriteriaDropDownList.ClientID, this.Localization.ValidationCriteria);
			webControl.Controls.Add(child);
			webControl.Controls.Add(this.View.CriteriaDropDownList);
			WebControl webControl2 = this.CreateListItem();
			this.CriteriaPanel.Controls.Add(webControl2);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Label);
			webControl2.Controls.Add(webControl3);
			webControl3.Controls.Add(this.View.CriteriaIgnoreCheckBox);
			webControl3.Controls.Add(new LiteralControl(this.Localization.ValidationIgnoreBlank));
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Label);
			webControl4.Controls.Add(this.View.CriteriaShowButtonCheckBox);
			Label label = new Label();
			label.CssClass = "showCalendarButton";
			label.Text = this.Localization.ValidationShowCalendarButton;
			webControl4.Controls.Add(label);
			Label label2 = new Label();
			label2.CssClass = "showListButton";
			label2.Text = this.Localization.ValidationShowListButton;
			webControl4.Controls.Add(label2);
			webControl2.Controls.Add(webControl4);
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x001065B0 File Offset: 0x001047B0
		private void CreateNumberCriteriaControls()
		{
			WebControl webControl = this.CreateListItem();
			this.NumberCriteriaPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(this.View.NumberCriteriaDropDownList.ClientID, this.Localization.ValidationData);
			webControl.Controls.Add(child);
			webControl.Controls.Add(this.View.NumberCriteriaDropDownList);
			WebControl webControl2 = this.CreateListItem();
			this.NumberCriteriaPanel.Controls.Add(webControl2);
			webControl2.Controls.Add(this.View.NumberCriteriaNumericMin);
			webControl2.Controls.Add(this.View.NumberCriteriaMinValidator);
			WebControl webControl3 = this.CreateListItem();
			this.NumberCriteriaPanel.Controls.Add(webControl3);
			webControl3.Controls.Add(this.View.NumberCriteriaNumericMax);
			webControl3.Controls.Add(this.View.NumberCriteriaMaxValidator);
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x001066A4 File Offset: 0x001048A4
		private void CreateTextCriteriaControls()
		{
			WebControl webControl = this.CreateListItem();
			this.TextCriteriaPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(this.View.TextCriteriaDropDownList.ClientID, this.Localization.ValidationData);
			webControl.Controls.Add(child);
			webControl.Controls.Add(this.View.TextCriteriaDropDownList);
			WebControl webControl2 = this.CreateListItem();
			this.TextCriteriaPanel.Controls.Add(webControl2);
			webControl2.Controls.Add(this.View.TextCriteriaTextBox);
			webControl2.Controls.Add(this.View.TextCriteriaValidator);
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x00106754 File Offset: 0x00104954
		private void CreateDateCriteriaControls()
		{
			WebControl webControl = this.CreateListItem();
			this.DateCriteriaPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(this.View.DateCriteriaDropDownList.ClientID, this.Localization.ValidationData);
			webControl.Controls.Add(child);
			webControl.Controls.Add(this.View.DateCriteriaDropDownList);
			WebControl webControl2 = this.CreateListItem();
			this.DateCriteriaPanel.Controls.Add(webControl2);
			WebControl child2 = this.CreateLabel(this.View.DateCriteriaDatePickerMin.ClientID, this.Localization.ValidationMin);
			webControl2.Controls.Add(child2);
			webControl2.Controls.Add(this.View.DateCriteriaDatePickerMin);
			webControl2.Controls.Add(this.View.DateCriteriaMinValidator);
			WebControl webControl3 = this.CreateListItem();
			this.DateCriteriaPanel.Controls.Add(webControl3);
			WebControl child3 = this.CreateLabel(this.View.DateCriteriaDatePickerMax.ClientID, this.Localization.ValidationMax);
			webControl3.Controls.Add(child3);
			webControl3.Controls.Add(this.View.DateCriteriaDatePickerMax);
			webControl3.Controls.Add(this.View.DateCriteriaMaxValidator);
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x001068A8 File Offset: 0x00104AA8
		private void CreateCustomCriteriaControls()
		{
			WebControl webControl = this.CreateListItem();
			this.CustomCriteriaPanel.Controls.Add(webControl);
			webControl.Controls.Add(this.View.CustomCriteriaTextBox);
			webControl.Controls.Add(this.View.CustomCriteriaValidator);
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x001068FC File Offset: 0x00104AFC
		private void CreateInvalidDataControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "label"
			};
			this.InvalidDataPanel.Controls.Add(webControl);
			webControl.Controls.Add(new LiteralControl(this.Localization.ValidationOnInvalidData + ":"));
			this.InvalidDataPanel.Controls.Add(this.View.InvalidDataRadioButtonList);
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x00106970 File Offset: 0x00104B70
		private void CreateHintControls()
		{
			WebControl webControl = this.CreateListItem();
			this.HintPanel.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Label);
			webControl.Controls.Add(webControl2);
			webControl2.Controls.Add(this.View.HintCheckBox);
			webControl2.Controls.Add(new LiteralControl(this.Localization.ValidationHintMessage + ":"));
			webControl.Controls.Add(this.View.HintTextBox);
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x001069FC File Offset: 0x00104BFC
		private void CreateCommandButtons()
		{
			this.ButtonsPanel.Controls.Add(this.View.SaveButton);
			this.ButtonsPanel.Controls.Add(this.View.CancelButton);
			this.ButtonsPanel.Controls.Add(this.View.RemoveButton);
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00106A5C File Offset: 0x00104C5C
		private WebControl CreateFormList(string cssClass = "")
		{
			return new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = string.Format("{0} {1}", "rssFormList", cssClass).Trim()
			};
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x00106A90 File Offset: 0x00104C90
		private WebControl CreateSeparator()
		{
			return new WebControl(HtmlTextWriterTag.Hr)
			{
				CssClass = "rssSeparator"
			};
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x00106AB4 File Offset: 0x00104CB4
		private WebControl CreateListItem()
		{
			return new WebControl(HtmlTextWriterTag.Li);
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x00106ACC File Offset: 0x00104CCC
		private WebControl CreateLabel(string forId, string text)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Label);
			webControl.Attributes.Add("for", forId);
			webControl.Controls.Add(new LiteralControl(text + ":"));
			return webControl;
		}

		// Token: 0x040014FA RID: 5370
		private IValidationView _view;
	}
}
