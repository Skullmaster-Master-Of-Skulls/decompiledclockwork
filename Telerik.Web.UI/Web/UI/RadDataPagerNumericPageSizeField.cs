using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001956 RID: 6486
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadDataPagerNumericPageSizeField : RadDataPagerField
	{
		// Token: 0x17004BE5 RID: 19429
		// (get) Token: 0x0600FB1F RID: 64287 RVA: 0x00389060 File Offset: 0x00387260
		// (set) Token: 0x0600FB20 RID: 64288 RVA: 0x0038908A File Offset: 0x0038728A
		[NotifyParentProperty(true)]
		[DefaultValue(30)]
		public int TextBoxWidth
		{
			get
			{
				object obj = base.ViewState["TextBoxWidth"];
				if (obj == null)
				{
					return 30;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["TextBoxWidth"] = value;
			}
		}

		// Token: 0x17004BE6 RID: 19430
		// (get) Token: 0x0600FB21 RID: 64289 RVA: 0x003890A2 File Offset: 0x003872A2
		// (set) Token: 0x0600FB22 RID: 64290 RVA: 0x003890D8 File Offset: 0x003872D8
		[DefaultValue("Change")]
		[NotifyParentProperty(true)]
		public string SubmitButtonText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["SubmitButtonText"], "Change") ?? base.Owner.Localization.PageSizeSubmitButtonText;
			}
			set
			{
				base.ViewState["SubmitButtonText"] = value;
			}
		}

		// Token: 0x17004BE7 RID: 19431
		// (get) Token: 0x0600FB23 RID: 64291 RVA: 0x003890EB File Offset: 0x003872EB
		// (set) Token: 0x0600FB24 RID: 64292 RVA: 0x00389121 File Offset: 0x00387321
		[DefaultValue("Page size")]
		[NotifyParentProperty(true)]
		public string LabelText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["LabelText"], "Page size") ?? base.Owner.Localization.LabelText;
			}
			set
			{
				base.ViewState["LabelText"] = value;
			}
		}

		// Token: 0x0600FB25 RID: 64293 RVA: 0x0038914C File Offset: 0x0038734C
		public override void InitializeFieldControls(RadDataPagerFieldItem inItem)
		{
			LiteralControl child = new LiteralControl
			{
				Text = string.Format("<span class='{1}'>{0}</span>", this.LabelText, "rdpPagerLabel")
			};
			inItem.Controls.Add(child);
			this.pageSizeTextBox = new RadNumericTextBox();
			this.pageSizeTextBox.RenderMode = base.Owner.RenderMode;
			this.pageSizeTextBox.ID = "PageSizeTextBox";
			this.pageSizeTextBox.NumberFormat.AllowRounding = false;
			this.pageSizeTextBox.NumberFormat.DecimalDigits = 0;
			this.pageSizeTextBox.MinValue = 1.0;
			this.pageSizeTextBox.MaxValue = (double)Math.Max(base.Owner.TotalRowCount, base.Owner.PageSize);
			this.pageSizeTextBox.Value = new double?((double)base.Owner.PageSize);
			this.pageSizeTextBox.EnableAriaSupport = base.Owner.EnableAriaSupport;
			if (base.Owner.ResolvedRenderMode == RenderMode.Classic || this.TextBoxWidth != 30)
			{
				this.pageSizeTextBox.Width = Unit.Pixel(this.TextBoxWidth);
			}
			else
			{
				this.pageSizeTextBox.Width = Unit.Parse(2.2857000827789307 + (double)(base.Owner.TotalRowCount.ToString().Length - 1) * 0.5 + "em");
				if (this.pageSizeTextBox.EnableAriaSupport)
				{
					this.pageSizeTextBox.Attributes.Add("aria-label", "Page size");
				}
			}
			this.pageSizeTextBox.PreRender += delegate(object sender, EventArgs e)
			{
				((RadNumericTextBox)sender).Skin = base.Owner.RuntimeSkin;
			};
			this.PrepareSkinnableControlProperties(this.pageSizeTextBox);
			inItem.Controls.Add(this.pageSizeTextBox);
			this.submitButton = new Button();
			this.submitButton.ID = "PageSizeButton";
			this.submitButton.Text = this.SubmitButtonText;
			this.submitButton.CausesValidation = false;
			this.submitButton.CssClass = "rdpPagerButton";
			this.submitButton.Click += this.ChangePageSizeClick;
			inItem.Controls.Add(this.submitButton);
		}

		// Token: 0x0600FB26 RID: 64294 RVA: 0x00389394 File Offset: 0x00387594
		protected virtual void ChangePageSizeClick(object sender, EventArgs e)
		{
			RadDataPagerCommandEventArgs commandArgs = new RadDataPagerCommandEventArgs(base.Owner, (RadDataPagerFieldItem)this.pageSizeTextBox.NamingContainer, this.pageSizeTextBox, new CommandEventArgs("PageSizeChange", this.pageSizeTextBox.Value.ToString()));
			base.Owner.FireCommand(commandArgs);
		}

		// Token: 0x04004766 RID: 18278
		protected const string PageSizeLabelClassName = "rdpPagerLabel";

		// Token: 0x04004767 RID: 18279
		protected const string SubmitButtonClassName = "rdpPagerButton";

		// Token: 0x04004768 RID: 18280
		private RadNumericTextBox pageSizeTextBox;

		// Token: 0x04004769 RID: 18281
		private Button submitButton;
	}
}
