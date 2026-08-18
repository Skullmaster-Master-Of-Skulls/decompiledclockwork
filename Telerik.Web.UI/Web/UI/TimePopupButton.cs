using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001011 RID: 4113
	[ToolboxItem(false)]
	public class TimePopupButton : CalendarPopupButton
	{
		// Token: 0x0600A1CD RID: 41421 RVA: 0x0023F5E4 File Offset: 0x0023D7E4
		public TimePopupButton(RadDateTimePicker owner) : base(owner)
		{
			this.owner = owner;
		}

		// Token: 0x17003335 RID: 13109
		// (get) Token: 0x0600A1CE RID: 41422 RVA: 0x0023F5F4 File Offset: 0x0023D7F4
		// (set) Token: 0x0600A1CF RID: 41423 RVA: 0x0023F5FC File Offset: 0x0023D7FC
		[DefaultValue("rcTimePopup")]
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

		// Token: 0x17003336 RID: 13110
		// (get) Token: 0x0600A1D0 RID: 41424 RVA: 0x0023F608 File Offset: 0x0023D808
		// (set) Token: 0x0600A1D1 RID: 41425 RVA: 0x0023F63D File Offset: 0x0023D83D
		public override string ImageUrl
		{
			get
			{
				string image = this.GetImage("clock.gif");
				object obj = this.ViewState["ImageUrl"];
				if (obj == null)
				{
					return image;
				}
				return (string)obj;
			}
			set
			{
				base.ImageUrl = value;
			}
		}

		// Token: 0x0600A1D2 RID: 41426 RVA: 0x0023F646 File Offset: 0x0023D846
		protected override bool ShouldSerializeImageUrl()
		{
			return !this.ImageUrl.StartsWith("mvwres:");
		}

		// Token: 0x17003337 RID: 13111
		// (get) Token: 0x0600A1D3 RID: 41427 RVA: 0x0023F660 File Offset: 0x0023D860
		// (set) Token: 0x0600A1D4 RID: 41428 RVA: 0x0023F695 File Offset: 0x0023D895
		public override string HoverImageUrl
		{
			get
			{
				string image = this.GetImage("clockHover.gif");
				object obj = this.ViewState["HoverImageUrl"];
				if (obj == null)
				{
					return image;
				}
				return (string)obj;
			}
			set
			{
				base.HoverImageUrl = value;
			}
		}

		// Token: 0x0600A1D5 RID: 41429 RVA: 0x0023F69E File Offset: 0x0023D89E
		protected override bool ShouldSerializeHoverImageUrl()
		{
			return !this.HoverImageUrl.StartsWith("mvwres:");
		}

		// Token: 0x0600A1D6 RID: 41430 RVA: 0x0023F6B8 File Offset: 0x0023D8B8
		protected override string GetImage(string imageName)
		{
			string result = "";
			RadDateTimePicker radDateTimePicker = this.owner;
			RadTimeView radTimeView = radDateTimePicker.TimeView;
			if (radDateTimePicker.SharedTimeView != null)
			{
				radTimeView = radDateTimePicker.SharedTimeView;
			}
			if (radTimeView != null)
			{
				result = radTimeView.GetImage(imageName);
			}
			return result;
		}

		// Token: 0x0600A1D7 RID: 41431 RVA: 0x0023F6F4 File Offset: 0x0023D8F4
		protected override void UpdateHoverImage()
		{
			if (string.IsNullOrEmpty(this.CssClass))
			{
				RadDateTimePicker radDateTimePicker = this.owner;
				if (!radDateTimePicker.TimeView.EmptySkin)
				{
					if (!this.ShouldRenderPopupImages())
					{
						this.CssClass = "rcTimePopup";
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

		// Token: 0x0600A1D8 RID: 41432 RVA: 0x0023F778 File Offset: 0x0023D978
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

		// Token: 0x0600A1D9 RID: 41433 RVA: 0x0023F7E4 File Offset: 0x0023D9E4
		protected override bool ShouldRenderPopupImages()
		{
			return !string.IsNullOrEmpty(this.owner.ImagesPath) || !string.IsNullOrEmpty(this.owner.TimePopupButton.ImageUrl);
		}

		// Token: 0x17003338 RID: 13112
		// (get) Token: 0x0600A1DA RID: 41434 RVA: 0x0023F812 File Offset: 0x0023DA12
		// (set) Token: 0x0600A1DB RID: 41435 RVA: 0x0023F83D File Offset: 0x0023DA3D
		[NotifyParentProperty(true)]
		[Description("Modifies the time button title text.")]
		[Localizable(true)]
		[DefaultValue("Open the time view popup.")]
		public override string ToolTip
		{
			get
			{
				return ((string)this.ViewState["ToolTip"]) ?? this.owner.Localization.TimePopupButtonToolTip;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x04002D07 RID: 11527
		protected new RadDateTimePicker owner;
	}
}
