using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000A28 RID: 2600
	[ToolboxItem(false)]
	public class RadButtonImage : StateManager
	{
		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x06006291 RID: 25233 RVA: 0x001736BC File Offset: 0x001718BC
		// (set) Token: 0x06006292 RID: 25234 RVA: 0x001736DD File Offset: 0x001718DD
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets a bool value indicating how the Image is used - i.e. as a background image or as an Image Button.")]
		public virtual bool IsBackgroundImage
		{
			get
			{
				return (bool)(base.ViewState["IsBackgroundImage"] ?? false);
			}
			set
			{
				base.ViewState["IsBackgroundImage"] = value;
			}
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x06006293 RID: 25235 RVA: 0x001736F5 File Offset: 0x001718F5
		// (set) Token: 0x06006294 RID: 25236 RVA: 0x00173715 File Offset: 0x00171915
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the location of an image to display in the RadButton control.")]
		[Bindable(true)]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		public virtual string ImageUrl
		{
			get
			{
				return (string)(base.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
				this.EnableImageButton = !string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x06006295 RID: 25237 RVA: 0x00173737 File Offset: 0x00171937
		// (set) Token: 0x06006296 RID: 25238 RVA: 0x00173757 File Offset: 0x00171957
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the location of an image to display when the RadButton control is disabled.")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public virtual string DisabledImageUrl
		{
			get
			{
				return (string)(base.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x0017376A File Offset: 0x0017196A
		// (set) Token: 0x06006298 RID: 25240 RVA: 0x0017378A File Offset: 0x0017198A
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the location of an image to display in the RadButton control, when the mouse pointer is over the control.")]
		[Bindable(true)]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		public virtual string HoveredImageUrl
		{
			get
			{
				return (string)(base.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x06006299 RID: 25241 RVA: 0x0017379D File Offset: 0x0017199D
		// (set) Token: 0x0600629A RID: 25242 RVA: 0x001737BD File Offset: 0x001719BD
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Bindable(true)]
		[UrlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the location of an image to display in the RadButton control, when the control is pressed.")]
		[Category("Appearance")]
		public virtual string PressedImageUrl
		{
			get
			{
				return (string)(base.ViewState["PressedImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PressedImageUrl"] = value;
			}
		}

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x0600629B RID: 25243 RVA: 0x001737D0 File Offset: 0x001719D0
		// (set) Token: 0x0600629C RID: 25244 RVA: 0x001737F1 File Offset: 0x001719F1
		[DefaultValue(false)]
		[Category("Appearance")]
		[Description("Gets or sets a bool value indicating whether the RadButton is rendered as Image Button.")]
		public virtual bool EnableImageButton
		{
			get
			{
				return (bool)(base.ViewState["EnableImageButton"] ?? false);
			}
			set
			{
				base.ViewState["EnableImageButton"] = value;
			}
		}
	}
}
