using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000A35 RID: 2613
	[ToolboxItem(false)]
	public class CalendarPopupButton : WebControl, ICustomTypeDescriptor
	{
		// Token: 0x060062ED RID: 25325 RVA: 0x001744E1 File Offset: 0x001726E1
		public CalendarPopupButton(RadDatePicker owner)
		{
			this.owner = owner;
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x001744F0 File Offset: 0x001726F0
		protected CalendarPopupButton()
		{
		}

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x060062EF RID: 25327 RVA: 0x001744F8 File Offset: 0x001726F8
		// (set) Token: 0x060062F0 RID: 25328 RVA: 0x00174500 File Offset: 0x00172700
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x00174509 File Offset: 0x00172709
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.IsEnabled)
			{
				this.AddAriaAttributesToRender(writer);
				writer.AddAttribute("title", this.ToolTip);
				writer.AddAttribute("href", "#");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x00174542 File Offset: 0x00172742
		protected virtual void AddAriaAttributesToRender(HtmlTextWriter writer)
		{
			if (this.owner != null && this.owner.EnableAriaSupport)
			{
				writer.AddAttribute("aria-label", this.ToolTip);
			}
		}

		// Token: 0x17002078 RID: 8312
		// (get) Token: 0x060062F3 RID: 25331 RVA: 0x0017456C File Offset: 0x0017276C
		// (set) Token: 0x060062F4 RID: 25332 RVA: 0x001745EB File Offset: 0x001727EB
		[NotifyParentProperty(true)]
		[DefaultValue("Open the calendar popup.")]
		[Description("Modifies the popup button title text.")]
		[Localizable(true)]
		public override string ToolTip
		{
			get
			{
				string text = (string)this.ViewState["ToolTip"];
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				if (this.owner != null)
				{
					RadDateTimePicker radDateTimePicker = this.owner as RadDateTimePicker;
					if (radDateTimePicker != null)
					{
						return radDateTimePicker.Localization.DatePopupButtonToolTip;
					}
					return this.owner.Localization.PopupButtonToolTip;
				}
				else
				{
					if (this.monthYearOwner != null)
					{
						return this.monthYearOwner.Localization.PopupButtonToolTip;
					}
					return "Open the calendar popup.";
				}
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17002079 RID: 8313
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x001745FE File Offset: 0x001727FE
		protected System.Web.UI.WebControls.Image PopupImage
		{
			get
			{
				if (this._popupImage == null)
				{
					this._popupImage = new System.Web.UI.WebControls.Image();
				}
				return this._popupImage;
			}
		}

		// Token: 0x1700207A RID: 8314
		// (get) Token: 0x060062F6 RID: 25334 RVA: 0x00174619 File Offset: 0x00172819
		// (set) Token: 0x060062F7 RID: 25335 RVA: 0x00174621 File Offset: 0x00172821
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x1700207B RID: 8315
		// (get) Token: 0x060062F8 RID: 25336 RVA: 0x0017462A File Offset: 0x0017282A
		// (set) Token: 0x060062F9 RID: 25337 RVA: 0x00174632 File Offset: 0x00172832
		[DefaultValue("rcCalPopup")]
		[NotifyParentProperty(true)]
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

		// Token: 0x1700207C RID: 8316
		// (get) Token: 0x060062FA RID: 25338 RVA: 0x0017463C File Offset: 0x0017283C
		// (set) Token: 0x060062FB RID: 25339 RVA: 0x00174671 File Offset: 0x00172871
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[Description("Modifies the popup button image URL.")]
		public virtual string ImageUrl
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
				this.PopupImage.ImageUrl = value;
			}
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x00174690 File Offset: 0x00172890
		protected virtual bool ShouldSerializeImageUrl()
		{
			return !this.ImageUrl.StartsWith("mvwres:");
		}

		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x060062FD RID: 25341 RVA: 0x001746A7 File Offset: 0x001728A7
		internal virtual string ResolvedImageUrl
		{
			get
			{
				return base.ResolveUrl(this.ImageUrl);
			}
		}

		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x060062FE RID: 25342 RVA: 0x001746B8 File Offset: 0x001728B8
		// (set) Token: 0x060062FF RID: 25343 RVA: 0x001746ED File Offset: 0x001728ED
		[Description("Modifies the popup button hover image URL.")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string HoverImageUrl
		{
			get
			{
				string image = this.GetImage("datePickerPopupHover.gif");
				object obj = this.ViewState["HoverImageUrl"];
				if (obj == null)
				{
					return image;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["HoverImageUrl"] = value;
			}
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x00174700 File Offset: 0x00172900
		protected virtual bool ShouldSerializeHoverImageUrl()
		{
			return !this.HoverImageUrl.StartsWith("mvwres:");
		}

		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x06006301 RID: 25345 RVA: 0x00174717 File Offset: 0x00172917
		// (set) Token: 0x06006302 RID: 25346 RVA: 0x0017471F File Offset: 0x0017291F
		[NotifyParentProperty(true)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x06006303 RID: 25347 RVA: 0x00174728 File Offset: 0x00172928
		// (set) Token: 0x06006304 RID: 25348 RVA: 0x00174730 File Offset: 0x00172930
		[NotifyParentProperty(true)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x06006305 RID: 25349 RVA: 0x00174739 File Offset: 0x00172939
		// (set) Token: 0x06006306 RID: 25350 RVA: 0x00174741 File Offset: 0x00172941
		[NotifyParentProperty(true)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x06006307 RID: 25351 RVA: 0x0017474A File Offset: 0x0017294A
		// (set) Token: 0x06006308 RID: 25352 RVA: 0x00174752 File Offset: 0x00172952
		[NotifyParentProperty(true)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x06006309 RID: 25353 RVA: 0x0017475B File Offset: 0x0017295B
		// (set) Token: 0x0600630A RID: 25354 RVA: 0x00174763 File Offset: 0x00172963
		[NotifyParentProperty(true)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x0600630B RID: 25355 RVA: 0x0017476C File Offset: 0x0017296C
		// (set) Token: 0x0600630C RID: 25356 RVA: 0x00174774 File Offset: 0x00172974
		[NotifyParentProperty(true)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x0600630D RID: 25357 RVA: 0x0017477D File Offset: 0x0017297D
		// (set) Token: 0x0600630E RID: 25358 RVA: 0x00174785 File Offset: 0x00172985
		[NotifyParentProperty(true)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				base.EnableViewState = value;
			}
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x0600630F RID: 25359 RVA: 0x0017478E File Offset: 0x0017298E
		[NotifyParentProperty(true)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x06006310 RID: 25360 RVA: 0x00174796 File Offset: 0x00172996
		// (set) Token: 0x06006311 RID: 25361 RVA: 0x0017479E File Offset: 0x0017299E
		[NotifyParentProperty(true)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x001747A7 File Offset: 0x001729A7
		// (set) Token: 0x06006313 RID: 25363 RVA: 0x001747AF File Offset: 0x001729AF
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x001747B8 File Offset: 0x001729B8
		// (set) Token: 0x06006315 RID: 25365 RVA: 0x001747C0 File Offset: 0x001729C0
		[NotifyParentProperty(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x06006316 RID: 25366 RVA: 0x001747C9 File Offset: 0x001729C9
		// (set) Token: 0x06006317 RID: 25367 RVA: 0x001747D1 File Offset: 0x001729D1
		[NotifyParentProperty(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x06006318 RID: 25368 RVA: 0x001747DA File Offset: 0x001729DA
		// (set) Token: 0x06006319 RID: 25369 RVA: 0x001747E2 File Offset: 0x001729E2
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x0600631A RID: 25370 RVA: 0x001747EB File Offset: 0x001729EB
		// (set) Token: 0x0600631B RID: 25371 RVA: 0x001747F3 File Offset: 0x001729F3
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x0600631C RID: 25372 RVA: 0x001747FC File Offset: 0x001729FC
		internal virtual string ResolvedHoverImageUrl
		{
			get
			{
				return base.ResolveUrl(this.HoverImageUrl);
			}
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x0017480C File Offset: 0x00172A0C
		protected virtual string GetImage(string imageName)
		{
			string result = "";
			RadDatePicker radDatePicker = this.owner;
			RadCalendar radCalendar = radDatePicker.Calendar;
			if (radDatePicker.SharedCalendar != null)
			{
				radCalendar = radDatePicker.SharedCalendar;
			}
			if (radCalendar != null)
			{
				result = radCalendar.GetImage(imageName);
			}
			return result;
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00174848 File Offset: 0x00172A48
		protected virtual void UpdateHoverImage()
		{
			if (string.IsNullOrEmpty(this.CssClass))
			{
				RadDatePicker radDatePicker = this.owner;
				RadCalendar radCalendar = radDatePicker.Calendar;
				if (radDatePicker.SharedCalendar != null)
				{
					radCalendar = radDatePicker.SharedCalendar;
				}
				if (!radCalendar.EmptySkin)
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
			this.PopupImage.ImageUrl = this.ImageUrl;
			this.PopupImage.AlternateText = this.ToolTip;
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x001748DC File Offset: 0x00172ADC
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ShouldRenderPopupImages())
			{
				this.PopupImage.ID = base.GetType().Name;
				this.Controls.Add(this.PopupImage);
				return;
			}
			if (this.owner.ResolvedRenderMode == RenderMode.Classic)
			{
				this.Controls.Add(new LiteralControl(this.ToolTip));
			}
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x00174948 File Offset: 0x00172B48
		protected override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			this.UpdateHoverImage();
			base.Render(writer);
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x0017495D File Offset: 0x00172B5D
		protected virtual bool ShouldRenderPopupImages()
		{
			return !string.IsNullOrEmpty(this.owner.ImagesPath) || !string.IsNullOrEmpty(this.owner.DatePopupButton.ImageUrl);
		}

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x06006322 RID: 25378 RVA: 0x0017498B File Offset: 0x00172B8B
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x0017498E File Offset: 0x00172B8E
		public System.ComponentModel.AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x00174997 File Offset: 0x00172B97
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x001749A0 File Offset: 0x00172BA0
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x001749A9 File Offset: 0x00172BA9
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x001749B2 File Offset: 0x00172BB2
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x001749BB File Offset: 0x00172BBB
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x001749C4 File Offset: 0x00172BC4
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x001749CE File Offset: 0x00172BCE
		public EventDescriptorCollection GetEvents()
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001749DB File Offset: 0x00172BDB
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x001749E8 File Offset: 0x00172BE8
		public PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x001749F1 File Offset: 0x00172BF1
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(this, attributes, true);
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x001749FB File Offset: 0x00172BFB
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x0400182F RID: 6191
		private System.Web.UI.WebControls.Image _popupImage;

		// Token: 0x04001830 RID: 6192
		protected RadDatePicker owner;

		// Token: 0x04001831 RID: 6193
		protected RadMonthYearPicker monthYearOwner;
	}
}
