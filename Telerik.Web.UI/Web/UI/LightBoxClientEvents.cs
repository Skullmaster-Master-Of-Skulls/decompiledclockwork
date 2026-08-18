using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000564 RID: 1380
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class LightBoxClientEvents : StateManager
	{
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000A3378 File Offset: 0x000A1578
		// (set) Token: 0x060031BB RID: 12731 RVA: 0x000A3398 File Offset: 0x000A1598
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("This client event will be fired when the RadLightBox client component is initializing.")]
		public virtual string OnLoad
		{
			get
			{
				return (base.ViewState["OnLoad"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000A33AB File Offset: 0x000A15AB
		// (set) Token: 0x060031BD RID: 12733 RVA: 0x000A33CB File Offset: 0x000A15CB
		[NotifyParentProperty(true)]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("This client event will be fired when closing the RadLightBox popup.")]
		public virtual string OnClosing
		{
			get
			{
				return (base.ViewState["OnClosing"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnClosing"] = value;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000A33DE File Offset: 0x000A15DE
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x000A33FE File Offset: 0x000A15FE
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when the RadLightBox popup is closed.")]
		public virtual string OnClosed
		{
			get
			{
				return (base.ViewState["OnClosed"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnClosed"] = value;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000A3411 File Offset: 0x000A1611
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000A3431 File Offset: 0x000A1631
		[Category("Client-side events")]
		[Description("This client event will be fired when the RadLightBox popup is opening.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnShowing
		{
			get
			{
				return (base.ViewState["OnShowing"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnShowing"] = value;
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000A3444 File Offset: 0x000A1644
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000A3464 File Offset: 0x000A1664
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("This client event will be fired when the RadLightBox popup is opened.")]
		[Category("Client-side events")]
		public virtual string OnShowed
		{
			get
			{
				return (base.ViewState["OnShowed"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnShowed"] = value;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000A3477 File Offset: 0x000A1677
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000A3497 File Offset: 0x000A1697
		[NotifyParentProperty(true)]
		[Description("This client event fires when the user navigates out of the current page.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnNavigating
		{
			get
			{
				return (base.ViewState["OnNavigating"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnNavigating"] = value;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000A34AA File Offset: 0x000A16AA
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000A34CA File Offset: 0x000A16CA
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("This client event fires when the client object is about to be destroyed.")]
		public virtual string OnDestroying
		{
			get
			{
				return (base.ViewState["OnDestroying"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnDestroying"] = value;
			}
		}
	}
}
