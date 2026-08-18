using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonBase
{
	// Token: 0x020000DE RID: 222
	[ToolboxItem(false)]
	public class ButtonImage : StateManager
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x000212DB File Offset: 0x0001F4DB
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x000212FB File Offset: 0x0001F4FB
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the location of an image to display in the RadButton control.")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public virtual string Url
		{
			get
			{
				return (string)(base.ViewState["Url"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Url"] = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0002130E File Offset: 0x0001F50E
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0002132E File Offset: 0x0001F52E
		[Category("Appearance")]
		[UrlProperty]
		[Description("Gets or sets the location of an image to display when the RadButton control is disabled.")]
		[DefaultValue("")]
		[Bindable(true)]
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		public virtual string DisabledUrl
		{
			get
			{
				return (string)(base.ViewState["DisabledUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledUrl"] = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00021341 File Offset: 0x0001F541
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x00021361 File Offset: 0x0001F561
		[Bindable(true)]
		[UrlProperty]
		[Description("Gets or sets the location of an image to display in the RadButton control, when the mouse pointer is over the control.")]
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		public virtual string HoveredUrl
		{
			get
			{
				return (string)(base.ViewState["HoveredUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredUrl"] = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00021374 File Offset: 0x0001F574
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x00021394 File Offset: 0x0001F594
		[Category("Appearance")]
		[DefaultValue("")]
		[Bindable(true)]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the location of an image to display in the RadButton control, when the control is pressed.")]
		public virtual string PressedUrl
		{
			get
			{
				return (string)(base.ViewState["PressedUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PressedUrl"] = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000213A7 File Offset: 0x0001F5A7
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x000213C8 File Offset: 0x0001F5C8
		[DefaultValue(ImageSizing.Original)]
		[Description("Gets or sets the sizing of the image.")]
		[Category("Behavior")]
		public virtual ImageSizing Sizing
		{
			get
			{
				return (ImageSizing)(base.ViewState["Sizing"] ?? ImageSizing.Original);
			}
			set
			{
				base.ViewState["Sizing"] = value;
			}
		}
	}
}
