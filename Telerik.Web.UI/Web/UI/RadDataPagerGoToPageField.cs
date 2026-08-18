using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001955 RID: 6485
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadDataPagerGoToPageField : RadDataPagerField
	{
		// Token: 0x0600FB0D RID: 64269 RVA: 0x003889B8 File Offset: 0x00386BB8
		public override void InitializeFieldControls(RadDataPagerFieldItem inItem)
		{
			LiteralControl child = new LiteralControl
			{
				Text = string.Format("<span class='{1}'>{0}</span>", this.CurrentPageText, "rdpPagerLabel")
			};
			inItem.Controls.Add(child);
			this.EnsureNumericTextBoxCreated();
			this.PrepareSkinnableControlProperties(this.gotoTextBox);
			inItem.Controls.Add(this.gotoTextBox);
			child = new LiteralControl
			{
				Text = string.Format("<span class='{2}'>{0} {1}</span>", this.TotalPageText, base.Owner.PageCount, "rdpPagerLabel")
			};
			inItem.Controls.Add(child);
			this.EnsureButtonCreated(this.gotoTextBox);
			inItem.Controls.Add(this.submitButton);
		}

		// Token: 0x0600FB0E RID: 64270 RVA: 0x00388A8C File Offset: 0x00386C8C
		private void EnsureNumericTextBoxCreated()
		{
			this.gotoTextBox = new RadNumericTextBox();
			this.gotoTextBox.RenderMode = base.Owner.RenderMode;
			this.gotoTextBox.ID = "GoToPageTextBox";
			this.gotoTextBox.NumberFormat.AllowRounding = false;
			this.gotoTextBox.NumberFormat.DecimalDigits = 0;
			this.gotoTextBox.MinValue = 1.0;
			this.gotoTextBox.MaxValue = (double)Math.Max(base.Owner.PageCount, 1);
			int num = base.Owner.CurrentPageIndex + 1;
			this.gotoTextBox.Value = new double?(Math.Min((double)num, this.gotoTextBox.MaxValue));
			if (base.Owner.EnableAriaSupport && (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile))
			{
				this.gotoTextBox.Attributes.Add("aria-label", "Page");
			}
			if (base.Owner.ResolvedRenderMode == RenderMode.Classic || this.TextBoxWidth != 30)
			{
				this.gotoTextBox.Width = Unit.Pixel(this.TextBoxWidth);
			}
			else
			{
				this.gotoTextBox.Width = Unit.Parse(2.2857000827789307 + (double)(base.Owner.PageCount.ToString().Length - 1) * 0.5 + "em");
			}
			this.gotoTextBox.AutoPostBack = !this.EnableSubmitButton;
			this.gotoTextBox.EnableAriaSupport = base.Owner.EnableAriaSupport;
			if (!this.EnableSubmitButton)
			{
				this.gotoTextBox.TextChanged += this.GoToTextBoxTextChanged;
			}
			this.gotoTextBox.PreRender += delegate(object sender, EventArgs e)
			{
				((RadNumericTextBox)sender).Skin = base.Owner.RuntimeSkin;
			};
		}

		// Token: 0x0600FB0F RID: 64271 RVA: 0x00388C70 File Offset: 0x00386E70
		private void EnsureButtonCreated(RadNumericTextBox gotoTextBox)
		{
			if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.submitButton = new ElasticButton(string.Empty, "t-text rdpButtonText");
			}
			else
			{
				this.submitButton = new Button();
			}
			this.submitButton.ID = "GoToPageButton";
			this.submitButton.Text = this.SubmitButtonText;
			this.submitButton.CausesValidation = false;
			this.submitButton.CssClass = "rdpPagerButton";
			this.submitButton.Visible = this.EnableSubmitButton;
			if (this.EnableSubmitButton)
			{
				this.submitButton.Click += this.SubmitButtonClick;
			}
		}

		// Token: 0x0600FB10 RID: 64272 RVA: 0x00388D1C File Offset: 0x00386F1C
		private void SubmitButtonClick(object sender, EventArgs e)
		{
			RadDataPager owner = base.Owner;
			RadDataPagerFieldItem dataPagerItem = (RadDataPagerFieldItem)this.submitButton.NamingContainer;
			object eventSource = this.submitButton;
			string commandName = "Page";
			double? value = this.gotoTextBox.Value;
			RadDataPagerCommandEventArgs arguments = new RadDataPagerCommandEventArgs(owner, dataPagerItem, eventSource, new CommandEventArgs(commandName, ((value != null) ? new double?(value.GetValueOrDefault() - 1.0) : null).ToString()));
			if (base.Owner.AllowSEOPaging)
			{
				base.Owner.Page.Response.Redirect(base.Owner.GeneratePagingStateAttributeLink(Convert.ToInt32(this.gotoTextBox.Text), base.Owner.PageSize), true);
				return;
			}
			this.CallOnCommand(arguments);
		}

		// Token: 0x0600FB11 RID: 64273 RVA: 0x00388DEC File Offset: 0x00386FEC
		private void GoToTextBoxTextChanged(object sender, EventArgs e)
		{
			RadDataPager owner = base.Owner;
			RadDataPagerFieldItem dataPagerItem = (RadDataPagerFieldItem)this.gotoTextBox.NamingContainer;
			object eventSource = this.gotoTextBox;
			string commandName = "Page";
			double? value = this.gotoTextBox.Value;
			RadDataPagerCommandEventArgs arguments = new RadDataPagerCommandEventArgs(owner, dataPagerItem, eventSource, new CommandEventArgs(commandName, ((value != null) ? new double?(value.GetValueOrDefault() - 1.0) : null).ToString()));
			this.CallOnCommand(arguments);
		}

		// Token: 0x0600FB12 RID: 64274 RVA: 0x00388E71 File Offset: 0x00387071
		protected virtual void CallOnCommand(RadDataPagerCommandEventArgs arguments)
		{
			base.Owner.FireCommand(arguments);
		}

		// Token: 0x17004BE0 RID: 19424
		// (get) Token: 0x0600FB13 RID: 64275 RVA: 0x00388E80 File Offset: 0x00387080
		// (set) Token: 0x0600FB14 RID: 64276 RVA: 0x00388F20 File Offset: 0x00387120
		[NotifyParentProperty(true)]
		[DefaultValue(30)]
		public int TextBoxWidth
		{
			get
			{
				object obj = base.ViewState["TextBoxWidth"];
				if (obj != null)
				{
					return (int)obj;
				}
				if (base.Owner.RuntimeSkin == "MetroTouch" || base.Owner.RuntimeSkin == "Glow" || base.Owner.RuntimeSkin == "Silk" || base.Owner.RuntimeSkin == "BlackMetroTouch" || base.Owner.RuntimeSkin == "Bootstrap")
				{
					return 40;
				}
				return 30;
			}
			set
			{
				base.ViewState["TextBoxWidth"] = value;
			}
		}

		// Token: 0x17004BE1 RID: 19425
		// (get) Token: 0x0600FB15 RID: 64277 RVA: 0x00388F38 File Offset: 0x00387138
		// (set) Token: 0x0600FB16 RID: 64278 RVA: 0x00388F6E File Offset: 0x0038716E
		[Localizable(true)]
		[DefaultValue("Page")]
		[NotifyParentProperty(true)]
		public string CurrentPageText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["CurrentPageText"], "Page") ?? base.Owner.Localization.CurrentPageText;
			}
			set
			{
				base.ViewState["CurrentPageText"] = value;
			}
		}

		// Token: 0x17004BE2 RID: 19426
		// (get) Token: 0x0600FB17 RID: 64279 RVA: 0x00388F81 File Offset: 0x00387181
		// (set) Token: 0x0600FB18 RID: 64280 RVA: 0x00388FB7 File Offset: 0x003871B7
		[Localizable(true)]
		[DefaultValue("of")]
		[NotifyParentProperty(true)]
		public string TotalPageText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["TotalPageText"], "of") ?? base.Owner.Localization.TotalPageText;
			}
			set
			{
				base.ViewState["TotalPageText"] = value;
			}
		}

		// Token: 0x17004BE3 RID: 19427
		// (get) Token: 0x0600FB19 RID: 64281 RVA: 0x00388FCC File Offset: 0x003871CC
		// (set) Token: 0x0600FB1A RID: 64282 RVA: 0x00388FF5 File Offset: 0x003871F5
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool EnableSubmitButton
		{
			get
			{
				object obj = base.ViewState["RenderSubmutButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["RenderSubmutButton"] = value;
			}
		}

		// Token: 0x17004BE4 RID: 19428
		// (get) Token: 0x0600FB1B RID: 64283 RVA: 0x0038900D File Offset: 0x0038720D
		// (set) Token: 0x0600FB1C RID: 64284 RVA: 0x00389043 File Offset: 0x00387243
		[DefaultValue("Go")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string SubmitButtonText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["SubmitButtonText"], "Go") ?? base.Owner.Localization.SubmitButtonText;
			}
			set
			{
				base.ViewState["SubmitButtonText"] = value;
			}
		}

		// Token: 0x04004762 RID: 18274
		protected const string GoToPageLabelClassName = "rdpPagerLabel";

		// Token: 0x04004763 RID: 18275
		protected const string SubmitButtonClassName = "rdpPagerButton";

		// Token: 0x04004764 RID: 18276
		private RadNumericTextBox gotoTextBox;

		// Token: 0x04004765 RID: 18277
		private Button submitButton;
	}
}
