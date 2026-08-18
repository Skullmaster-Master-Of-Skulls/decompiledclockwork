using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200007A RID: 122
	[ToolboxItem(false)]
	public class RadButtonConfirmSettings : StateManager
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000C8C1 File Offset: 0x0000AAC1
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		[Description("Gets or sets the text shown in the confirmation dialog the user receives on click. Setting text to it enables the dialog.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string ConfirmText
		{
			get
			{
				return base.GetViewStateValue<string>("ConfirmText", string.Empty);
			}
			set
			{
				base.ViewState["ConfirmText"] = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		[Description("Gets or sets the RadConfirm title.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual string Title
		{
			get
			{
				return base.GetViewStateValue<string>("Title", string.Empty);
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000C90B File Offset: 0x0000AB0B
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000C919 File Offset: 0x0000AB19
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Get or set whether to use a RadConfirm instead of the browser confirm. Requiers a RadWindowManager on the page.")]
		public virtual bool UseRadConfirm
		{
			get
			{
				return base.GetViewStateValue<bool>("UseRadConfirm", true);
			}
			set
			{
				base.ViewState["UseRadConfirm"] = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000C931 File Offset: 0x0000AB31
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000C93F File Offset: 0x0000AB3F
		[DefaultValue(typeof(int), "0")]
		[Category("Layout")]
		[Description("Get or set the width of the RadConfirm dialog in pixels.")]
		public virtual int Width
		{
			get
			{
				return base.GetViewStateValue<int>("Width", 0);
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("Confirm dialog width must be a positive number.");
				}
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000C966 File Offset: 0x0000AB66
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0000C974 File Offset: 0x0000AB74
		[Description("Get or set the height of the RadConfirm dialog in pixels.")]
		[DefaultValue(typeof(int), "0")]
		[Category("Layout")]
		public virtual int Height
		{
			get
			{
				return base.GetViewStateValue<int>("Height", 0);
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("Confirm dialog height must be a positive number.");
				}
				base.ViewState["Height"] = value;
			}
		}
	}
}
