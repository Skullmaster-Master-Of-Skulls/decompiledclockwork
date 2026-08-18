using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000A36 RID: 2614
	[ToolboxItem(false)]
	public class MonthYearPopupButton : CalendarPopupButton
	{
		// Token: 0x0600632F RID: 25391 RVA: 0x001749FE File Offset: 0x00172BFE
		public MonthYearPopupButton(RadMonthYearPicker owner)
		{
			this.owner = owner;
			this.monthYearOwner = owner;
		}

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x06006330 RID: 25392 RVA: 0x00174A14 File Offset: 0x00172C14
		// (set) Token: 0x06006331 RID: 25393 RVA: 0x00174A1C File Offset: 0x00172C1C
		[NotifyParentProperty(true)]
		[DefaultValue("rcCalPopup")]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x00174A25 File Offset: 0x00172C25
		protected override bool ShouldSerializeHoverImageUrl()
		{
			return !this.HoverImageUrl.StartsWith("mvwres:", StringComparison.InvariantCulture);
		}

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x06006333 RID: 25395 RVA: 0x00174A40 File Offset: 0x00172C40
		// (set) Token: 0x06006334 RID: 25396 RVA: 0x00174A75 File Offset: 0x00172C75
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[Description("Modifies the popup button image URL.")]
		public override string ImageUrl
		{
			get
			{
				string image = this.GetImage("datePickerPopup.gif");
				object obj = this.ViewState["ImageUrl"];
				if (obj == null)
				{
					return image;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
				base.PopupImage.ImageUrl = value;
			}
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x00174A94 File Offset: 0x00172C94
		protected override string GetImage(string imageName)
		{
			string result = "";
			RadMonthYearPicker radMonthYearPicker = this.owner;
			if (radMonthYearPicker != null)
			{
				result = radMonthYearPicker.GetImage(imageName);
			}
			return result;
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00174ABC File Offset: 0x00172CBC
		protected override void UpdateHoverImage()
		{
			if (string.IsNullOrEmpty(this.CssClass))
			{
				RadMonthYearPicker radMonthYearPicker = this.owner;
				if (!radMonthYearPicker.EmptySkin)
				{
					if (!this.ShouldRenderPopupImages())
					{
						this.CssClass = "rcCalPopup";
					}
					if (!base.IsEnabled)
					{
						this.CssClass += " rcDisabled";
					}
				}
			}
			base.PopupImage.ImageUrl = this.ImageUrl;
			base.PopupImage.AlternateText = this.ToolTip;
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x00174B38 File Offset: 0x00172D38
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ShouldRenderPopupImages())
			{
				base.PopupImage.ID = base.GetType().Name;
				this.Controls.Add(base.PopupImage);
				return;
			}
			if (this.owner.ResolvedRenderMode == RenderMode.Classic)
			{
				this.Controls.Add(new LiteralControl(this.ToolTip));
			}
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x00174BA4 File Offset: 0x00172DA4
		protected override bool ShouldRenderPopupImages()
		{
			return !string.IsNullOrEmpty(this.owner.ImagesPath) || !string.IsNullOrEmpty(this.owner.DatePopupButton.ImageUrl);
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x00174BD2 File Offset: 0x00172DD2
		protected override void AddAriaAttributesToRender(HtmlTextWriter writer)
		{
			if ((this.owner != null && this.owner.EnableAriaSupport) || (this.monthYearOwner != null && this.monthYearOwner.EnableAriaSupport))
			{
				writer.AddAttribute("aria-label", this.ToolTip);
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x0600633A RID: 25402 RVA: 0x00174C0F File Offset: 0x00172E0F
		// (set) Token: 0x0600633B RID: 25403 RVA: 0x00174C3A File Offset: 0x00172E3A
		[Description("Modifies the MonthYearViewPopup button title text.")]
		[Localizable(true)]
		[DefaultValue("Open the monthyear view popup.")]
		[NotifyParentProperty(true)]
		public override string ToolTip
		{
			get
			{
				return ((string)this.ViewState["ToolTip"]) ?? this.owner.Localization.PopupButtonToolTip;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x04001832 RID: 6194
		public new RadMonthYearPicker owner;
	}
}
